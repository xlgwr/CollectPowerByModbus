using Amib.Threading;
using EPMCS.DAL;
using EPMCS.Model;
using EPMCS.Model.NotInDb;
using EPMCS.Service.Conf;
using EPMCS.Service.Entity;
using EPMCS.Service.Thread;
using EPMCS.Service.Util;
using log4net;
using Modbus.Device;
using MySql.Data.MySqlClient;
using NCalc;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection;

namespace EPMCS.Service.Job
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class CollectJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //add by xlg
        public static Dictionary<string, List<UploadData>> _dataErrCollect { get; set; }

        #region "Execute"

        public void Execute(IJobExecutionContext context)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            logger.Debug("执行采集任务!!!!!!!!!!!!!!!");
            try
            {
                using (MysqlDbContext dbcontext = new MysqlDbContext())
                {
                    var result = dbcontext.Database.SqlQuery<int>("select count(1) from uploaddatas ");
                    logger.DebugFormat("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~采集表中共有{0}条数据 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", result.FirstOrDefault());
                }
            }
            finally
            {

            }

            List<UploadData> alldata = new List<UploadData>();

            int meterCount = 0;
            Stopwatch stopWatch = new Stopwatch();
            try
            {
                MeterGroup metersgroup = ConfUtil.Meters();
                if (metersgroup == null || metersgroup.RMeters == null || metersgroup.RMeters.Count == 0)
                {
                    logger.Debug("执行采集任务<<<<没有发现表>>>>>");
                    return;
                }
                meterCount = metersgroup.RMeters.Sum(m => m.Value.Count) + metersgroup.VMeters.Count;

                logger.DebugFormat("执行采集任务!!!!!!!!!!!!!!! metersgroup == null [{0}]", metersgroup == null);
                DateTime taskgroup = DateTime.Now;

                stopWatch.Start();

                //add by xlg
                var taskgroupMin = new DateTime(taskgroup.Year, taskgroup.Month, taskgroup.Day, taskgroup.Hour, taskgroup.Minute, 0);
                //one min only a collect job
                using (MysqlDbContext dbcontext = new MysqlDbContext())
                {
                    try
                    {
                        logger.DebugFormat("开始获取时间min!");

                        var dd = dbcontext.Database.SqlQuery<DateTime?>("select max(PowerDate) from uploaddatas").SingleOrDefault();

                        if (dd.HasValue)
                        {
                            if (taskgroupMin.Ticks <= dd.Value.Ticks)
                            {
                                logger.DebugFormat("***************开始获取时间min!,curr:{0}:{1} <= db:{2}:{3}", taskgroupMin, taskgroupMin.Ticks, dd.Value, dd.Value.Ticks);

                                return;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.ErrorFormat("**获取时间min失败，{0}", ex);
                    }
                }

                if (metersgroup != null && metersgroup.RMeters.Count > 0)
                {
                    logger.DebugFormat("执行采集任务!!!!!!!!!!!!!!! metersgroup.RMeters.Count= [{0}]", metersgroup.RMeters.Count);

                    int portCount = metersgroup.RMeters.Count;
                    IWorkItemsGroup collectWorkItemsGroup = PoolsManager.GetCollectDataThreadPoolInstance().CreateWorkItemsGroup(portCount);
                    IWorkItemResult<List<UploadData>>[] wirs = new IWorkItemResult<List<UploadData>>[portCount];
                    Amib.Threading.Func<StateData, List<UploadData>> func = new Amib.Threading.Func<StateData, List<UploadData>>(CollectPortData);
                    logger.DebugFormat("执行采集任务!!!!!!!!!!!!!!! 建立线程工作项Count = [{0}]", portCount);
                    for (var i = 0; i < portCount; i++)
                    {
                        string port = metersgroup.RMeters.Keys.ToList()[i];
                        logger.DebugFormat("执行采集任务!!!!!!!!!!!!!!! 建立线程工作项 [{0}] , [{1}], [{2}]", i, port, metersgroup.RMeters[port].Count);
                        wirs[i] = collectWorkItemsGroup.QueueWorkItem<StateData, List<UploadData>>(func, new StateData { Port = port.ToUpper(), Group = taskgroup, Meters = metersgroup.RMeters[port] });
                    }
                    logger.DebugFormat("执行采集任务!!!!!!!!!!!!!!!  执行所有工作项,等待全部完成");
                    bool success = SmartThreadPool.WaitAll(wirs, TimeSpan.FromMinutes(1), true); //
                    logger.DebugFormat("执行采集任务!!!!!!!!!!!!!!! 线程池是否成功? [{0}]", success);
                    if (success)
                    {
                        foreach (var wir in wirs)
                        {
                            alldata.AddRange(wir.Result);
                        }

                        //处理虚拟表(虚拟表计算失败,不影响实体表数据上传)
                        try
                        {
                            //为虚拟表表达式准备数据
                            var paramz = new Dictionary<string, Dictionary<string, object>>();
                            foreach (var mm in alldata)
                            {
                                if (!paramz.ContainsKey("PowerValue"))
                                {
                                    paramz["PowerValue"] = new Dictionary<string, object>();
                                }
                                paramz["PowerValue"].Add(mm.DeviceId, mm.PowerValue);

                                if (!paramz.ContainsKey("A1"))
                                {
                                    paramz["A1"] = new Dictionary<string, object>();
                                }
                                paramz["A1"].Add(mm.DeviceId, mm.A1);

                                if (!paramz.ContainsKey("A2"))
                                {
                                    paramz["A2"] = new Dictionary<string, object>();
                                }
                                paramz["A2"].Add(mm.DeviceId, mm.A2);

                                if (!paramz.ContainsKey("A3"))
                                {
                                    paramz["A3"] = new Dictionary<string, object>();
                                }
                                paramz["A3"].Add(mm.DeviceId, mm.A3);

                                //add by xlg 
                                if (!paramz.ContainsKey("DiffMeterValuePre"))
                                {
                                    paramz["DiffMeterValuePre"] = new Dictionary<string, object>();
                                }
                                paramz["DiffMeterValuePre"].Add(mm.DeviceId, mm.DiffMeterValuePre);
                                //add end
                            }

                            //生成虚拟表数据
                            if (metersgroup.VMeters != null && metersgroup.VMeters.Count > 0)
                            {
                                foreach (var mt in metersgroup.VMeters)
                                {
                                    try
                                    {
                                        //check startdate and enddate add by xlgwr
                                        if (!(taskgroup >= mt.StartDate && taskgroup <= mt.EndDate))
                                        {
                                            logger.DebugFormat("********************虚拟表数据:{0}不在有效日期内：{1} 至 {2}。 当前时间：{3}", mt.DeviceName, mt.StartDate, mt.EndDate, taskgroup);
                                            continue;
                                        }

                                        UploadData dd = new UploadData
                                        {
                                            PowerDate = taskgroupMin,
                                            Groupstamp = taskgroup.Ticks.ToString(),
                                            CustomerId = mt.CustomerId,
                                            DeviceCd = mt.DeviceCd,
                                            DeviceId = mt.DeviceId,
                                            PrePowerDate = taskgroupMin,
                                            Uploaded = 2 // by xlg 虚拟表不计算前值电量

                                        };

                                        Expression e = new Expression(mt.ComputationRule);
                                        e.Parameters = paramz["PowerValue"];
                                        dd.PowerValue = (double)e.Evaluate();

                                        e.Parameters = paramz["A1"];
                                        dd.A1 = (double)e.Evaluate();

                                        e.Parameters = paramz["A2"];
                                        dd.A2 = (double)e.Evaluate();

                                        e.Parameters = paramz["A3"];
                                        dd.A3 = (double)e.Evaluate();
                                        //add by xlg 
                                        e.Parameters = paramz["DiffMeterValuePre"];
                                        dd.DiffMeterValuePre = (double)e.Evaluate();
                                        //end by xlg

                                        //判断虚拟表有否超过阈值

                                        dd.ValueLevel = AlarmLevel(dd.PowerValue, mt);

                                        alldata.Add(dd);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error("虚拟表处理失败,DeviceId=" + mt.DeviceId, ex);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error("虚拟表处理失败", ex);
                        }
                    }//success
                    else
                    {
                        //add by xlg 2015-08-05 
                        //同一组中，如果一表维修或已坏，old不会保存已采集的数据。
                        //修正：线程池为false，保存已采集到的数据。
                        if (_dataErrCollect.ContainsKey(taskgroup.Ticks.ToString()))
                        {
                            alldata.AddRange(_dataErrCollect[taskgroup.Ticks.ToString()]);
                            var tmpAllcount = 0;
                            foreach (var m in metersgroup.RMeters)
                            {
                                tmpAllcount += m.Value.Count();
                            }

                            logger.ErrorFormat("执行采集任务!!!!!!总共:[{0}], [{1}] 已采集, [{2}] 有异常无法采集到.", tmpAllcount, alldata.Count, (tmpAllcount - alldata.Count));
                        }
                    }



                }

                logger.DebugFormat("执行采集任务!!!!!!采集到数据 [{0}]条", alldata.Count);


                //存数据库
                if (alldata.Count > 0)
                {
                    using (MysqlDbContext dbcontext = new MysqlDbContext())
                    {
                        dbcontext.Datas.AddRange(alldata);
                        dbcontext.SaveChanges();

                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

                        logger.DebugFormat("########################执行采集任务!!!!!!共 [{0}]数据存储到本地数据库,UseTime(h:M:S.ms):{1}", alldata.Count, elapsedTime);
                    }
                    var maxlvl = alldata.Where(m => metersgroup.MMeter.Contains(m.DeviceId)).Max(m => m.ValueLevel);//
                    context.JobDetail.JobDataMap.Put(Consts.AlarmLevelKey, maxlvl);
                }

                //add xlg remove bak
                if (_dataErrCollect != null)
                {
                    if (_dataErrCollect.ContainsKey(taskgroup.Ticks.ToString()))
                    {
                        _dataErrCollect.Remove(taskgroup.Ticks.ToString());
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Error("串口采集失败", ex);
                //throw ex;
            }
            finally
            {
                //超过5分钟未上传，全报警。
                try
                {
                    if (alldata.Count() == meterCount) //数量一致,2分钟之前且5分钟以内有未上传数据,报警灯全亮
                    {
                        using (MysqlDbContext dbcontext = new MysqlDbContext())
                        {
                            var tmpDateNow = DateTime.Now;
                            var tmpDateMin5 = tmpDateNow.AddMinutes(-5);
                            var tmpDateMin2 = tmpDateNow.AddMinutes(-2);

                            var lastData = dbcontext.Datas.Where(m => (m.PowerDate <= tmpDateMin2 && m.PowerDate >= tmpDateMin5 && (m.Uploaded | 1) == 0)).Count();
                            logger.DebugFormat("********有超过5分钟未上传，全报警。{0}.", lastData);
                            if (lastData > 0)
                            {
                                context.JobDetail.JobDataMap.Put(Consts.AlarmLevelKey, -1);
                            }

                        }
                    }
                    else //数量不一致,报警灯全亮
                    {
                        context.JobDetail.JobDataMap.Put(Consts.AlarmLevelKey, -1);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }

            }
            timer.Stop();
            logger.DebugFormat("********一次采集任务全程耗费时间:{0} 毫秒.", timer.ElapsedMilliseconds);
        }

        /// <summary>
        /// 判断报警级别
        /// </summary>
        /// <param name="powerValue"></param>
        /// <param name="meter"></param>
        /// <returns></returns>
        private static int AlarmLevel(double powerValue, MeterParam meter)
        {
            //判断虚拟表有否超过阈值
            int ValueLevel = 0;
            if (powerValue > meter.Level1)
            {
                ValueLevel = 1;
            }
            if (powerValue > meter.Level2)
            {
                ValueLevel = 2;
            }
            if (powerValue > meter.Level3)
            {
                ValueLevel = 3;
            }
            if (powerValue > meter.Level4)
            {
                ValueLevel = 4;
            }

            return ValueLevel;
        }

        #endregion "Execute"

        /// <summary>
        /// 实体表采集
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static List<UploadData> CollectPortData(StateData state)
        {
            logger.DebugFormat("采集串口{0},一共有{1}颗表需要采集", state.Port, state.Meters.Count);

            List<UploadData> alldata = new List<UploadData>();
            try
            {
                try
                {
                    foreach (MeterParam meter in state.Meters)
                    {
                        var tmpnow = state.Group;
                        if (string.IsNullOrEmpty(tmpnow.ToString()))
                        {
                            tmpnow = DateTime.Now;
                        }
                        //check startdate and enddate add by xlgwr
                        if (!(tmpnow <= meter.EndDate && tmpnow >= meter.StartDate))
                        {
                            logger.DebugFormat("********************实体表采集数据:{0}不在有效日期内：{1} 至 {2}。 当前时间：{3}", meter.DeviceName, meter.StartDate, meter.EndDate, tmpnow);
                            continue;
                        }

                        SerialPort serialPort = null;
                        IModbusSerialMaster master = null;
                        try
                        {
                            Conf.ComSerialPortCollection paramz = Conf.ConfUtil.GetComPortsParams();

                            if (!(ConfUtil.Ports().ContainsKey(state.Port)))
                            {
                                //TODO 串口个参数变为可配置,各个端口分别配置
                                serialPort = new SerialPort(state.Port);
                                serialPort.BaudRate = paramz[state.Port].BaudRate;
                                serialPort.DataBits = paramz[state.Port].DataBits;
                                serialPort.Parity = paramz[state.Port].Parity;
                                serialPort.StopBits = paramz[state.Port].StopBits;
                                //add by xlg
                                serialPort.ReadTimeout = paramz[state.Port].ReadTimeout;
                                serialPort.WriteTimeout = paramz[state.Port].WriteTimeout;

                                ConfUtil.Ports()[state.Port] = serialPort;
                            }
                            else
                            {
                                serialPort = ConfUtil.Ports()[state.Port];
                            }

                            if (!serialPort.IsOpen)
                            {
                                serialPort.Open();
                                System.Threading.Thread.Sleep(paramz[state.Port].ReadDelay);
                            }


                            master = ModbusSerialMaster.CreateRtu(serialPort);

                            logger.DebugFormat("***开始采集表{0},地址{1},{2},{3},{4},{5},{6},一共有{7}个采集项目", meter.DeviceName, meter.DeviceAdd, serialPort.PortName, serialPort.BaudRate, serialPort.Parity, serialPort.StopBits, serialPort.ReadTimeout, meter.CmdInfos.Count());

                            UploadData data = new UploadData();
                            data.CustomerId = meter.CustomerId;
                            data.DeviceId = meter.DeviceId;
                            data.DeviceCd = meter.DeviceCd;
                            data.Groupstamp = state.Group.Ticks.ToString();

                            byte slaveId = byte.Parse(meter.DeviceAdd);
                            bool isTimeOutOrError = false;

                            foreach (CmdInfo info in meter.CmdInfos)
                            {
                                logger.DebugFormat("采集项目[{0}],采集地址[{1}]", info.Name, info.Address);
                                ushort startAddress = Convert.ToUInt16(info.Address, 16);
                                ushort npoints = Ints.Reg16Count(info.CsharpType);

                                var tomethod = "To" + info.CsharpType.Split('.')[1];

                                var rountLen = 2;
                                if (info.UnitFactor < 1)
                                {
                                    rountLen = info.UnitFactor.ToString().Length - 1;
                                }

                                ushort[] dd;
                                try
                                {
                                    dd = master.ReadHoldingRegisters(slaveId, startAddress, npoints);
                                }
                                catch (Exception ex)
                                {
                                    logger.ErrorFormat("***********采集项目[{0}],采集地址[{1}],设备地址：[{2}],Error:[{3}]", info.Name, info.Address, slaveId, ex.Message);
                                    isTimeOutOrError = true;
                                    break;
                                }

                                string[] cc = dd.ToList().Select(m => m.ToString("X")).ToArray();

                                var ddvalue = Ints.ToValue(dd, tomethod, info.DaDuan);

                                var EndValue = Math.Round(Convert.ToDouble(ddvalue), rountLen);

                                logger.InfoFormat("**收到数据:{0},value:{1},UnitFactor:{2}，Name:{3}", String.Join(",", cc), ddvalue, info.UnitFactor, info.Name);

                                if (info.Name.ToLower() == "zljyggl") //总累计有功功率
                                {
                                    data.MeterValue = EndValue * info.UnitFactor;
                                }

                                if (info.Name.ToLower() == "zljwggl") //总累计无功功率
                                {
                                    data.MeterValueW = EndValue * info.UnitFactor;
                                }

                                if (info.Name.ToLower() == "zssyggl")
                                {//总瞬时有功功率
                                    data.PowerValue = EndValue * info.UnitFactor;
                                }
                                if (info.Name.ToLower() == "a1")
                                {
                                    data.A1 = EndValue * info.UnitFactor;
                                }
                                if (info.Name.ToLower() == "a2")
                                {
                                    data.A2 = EndValue * info.UnitFactor;
                                }
                                if (info.Name.ToLower() == "a3")
                                {
                                    data.A3 = EndValue * info.UnitFactor;
                                }
                                if (info.Name.ToLower() == "v1")
                                {
                                    data.V1 = EndValue * info.UnitFactor;
                                }
                                if (info.Name.ToLower() == "v2")
                                {
                                    data.V2 = EndValue * info.UnitFactor;
                                }
                                if (info.Name.ToLower() == "v3")
                                {
                                    data.V3 = EndValue * info.UnitFactor;
                                }
                                if (info.Name.ToLower() == "pf")
                                {
                                    data.Pf = EndValue * info.UnitFactor;
                                }
                                //判断本表有否超过阈值
                                data.ValueLevel = AlarmLevel(data.PowerValue, meter);



                                serialPort.BreakState = true;
                                System.Threading.Thread.Sleep(paramz[state.Port].ReadDelay);
                                if (serialPort.BytesToRead > 0)
                                    serialPort.DiscardInBuffer();
                                if (serialPort.BytesToWrite > 0)
                                    serialPort.DiscardOutBuffer();
                                serialPort.BreakState = false;

                            }
                            if (isTimeOutOrError)
                            {
                                logger.DebugFormat("********继续下个设备采集，当前采集表[{0}],地址{1}", meter.DeviceName, meter.DeviceAdd);
                                continue;
                            }
                            //change to yyyyMMddmm
                            var mssec = state.Group.Millisecond;
                            var sec = state.Group.Second;
                            data.PowerDate = new DateTime(state.Group.Year, state.Group.Month, state.Group.Day, state.Group.Hour, state.Group.Minute, 0);
                            data.Uploaded = 0;
                            //add by xlg 2015-07-05

                            //计算电量差值
                            logger.DebugFormat("开始计算与上一次电量差值:表[{0}],地址{1},一共有{2}个采集项目", meter.DeviceName, meter.DeviceAdd, meter.CmdInfos.Count());
                            using (MysqlDbContext dbcontext = new MysqlDbContext())
                            {
                                var tmpUpdateData = dbcontext
                                    .Datas
                                    .Where(d => d.CustomerId.Equals(data.CustomerId)
                                        && d.DeviceId.Equals(data.DeviceId)
                                        && d.DeviceCd.Equals(data.DeviceCd)
                                        && d.PowerDate < data.PowerDate);//.ToList();
                                var allCount = tmpUpdateData.Count();
                                if (allCount > 0)
                                {
                                    var tmpMaxId = tmpUpdateData.Max(d => d.Id);
                                    logger.DebugFormat("上一次采集ID:[{0}],总计录：[{1}]", tmpMaxId, allCount);
                                    var tmpPredata = dbcontext.Datas.Where(d => d.Id == tmpMaxId).FirstOrDefault();

                                    data.PrePowerDate = tmpPredata.PowerDate;
                                    var tmpeiff = data.MeterValue - tmpPredata.MeterValue;
                                    data.DiffMeterValuePre = tmpeiff > 0 ? tmpeiff : 0;

                                    tmpPredata.Uploaded = (tmpPredata.Uploaded == 0 || tmpPredata.Uploaded == 2) ? 2 : 3;

                                    dbcontext.SaveChanges();
                                }
                                else
                                {
                                    data.DiffMeterValuePre = 0;
                                    data.PrePowerDate = data.PowerDate;
                                }
                            }

                            // add to bak data
                            if (_dataErrCollect == null)
                            {
                                _dataErrCollect = new Dictionary<string, List<UploadData>>();
                            }

                            if (!_dataErrCollect.ContainsKey(data.Groupstamp))
                            {
                                _dataErrCollect[data.Groupstamp] = new List<UploadData>();
                            }
                            _dataErrCollect[data.Groupstamp].Add(data);
                            //end by xlg
                            alldata.Add(data);

                        }
                        catch (System.Threading.ThreadAbortException tae)
                        {
                            logger.Error("表采集失败ThreadAbortException", tae);
                            if (ConfUtil.Ports().TryRemove(state.Port, out serialPort))
                            {
                                try
                                {
                                    serialPort.BreakState = true;
                                    System.Threading.Thread.Sleep(100);
                                    while (serialPort.BytesToRead > 0)
                                        serialPort.DiscardInBuffer();
                                    while (serialPort.BytesToWrite > 0)
                                        serialPort.DiscardOutBuffer();
                                    serialPort.BreakState = false;
                                    if (master != null) master.Dispose();
                                    serialPort.Dispose();
                                }
                                finally
                                {

                                }

                            }
                        }
                        catch (Exception exm)
                        {
                            logger.Error("表采集失败", exm);
                            //add by xlg
                            logger.ErrorFormat("采集表[{0}],地址{1},一共有{2}个采集项目", meter.DeviceName, meter.DeviceAdd, meter.CmdInfos.Count());

                        }
                    }

                }
                catch (Exception ex)
                {
                    logger.Error("表采集失败2", ex);
                }
            }
            catch (Exception exsp)
            {
                logger.Error(String.Format("串口[{0}]采集失败", state.Port), exsp);
            }
            return alldata;
        }
    }
}

/*
数字类型范围
https://technet.microsoft.com/zh-cn/exx3b86w
*/
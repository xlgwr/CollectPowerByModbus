using EPMCS.DAL;
using EPMCS.Model;
using EPMCS.Model.NotInDb;
using EPMCS.Service.Util;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using Xstream.Core;

namespace EPMCS.Service.Conf
{
    public class ConfUtil
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static MeterGroup meters = null;

        public static MeterGroup Meters()
        {
            if (meters == null)
            {
                meters = LoadMeters();
            }
            return (MeterGroup)meters.Clone();
        }

        public static void ReloadMeters()
        {
            meters = LoadMeters();
        }

        private static MeterGroup LoadMeters()
        {
            MeterGroup mg = new MeterGroup();
            mg.RMeters = new Dictionary<string, List<MeterParam>>();
            mg.VMeters = new List<MeterParam>();
            using (MysqlDbContext dbcontext = new MysqlDbContext())
            {
                List<MeterParam> meters = dbcontext.Meters.ToList();
                if (meters != null && meters.Count > 0)
                {
                    foreach (MeterParam m in meters)
                    {
                        if (string.IsNullOrEmpty(m.ComputationRule))
                        {
                            if (!mg.RMeters.ContainsKey(m.Port.ToUpper()))
                            {
                                mg.RMeters[m.Port.ToUpper()] = new List<MeterParam>();
                            }
                            m.CmdInfos = FromXML(m.Message);
                            mg.RMeters[m.Port.ToUpper()].Add(m);
                        }
                        else
                        {
                            mg.VMeters.Add(m);
                        }
                    }
                }
            }
            return mg;
        }

        private static CmdInfo[] FromXML(string xml)
        {
            XStream xstream = new XStream();
            xstream.Alias("Device", typeof(Device));
            xstream.Alias("CmdInfo", typeof(CmdInfo));
            xstream.Alias("CmdInfos", typeof(CmdInfo[]));
            try
            {
                if (xml != null)
                {
                    xml = xml.Replace(@"<\/", "</");
                    Device ddd = (Device)xstream.FromXml(xml);//"<Device><cmdInfos><CmdInfo><name>yearmonth</name> <address>002F</address> <csharpType>System.Int16</csharpType> <unitFactor>0</unitFactor></CmdInfo> <CmdInfo><name>dayhour</name> <address>002E</address> <csharpType>System.Int16</csharpType> <unitFactor>0</unitFactor></CmdInfo> <CmdInfo><name>minutesecond</name> <address>002D</address> <csharpType>System.Int16</csharpType> <unitFactor>0</unitFactor></CmdInfo> <CmdInfo><name>zljyggl</name> <address>006A</address> <csharpType>System.UInt32</csharpType> <unitFactor>0.01</unitFactor></CmdInfo> <CmdInfo><name>zssyggl</name> <address>0092</address> <csharpType>System.Int32</csharpType> <unitFactor>0.01</unitFactor></CmdInfo> <CmdInfo><name>a1</name> <address>00B4</address> <csharpType>System.UInt32</csharpType> <unitFactor>0.01</unitFactor></CmdInfo> <CmdInfo><name>a2</name> <address>00B6</address> <csharpType>System.UInt32</csharpType> <unitFactor>0.01</unitFactor></CmdInfo> <CmdInfo><name>a3</name> <address>00B8</address> <csharpType>System.UInt32</csharpType> <unitFactor>0.01</unitFactor></CmdInfo> <CmdInfo><name>v1</name> <address>00A4</address> <csharpType>System.UInt32</csharpType> <unitFactor>0.1</unitFactor></CmdInfo> <CmdInfo><name>v2</name> <address>00A6</address> <csharpType>System.UInt32</csharpType> <unitFactor>0.1</unitFactor></CmdInfo> <CmdInfo><name>v3</name> <address>00A8</address> <csharpType>System.UInt32</csharpType> <unitFactor>0.1</unitFactor></CmdInfo> <CmdInfo><name>pf</name> <address>00C5</address> <csharpType>System.Int16</csharpType> <unitFactor>0.001</unitFactor></CmdInfo></cmdInfos></Device>");
                    if (ddd == null || ddd.CmdInfos == null)
                    {
                        return new CmdInfo[] { };
                    }
                    else
                    {
                        return ddd.CmdInfos;
                    }
                }
                else
                {
                    return new CmdInfo[] { };
                }
            }
            catch (Exception ex)
            {
                logger.DebugFormat("解析xml异常: {0}", xml);
                throw ex;
            }
        }

        private static int unuploadKeepDays = 0;

        /// <summary>
        /// 读取app.config配置时间,没有读到则为默认7天
        /// </summary>
        /// <returns></returns>
        public static int ForceClearDays()
        {
            if (unuploadKeepDays <= 0)
            {
                string txt = ConfigurationManager.AppSettings.Get(Consts.UnuploadKeepDays);
                if (!int.TryParse(txt, out unuploadKeepDays))
                {
                    unuploadKeepDays = 7; //默认7天
                }
                if (unuploadKeepDays < 1) unuploadKeepDays = 1;
            }
            return unuploadKeepDays;
        }

        private static int clearIntervalInMinutes = 0;

        public static int ClearIntervalInMinutes()
        {
            if (clearIntervalInMinutes <= 0)
            {
                string txt = ConfigurationManager.AppSettings.Get(Consts.ClearIntervalInMinutes);
                if (!int.TryParse(txt, out clearIntervalInMinutes))
                {
                    clearIntervalInMinutes = 60; //默认60分钟
                }
                if (clearIntervalInMinutes < 5) clearIntervalInMinutes = 5;
            }
            return clearIntervalInMinutes;
        }

        private static int uploadIntervalInSeconds = 0;

        public static int UploadIntervalInSeconds()
        {
            if (uploadIntervalInSeconds <= 0)
            {
                string txt = ConfigurationManager.AppSettings.Get(Consts.UploadIntervalInSeconds);
                if (!int.TryParse(txt, out uploadIntervalInSeconds))
                {
                    uploadIntervalInSeconds = 60; //默认60秒
                }
                if (uploadIntervalInSeconds < 15) uploadIntervalInSeconds = 15;
            }
            return uploadIntervalInSeconds;
        }

        private static string uploadUrl = "";

        public static string UploadUrl()
        {
            if (string.IsNullOrEmpty(uploadUrl))
            {
                uploadUrl = ConfigurationManager.AppSettings.Get(Consts.UploadUrlKey);
            }
            return uploadUrl;
        }

        private static string metersUrl = "";

        public static string MetersUrl()
        {
            if (string.IsNullOrEmpty(metersUrl))
            {
                metersUrl = ConfigurationManager.AppSettings.Get(Consts.MetersUrlKey);
            }
            return metersUrl;
        }

        private static string customerId = "";

        public static string CustomerId()
        {
            if (string.IsNullOrEmpty(customerId))
            {
                customerId = ConfigurationManager.AppSettings.Get(Consts.CustomerIdKey);
            }
            return customerId;
        }

        private static string autoUpdateUrl = "";

        public static string AutoUpdateUrl()
        {
            if (string.IsNullOrEmpty(autoUpdateUrl))
            {
                autoUpdateUrl = ConfigurationManager.AppSettings.Get("autoUpdateUrl");
            }
            return autoUpdateUrl;
        }    
        private static string alarmSerialPort = "";

        public static string AlarmSerialPort()
        {
            if (string.IsNullOrEmpty(alarmSerialPort))
            {
                alarmSerialPort = ConfigurationManager.AppSettings.Get(Consts.AlarmSerialPortKey);
            }
            return alarmSerialPort;
        }

        private static ConcurrentDictionary<string, SerialPort> ports = new ConcurrentDictionary<string, SerialPort>(2, 2);

        public static ConcurrentDictionary<string, SerialPort> Ports()
        {
            if (ports == null)
            {
                ports = new ConcurrentDictionary<string, SerialPort>();
            }
            return ports;
        }

        private static ConcurrentDictionary<string, MyCom> myComports = new ConcurrentDictionary<string, MyCom>(2, 2);
        public static ConcurrentDictionary<string, MyCom> MyComports()
        {
            if (myComports == null)
            {
                myComports = new ConcurrentDictionary<string, MyCom>();
            }
            return myComports;
        }

        private static ComSerialPortCollection comSerialPorts;
        public static ComSerialPortCollection GetComPortsParams()
        {
            if (comSerialPorts == null)
            {
                return comSerialPorts = ((ComSerialPortsSection)ConfigurationManager.GetSection("ComSerialPortsSection")).ComSerialPortList;
            }
            return comSerialPorts;
        }
    }

    public class Device
    {
        private CmdInfo[] cmdInfos;

        public CmdInfo[] CmdInfos
        {
            get { return cmdInfos; }
            set { cmdInfos = value; }
        }
    }
}
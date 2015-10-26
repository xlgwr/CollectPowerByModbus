using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Modbus.Device;
using System.Diagnostics;

namespace TestDevices
{
    public partial class Form1 : Form
    {
        private static SerialPort _sp;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            txt1Rece.WordWrap = false;
            txt1Rece.Multiline = true;
            _sp = new SerialPort();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            api.commSetSerialPara(cbox0PortName, SerialPort.GetPortNames(), 0);
            api.commSetSerialPara<SerialPortBaudRates>(cbox1BaudRate, "9600", true);
            api.commSetSerialPara<SerialPortDatabits>(cbox2DataBits, "8", true);
            api.commSetSerialPara<Parity>(cbox3Parity, 1, false);
            api.commSetSerialPara<StopBits>(cbox4StopBits, 1, false);
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                cbox1BaudRate.Text = "9600";
                cbox2DataBits.Text = "8";
                cbox3Parity.SelectedIndex = 1;
                cbox4StopBits.SelectedIndex = 1;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                cbox1BaudRate.Text = "9600";
                cbox2DataBits.Text = "8";
                cbox3Parity.SelectedIndex = 0;
                cbox4StopBits.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt1Rece.Text = "开始测试报警设备.";
            btn1Alert.Enabled = false;
            string alarmPort = cbox0PortName.Text;
            if (alarmPort != null && alarmPort.ToUpper().StartsWith("COM"))
            {
                short[] d = new short[] { 0, 0, 0, 0 };
                ModbusPoll mp = new ModbusPoll();
                try
                {
                    initSP();

                    for (int i = 0; i < 4; i++)
                    {
                        d[i] = 1;
                        txt1Rece.Invoke(new MethodInvoker(delegate()
                        {
                            txt1Rece.Text += "\n" + (i + 1).ToString();
                        }));
                        mp.StartPoll(_sp, d);
                    }

                }
                catch (Exception ex)
                {
                    txt1Rece.Text += "\n访问报警设备异常:" + ex.Message;
                    btn1Alert.Enabled = true;
                }
                finally
                {
                    mp.StopPoll();
                }
            }
            else
            {
                txt1Rece.Text += "\n没有发现报警串口,不报警!";
            }
            txt1Rece.Text += "\n报警设备测试完成.";
            btn1Alert.Enabled = true;
        }
        void initSP()
        {
            if (_sp.IsOpen)
            {
                _sp.Close();
            }
            var timeoff = 100;

            if (!int.TryParse(txt6timeOff.Text, out timeoff))
            {
                timeoff = 100;
            }
            _sp.ReadBufferSize = 10240;
            _sp.WriteBufferSize = 10240;

            _sp.PortName = cbox0PortName.Text;
            _sp.BaudRate = (int)((SerialPortBaudRates)Enum.Parse(typeof(SerialPortBaudRates), cbox1BaudRate.Text));
            _sp.DataBits = (int)((SerialPortDatabits)Enum.Parse(typeof(SerialPortDatabits), cbox2DataBits.Text));
            _sp.Parity = (Parity)Enum.Parse(typeof(Parity), cbox3Parity.Text);
            _sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbox4StopBits.Text);
            _sp.RtsEnable = true;
            _sp.DtrEnable = true;
            //These timeouts are default and cannot be editted through the class at this point:

            _sp.ReadTimeout = timeoff;
            _sp.WriteTimeout = timeoff;

            if (!_sp.IsOpen)
            {
                _sp.Open();
            }

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        void getInfoPower(string msg)
        {
            txt1Rece.Invoke(new MethodInvoker(delegate()
            {
                txt1Rece.Text += "\n" + msg;
            }));
        }
        private void btn2Power_Click(object sender, EventArgs e)
        {
            try
            {
                txt1Rece.Text = "开始测试电表: " + cbox7ID.Text + " 设备.";
                this.btn2Power.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                var devicefile = comb0Devices.Text;
                if (string.IsNullOrEmpty(devicefile))
                {
                    devicefile = "device";
                }
                var tmpCmdInfoFile = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\" + devicefile + ".xml", System.Text.Encoding.UTF8);

                var CmdInfo = Ints.FromXML(tmpCmdInfoFile).CmdInfos;

                int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;

                initSP();


                Stopwatch timer = new Stopwatch();
                timer.Start();

                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(_sp);

                byte slaveId = byte.Parse(cbox7ID.Text);
                bool isTimeOutOrError = false;
                object ddvalue = null;
                foreach (CmdInfo info in CmdInfo)
                {
                    getInfoPower("采集项目[" + info.Name + "],采集地址[" + info.Address + "]");
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
                        getInfoPower("***********采集项目[" + info.Name + "],采集地址[" + info.Address + "],设备地址：[" + slaveId + "],Error:[" + ex.Message + "]");
                        isTimeOutOrError = true;

                        break;
                    }

                    string[] cc = dd.ToList().Select(m => m.ToString("X")).ToArray();

                    ddvalue = Ints.ToValue(dd, tomethod, info.DaDuan);

                    var EndValue = Math.Round(Convert.ToDouble(ddvalue), rountLen);

                    getInfoPower("收到数据," + String.Join(",", cc));

                    //if (info.Name.ToLower() == "yearmonth")
                    //{
                    //    year = 2000 + Ints.UshortHighByteToInt(dd[0]);
                    //    month = Ints.UshortLowByteToInt(dd[0]);
                    //}
                    //if (info.Name.ToLower() == "dayhour")
                    //{
                    //    day = Ints.UshortHighByteToInt(dd[0]);
                    //    hour = Ints.UshortLowByteToInt(dd[0]);
                    //}
                    //if (info.Name.ToLower() == "minutesecond")
                    //{
                    //    minute = Ints.UshortHighByteToInt(dd[0]);
                    //    second = Ints.UshortLowByteToInt(dd[0]);
                    //}
                    var tmpname = info.Name.ToLower();
                    switch (tmpname)
                    {
                        case "zljyggl":
                            var MeterValue = EndValue * info.UnitFactor;
                            getInfoPower("总累计有功功率:" + MeterValue.ToString());
                            break;
                        case "zljwggl":
                            var MeterValueW = EndValue * info.UnitFactor;
                            getInfoPower("总累计无功功率:" + MeterValueW.ToString());
                            break;
                        case "zssyggl":
                            var PowerValue = EndValue * info.UnitFactor;
                            getInfoPower("总瞬时有功功率:" + PowerValue.ToString());
                            break;
                        case "zsswggl":
                            var PowerValueW = EndValue * info.UnitFactor;
                            getInfoPower("总瞬时无功功率:" + PowerValueW.ToString());
                            break;
                        case "a1":
                            var A1 = EndValue * info.UnitFactor;
                            getInfoPower("A1:" + A1.ToString());
                            break;
                        case "a2":
                            var A2 = EndValue * info.UnitFactor;
                            getInfoPower("A2:" + A2.ToString());
                            break;
                        case "a3":
                            var A3 = EndValue * info.UnitFactor;
                            getInfoPower("A3:" + A3.ToString());
                            break;
                        case "v1":
                            var V1 = EndValue * info.UnitFactor;
                            getInfoPower("V1:" + V1.ToString());
                            break;
                        case "v2":
                            var V2 = EndValue * info.UnitFactor;
                            getInfoPower("V2:" + V2.ToString());
                            break;
                        case "v3":
                            var V3 = EndValue * info.UnitFactor;
                            getInfoPower("V3:" + V3.ToString());
                            break;
                        case "pf":
                            var Pf = EndValue * info.UnitFactor;
                            getInfoPower("PF:" + Pf.ToString());
                            break;
                        default:
                            break;
                    }

                }
                getInfoPower("测试电表：[ " + cbox7ID.Text + " ]完成。");
                this.btn2Power.Enabled = true;
                this.Cursor = Cursors.Default;

                timer.Stop();
                getInfoPower("####################使用时间[" + timer.Elapsed + "],毫秒[" + timer.ElapsedMilliseconds + "]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (_sp.IsOpen)
                {
                    _sp.Close();
                }
                this.btn2Power.Enabled = true;
                this.Cursor = Cursors.Default;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (_sp.IsOpen)
                {
                    _sp.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn2NewTest_Click(object sender, EventArgs e)
        {
            try
            {
                txt1Rece.Text = "new开始测试电表: " + cbox7ID.Text + " 设备.";
                this.btn2Power.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                var devicefile = comb0Devices.Text;
                if (string.IsNullOrEmpty(devicefile))
                {
                    devicefile = "device";
                }
                var tmpCmdInfoFile = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\" + devicefile + ".xml", System.Text.Encoding.UTF8);

                var main = Ints.FromXML(tmpCmdInfoFile).Main;
                var CmdInfo = Ints.FromXML(tmpCmdInfoFile).CmdInfos;

                //初始化serial port 
                initSP();

                //记时
                Stopwatch timer = new Stopwatch();//new一个stopwatch
                timer.Start();


                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(_sp);

                byte slaveId = byte.Parse(cbox7ID.Text);
                object ddvalue = null;


                ushort[] Alldd;
                try
                {
                    getInfoPower("***********采集项目[" + main.Name + "],采集地址[" + main.Address + ",连续数量：" + main.CsharpType + "],设备地址：[" + slaveId + "]");

                    var allLen = Convert.ToInt32(main.CsharpType);
                    ushort startAddress = Convert.ToUInt16(main.Address, 16);
                    ushort npoints = Convert.ToUInt16(main.CsharpType);

                    //Alldd = new ushort[allLen];
                    //get Data
                    Alldd = master.ReadHoldingRegisters(slaveId, startAddress, npoints);

                    //get
                    #region getValue
                    foreach (CmdInfo info in CmdInfo)
                    {


                        var tomethod = "To" + info.CsharpType.Split('.')[1];
                        var rountLen = 2;
                        if (info.UnitFactor < 1)
                        {
                            rountLen = info.UnitFactor.ToString().Length - 1;
                        }
                        //从第几个开始取
                        var currAddress = Convert.ToUInt16(info.Address, 16) - startAddress;
                        //取几个
                        var tmplen = Ints.Reg16Count(info.CsharpType);

                        ushort[] dd = new ushort[tmplen];

                        //log
                        getInfoPower("采集项目[" + info.Name + "],采集地址[" + info.Address + "],长度：" + tmplen + ",第：" + currAddress + "个");

                        Array.Copy(Alldd, currAddress, dd, 0, tmplen);

                        string[] cc = dd.ToList().Select(m => m.ToString("X")).ToArray();

                        ddvalue = Ints.ToValue(dd, tomethod, info.DaDuan);

                        var EndValue = Math.Round(Convert.ToDouble(ddvalue), rountLen);

                        getInfoPower("收到数据," + String.Join(",", cc));

                        var tmpname = info.Name.ToLower();
                        switch (tmpname)
                        {
                            case "zljyggl":
                                var MeterValue = EndValue * info.UnitFactor;
                                getInfoPower("总累计有功功率:" + MeterValue.ToString());
                                break;
                            case "zljwggl":
                                var MeterValueW = EndValue * info.UnitFactor;
                                getInfoPower("总累计无功功率:" + MeterValueW.ToString());
                                break;
                            case "zssyggl":
                                var PowerValue = EndValue * info.UnitFactor;
                                getInfoPower("总瞬时有功功率:" + PowerValue.ToString());
                                break;
                            case "zsswggl":
                                var PowerValueW = EndValue * info.UnitFactor;
                                getInfoPower("总瞬时无功功率:" + PowerValueW.ToString());
                                break;
                            case "a1":
                                var A1 = EndValue * info.UnitFactor;
                                getInfoPower("A1:" + A1.ToString());
                                break;
                            case "a2":
                                var A2 = EndValue * info.UnitFactor;
                                getInfoPower("A2:" + A2.ToString());
                                break;
                            case "a3":
                                var A3 = EndValue * info.UnitFactor;
                                getInfoPower("A3:" + A3.ToString());
                                break;
                            case "v1":
                                var V1 = EndValue * info.UnitFactor;
                                getInfoPower("V1:" + V1.ToString());
                                break;
                            case "v2":
                                var V2 = EndValue * info.UnitFactor;
                                getInfoPower("V2:" + V2.ToString());
                                break;
                            case "v3":
                                var V3 = EndValue * info.UnitFactor;
                                getInfoPower("V3:" + V3.ToString());
                                break;
                            case "pf":
                                var Pf = EndValue * info.UnitFactor;
                                getInfoPower("PF:" + Pf.ToString());
                                break;
                            default:
                                break;
                        }

                    }
                    getInfoPower("测试电表：[ " + cbox7ID.Text + " ]完成。");
                    this.btn2Power.Enabled = true;
                    this.Cursor = Cursors.Default;
                    #endregion
                }
                catch (Exception ex)
                {
                    getInfoPower("***********采集项目[" + main.Name + "],采集地址[" + main.Address + ",连续数量：" + main.CsharpType + "],设备地址：[" + slaveId + "],Error:[" + ex.Message + "]");

                }
                finally
                {
                    timer.Stop();
                    getInfoPower("####################使用时间[" + timer.Elapsed + "],毫秒[" + timer.ElapsedMilliseconds + "]");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (_sp.IsOpen)
                {
                    _sp.Close();
                }
                this.btn2Power.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }
    }
}

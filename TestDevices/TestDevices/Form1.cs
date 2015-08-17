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
            api.commSetSerialPara<SerialPortBaudRates>(cbox1BaudRate, "19200", true);
            api.commSetSerialPara<SerialPortDatabits>(cbox2DataBits, "8", true);
            api.commSetSerialPara<Parity>(cbox3Parity, 1, false);
            api.commSetSerialPara<StopBits>(cbox4StopBits, 1, false);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                cbox1BaudRate.Text = "19200";
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
            _sp.PortName = cbox0PortName.Text;
            _sp.BaudRate = (int)((SerialPortBaudRates)Enum.Parse(typeof(SerialPortBaudRates), cbox1BaudRate.Text));
            _sp.DataBits = (int)((SerialPortDatabits)Enum.Parse(typeof(SerialPortDatabits), cbox2DataBits.Text));
            _sp.Parity = (Parity)Enum.Parse(typeof(Parity), cbox3Parity.Text);
            _sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbox4StopBits.Text);
            //These timeouts are default and cannot be editted through the class at this point:
            _sp.ReadTimeout = 100;
            _sp.WriteTimeout = 100;

            if (_sp.IsOpen)
            {
                _sp.Close();
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
            var tmpCmdInfoFile = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\device.xml", System.Text.Encoding.UTF8);
            var CmdInfo = Ints.FromXML(tmpCmdInfoFile);
            int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;

            initSP();

            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(_sp);

            byte slaveId = byte.Parse(cbox7ID.Text);
            bool isTimeOutOrError = false;

            foreach (CmdInfo info in CmdInfo)
            {
                getInfoPower("采集项目[" + info.Name + "],采集地址[" + info.Address + "]");
                ushort startAddress = Convert.ToUInt16(info.Address, 16);
                ushort npoints = Ints.Reg16Count(info.CsharpType);


                ushort[] dd;
                try
                {
                    dd = master.ReadHoldingRegisters(slaveId, startAddress, npoints);
                }
                catch (Exception ex)
                {
                    getInfoPower("***********采集项目["+info.Name+"],采集地址["+info.Address+"],设备地址：["+slaveId+"],Error:["+ex.Message+"]");
                    isTimeOutOrError = true;
                    break;
                }

                string[] cc = dd.ToList().Select(m => m.ToString("X")).ToArray();


                getInfoPower("收到数据," + String.Join(",", cc));

                if (info.Name.ToLower() == "yearmonth")
                {
                    year = 2000 + Ints.UshortHighByteToInt(dd[0]);
                    month = Ints.UshortLowByteToInt(dd[0]);
                }
                if (info.Name.ToLower() == "dayhour")
                {
                    day = Ints.UshortHighByteToInt(dd[0]);
                    hour = Ints.UshortLowByteToInt(dd[0]);
                }
                if (info.Name.ToLower() == "minutesecond")
                {
                    minute = Ints.UshortHighByteToInt(dd[0]);
                    second = Ints.UshortLowByteToInt(dd[0]);
                }
                if (info.Name.ToLower() == "zljyggl") //总累计有功功率
                {
                    var MeterValue = Ints.UShortArrayToUInt32(dd) * info.UnitFactor;
                    getInfoPower("总累计有功功率:"+MeterValue.ToString());
                }
                if (info.Name.ToLower() == "zssyggl")
                {//总瞬时有功功率
                    var PowerValue = Ints.UShortArrayToInt32(dd) * info.UnitFactor;
                    getInfoPower("总瞬时有功功率:" + PowerValue.ToString());
                }
                if (info.Name.ToLower() == "a1")
                {
                    var A1 = Ints.UShortArrayToUInt32(dd) * info.UnitFactor;
                    getInfoPower("A1:" + A1.ToString());
                }
                if (info.Name.ToLower() == "a2")
                {
                    var A2 = Ints.UShortArrayToUInt32(dd) * info.UnitFactor;
                    getInfoPower("A2:" + A2.ToString());

                }
                if (info.Name.ToLower() == "a3")
                {
                    var A3 = Ints.UShortArrayToUInt32(dd) * info.UnitFactor;
                    getInfoPower("A3:" + A3.ToString());
                }
                if (info.Name.ToLower() == "v1")
                {
                    var V1 = Ints.UShortArrayToUInt32(dd) * info.UnitFactor;
                    getInfoPower("V1:" + V1.ToString());
                }
                if (info.Name.ToLower() == "v2")
                {
                    var V2 = Ints.UShortArrayToUInt32(dd) * info.UnitFactor;
                    getInfoPower("V2:" + V2.ToString());
                }
                if (info.Name.ToLower() == "v3")
                {
                    var V3 = Ints.UShortArrayToUInt32(dd) * info.UnitFactor;
                    getInfoPower("V3:" + V3.ToString());
                }
                if (info.Name.ToLower() == "pf")
                {
                    var Pf = Ints.UShortToShort(dd[0]) * info.UnitFactor;
                    getInfoPower("PF:" + Pf.ToString());
                }

            }
        }
    }
}

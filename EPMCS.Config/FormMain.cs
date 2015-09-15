using EPMCS.Model;
using EPMCS.Service.Entity;
using EPMCS.Service.Util;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EPMCS.Config
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //获取config路径
            string path = System.Windows.Forms.Application.StartupPath + "/EPMCS.Service.exe.config";
            XDocument doc = XDocument.Load(path);
            //查找所有节点
            IEnumerable<XElement> elementz = doc.Element("configuration").Element("appSettings").Elements();
            if (elementz.Count(m => m.Attribute("key")!=null && m.Attribute("key").Value == "ServerIP") == 0)
            {
                doc.Element("configuration").Element("appSettings").Add(new XElement("add", new XAttribute("key", "ServerIP"), new XAttribute("value", "58.248.164.61")));
            }
            //遍历节点
            foreach (XElement item in elementz)
            {
                this.SaveCtrl(this.nudUnuploadKeepDays, item, "key", "UnuploadKeepDays", "value", "35");
                this.SaveCtrl(this.nudUploadedTake, item, "key", "UploadedTake", "value", "30");
                this.SaveCtrl(this.tbIP, item, "key", "ServerIP", "value", "58.248.164.61");
                this.SaveCtrl(this.cbAlarmSerialPort, item, "key", "AlarmSerialPort", "value", "COM3");
                this.SaveCtrl(this.tbCustomerId, item, "key", "CustomerId", "value", "");
            }

            //查找所有节点
            IEnumerable<XElement> comelements = doc.Element("configuration").Element("ComSerialPortsSection").Element("ComSerialPortList").Elements();
            //遍历节点
            foreach (XElement item in comelements)
            {
                this.SaveCtrl(this.cbBR1, item, "name", "COM1", "BaudRate", "19200");
                this.SaveCtrl(this.cbBR2, item, "name", "COM2", "BaudRate", "19200");
                this.SaveCtrl(this.cbBR3, item, "name", "COM3", "BaudRate", "19200");
                this.SaveCtrl(this.cbBR4, item, "name", "COM4", "BaudRate", "19200");
                this.SaveCtrl(this.cbBR5, item, "name", "COM5", "BaudRate", "19200");
                this.SaveCtrl(this.cbBR6, item, "name", "COM6", "BaudRate", "19200");

                this.SaveCtrl(this.nudDB1, item, "name", "COM1", "DataBits", "8");
                this.SaveCtrl(this.nudDB2, item, "name", "COM2", "DataBits", "8");
                this.SaveCtrl(this.nudDB3, item, "name", "COM3", "DataBits", "8");
                this.SaveCtrl(this.nudDB4, item, "name", "COM4", "DataBits", "8");
                this.SaveCtrl(this.nudDB5, item, "name", "COM5", "DataBits", "8");
                this.SaveCtrl(this.nudDB6, item, "name", "COM6", "DataBits", "8");

                this.SaveCtrl(this.cbPy1, item, "name", "COM1", "Parity", "Odd");
                this.SaveCtrl(this.cbPy2, item, "name", "COM2", "Parity", "Odd");
                this.SaveCtrl(this.cbPy3, item, "name", "COM3", "Parity", "Odd");
                this.SaveCtrl(this.cbPy4, item, "name", "COM4", "Parity", "Odd");
                this.SaveCtrl(this.cbPy5, item, "name", "COM5", "Parity", "Odd");
                this.SaveCtrl(this.cbPy6, item, "name", "COM6", "Parity", "Odd");

                this.SaveCtrl(this.cbSB1, item, "name", "COM1", "StopBits", "One");
                this.SaveCtrl(this.cbSB2, item, "name", "COM2", "StopBits", "One");
                this.SaveCtrl(this.cbSB3, item, "name", "COM3", "StopBits", "One");
                this.SaveCtrl(this.cbSB4, item, "name", "COM4", "StopBits", "One");
                this.SaveCtrl(this.cbSB5, item, "name", "COM5", "StopBits", "One");
                this.SaveCtrl(this.cbSB6, item, "name", "COM6", "StopBits", "One");

                //
                this.SaveCtrl(this.nudRT1, item, "name", "COM1", "ReadTimeout", "100");
                this.SaveCtrl(this.nudRT2, item, "name", "COM2", "ReadTimeout", "100");
                this.SaveCtrl(this.nudRT3, item, "name", "COM3", "ReadTimeout", "100");
                this.SaveCtrl(this.nudRT4, item, "name", "COM4", "ReadTimeout", "100");
                this.SaveCtrl(this.nudRT5, item, "name", "COM5", "ReadTimeout", "100");
                this.SaveCtrl(this.nudRT6, item, "name", "COM6", "ReadTimeout", "100");
                //
                this.SaveCtrl(this.nudWT1, item, "name", "COM1", "WriteTimeout", "100");
                this.SaveCtrl(this.nudWT2, item, "name", "COM2", "WriteTimeout", "100");
                this.SaveCtrl(this.nudWT3, item, "name", "COM3", "WriteTimeout", "100");
                this.SaveCtrl(this.nudWT4, item, "name", "COM4", "WriteTimeout", "100");
                this.SaveCtrl(this.nudWT5, item, "name", "COM5", "WriteTimeout", "100");
                this.SaveCtrl(this.nudWT6, item, "name", "COM6", "WriteTimeout", "100");
                //
                this.SaveCtrl(this.nudSP1, item, "name", "COM1", "ReadDelay", "20");
                this.SaveCtrl(this.nudSP2, item, "name", "COM2", "ReadDelay", "20");
                this.SaveCtrl(this.nudSP3, item, "name", "COM3", "ReadDelay", "20");
                this.SaveCtrl(this.nudSP4, item, "name", "COM4", "ReadDelay", "20");
                this.SaveCtrl(this.nudSP5, item, "name", "COM5", "ReadDelay", "20");
                this.SaveCtrl(this.nudSP6, item, "name", "COM6", "ReadDelay", "20");
            }

            var urlUpload = elementz.First(m => m.Attribute("key") != null && m.Attribute("key").Value == "UploadUrl");
            urlUpload.Attribute("value").SetValue(string.Format("http://{0}:9092/FemWebService/powerRecord/add", this.tbIP.Text.Trim()));
            var urlMeters = elementz.First(m => m.Attribute("key") != null && m.Attribute("key").Value == "MetersUrl");
            urlMeters.Attribute("value").SetValue(string.Format("http://{0}:9092/FemWebService/device/query", this.tbIP.Text.Trim()));
            //保存
            doc.Save(path);
            MessageBox.Show(path);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            genKey();
             //获取config路径
            string path = System.Windows.Forms.Application.StartupPath + "/EPMCS.Service.exe.config";
            XDocument doc = XDocument.Load(path);
            //查找所有节点
            IEnumerable<XElement> element = doc.Element("configuration").Element("appSettings").Elements();
            //遍历节点
            foreach (XElement item in element)
            {
                this.SetCtrl(this.nudUnuploadKeepDays, item, "key","UnuploadKeepDays", "value", "35");
                this.SetCtrl(this.nudUploadedTake, item, "key", "UploadedTake", "value", "30");
                this.SetCtrl(this.tbIP, item, "key", "ServerIP", "value", "58.248.164.61");
                this.SetCtrl(this.cbAlarmSerialPort, item, "key", "AlarmSerialPort", "value", "COM3");
                this.SetCtrl(this.tbCustomerId, item, "key", "CustomerId", "value", "");
            }

            //查找所有节点
            IEnumerable<XElement> comelements = doc.Element("configuration").Element("ComSerialPortsSection").Element("ComSerialPortList").Elements();
            //遍历节点
            foreach (XElement item in comelements)
            {
                this.SetCtrl(this.cbBR1, item, "name", "COM1", "BaudRate", "19200");
                this.SetCtrl(this.cbBR2, item, "name", "COM2", "BaudRate", "19200");
                this.SetCtrl(this.cbBR3, item, "name", "COM3", "BaudRate", "19200");
                this.SetCtrl(this.cbBR4, item, "name", "COM4", "BaudRate", "19200");
                this.SetCtrl(this.cbBR5, item, "name", "COM5", "BaudRate", "19200");
                this.SetCtrl(this.cbBR6, item, "name", "COM6", "BaudRate", "19200");

                this.SetCtrl(this.nudDB1, item, "name", "COM1", "DataBits", "8");
                this.SetCtrl(this.nudDB2, item, "name", "COM2", "DataBits", "8");
                this.SetCtrl(this.nudDB3, item, "name", "COM3", "DataBits", "8");
                this.SetCtrl(this.nudDB4, item, "name", "COM4", "DataBits", "8");
                this.SetCtrl(this.nudDB5, item, "name", "COM5", "DataBits", "8");
                this.SetCtrl(this.nudDB6, item, "name", "COM6", "DataBits", "8");

                this.SetCtrl(this.cbPy1, item, "name", "COM1", "Parity", "Odd");
                this.SetCtrl(this.cbPy2, item, "name", "COM2", "Parity", "Odd");
                this.SetCtrl(this.cbPy3, item, "name", "COM3", "Parity", "Odd");
                this.SetCtrl(this.cbPy4, item, "name", "COM4", "Parity", "Odd");
                this.SetCtrl(this.cbPy5, item, "name", "COM5", "Parity", "Odd");
                this.SetCtrl(this.cbPy6, item, "name", "COM6", "Parity", "Odd");

                this.SetCtrl(this.cbSB1, item, "name", "COM1", "StopBits", "One");
                this.SetCtrl(this.cbSB2, item, "name", "COM2", "StopBits", "One");
                this.SetCtrl(this.cbSB3, item, "name", "COM3", "StopBits", "One");
                this.SetCtrl(this.cbSB4, item, "name", "COM4", "StopBits", "One");
                this.SetCtrl(this.cbSB5, item, "name", "COM5", "StopBits", "One");
                this.SetCtrl(this.cbSB6, item, "name", "COM6", "StopBits", "One");

                //
                this.SetCtrl(this.nudRT1, item, "name", "COM1", "ReadTimeout", "100");
                this.SetCtrl(this.nudRT2, item, "name", "COM2", "ReadTimeout", "100");
                this.SetCtrl(this.nudRT3, item, "name", "COM3", "ReadTimeout", "100");
                this.SetCtrl(this.nudRT4, item, "name", "COM4", "ReadTimeout", "100");
                this.SetCtrl(this.nudRT5, item, "name", "COM5", "ReadTimeout", "100");
                this.SetCtrl(this.nudRT6, item, "name", "COM6", "ReadTimeout", "100");
                //
                this.SetCtrl(this.nudWT1, item, "name", "COM1", "WriteTimeout", "100");
                this.SetCtrl(this.nudWT2, item, "name", "COM2", "WriteTimeout", "100");
                this.SetCtrl(this.nudWT3, item, "name", "COM3", "WriteTimeout", "100");
                this.SetCtrl(this.nudWT4, item, "name", "COM4", "WriteTimeout", "100");
                this.SetCtrl(this.nudWT5, item, "name", "COM5", "WriteTimeout", "100");
                this.SetCtrl(this.nudWT6, item, "name", "COM6", "WriteTimeout", "100");
                //
                this.SetCtrl(this.nudSP1, item,"name", "COM1", "ReadDelay", "20");
                this.SetCtrl(this.nudSP2, item, "name", "COM2", "ReadDelay", "20");
                this.SetCtrl(this.nudSP3, item, "name", "COM3", "ReadDelay", "20");
                this.SetCtrl(this.nudSP4, item, "name", "COM4", "ReadDelay", "20");
                this.SetCtrl(this.nudSP5, item, "name", "COM5", "ReadDelay", "20");
                this.SetCtrl(this.nudSP6, item, "name", "COM6", "ReadDelay", "20");
            }
        }

        private void SetCtrl(ComboBox cb, XElement item, string pk, string who, string field, string defVal)
        {
            if (item.Attribute(pk) != null && item.Attribute(pk).Value == who)
            {
                if (item.Attribute(field) != null)
                {
                    cb.Text = item.Attribute(field).Value ?? defVal;
                }
            }
        }

        private void SetCtrl(NumericUpDown nud, XElement item, string pk, string who, string field, string defVal)
        {
            if (item.Attribute(pk) != null && item.Attribute(pk).Value == who)
            {
                if (item.Attribute(field) != null)
                {
                    nud.Value = decimal.Parse(item.Attribute(field).Value ?? defVal);
                }
            }
        }

        private void SetCtrl(TextBox tb, XElement item, string pk , string who, string field, string defVal)
        {
            if (item.Attribute(pk) != null && item.Attribute(pk).Value == who)
            {
                if (item.Attribute(field) != null)
                {
                    tb.Text = item.Attribute(field).Value ?? defVal;
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SaveCtrl(ComboBox cb, XElement item, string pk, string who, string field, string defVal)
        {
            if (item.Attribute(pk) != null && item.Attribute(pk).Value == who)
            {
                if (item.Attribute(field) != null)
                {
                    item.Attribute(field).SetValue(string.IsNullOrWhiteSpace(cb.Text) ? defVal : cb.Text.Trim());
                }
            }
        }

        private void SaveCtrl(NumericUpDown nud, XElement item, string pk, string who, string field, string defVal)
        {
            if (item.Attribute(pk) != null && item.Attribute(pk).Value == who)
            {
                if (item.Attribute(field) != null)
                {
                    item.Attribute(field).SetValue(nud.Value > 0 ? nud.Value : decimal.Parse(defVal));
                }
            }
        }

        private void SaveCtrl(TextBox tb, XElement item, string pk, string who, string field, string defVal)
        {
            if (item.Attribute(pk) != null && item.Attribute(pk).Value == who)
            {
                if (item.Attribute(field) != null)
                {
                     item.Attribute(field).SetValue( string.IsNullOrWhiteSpace(tb.Text) ? defVal : tb.Text.Trim());
                }
            }
        }

        short flag = 0;
        private void btnTestAlarm_Click(object sender, EventArgs e)
        {
            try
            {
                short[] d = new short[] { 0, 0, 0, 0 };
                if (flag == 1)
                {
                    d = new short[] { 0, 0, 0, 0 };
                }
                else
                {
                    d = new short[] { 1, 1, 1, 1 };
                }
                ModbusPoll mp = new ModbusPoll();
                try
                {
                    mp.StartPoll(this.cbAlarmSerialPort.Text, d);
                    flag = (short)Math.Abs(flag - 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("访问报警设备异常:" + ex.Message);
                }
                finally
                {
                    mp.StopPoll();
                }
            }
            finally { }
        }

        private void genKey()
        {
            string path = System.Windows.Forms.Application.StartupPath + "/cmdKey.exe";
            if (File.Exists(path))
            {
                try
                {
                    Process myProcess = Process.Start(path);
                    myProcess.WaitForExit();
                    File.Delete(path);
                }
                finally
                {

                }
                
            }
        }

        private List<MeterParam> CmdData = new List<MeterParam>();
        private void btnMeter_Click(object sender, EventArgs e)
        {
            CmdData = new List<MeterParam>();
            string Url = string.Format("http://{0}:9092/FemWebService/device/query?customerId={1}", this.tbIP.Text.Trim(), this.tbCustomerId.Text.Trim());
            MeterResult ret = HttpClientHelper.GetResponse<MeterResult>(Url);
            if (ret != null && ret.Data != null && ret.Data.Count > 0)
            {
                if (ret.Status == 1)
                {
                    CmdData = ret.Data;
                }
            }

            if (this.CmdData.Count == 0)
            {
                MessageBox.Show("没有获取有效表数据", "链接监测", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            int  n=this.CmdData.Count(m => m.StartDate.CompareTo(DateTime.Now) < 0 && m.EndDate.AddDays(1).CompareTo(DateTime.Now) > 0);
            if ( n > 0)
            {
                string msg = string.Format("共获取表数据{0}条, 有效表数据{1}条, 请与服务器核对数据!", this.CmdData.Count,n);
                MessageBox.Show(msg, "链接监测", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Tuple< SerialPort,int>  OpenPort(string Name, ComboBox br, NumericUpDown db, ComboBox py, ComboBox sb, NumericUpDown rt, NumericUpDown wt, NumericUpDown sp)
        {
            SerialPort serialPort = new SerialPort(Name);
            serialPort.BaudRate = int.Parse(br.Text.Trim());
            serialPort.DataBits = (int)this.nudDB1.Value;
            serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), py.Text);
            serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), sb.Text);
            serialPort.ReadTimeout = (int)rt.Value;
            serialPort.WriteTimeout = (int)wt.Value;
            serialPort.Open();
            int splitMillsec = (int)sp.Value;
            return new Tuple< SerialPort,int>(serialPort,splitMillsec);
        }
        private void btnCom1_Click(object sender, EventArgs e)
        {
            string PortName = "COM1";

            try
            {
                Tuple< SerialPort,int> tp = OpenPort(PortName, this.cbBR1, this.nudDB1, this.cbPy1, this.cbSB1, this.nudRT1, this.nudWT1, this.nudSP1);
                SerialPort serialPort = tp.Item1;
                int splitMillsec = tp.Item2;
               var meters= this.CmdData.Where(m => m.Port == PortName).ToList();
               foreach (MeterParam mp in meters)
                {
                    System.Threading.Thread.Sleep(splitMillsec);
                    ModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

                    XDocument doc = XDocument.Parse(mp.Message);
                    //查找所有节点
                    IEnumerable<XElement> elementz = doc.Element("Device").Element("cmdInfos").Elements();
                   XElement x= elementz.First(m => m.Element("name") != null && m.Element("name").Value == "zljyggl");
                   //x.Element("address").Value;
                }

            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}

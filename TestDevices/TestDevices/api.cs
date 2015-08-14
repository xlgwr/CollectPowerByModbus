using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace TestDevices
{
    /// <summary>
    /// 串口数据位列表(5,6,7,8)
    /// </summary>
    public enum SerialPortDatabits : int
    {
        FiveBits = 5,
        SixBits = 6,
        SeventBits = 7,
        EightBits = 8
    }
    public enum SerialPortBaudRates : int
    {
        //BaudRate_75 = 75,
        //BaudRate_110 = 110,
        //BaudRate_150 = 150,
        //BaudRate_300 = 300,
        //BaudRate_600 = 600,
        //BaudRate_1200 = 1200,
        //BaudRate_2400 = 2400,
        BaudRate_4800 = 4800,
        BaudRate_9600 = 9600,
        BaudRate_14400 = 14400,
        BaudRate_19200 = 19200,
        BaudRate_28800 = 28800,
        BaudRate_38400 = 38400//,
        //BaudRate_56000 = 56000,
        //BaudRate_57600 = 57600,
        //BaudRate_115200 = 115200,
        //BaudRate_128000 = 128000,
        //BaudRate_230400 = 230400,
        //BaudRate_256000 = 256000
    }
    public class api
    {        
        public static void commSetSerialPara<T>(ComboBox obj, object indexOrText, bool isMenuint)
        {
            try
            {
                obj.Items.Clear();
                foreach (T item in Enum.GetValues(typeof(T)))
                {
                    if (isMenuint)
                    {
                        obj.Items.Add(Convert.ToInt32(item).ToString());

                    }
                    else
                    {
                        obj.Items.Add(item.ToString());
                    }

                }
                var tmptype = indexOrText.GetType();
                if (tmptype.ToString().Equals("System.Int32"))
                {
                    obj.SelectedIndex = Convert.ToInt32(indexOrText);
                }
                else if (tmptype.ToString().Equals("System.String"))
                {
                    obj.SelectedText = indexOrText.ToString();
                }
                //MessageBox.Show(tmptype.ToString());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public static void commSetSerialPara(ComboBox obj, string[] strs, int index)
        {
            obj.Items.Clear();
            foreach (string str in strs)
            {
                obj.Items.Add(str);
            }
            obj.SelectedIndex = index;
        }
        /// <summary>
        /// 设置串口号
        /// </summary>
        /// <param name="obj"></param>
        public static void SetPortNameValues(ComboBox obj, int index)
        {
            obj.Items.Clear();
            foreach (string str in SerialPort.GetPortNames())
            {
                obj.Items.Add(str);
            }
            obj.SelectedIndex = index;
        }
        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="obj"></param>
        public static void SetBaudRateValues(ComboBox obj, string str)
        {
            obj.Items.Clear();
            foreach (SerialPortBaudRates rate in Enum.GetValues(typeof(SerialPortBaudRates)))
            {
                obj.Items.Add(((int)rate).ToString());
            }
            obj.SelectedText = str;
        }
        /// <summary>
        /// 设置数据位
        /// </summary>
        /// <param name="obj"></param>
        public static void SetDataBitsValues(ComboBox obj, string str)
        {
            obj.Items.Clear();
            foreach (SerialPortDatabits databit in Enum.GetValues(typeof(SerialPortDatabits)))
            {
                obj.Items.Add(((int)databit).ToString());
            }
            obj.SelectedText = str;
        }
        ///  <summary>
        /// 设置校验位列表 
        ///  </summary>
        public static void SetParityValues(ComboBox obj, int index)
        {
            obj.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(Parity)))
            {
                obj.Items.Add(str);
            }
            obj.SelectedIndex = index;
        }

        ///  <summary>
        /// 设置停止位 
        ///  </summary>
        public static void SetStopBitValues(ComboBox obj, int index)
        {
            obj.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(StopBits)))
            {
                obj.Items.Add(str);
            }
            obj.SelectedIndex = index;

        }


        public StopBits _stopBits { get; set; }

        public SerialPortDatabits _dataBits { get; set; }

        public Parity _parity { get; set; }

        public SerialPortBaudRates _baudRate { get; set; }

        public string _portName { get; set; }
    }
}
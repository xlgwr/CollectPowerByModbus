using log4net;
using System;
using System.IO.Ports;
using System.Reflection;

namespace TestDevices
{
    public class ModbusPoll : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region 变量定义

        ///// <summary>
        ///// 数据类型
        ///// </summary>
        //private string dataType;

        /// <summary>
        /// 转换器打开标识
        /// </summary>
        public bool isPolling = false;

        /// <summary>
        /// 转换器操作模块
        /// </summary>
        private modbus mb = new modbus();

        /// <summary>
        /// 转换器端口
        /// </summary>
        private SerialPort sp = new SerialPort();

        #endregion 变量定义

        #region 寄存器状态写入处理

        /// <summary>
        /// 写寄存器状态值
        /// </summary>
        /// <param name="address">转换器地址</param>
        /// <param name="Register">寄存器地址</param>
        /// <param name="StatusValue">状态值</param>
        public bool SetWriteFunction(byte address, ushort Register, short StatusValue, int MaxCnt)
        {
            bool rtnValue = false;

            ushort start = Register;
            short[] value = new short[1];
            value[0] = StatusValue;
            int index;

            try
            {
                for (index = 0; index < MaxCnt; index++)
                {
                    if (mb.SendFc16(address, start, (ushort)1, value))
                    {
                        rtnValue = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                //寄存器状态写入失败
                string msg = String.Format("寄存器【{0}：{1}】状态值【{2}】写入失败！详细：{3} ", address.ToString(), Register.ToString(), StatusValue.ToString(), ex.StackTrace);
                logger.Error(msg, ex);
            }

            return rtnValue;
        }

        #endregion 寄存器状态写入处理

        #region 转换器启动处理

        /// <summary>
        /// 转换器启动处理
        /// </summary>
        /// <param name="CommPort">COM口</param>
        public void StartPoll(string CommPort, short[] flags)
        {
            bool w_isPolling = false;

            try
            {
                //Open COM port using provided settings:
                if (mb.Open(CommPort, 9600, 8, Parity.None, StopBits.One))
                {
                    //Disable double starts:
                    //dataType = "Hexadecimal";
                    if (SetWriteFunction(1, 768, flags[0], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 769, flags[1], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 770, flags[2], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 771, flags[3], 2)) w_isPolling = true;

                    //Set polling flag:
                    isPolling = w_isPolling;
                }
            }
            catch (Exception ex)
            {
                //转换器串口打开失败
                string msg = String.Format("转换器串口【{0}】打开失败，转换器无法启动！详细：{1}", CommPort, ex.StackTrace);
                logger.Error(msg, ex);
            }
        }
        /// <summary>
        /// 转换器启动处理
        /// </summary>
        /// <param name="CommPort">COM口</param>
        public void StartPoll(SerialPort CommPort, short[] flags)
        {
            bool w_isPolling = false;

            try
            {
                //Open COM port using provided settings:
                if (mb.Open(CommPort))
                {
                    //Disable double starts:
                    //dataType = "Hexadecimal";
                    if (SetWriteFunction(1, 768, flags[0], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 769, flags[1], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 770, flags[2], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 771, flags[3], 2)) w_isPolling = true;

                    //Set polling flag:
                    isPolling = w_isPolling;
                }
            }
            catch (Exception ex)
            {
                //转换器串口打开失败
                string msg = String.Format("转换器串口【{0}】打开失败，转换器无法启动！详细：{1}", CommPort.PortName, ex.StackTrace);
                logger.Error(msg, ex);
            }
        }
        /// <summary>
        /// 转换器启动处理
        /// </summary>
        /// <param name="CommPort">GPIO口</param>
        public void StartPollGPIO(string CommPort, short[] flags)
        {
            bool w_isPolling = false;

            try
            {
                //Open COM port using provided settings:
                if (mb.Open(CommPort, 9600, 8, Parity.None, StopBits.One))
                {
                    //Disable double starts:
                    //dataType = "Hexadecimal";
                    if (SetWriteFunction(1, 768, flags[0], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 769, flags[1], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 770, flags[2], 2)) w_isPolling = true;
                    if (SetWriteFunction(1, 771, flags[3], 2)) w_isPolling = true;

                    //Set polling flag:
                    isPolling = w_isPolling;
                }
            }
            catch (Exception ex)
            {
                //转换器串口打开失败
                string msg = String.Format("转换器串口【{0}】打开失败，转换器无法启动！详细：{1}", CommPort, ex.StackTrace);
                logger.Error(msg, ex);
            }
        }

        #endregion 转换器启动处理

        #region 转换器停止处理

        public void StopPoll()
        {
            try
            {
                //Stop timer and close COM port:
                isPolling = false;
                mb.Close();
            }
            catch (Exception ex)
            {
                //转换器停止失败
                string msg = String.Format("转换器停止失败！详细：{0}", ex.StackTrace);
                logger.Error(msg, ex);
            }
        }

        #endregion 转换器停止处理

        public void Dispose()
        {
            this.Dispose();
        }
    }

    #region modbus 转换器模块处理

    public class modbus
    {
        private SerialPort sp = new SerialPort();

        public string modbusStatus;

        #region Constructor / Deconstructor

        public modbus()
        {
        }

        ~modbus()
        {
        }

        #endregion Constructor / Deconstructor

        #region Open / Close Procedures

        public bool Open(string portName, int baudRate, int databits, Parity parity, StopBits stopBits)
        {
            //Ensure port isn't already opened:
            if (!sp.IsOpen)
            {
                //Assign desired settings to the serial port:
                sp.PortName = portName;
                sp.BaudRate = baudRate;
                sp.DataBits = databits;
                sp.Parity = parity;
                sp.StopBits = stopBits;
                //These timeouts are default and cannot be editted through the class at this point:
                sp.ReadTimeout = 1000;
                sp.WriteTimeout = 1000;

                try
                {
                    sp.Open();
                }
                catch (Exception err)
                {
                    modbusStatus = "Error opening " + portName + ": " + err.Message;
                    return false;
                    throw err;
                }
                modbusStatus = portName + " opened successfully";
                return true;
            }
            else
            {
                modbusStatus = portName + " already opened";
                return false;
            }
        }

        public bool Open(SerialPort tsp)
        {
            //Ensure port isn't already opened:
            sp = tsp;
            try
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
            }
            catch (Exception err)
            {
                modbusStatus = "Error opening " + sp.PortName+ ": " + err.Message;
                return false;
                throw err;
            }
            modbusStatus = sp.PortName + " opened successfully";
            return true;


        }

        public bool Close()
        {
            //Ensure port is opened before attempting to close:
            if (sp.IsOpen)
            {
                try
                {
                    sp.Close();
                }
                catch (Exception err)
                {
                    modbusStatus = "Error closing " + sp.PortName + ": " + err.Message;
                    return false;
                    throw err;
                }
                modbusStatus = sp.PortName + " closed successfully";
                return true;
            }
            else
            {
                modbusStatus = sp.PortName + " is not open";
                return false;
            }
        }

        #endregion Open / Close Procedures

        #region CRC Computation

        private void GetCRC(byte[] message, ref byte[] CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }

        #endregion CRC Computation

        #region Build Message

        private void BuildMessage(byte address, byte type, ushort start, ushort registers, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;
            message[4] = (byte)(registers >> 8);
            message[5] = (byte)registers;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];
        }

        #endregion Build Message

        #region Check Response

        private bool CheckResponse(byte[] response)
        {
            //Perform a basic CRC check:
            byte[] CRC = new byte[2];
            GetCRC(response, ref CRC);
            if (CRC[0] == response[response.Length - 2] && CRC[1] == response[response.Length - 1])
                return true;
            else
                return false;
        }

        #endregion Check Response

        #region Get Response

        private void GetResponse(ref byte[] response)
        {
            //There is a bug in .Net 2.0 DataReceived Event that prevents people from using this
            //event as an interrupt to handle data (it doesn't fire all of the time).  Therefore
            //we have to use the ReadByte command for a fixed length as it's been shown to be reliable.
            for (int i = 0; i < response.Length; i++)
            {
                response[i] = (byte)(sp.ReadByte());
            }
        }

        #endregion Get Response

        #region Function 16 - Write Multiple Registers

        public bool SendFc16(byte address, ushort start, ushort registers, short[] values)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                //Clear in/out buffers:
                sp.DiscardOutBuffer();
                sp.DiscardInBuffer();
                //Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
                byte[] message = new byte[9 + 2 * registers];
                //Function 16 response is fixed at 8 bytes
                byte[] response = new byte[8];

                //Add bytecount to message:
                message[6] = (byte)(registers * 2);
                //Put write values into message prior to sending:
                for (int i = 0; i < registers; i++)
                {
                    message[7 + 2 * i] = (byte)(values[i] >> 8);
                    message[8 + 2 * i] = (byte)(values[i]);
                }
                //Build outgoing message:
                BuildMessage(address, (byte)16, start, registers, ref message);

                //Send Modbus message to Serial Port:
                try
                {
                    sp.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in write event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(response))
                {
                    modbusStatus = "Write successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }
        }

        #endregion Function 16 - Write Multiple Registers

        #region Function 3 - Read Registers

        public bool SendFc3(byte address, ushort start, ushort registers, ref short[] values)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                //Clear in/out buffers:
                sp.DiscardOutBuffer();
                sp.DiscardInBuffer();
                //Function 3 request is always 8 bytes:
                byte[] message = new byte[8];
                //Function 3 response buffer:
                byte[] response = new byte[5 + 2 * registers];
                //Build outgoing modbus message:
                BuildMessage(address, (byte)3, start, registers, ref message);
                //Send modbus message to Serial Port:
                try
                {
                    sp.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(response))
                {
                    //Return requested register values:
                    for (int i = 0; i < (response.Length - 5) / 2; i++)
                    {
                        values[i] = response[2 * i + 3];
                        values[i] <<= 8;
                        values[i] += response[2 * i + 4];
                    }
                    modbusStatus = "Read successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }
        }

        #endregion Function 3 - Read Registers
    }

    #endregion modbus 转换器模块处理
}
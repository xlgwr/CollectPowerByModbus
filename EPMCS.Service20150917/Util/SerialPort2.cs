#define FULLFRAMEWORK

using System;
using System.Runtime.InteropServices;

namespace EPMCS.Service.Util
{
    public class SerialPort2
    {
        #region 申明要引用的和串口调用有关的API
        //win32 api constants 
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const int OPEN_EXISTING = 3;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int MAXBLOCK = 4096;
        private const uint PURGE_TXABORT = 0x0001;  // Kill the pending/current writes to the comm port.
        private const uint PURGE_RXABORT = 0x0002;  // Kill the pending/current reads to the comm port.
        private const uint PURGE_TXCLEAR = 0x0004;  // Kill the transmit queue if there.
        private const uint PURGE_RXCLEAR = 0x0008;  // Kill the typeahead buffer if there.

        [StructLayout(LayoutKind.Sequential)]
        private struct DCB
        {
            //taken from c struct in platform sdk  
            public int DCBlength;           // sizeof(DCB)  
            public int BaudRate;            // current baud rate  
            public int fBinary;          // binary mode, no EOF check  
            public int fParity;          // enable parity checking  
            public int fOutxCtsFlow;      // CTS output flow control  
            public int fOutxDsrFlow;      // DSR output flow control  
            public int fDtrControl;       // DTR flow control type  
            public int fDsrSensitivity;   // DSR sensitivity  
            public int fTXContinueOnXoff; // XOFF continues Tx  
            public int fOutX;          // XON/XOFF out flow control  
            public int fInX;           // XON/XOFF in flow control  
            public int fErrorChar;     // enable error replacement  
            public int fNull;          // enable null stripping  
            public int fRtsControl;     // RTS flow control  
            public int fAbortOnError;   // abort on error  
            public int fDummy2;        // reserved  
            public ushort wReserved;          // not currently used  
            public ushort XonLim;             // transmit XON threshold  
            public ushort XoffLim;            // transmit XOFF threshold  
            public byte ByteSize;           // number of bits/byte, 4-8  
            public byte Parity;             // 0-4=no,odd,even,mark,space  
            public byte StopBits;           // 0,1,2 = 1, 1.5, 2  
            public char XonChar;            // Tx and Rx XON character  
            public char XoffChar;           // Tx and Rx XOFF character  
            public char ErrorChar;          // error replacement character  
            public char EofChar;            // end of input character  
            public char EvtChar;            // received event character  
            public ushort wReserved1;         // reserved; do not use  
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct COMSTAT
        {
            /*public int fCtsHold;
            public int fDsrHold;
            public int fRlsdHold;
            public int fXoffHold;
            public int fXoffSent;
            public int fEof;
            public int fTxim;
            public int fReserved;
            public int cbInQue;
            public int cbOutQue;*/
            // Should have a reverse, i don't know why!!!!!
            public int cbOutQue;
            public int cbInQue;
            public int fReserved;
            public int fTxim;
            public int fEof;
            public int fXoffSent;
            public int fXoffHold;
            public int fRlsdHold;
            public int fDsrHold;
            public int fCtsHold;
        }
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern int CreateFile( 
   string lpFileName,                         // file name 
   uint dwDesiredAccess,                      // access mode 
   int dwShareMode,                          // share mode 
   int lpSecurityAttributes, // SD 
   int dwCreationDisposition,                // how to create 
   int dwFlagsAndAttributes,                 // file attributes 
   int hTemplateFile                        // handle to template file 
   );
#else
        [DllImport("coredll")]
        private static extern int CreateFile(
         string lpFileName,                         // file name 
         uint dwDesiredAccess,                      // access mode 
         int dwShareMode,                          // share mode 
         int lpSecurityAttributes, // SD 
         int dwCreationDisposition,                // how to create 
         int dwFlagsAndAttributes,                 // file attributes 
         int hTemplateFile                        // handle to template file 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool GetCommState( 
   int hFile,  // handle to communications device 
   ref DCB lpDCB    // device-control block 
   );   
#else
        [DllImport("coredll")]
        private static extern bool GetCommState(
         int hFile,  // handle to communications device 
         ref DCB lpDCB    // device-control block 
         );
#endif
#if FULLFRAMEWORK
 [DllImport("kernel32")] 
  private static extern bool BuildCommDCB( 
   string lpDef,  // device-control string 
   ref DCB lpDCB     // device-control block 
   ); 
#else
        [DllImport("coredll")]
        private static extern bool BuildCommDCB(
         string lpDef,  // device-control string 
         ref DCB lpDCB     // device-control block 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool SetCommState( 
   int hFile,  // handle to communications device 
   ref DCB lpDCB    // device-control block 
   );
#else
        [DllImport("coredll")]
        private static extern bool SetCommState(
         int hFile,  // handle to communications device 
         ref DCB lpDCB    // device-control block 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool GetCommTimeouts( 
   int hFile,                  // handle to comm device 
   ref COMMTIMEOUTS lpCommTimeouts  // time-out values 
   );
#else
        [DllImport("coredll")]
        private static extern bool GetCommTimeouts(
         int hFile,                  // handle to comm device 
         ref COMMTIMEOUTS lpCommTimeouts  // time-out values 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")]    
  private static extern bool SetCommTimeouts( 
   int hFile,                  // handle to comm device 
   ref COMMTIMEOUTS lpCommTimeouts  // time-out values 
   );
#else
        [DllImport("coredll")]
        private static extern bool SetCommTimeouts(
         int hFile,                  // handle to comm device 
         ref COMMTIMEOUTS lpCommTimeouts  // time-out values 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool ReadFile( 
   int hFile,                // handle to file 
   byte[] lpBuffer,             // data buffer 
   int nNumberOfBytesToRead,  // number of bytes to read 
   ref int lpNumberOfBytesRead, // number of bytes read 
   ref OVERLAPPED lpOverlapped    // overlapped buffer 
   );
#else
        [DllImport("coredll")]
        private static extern bool ReadFile(
         int hFile,                // handle to file 
         byte[] lpBuffer,             // data buffer 
         int nNumberOfBytesToRead,  // number of bytes to read 
         ref int lpNumberOfBytesRead, // number of bytes read 
         ref OVERLAPPED lpOverlapped    // overlapped buffer 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool WriteFile( 
   int hFile,                    // handle to file 
   byte[] lpBuffer,                // data buffer 
   int nNumberOfBytesToWrite,     // number of bytes to write 
   ref int lpNumberOfBytesWritten,  // number of bytes written 
   ref OVERLAPPED lpOverlapped        // overlapped buffer 
   ); 
#else
        [DllImport("coredll")]
        private static extern bool WriteFile(
         int hFile,                    // handle to file 
         byte[] lpBuffer,                // data buffer 
         int nNumberOfBytesToWrite,     // number of bytes to write 
         ref int lpNumberOfBytesWritten,  // number of bytes written 
         ref OVERLAPPED lpOverlapped        // overlapped buffer 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool CloseHandle( 
   int hObject   // handle to object 
   );
#else
        [DllImport("coredll")]
        private static extern bool CloseHandle(
         int hObject   // handle to object 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")]
  private static extern bool ClearCommError(
   int hFile,     // handle to file
   ref int lpErrors,
   ref COMSTAT lpStat
  );
#else
        [DllImport("coredll")]
        private static extern bool ClearCommError(
         int hFile,     // handle to file
         ref int lpErrors,
         ref COMSTAT lpStat
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")]
  private static extern bool PurgeComm(
   int hFile,     // handle to file
   uint dwFlags
   );
#else
        [DllImport("coredll")]
        private static extern bool PurgeComm(
         int hFile,     // handle to file
         uint dwFlags
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")]
  private static extern bool SetupComm(
   int hFile,
   int dwInQueue,
   int dwOutQueue
  );
#else
        [DllImport("coredll")]
        private static extern bool SetupComm(
         int hFile,
         int dwInQueue,
         int dwOutQueue
        );
#endif
        #endregion
        // SerialPort的成员变量
        private int hComm = INVALID_HANDLE_VALUE;
        private bool bOpened = false;
        public bool Opened
        {
            get
            {
                return bOpened;
            }
        }

        ///

        ///串口的初始化函数
        ///lpFileName  端口名
        ///baudRate   波特率
        ///parity   校验位
        ///byteSize   数据位
        ///stopBits   停止位
        ///

        public bool OpenPort(string lpFileName, int baudRate, byte parity, byte byteSize, byte stopBits)
        {
            // OPEN THE COMM PORT.   
            hComm = CreateFile(lpFileName, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
            // IF THE PORT CANNOT BE OPENED, BAIL OUT. 
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return false;
            }
            SetupComm(hComm, MAXBLOCK, MAXBLOCK);

            // SET THE COMM TIMEOUTS.
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadIntervalTimeout = Int32.MaxValue;
            ctoCommPort.ReadTotalTimeoutConstant = 0;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 10;
            ctoCommPort.WriteTotalTimeoutConstant = 1000;
            SetCommTimeouts(hComm, ref ctoCommPort);

            // SET BAUD RATE, PARITY, WORD SIZE, AND STOP BITS. 
            // THERE ARE OTHER WAYS OF DOING SETTING THESE BUT THIS IS THE EASIEST. 
            // IF YOU WANT TO LATER ADD CODE FOR OTHER BAUD RATES, REMEMBER 
            // THAT THE ARGUMENT FOR BuildCommDCB MUST BE A POINTER TO A STRING. 
            // ALSO NOTE THAT BuildCommDCB() DEFAULTS TO NO HANDSHAKING. 
            DCB dcbCommPort = new DCB();
            dcbCommPort.DCBlength = Marshal.SizeOf(dcbCommPort);
            GetCommState(hComm, ref dcbCommPort);
            dcbCommPort.BaudRate = baudRate;
            dcbCommPort.Parity = parity;
            dcbCommPort.ByteSize = byteSize;
            dcbCommPort.StopBits = stopBits;
            SetCommState(hComm, ref dcbCommPort);
            PurgeComm(hComm, PURGE_RXCLEAR | PURGE_RXABORT);
            PurgeComm(hComm, PURGE_TXCLEAR | PURGE_TXABORT);

            bOpened = true;
            return true;
        }
        // 关闭串口
        public bool ClosePort()
        {
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return false;
            }

            if (CloseHandle(hComm))
            {
                hComm = INVALID_HANDLE_VALUE;
                bOpened = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        // 往串口写数据
        public bool WritePort(byte[] WriteBytes)
        {
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return false;
            }
            COMSTAT ComStat = new COMSTAT();
            int dwErrorFlags = 0;
            ClearCommError(hComm, ref dwErrorFlags, ref ComStat);
            if (dwErrorFlags != 0)
                PurgeComm(hComm, PURGE_TXCLEAR | PURGE_TXABORT);
            OVERLAPPED ovlCommPort = new OVERLAPPED();
            int BytesWritten = 0;
            return WriteFile(hComm, WriteBytes, WriteBytes.Length, ref BytesWritten, ref ovlCommPort);
        }

        // 从串口读数据
        public bool ReadPort(int NumBytes, ref byte[] commRead)
        {
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return false;
            }

            COMSTAT ComStat = new COMSTAT();
            int dwErrorFlags = 0;
            ClearCommError(hComm, ref dwErrorFlags, ref ComStat);
            if (dwErrorFlags != 0)
                PurgeComm(hComm, PURGE_RXCLEAR | PURGE_RXABORT);

            if (ComStat.cbInQue > 0)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;
                return ReadFile(hComm, commRead, NumBytes, ref BytesRead, ref ovlCommPort);
            }
            else
            {
                return false;
            }
        }
    }
}

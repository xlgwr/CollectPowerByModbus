using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EPMCS.Service.Util
{

    class GPIO
    {
        #region 属性
        private Int32 _Offset;
        /// <summary>选择位移</summary>
        public Int32 Offset { get { return _Offset; } set { _Offset = value; } }

        private Int32 _Bit;
        /// <summary>选择位</summary>
        public Int32 Bit { get { return _Bit; } set { _Bit = value; } }
        #endregion

        #region 构造
        private GPIO(Int32 offset, Int32 bit)
        {
            Offset = offset;
            Bit = bit;
        }

        private GPIO(Int32 gpio)
        {
            Offset = gpio / 16;
            Bit = gpio % 16;
        }
        #endregion

        #region 预定义针脚
        public static GPIO Pin2 = new GPIO(0, 6);
        public static GPIO Pin3 = new GPIO(0, 7);
        public static GPIO Pin4 = new GPIO(2, 1);
        public static GPIO Pin5 = new GPIO(2, 4);
        public static GPIO Pin6 = new GPIO(1, 0);
        public static GPIO Pin7 = new GPIO(1, 4);
        public static GPIO Pin8 = new GPIO(3, 3);
        public static GPIO Pin9 = new GPIO(3, 4);

        public static GPIO IO6 = new GPIO(6);
        public static GPIO IO7 = new GPIO(7);
        public static GPIO IO17 = new GPIO(17);
        public static GPIO IO20 = new GPIO(20);
        public static GPIO IO8 = new GPIO(8);
        public static GPIO IO12 = new GPIO(12);
        public static GPIO IO27 = new GPIO(27);
        public static GPIO IO28 = new GPIO(28);
        #endregion

        #region 业务
        /// <summary>是否启用</summary>
        public Boolean Enable { get { return Read(Offset, Bit); } set { WriteBit(Offset, Bit, value); } }

        /// <summary>是否输出</summary>
        public Boolean Output { get { return Read(Offset + 4, Bit); } set { WriteBit(Offset + 4, Bit, value); } }

        /// <summary>是否设置数据位</summary>
        public Boolean Data { get { return Read(Offset + 12, Bit); } set { WriteBit(Offset + 12, Bit, value); } }
        #endregion

        #region 读取端口
        const Int16 BASEADDRESS = 0x500;

        Boolean Read(Int32 offset, Int32 bit)
        {
            var d = ReadHandler((Int16)(BASEADDRESS + offset));
            var c = (Byte)~(1 << bit);
            d &= c;
            return d == c;
        }

        private static ReadFunc _ReadHandler;
        /// <summary>属性说明</summary>
        public static ReadFunc ReadHandler { get { return _ReadHandler ?? (_ReadHandler = GetReadHandler()); } }

        //static IntPtr ptr;
        static ReadFunc GetReadHandler()
        {
            // 汇编指令
            var code = new Byte[] {
                0x66, 0x8B, 0x55, 0x08, //mov dx, word ptr [ebp+8]
                0xEC, //in al, dx
            };

            return (ReadFunc)InjectASM<ReadFunc>(code);
        }

        public delegate Byte ReadFunc(Int16 address);
        #endregion

        #region 写入端口
        void Write(Int32 offset, Int32 value)
        {
            WriteHandler((Int16)(BASEADDRESS + offset), (Byte)value);
        }

        private static WriteFunc _WriteHandler;
        /// <summary>属性说明</summary>
        public static WriteFunc WriteHandler { get { return _WriteHandler ?? (_WriteHandler = GetWriteHandler()); } }

        static WriteFunc GetWriteHandler()
        {
            // 汇编指令
            var code = new Byte[] {
                0x66, 0x8B, 0x55, 0x08, //mov dx, word ptr [ebp+8]
                0x8A, 0x45, 0x0C, //mov al, byte ptr [ebp+C]
                0xEE  //out dx, al
            };

            return InjectASM<WriteFunc>(code);
        }

        public delegate void WriteFunc(Int16 address, Byte bit);
        #endregion

        #region 写入端口位
        void WriteBit(Int32 offset, Int32 bit, Boolean value)
        {
            if (value)
                SetBitHandler((Int16)(BASEADDRESS + offset), (Byte)bit);
            else
                ClearBitHandler((Int16)(BASEADDRESS + offset), (Byte)bit);
        }

        private static WriteBitFunc _SetBitHandler;
        /// <summary>设置位</summary>
        public static WriteBitFunc SetBitHandler { get { return _SetBitHandler ?? (_SetBitHandler = GetSetBitHandler()); } }

        private static WriteBitFunc _ClearBitHandler;
        /// <summary>清除位</summary>
        public static WriteBitFunc ClearBitHandler { get { return _ClearBitHandler ?? (_ClearBitHandler = GetClearBitHandler()); } }

        static WriteBitFunc GetSetBitHandler()
        {
            // 汇编指令
            var code = new Byte[] {
                0x53, //push ebx
                0x51, //push ecx
                0x66, 0x8B, 0x55, 0x08, //mov dx, word ptr [ebp+8]
                0x8A, 0x45, 0x0C, //mov al, byte ptr [ebp+C]
                0xB3, 0x01, //mov bl, 1
                0xD2, 0xE3, //shl bl, cl
                0xEC, //in al, dx
                0x08, 0xD8, //or al, bl
                0xEE, //out dx, al
                0x59, //pop ecx
                0x5B  //pop ebx
            };

            return InjectASM<WriteBitFunc>(code);
        }

        static WriteBitFunc GetClearBitHandler()
        {
            // 读出字节，取消指定位后重新写回去
            var code = new Byte[] {
                0x53, //push ebx
                0x51, //push ecx
                0x66, 0x8B, 0x55, 0x08, //mov dx, word ptr [ebp+8]
                0x8A, 0x45, 0x0C, //mov al, byte ptr [ebp+C]
                0xB3, 0x01, //mov bl, 1
                0xD2, 0xE3, //shl bl, cl
                0xF6, 0xD3, //not bl
                0xEC, //in al, dx
                0x20, 0xD8, //and al, bl
                0xEE, //out dx, al
                0x59, //pop ecx
                0x5B, //pop ebx
            };

            return InjectASM<WriteBitFunc>(code);
        }

        public delegate void WriteBitFunc(Int16 address, Byte bit);
        #endregion

        #region 注入汇编
        static T InjectASM<T>(Byte[] code)
        {
            // 汇编指令
            var code1 = new Byte[] {
                0x55, //push ebp
                0x8B, 0xEC, //mov ebp, esp
                0x52, //push edx
            };
            var code2 = new Byte[] {
                0x5A, //pop edx
                0x8B, 0xE5, //mov esp, ebp
                0x5D, //pop ebp
                0xC3  //ret
            };

            //var cbs = new Byte[code1.Length + code.Length + code2.Length];
            var ms = new MemoryStream();
            ms.Write(code1, 0, code1.Length);
            ms.Write(code, 0, code.Length);
            ms.Write(code2, 0, code2.Length);
            code = ms.ToArray();

            // 分配内存
            var ptr = Marshal.AllocHGlobal(code.Length);
            // 写入汇编指令
            Marshal.Copy(code, 0, ptr, code.Length);
            // 设为可执行
            VirtualProtectExecute(ptr, code.Length);

            Console.WriteLine("0x{0:X8}", ptr.ToInt32());
            Console.ReadKey(true);

            // 转为委托
            return (T)(Object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
        }
        #endregion

        #region 辅助
        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern int VirtualQueryEx(int hProcess, ref object lpAddress, ref MEMORY_BASIC_INFORMATION lpBuffer, int dwLength);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int flNewProtect, ref int lpflOldProtect);
        static Boolean VirtualProtectExecute(IntPtr address, Int32 size)
        {
            const Int32 PAGE_EXECUTE_READWRITE = 0x40;
            Int32 old = 0;
            return VirtualProtectEx(Process.GetCurrentProcess().Handle, address, size, PAGE_EXECUTE_READWRITE, ref old) == 0;
        }
        #endregion
    }

}

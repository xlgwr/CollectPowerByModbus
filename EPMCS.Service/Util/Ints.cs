using System;
using System.Reflection;

namespace EPMCS.Service.Util
{
    public class Ints
    {
        /// <summary>
        /// 仅用于松下电表
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int UshortHighByteToInt(ushort val)
        {
            byte[] b = BitConverter.GetBytes(val);
            return int.Parse(b[1].ToString("x"));
        }

        /// <summary>
        /// 仅用于松下电表
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int UshortLowByteToInt(ushort val)
        {
            byte[] b = BitConverter.GetBytes(val);
            return int.Parse(b[0].ToString("x"));
        }

        public static short UShortToShort(ushort val)
        {
            return BitConverter.ToInt16(BitConverter.GetBytes(val), 0);
        }

        public static int UShortHighToInt(ushort val)
        {
            return val >> 8;
        }

        public static int UShortLowToInt(ushort val)
        {
            return val & 0x00FF;
        }

        public static int UShortArrayToInt32(ushort[] val)
        {
            byte[] i = BitConverter.GetBytes(val[0]); //
            byte[] j = BitConverter.GetBytes(val[1]);
            byte[] x = new byte[i.Length + j.Length];
            //将第一个数组的值放到你要的数组开头
            i.CopyTo(x, 0);
            //将第二数组的值接着第一个数组最后1位放
            j.CopyTo(x, i.Length);

            return BitConverter.ToInt32(x, 0);
        }
        public static object ToValue(ushort[] arr, string methodName)
        {
            var bb = UshortArrayToByteArray(arr);
            MethodInfo method = typeof(BitConverter).GetMethod(methodName, new Type[] {
				typeof(byte[]),
				typeof(int)
			});
            return method != null ? method.Invoke(null, new object[] {
				bb,
				0
			}) : 0;
        }

        public static byte[] UshortArrayToByteArray(ushort[] arr)
        {
            var x = new byte[arr.Length * 2];
            for (int i = 0; i < arr.Length; i++)
            {
                var t = BitConverter.GetBytes(arr[i]);
                //Array.Reverse(t);
                t.CopyTo(x, i * 2);
            }
            return x;
        }

        public static ushort Reg16Count(string intType)
        {
            if (typeof(System.Int16).ToString() == intType)
            {
                return 1;
            }
            if (typeof(System.Int32).ToString() == intType)
            {
                return 2;
            }
            if (typeof(System.UInt16).ToString() == intType)
            {
                return 1;
            }
            if (typeof(System.UInt32).ToString() == intType)
            {
                return 2;
            }
            return 0;
        }
    }
}
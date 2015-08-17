using System;
using Xstream.Core;

namespace TestDevices
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

        public static uint UShortArrayToUInt32(ushort[] val)
        {
            byte[] i = BitConverter.GetBytes(val[0]);
            byte[] j = BitConverter.GetBytes(val[1]);
            byte[] x = new byte[i.Length + j.Length];
            //将第一个数组的值放到你要的数组开头
            i.CopyTo(x, 0);
            //将第二数组的值接着第一个数组最后1位放
            j.CopyTo(x, i.Length);

            return BitConverter.ToUInt32(x, 0);
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

        public static CmdInfo[] FromXML(string xml)
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
                throw ex;
            }
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
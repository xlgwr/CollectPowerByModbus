using System;
using System.Collections.Generic;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace cmdKey
{
    class getInfoToMd5
    {
        public static List<string> nicList = new List<string>();
        public static string getCPU()
        {
            string cpuInfo = "";//cpu序列号
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();

            }
            return cpuInfo;
        }
        public static string getMacAddress(int count)
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties(); //  .GetIPInterfaceProperties();
                PhysicalAddress address = adapter.GetPhysicalAddress();       // mac address.
                if (address.GetAddressBytes().Length > 0)
                {
                    if (adapter.OperationalStatus == OperationalStatus.Up)
                    {

                        nicList.Add(address.ToString());
                    }
                }
            }

            if (nics == null || nicList.Count == 0)
            {
                return "";
            }
            if (count > nicList.Count)
            {
                count = nicList.Count - 1;
            }
            return nicList[count];

        }
       
        /// <summary>
        /// get MAC Address
        /// </summary>
        public static List<string> getMacAddress()
        {
            nicList.Clear();
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if ((bool)mo["IPEnabled"] == true)

                    nicList.Add(mo["MacAddress"].ToString());
            }

            moc2.Dispose();
            return nicList;
        }
        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns></returns>
        public static string getDiskDriveid()
        {
            //获取硬盘ID
            String HDid = "";
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid = (string)mo.Properties["Model"].Value;
                break;
            }
            cimobject1.Dispose();
            moc1.Dispose();
            return HDid;
        }
        /// <summary>
        /// 主板
        /// </summary>
        /// <returns></returns>
        public static string getSerialNumber()
        {

            string strbNumber = string.Empty;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_baseboard");
            foreach (ManagementObject mo in mos.Get())
            {
                var pname = mo["Product"].ToString();
                strbNumber = mo["SerialNumber"].ToString();

                break;
            }
            mos.Dispose();
            return strbNumber;
        }
        /**/
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string str)
        {
            string tmpStrmd5 = string.Empty;
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                tmpStrmd5 = tmpStrmd5 + s[i].ToString("X");

            }
            return tmpStrmd5;
        }
    }
}

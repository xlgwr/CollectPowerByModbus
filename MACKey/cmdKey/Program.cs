using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cmdKey
{
    class Program
    {
        static void Main(string[] args)
        {
            inikey();
        }
        static void inikey()
        {
            try
            {
                var tomd5key = getInfoToMd5.getCPU() + getInfoToMd5.getSerialNumber() + "www.szisec.com";
                var filename = AppDomain.CurrentDomain.BaseDirectory + "\\FtdAdapter.Core.dll";
                var key = getInfoToMd5.MD5Encrypt(tomd5key);
                if (File.Exists(filename))
                {
                    File.Delete(filename);

                }
                using (var tmpfile = File.CreateText(filename))
                {
                    tmpfile.AutoFlush = true;
                    tmpfile.Write(key);
                }
                Console.WriteLine("生成 key Success." + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}

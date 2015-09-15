using System;

namespace TestDevices
{
    public class CmdInfo : ICloneable
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
       
        private string csharpType;

        public string CsharpType
        {
            get { return csharpType; }
            set { csharpType = value; }
        }

        private Double unitFactor;

        public Double UnitFactor
        {
            get { return unitFactor; }
            set { unitFactor = value; }
        }

        private bool daDuan;

        public bool DaDuan
        {
            get { return daDuan; }
            set { daDuan = value; }
        }

        public Object Clone()
        {
            return this.MemberwiseClone();
        }
    }

      
}
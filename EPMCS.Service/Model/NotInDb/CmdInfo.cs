using System;

namespace EPMCS.Model.NotInDb
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

        public Object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
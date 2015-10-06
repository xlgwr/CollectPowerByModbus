using EPMCS.Model;
using System;
using System.Collections.Generic;

namespace EPMCS.Service.Conf
{
    public class MeterGroup : ICloneable
    {
        public List<MeterParam> VMeters { get; set; }

        public Dictionary<string, List<MeterParam>> RMeters { get; set; }

        public List<string> MMeter { get; set; }

        public Object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
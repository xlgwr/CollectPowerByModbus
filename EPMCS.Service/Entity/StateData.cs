using EPMCS.Model;
using System;
using System.Collections.Generic;

namespace EPMCS.Service.Entity
{
    public class StateData
    {
        public string Port { get; set; }

        public DateTime Group { get; set; }

        public List<MeterParam> Meters { get; set; }
    }
}
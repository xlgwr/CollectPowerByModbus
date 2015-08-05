using EPMCS.Model;
using System.Collections.Generic;

namespace EPMCS.Service.Entity
{
    public class MeterResult
    {
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private List<MeterParam> data;

        public List<MeterParam> Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
using System.Collections.Generic;

namespace EPMCS.Service.Entity
{
    public class DataResult
    {
        private int status;

        private string mxg;

        public string Mxg
        {
            get { return mxg; }
            set { mxg = value; }
        }

        
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private int intervalSeconds;

        public int IntervalSeconds
        {
            get { return intervalSeconds; }
            set { intervalSeconds = value; }
        }

        private List<Customer> customer;

        public List<Customer> Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        private string deviceLatestUpdateMsec;

        public string DeviceLatestUpdateMsec
        {
            get { return deviceLatestUpdateMsec; }
            set { deviceLatestUpdateMsec = value; }
        }
    }
}
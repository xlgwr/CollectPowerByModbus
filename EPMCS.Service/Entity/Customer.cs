using System.Collections.Generic;

namespace EPMCS.Service.Entity
{
    public class Customer
    {
        private string customerId;

        public string CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        private List<Device> devices;

        public List<Device> Devices
        {
            get { return devices; }
            set { devices = value; }
        }
    }
}
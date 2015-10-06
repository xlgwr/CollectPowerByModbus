using System.Collections.Generic;

namespace EPMCS.Service.Entity
{
    public class Device
    {
        private string deviceId;

        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }

        private List<Once> data;

        public List<Once> Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
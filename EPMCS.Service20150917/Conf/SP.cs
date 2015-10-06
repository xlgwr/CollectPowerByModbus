using System.Configuration;
using System.IO.Ports;

namespace EPMCS.Service.Conf
{
    public class ComSerialPort : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true)]
        public string Name
        {
            get { return ((string)(this["name"]??"")).ToUpper(); }
            set { this["name"] = (value??"").ToUpper(); }
        }

        [ConfigurationProperty("BaudRate", DefaultValue = 19200, IsRequired = true)]
        public int BaudRate
        {
            get { return (int)this["BaudRate"]; }
            set { this["BaudRate"] = value; }
        }

        [ConfigurationProperty("DataBits", DefaultValue = 8, IsRequired = true)]
        public int DataBits
        {
            get { return (int)this["DataBits"]; }
            set { this["DataBits"] = value; }
        }

        [ConfigurationProperty("Parity", DefaultValue = Parity.Odd, IsRequired = true)]
        public Parity Parity
        {
            get { return (Parity)this["Parity"]; }
            set { this["Parity"] = value; }
        }

        [ConfigurationProperty("StopBits", DefaultValue = StopBits.One, IsRequired = true)]
        public StopBits StopBits
        {
            get { return (StopBits)this["StopBits"]; }
            set { this["StopBits"] = value; }
        }

        [ConfigurationProperty("ReadTimeout", DefaultValue = 50, IsRequired = true)]
        public int ReadTimeout
        {
            get { return (int)this["ReadTimeout"]; }
            set { this["ReadTimeout"] = value; }
        }

        [ConfigurationProperty("WriteTimeout", DefaultValue = 50, IsRequired = true)]
        public int WriteTimeout
        {
            get { return (int)this["WriteTimeout"]; }
            set { this["WriteTimeout"] = value; }
        }

         [ConfigurationProperty("ReadDelay", DefaultValue = 20, IsRequired = true)]
        public int ReadDelay
        {
            get { return (int)this["ReadDelay"]; }
            set { this["ReadDelay"] = value; }
        }
    }

    public class ComSerialPortCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ComSerialPort();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ComSerialPort)element).Name;
        }

        public new string AddElementName
        {
            get { return base.AddElementName; }
            set { base.AddElementName = value; }
        }

        public new string ClearElementName
        {
            get { return base.ClearElementName; }
            set { base.ClearElementName = value; }
        }

        public new string RemoveElementName
        {
            get { return base.RemoveElementName; }
        }

        public new int Count
        {
            get { return base.Count; }
        }

        public ComSerialPort this[int index]
        {
            get
            {
                return (ComSerialPort)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public ComSerialPort this[string Name]
        {
            get { return (ComSerialPort)BaseGet(Name); }
        }

        public int IndexOf(ComSerialPort exchange)
        {
            return BaseIndexOf(exchange);
        }

        public void Add(ComSerialPort exchange)
        {
            BaseAdd(exchange);
            // Add custom code here.
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(ComSerialPort exchange)
        {
            if (BaseIndexOf(exchange) >= 0)
                BaseRemove(exchange.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }
    }

    public class ComSerialPortsSection : ConfigurationSection
    {
        [ConfigurationProperty("ComSerialPortList", IsDefaultCollection = false)]
        public ComSerialPortCollection ComSerialPortList
        {
            get
            {
                ComSerialPortCollection exchanges = (ComSerialPortCollection)this["ComSerialPortList"];
                return exchanges;
            }
        }

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
            // You can add custom processing code here.
        }

        protected override string SerializeSection(ConfigurationElement parentElement,string name, ConfigurationSaveMode saveMode)
        {
            string s =  base.SerializeSection(parentElement, name, saveMode);
            // You can add custom processing code here.
            return s;
        }
    }
}
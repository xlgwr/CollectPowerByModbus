
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace EPMCS.Model
{
    /// <summary>
    ///
    /// </summary>
    public class MeterParam 
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //客户编号
        private string customerId;

        public string CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        //设备编号
 
        private string deviceId;

        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }

        //设备自身编号
     
        private string deviceCd;


  
        public string DeviceCd
        {
            get { return deviceCd; }
            set { deviceCd = value; }
        }

        //名称
    
        private string deviceName;


        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }

        //父设备编号
   
        private string fDeviceId;


       
        public string FDeviceId
        {
            get { return fDeviceId; }
            set { fDeviceId = value; }
        }

        //需量管理值
        private int demandValue;

        public int DemandValue
        {
            get { return demandValue; }
            set { demandValue = value; }
        }

        //第1级
        private int level1;

        public int Level1
        {
            get { return level1; }
            set { level1 = value; }
        }

        //第2级
        private int level2;

        public int Level2
        {
            get { return level2; }
            set { level2 = value; }
        }

        //第3级
        private int level3;

        public int Level3
        {
            get { return level3; }
            set { level3 = value; }
        }

        //第4级
        private int level4;

        public int Level4
        {
            get { return level4; }
            set { level4 = value; }
        }

        //端口
     
        private string port;


   
        public string Port
        {
            get { return port; }
            set { port = (value??"").ToUpper(); }
        }

        //设备地址

        private string deviceAdd;


        public string DeviceAdd
        {
            get { return deviceAdd; }
            set { deviceAdd = value; }
        }

        //报文 
    
        private string message;


      
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

       
        private string computationRule;


     
        public string ComputationRule
        {
            get { return computationRule; }
            set { computationRule = value; }
        }

        private DateTime startDate;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StartDate
        {
            get {
                return startDate; 
            }
            set { startDate = value; }
        }
        private DateTime endDate;

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
    }
}

public class UnixDateTimeConverter : DateTimeConverterBase
{
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.Integer)
        {
            throw new Exception(String.Format("日期格式错误,got {0}.", reader.TokenType));
        }
        var ticks = (long)reader.Value;
        var date = new DateTime(1970, 1, 1).ToLocalTime();
        date = date.AddMilliseconds(ticks);
        return date;
    }
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        long ticks;
        if (value is DateTime)
        {
            var epoc = new DateTime(1970, 1, 1).ToLocalTime();
            var delta = ((DateTime)value) - epoc;
            if (delta.TotalMilliseconds < 0)
            {
                throw new ArgumentOutOfRangeException("时间格式错误.1");
            }
            ticks = (long)(delta.TotalMilliseconds);
        }
        else
        {
            throw new Exception("时间格式错误.2");
        }
        writer.WriteValue(ticks);
    }
}

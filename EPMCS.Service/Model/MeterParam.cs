using EPMCS.Model.NotInDb;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPMCS.Model
{
    /// <summary>
    ///
    /// </summary>
    public class MeterParam : IEntity, ICloneable
    {
        [Key]
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //客户编号

        [Required]
        [StringLength(75)]
        private string customerId;

        [Required]
        [StringLength(75)]
        public string CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        //设备编号
        [Required]
        [StringLength(75)]
        private string deviceId;


        [Required]
        [StringLength(75)]
        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }

        //设备自身编号
        [Required]
        [StringLength(75)]
        private string deviceCd;


        [Required]
        [StringLength(75)]
        public string DeviceCd
        {
            get { return deviceCd; }
            set { deviceCd = value; }
        }

        //名称
        [StringLength(600)]
        private string deviceName;


        [StringLength(600)]
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }

        //父设备编号
        [StringLength(75)]
        private string fDeviceId;


        [StringLength(75)]
        public string FDeviceId
        {
            get { return fDeviceId; }
            set { fDeviceId = value; }
        }

        //需量管理值
        [Required]
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
        [StringLength(75)]
        private string port;


        [StringLength(75)]
        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        //设备地址
        [StringLength(120)]
        private string deviceAdd;


        [StringLength(120)]
        public string DeviceAdd
        {
            get { return deviceAdd; }
            set { deviceAdd = value; }
        }

        //报文 
        [StringLength(4096)]
        private string message;


        [StringLength(4096)]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        [StringLength(600)]
        private string computationRule;


        [StringLength(600)]
        public string ComputationRule
        {
            get { return computationRule; }
            set { computationRule = value; }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get {
                return startDate; 
            }
            set { startDate = value; }
        }
        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        

        [JsonIgnore]
        public CmdInfo[] CmdInfos { get; set; }

        public Object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

/*
 *
CREATE TABLE [MeterParams] (
[Id] VARCHAR(80)  UNIQUE NOT NULL PRIMARY KEY,
[CustomerId] VARCHAR(25)  NOT NULL,
[DeviceId] VARCHAR(25)  NOT NULL,
[DeviceCd] VARCHAR(25)  NOT NULL,
[DeviceName] VARCHAR(200)  NOT NULL,
[FDeviceId] VARCHAR(25)  NULL,
[DemandValue] INTEGER  NOT NULL,
[Level1] INTEGER  NOT NULL,
[Level2] INTEGER  NOT NULL,
[Level3] INTEGER  NOT NULL,
[Level4] INTEGER  NOT NULL,
[Port] VARCHAR(32)  NULL,
[DeviceAdd] VARCHAR(32)  NULL,
[Message] VARCHAR(1024)  NULL,
[ComputationRule] VARCHAR(400)  NULL
)
 *
 */
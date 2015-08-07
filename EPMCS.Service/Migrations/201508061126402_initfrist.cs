namespace EPMCS.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initfrist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(nullable: false, maxLength: 25, unicode: false, storeType: "nvarchar"),
                        DeviceId = c.String(nullable: false, maxLength: 25, unicode: false, storeType: "nvarchar"),
                        DeviceCd = c.String(nullable: false, maxLength: 25, unicode: false, storeType: "nvarchar"),
                        Groupstamp = c.String(nullable: false, unicode: false),
                        PowerDate = c.DateTime(nullable: false, precision: 0),
                        PowerValue = c.Double(nullable: false),
                        ValueLevel = c.Int(nullable: false),
                        MeterValue = c.Double(nullable: false),
                        DiffMeterValuePre = c.Double(nullable: false),
                        PrePowerDate = c.DateTime(nullable: false, precision: 0),
                        A1 = c.Double(nullable: false),
                        A2 = c.Double(nullable: false),
                        A3 = c.Double(nullable: false),
                        V1 = c.Double(nullable: false),
                        V2 = c.Double(nullable: false),
                        V3 = c.Double(nullable: false),
                        Pf = c.Double(nullable: false),
                        Uploaded = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeterParams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(unicode: false),
                        DeviceId = c.String(unicode: false),
                        DeviceCd = c.String(unicode: false),
                        DeviceName = c.String(unicode: false),
                        FDeviceId = c.String(unicode: false),
                        DemandValue = c.Int(nullable: false),
                        Level1 = c.Int(nullable: false),
                        Level2 = c.Int(nullable: false),
                        Level3 = c.Int(nullable: false),
                        Level4 = c.Int(nullable: false),
                        Port = c.String(unicode: false),
                        DeviceAdd = c.String(unicode: false),
                        Message = c.String(unicode: false),
                        ComputationRule = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KeyValParams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        K = c.String(nullable: false, maxLength: 32, unicode: false, storeType: "nvarchar"),
                        V = c.String(nullable: false, maxLength: 400, unicode: false, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KeyValParams");
            DropTable("dbo.MeterParams");
            DropTable("dbo.UploadDatas");
        }
    }
}

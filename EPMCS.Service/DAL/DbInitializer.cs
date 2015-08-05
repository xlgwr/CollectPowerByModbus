using EPMCS.Model;
using System.Data.Entity;

namespace EPMCS.DAL
{
    public class InitializerForDropCreateDatabaseAlways : DropCreateDatabaseAlways<MysqlDbContext>
    {
        public InitializerForDropCreateDatabaseAlways()
            : base()
        { }

        protected override void Seed(MysqlDbContext context)
        {
            MeterParam mp = new MeterParam
            {
                CustomerId = "C000001",
                DemandValue = 900,
                DeviceAdd = "1",
                DeviceId = "123456",
                DeviceCd = "A001",
                DeviceName = "测试表1",
                Level1 = 700,
                Level2 = 800,
                Level3 = 900,
                Level4 = 1000,
                Port = "COM10",
                Message = @"<Device><cmdInfos>
        <CmdInfo>
          <name>yearmonth</name>
          <address>002F</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>dayhour</name>
          <address>002E</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>minutesecond</name>
          <address>002D</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>zljyggl</name>
          <address>006A</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>zssyggl</name>
          <address>0092</address>
          <csharpType>System.Int32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a1</name>
          <address>00B4</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a2</name>
          <address>00B6</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a3</name>
          <address>00B8</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v1</name>
          <address>00A4</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v2</name>
          <address>00A6</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v3</name>
          <address>00A8</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>pf</name>
          <address>00C5</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0.001</unitFactor>
        </CmdInfo>
      </cmdInfos>
     </Device>",
            };
            MeterParam mp1 = new MeterParam
            {
                CustomerId = "C000001",
                DemandValue = 550,
                DeviceAdd = "2",
                DeviceId = "123457",
                DeviceCd = "A002",
                DeviceName = "测试表2",
                Level1 = 300,
                Level2 = 400,
                Level3 = 500,
                Level4 = 600,
                Port = "COM10",
                Message = @"<Device><cmdInfos>
        <CmdInfo>
          <name>yearmonth</name>
          <address>002F</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>dayhour</name>
          <address>002E</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>minutesecond</name>
          <address>002D</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>zljyggl</name>
          <address>006A</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>zssyggl</name>
          <address>0092</address>
          <csharpType>System.Int32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a1</name>
          <address>00B4</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a2</name>
          <address>00B6</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a3</name>
          <address>00B8</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v1</name>
          <address>00A4</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v2</name>
          <address>00A6</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v3</name>
          <address>00A8</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>pf</name>
          <address>00C5</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0.001</unitFactor>
        </CmdInfo>
      </cmdInfos>
     </Device>",
            };
            MeterParam mp2 = new MeterParam
            {
                CustomerId = "C000001",
                DemandValue = 1300,
                DeviceId = "123458",
                DeviceCd = "A003",
                DeviceName = "虚拟表1",
                Level1 = 300,
                Level2 = 400,
                Level3 = 500,
                Level4 = 600,
                ComputationRule = "[A001]+[A002]"
            };
            //context.Set<MeterParam>().Add(mp);
            //context.Set<MeterParam>().Add(mp1);
            //context.Set<MeterParam>().Add(mp2);
            base.Seed(context);
        }
    }

    public class InitializerForCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<MysqlDbContext>
    {
        //public InitializerForCreateDatabaseIfNotExists()
        //    : base()
        //{
        //}
        protected override void Seed(MysqlDbContext context)
        {
            MeterParam mp = new MeterParam
            {
                CustomerId = "C000001",
                DemandValue = 900,
                DeviceAdd = "1",
                DeviceId = "123456",
                DeviceCd = "A001",
                DeviceName = "测试表1",
                Level1 = 700,
                Level2 = 800,
                Level3 = 900,
                Level4 = 1000,
                Port = "COM10",
                Message = @"<Device><cmdInfos>
        <CmdInfo>
          <name>yearmonth</name>
          <address>002F</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>dayhour</name>
          <address>002E</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>minutesecond</name>
          <address>002D</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>zljyggl</name>
          <address>006A</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>zssyggl</name>
          <address>0092</address>
          <csharpType>System.Int32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a1</name>
          <address>00B4</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a2</name>
          <address>00B6</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a3</name>
          <address>00B8</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v1</name>
          <address>00A4</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v2</name>
          <address>00A6</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v3</name>
          <address>00A8</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>pf</name>
          <address>00C5</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0.001</unitFactor>
        </CmdInfo>
      </cmdInfos>
     </Device>",
            };
            MeterParam mp1 = new MeterParam
            {
                CustomerId = "C000001",
                DemandValue = 550,
                DeviceAdd = "2",
                DeviceId = "123457",
                DeviceCd = "A002",
                DeviceName = "测试表2",
                Level1 = 300,
                Level2 = 400,
                Level3 = 500,
                Level4 = 600,
                Port = "COM10",
                Message = @"<Device><cmdInfos>
        <CmdInfo>
          <name>yearmonth</name>
          <address>002F</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>dayhour</name>
          <address>002E</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>minutesecond</name>
          <address>002D</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>zljyggl</name>
          <address>006A</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>zssyggl</name>
          <address>0092</address>
          <csharpType>System.Int32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a1</name>
          <address>00B4</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a2</name>
          <address>00B6</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>a3</name>
          <address>00B8</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.01</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v1</name>
          <address>00A4</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v2</name>
          <address>00A6</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>v3</name>
          <address>00A8</address>
          <csharpType>System.UInt32</csharpType>
          <unitFactor>0.1</unitFactor>
        </CmdInfo>
        <CmdInfo>
          <name>pf</name>
          <address>00C5</address>
          <csharpType>System.Int16</csharpType>
          <unitFactor>0.001</unitFactor>
        </CmdInfo>
      </cmdInfos>
     </Device>",
            };
            MeterParam mp2 = new MeterParam
            {
                CustomerId = "C000001",
                DemandValue = 1300,
                DeviceId = "123458",
                DeviceCd = "A003",
                DeviceName = "虚拟表1",
                Level1 = 300,
                Level2 = 400,
                Level3 = 500,
                Level4 = 600,
                ComputationRule = "[A001]+[A002]"
            };
            //context.Set<MeterParam>().Add(mp);
            //context.Set<MeterParam>().Add(mp1);
            //context.Set<MeterParam>().Add(mp2);
            context.Meters.Add(mp);
            context.Meters.Add(mp1);
            context.Meters.Add(mp2);
            context.SaveChanges();
            base.Seed(context);
        }
    }

    public class InitializerForDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<MysqlDbContext>
    {
        protected override void Seed(MysqlDbContext context)
        {
        }
    }
}
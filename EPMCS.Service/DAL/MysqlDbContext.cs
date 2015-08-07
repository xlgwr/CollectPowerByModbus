using EPMCS.Model;
using log4net;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Reflection;

namespace EPMCS.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class MysqlDbContext : DbContext
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static MysqlDbContext()
        {
            Database.SetInitializer(new InitializerForCreateDatabaseIfNotExists());
        }

        public MysqlDbContext()
            : base("name=MysqlDbConn")
        {
            //add log for EF gen SQL by xlg
            Database.Log = logger.Debug;

            //Configuration.ProxyCreationEnabled = true;
            //Configuration.LazyLoadingEnabled = true;

            //Enable-Migrations
            //Enable-Migrations -Force
            //Update-Database
        }

        //以下是数据库上下文对象，以后对数据库的访问就用下面对象
        public DbSet<UploadData> Datas { get; set; }

        public DbSet<MeterParam> Meters { get; set; }

        public DbSet<KeyValParam> Params { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //指定单数形式的表名
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<UploadData>();
            modelBuilder.Entity<KeyValParam>();
            modelBuilder.Entity<MeterParam>().Ignore(p => p.CmdInfos);
        }

        //public override int SaveChanges()
        //{
        //    try
        //    {
        //        this.ChangeTracker.DetectChanges();
        //        var objectContext = ((IObjectContextAdapter)this).ObjectContext;
        //        foreach (ObjectStateEntry entry in objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
        //        {
        //            var v = entry.Entity as IEntity;
        //            if (v != null && v.Id == null)
        //            {
        //                v.Id =(int) DateTime.Now.Ticks;
        //            }
        //        }
        //        foreach (ObjectStateEntry entry in objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added))
        //        {
        //            var v = entry.Entity as IRowVersion;
        //            if (v != null)
        //            {
        //                v.RowVersion = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.Ticks.ToString()); //Guid.NewGuid().ToString()
        //            }
        //        }
        //        return base.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is System.InvalidOperationException || ex.InnerException is System.InvalidOperationException)
        //        {
        //        }
        //        else
        //        {
        //            throw ex;
        //        }
        //    }
        //    return 0;
        //}

        //private volatile static MysqlDbContext _instance = null;
        //private static readonly object lockHelper = new object();

        //public static readonly object  Locker = new object();
        //public static MysqlDbContext GetInstance()
        //{
        //    if (_instance == null)
        //    {
        //        lock (lockHelper)
        //        {
        //            if (_instance == null)
        //                _instance = new MysqlDbContext();
        //        }
        //    }
        //    return _instance;
        //}
    }
}
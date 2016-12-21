using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
        public class MigrationsContextFactory : IDbContextFactory<AppContext>
        {
            public AppContext Create()
            {
                return new AppContext();
            }
        }

        public class AppContext : DbContext
        {

            public AppContext(Boolean DropInitializeDB = false, string ConnectionStringName = "SqlDb")
                : base(ConnectionStringName)
            {
                if (DropInitializeDB)
                {
                    Database.SetInitializer(new DropCreateDatabaseAlways<AppContext>());
                }
            }

            //public DbSet<Server> Servers { get; set; }
            //public DbSet<Setting> Settings { get; set; }

        }

}


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamTools.lib.Data
{
    public class DataBaseContext : DbContext
    {
        static DataBaseContext()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, Migrations.Configuration>(true));
        }

        public DbSet<Mail> Mails { get; set; }
        public DbSet<MailServer> Servers { get; set; }

        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Sender> Senders { get; set; }

        public DataBaseContext() : this("name=SpamDatabase") { }

        public DataBaseContext(string ConnectionString) 
            : base(ConnectionString)
        {

        }
    }
}

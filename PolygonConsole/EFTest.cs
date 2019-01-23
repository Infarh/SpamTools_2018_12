using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamTools.lib.Data;

namespace PolygonConsole
{
    internal static class EFTest
    {
        public static void Test()
        {
            using (var db = new DataBaseContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                db.Database.Log = str => Console.WriteLine("EF:{0}", str);

                foreach (var server in db.Servers.Where(server => server.Address.EndsWith(".ru")))
                {
                    Console.WriteLine(server.Address);
                }

                db.Database.Log = null;

                //foreach (var task in db.SchedulerTasks.Include(t => t.Mail).Include(t => t.Server))
                //{
                //}

                db.ChangeTracker.DetectChanges();

                if (db.ChangeTracker.HasChanges())
                    db.SaveChanges();
            }
        }
    }
}

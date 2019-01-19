using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SpamTools.lib.Data;
using SpamTools.lib.Data.EFModelFirst;

namespace SpamTools
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //    using (var db = new SpamDBContainer())
            //    {
            //        var servers = db.SchedulerTsaskSet
            //            .Where(task => task.Time < DateTime.Now)
            //            .Where(task => task.EmailList.Count() > 3)
            //            .Select(task => task.Server);

            //        var count = servers.Count();

            //        foreach (var server in servers)
            //        {

            //        }

            //        db.SchedulerTsaskSet.Add(new SchedulerTsask
            //        {
            //             Name = "Test",
            //             Time = DateTime.Now.Add(TimeSpan.FromMinutes(50)),
            //             Server = new Server { Address = "address", Port = 255, UseSSL = true },
            //             EmailSender = new EmailSender { Name = "name", Email = "email", Password = "pass", Login = "login" },
            //             EmailList = new []
            //             {
            //                 new EmailList { Description = "", Name = ""} 
            //             }
            //        });

            //        db.SaveChanges();
            //    }

            using (var db = new DataBaseContext())
            {
                foreach (var server in db.Servers.Where(server => server.Address.EndsWith(".ru")))
                {
                    Debug.WriteLine(server.Address);
                }

                //db.Servers.Attach()
                db.SaveChanges();
            }
        }
    }
}

using SpamTools.lib.Data;

namespace SpamTools.lib.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data.DataBaseContext context)
        {
            context.Servers.AddOrUpdate(
                server => server.Address,
                new MailServer("mail.yandex.ru"),
                new MailServer("mail.mail.ru"),
                new MailServer("mail.gmail.com", 455)
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

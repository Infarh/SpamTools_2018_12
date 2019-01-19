namespace SpamTools.lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MailServerDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MailServers", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MailServers", "Description");
        }
    }
}

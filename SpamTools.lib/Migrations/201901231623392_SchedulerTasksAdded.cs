namespace SpamTools.lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchedulerTasksAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchedulerTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Mail_Id = c.Int(),
                        Sender_Id = c.Int(),
                        Server_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MailLists", t => t.Mail_Id)
                .ForeignKey("dbo.Senders", t => t.Sender_Id)
                .ForeignKey("dbo.MailServers", t => t.Server_Id)
                .Index(t => t.Mail_Id)
                .Index(t => t.Sender_Id)
                .Index(t => t.Server_Id);
            
            AddColumn("dbo.Mails", "MailList_Id", c => c.Int());
            AddColumn("dbo.Recipients", "SchedulerTask_Id", c => c.Int());
            CreateIndex("dbo.Mails", "MailList_Id");
            CreateIndex("dbo.Recipients", "SchedulerTask_Id");
            AddForeignKey("dbo.Mails", "MailList_Id", "dbo.MailLists", "Id");
            AddForeignKey("dbo.Recipients", "SchedulerTask_Id", "dbo.SchedulerTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulerTasks", "Server_Id", "dbo.MailServers");
            DropForeignKey("dbo.SchedulerTasks", "Sender_Id", "dbo.Senders");
            DropForeignKey("dbo.Recipients", "SchedulerTask_Id", "dbo.SchedulerTasks");
            DropForeignKey("dbo.SchedulerTasks", "Mail_Id", "dbo.MailLists");
            DropForeignKey("dbo.Mails", "MailList_Id", "dbo.MailLists");
            DropIndex("dbo.SchedulerTasks", new[] { "Server_Id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Sender_Id" });
            DropIndex("dbo.SchedulerTasks", new[] { "Mail_Id" });
            DropIndex("dbo.Recipients", new[] { "SchedulerTask_Id" });
            DropIndex("dbo.Mails", new[] { "MailList_Id" });
            DropColumn("dbo.Recipients", "SchedulerTask_Id");
            DropColumn("dbo.Mails", "MailList_Id");
            DropTable("dbo.SchedulerTasks");
            DropTable("dbo.MailLists");
        }
    }
}

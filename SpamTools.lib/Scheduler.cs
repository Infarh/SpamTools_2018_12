using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SpamTools.lib.Data;
using SpamTools.lib.Database;

namespace SpamTools.lib
{
    public class Scheduler
    {
        private readonly ObservableCollection<SchedulerTask> _Tasks;

        public ObservableCollection<SchedulerTask> Tasks => _Tasks;

        public Scheduler()
        {
            _Tasks = new ObservableCollection<SchedulerTask>
            {
                new SchedulerTask
                {
                    Mail = new Mail { Body = "Test mail 1", Subject = "Mail 1" },
                    Sender = new Sender { Address = "sender@server.ru", Name = "Sender name", Password = "Sender password" },
                    Recipients = new[]
                    {
                        new Recipient { Id = 0, Name = "Resipient 1", Address = "recipient1@mail.ru"},
                        new Recipient { Id = 1, Name = "Resipient 2", Address = "recipient2@mail.ru"},
                        new Recipient { Id = 2, Name = "Resipient 3", Address = "recipient3@mail.ru"}
                    },
                    Server = new MailServer { Address = "http://Server.ru", Port = 123, UseSSL = false },
                    Time = DateTime.Now.Add(TimeSpan.FromMinutes(20))
                },
                new SchedulerTask
                {
                    Mail = new Mail { Body = "Test mail 2", Subject = "Mail 2" },
                    Sender = new Sender { Address = "sender1@server.ru", Name = "Sender name 1", Password = "Sender password" },
                    Recipients = new[]
                    {
                        new Recipient { Id = 0, Name = "Resipient 1", Address = "recipient1@mail.ru"},
                        new Recipient { Id = 1, Name = "Resipient 2", Address = "recipient2@mail.ru"},
                        new Recipient { Id = 2, Name = "Resipient 3", Address = "recipient3@mail.ru"}
                    },
                    Server = new MailServer { Address = "http://Server2.ru", Port = 321, UseSSL = false },
                    Time = DateTime.Now.Add(TimeSpan.FromMinutes(40))
                }
            };
        }

        public Scheduler(IEnumerable<SchedulerTask> tasks) => _Tasks = new ObservableCollection<SchedulerTask>(tasks);

        public void Start() { }

        public void AddTask(SchedulerTask task)
        {
            if (_Tasks.Contains(task)) return;
            _Tasks.Add(task);
        }

        public bool RemoveTask(SchedulerTask task) => _Tasks.Remove(task);
    }
}

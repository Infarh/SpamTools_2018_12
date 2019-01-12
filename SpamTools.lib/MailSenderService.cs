using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using SpamTools.lib.Database;
using System.Threading;

namespace SpamTools.lib
{
    public class MailSenderService
    {
        private string _ServerAddress;
        private int _Port;
        private bool _UseSSL;
        private string _UserName;
        private SecureString _Password;

        public MailSenderService(string ServerAddress, int Port, bool SSL, string User, SecureString Password)
        {
            _ServerAddress = ServerAddress;
            _Port = Port;
            _UseSSL = SSL;
            _UserName = User;
            _Password = Password;
        }

        public void Send(string Subject, string Body, string Address)
        {
            using (var message = new MailMessage(_UserName, Address))
            {
                message.Subject = Subject;
                message.Body = Body;

                using (var client = new SmtpClient(_ServerAddress, _Port))
                {
                    client.EnableSsl = _UseSSL;
                    client.Credentials = new NetworkCredential(_UserName, _Password);

                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError(e.Message);
                        Trace.TraceError(e.ToString());
                    }
                }
            }
        }

        public void Send(string Subject, string Body, IEnumerable<EmailRecipient> recipients)
        {
            foreach (var email_recipient in recipients)
            {
                Send(Subject, Body, email_recipient.EmailAddress);
            }
        }

        public void SendParallel_Thread(string Subject, string Body, IEnumerable<EmailRecipient> recipients)
        {
            foreach (var email_recipient in recipients)
            {
                var sending_thread = new Thread(() => Send(Subject, Body, email_recipient.EmailAddress));
                sending_thread.Priority = ThreadPriority.BelowNormal;
                sending_thread.IsBackground = true;
                sending_thread.Start();
            }
        }

        public void SendParallel(string Subject, string Body, IEnumerable<EmailRecipient> recipients)
        {
            foreach (var address in recipients.Select(r => r.EmailAddress))
            {
                //ThreadPool.QueueUserWorkItem(p => Send((string)((object[])p)[0], (string)((object[])p)[1], (string)((object[])p)[2]), new object[] { Subject, Body, address });
                ThreadPool.QueueUserWorkItem(p => Send(Subject, Body, address));
            }
        }
    }
}

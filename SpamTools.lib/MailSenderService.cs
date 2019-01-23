using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using SpamTools.lib.Data;

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

        public async Task SendAsync(string Subject, string Body, string Address)
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
                        //client.Send(message);
                        await client.SendMailAsync(message).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError(e.Message);
                        Trace.TraceError(e.ToString());
                    }
                }
            }
        }

        public void Send(string Subject, string Body, IEnumerable<Recipient> recipients)
        {
            foreach (var email_recipient in recipients)
                Send(Subject, Body, email_recipient.Address);
        }

        public async Task SendAsync(string Subject, string Body, IEnumerable<Recipient> recipients)
        {
            foreach (var email_recipient in recipients)
                await SendAsync(Subject, Body, email_recipient.Address).ConfigureAwait(false);
        }

        public async Task SendParallelAsync(string Subject, string Body, IEnumerable<Recipient> recipients)
        {
            await Task.WhenAll(recipients.Select(recipient => SendAsync(Subject, Body, recipient.Address)))
                .ConfigureAwait(false);
        }

        public void SendParallel(string Subject, string Body, IEnumerable<Recipient> recipients, int DegreeOfParallelism)
        {
            recipients.AsParallel()
                .WithDegreeOfParallelism(DegreeOfParallelism)
                .Select(recipient => recipient.Address)
                .ForAll(recipient_address => Send(Subject, Body, recipient_address));
        }
    }
}

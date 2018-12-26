using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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

        public void Send(string Subject, string Email, string Address)
        {
            using (var message = new MailMessage(_UserName, Address))
            {
                message.Subject = Subject;
                message.Body = Email;

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
    }
}

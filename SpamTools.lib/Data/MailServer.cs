using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamTools.lib.Data
{
    public class MailServer
    {   
        public int Id { get; set; }
        public string Address { get; set; }
        public int Port { get; set; } = 25;

        public bool UseSSL { get; set; } = true;

        public string Description { get; set; }

        public MailServer() { }

        public MailServer(string Address, int Port = 25, bool SSL = true)
        {
            this.Address = Address;
            this.Port = Port;
            this.UseSSL = SSL;
        }
    }
}

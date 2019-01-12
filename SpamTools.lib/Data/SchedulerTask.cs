using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamTools.lib.Database;

namespace SpamTools.lib.Data
{
    public class SchedulerTask
    {
        public DateTime Time { get; set; }
        public IEnumerable<EmailRecipient> Recipients { get; set; }
        public MailServer Server { get; set; }
        public Sender Sender { get; set; }
        public Mail Mail { get; set; }
    }
}

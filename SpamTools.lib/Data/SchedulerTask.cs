using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamTools.lib.Data
{
    public class SchedulerTask
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public virtual ICollection<Recipient> Recipients { get; set; }
        public virtual MailServer Server { get; set; }
        public virtual Sender Sender { get; set; }
        public virtual MailList Mail { get; set; }
    }
}

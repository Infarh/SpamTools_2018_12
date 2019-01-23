using System.Collections.Generic;

namespace SpamTools.lib.Data
{
    public class MailList
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public virtual ICollection<Mail> Mails { get; set; }
    }
}
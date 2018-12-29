using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamTools.lib.Database;

namespace SpamTools.lib
{
    public interface IDataService
    {
        IEnumerable<EmailRecipient> GetEmailRecipient();

        bool CreateRecipient(EmailRecipient Recipient);
        bool UpdateRecipient(EmailRecipient Recipient);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamTools.lib.Data;

namespace SpamTools.lib
{
    public interface IDataService
    {
        IEnumerable<Recipient> GetEmailRecipient();

        bool CreateRecipient(Recipient Recipient);
        bool UpdateRecipient(Recipient Recipient);
    }
}

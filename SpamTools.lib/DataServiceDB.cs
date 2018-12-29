using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamTools.lib.Database;

namespace SpamTools.lib
{
    public class DataServiceDB : IDataService
    {
        private readonly SpamDatabaseDataContext _DataBaseContext;

        public DataServiceDB(SpamDatabaseDataContext Context) => _DataBaseContext = Context;

        public IEnumerable<EmailRecipient> GetEmailRecipient()
        {
            return new ObservableCollection<EmailRecipient>(_DataBaseContext.EmailRecipient);
        }

        public bool CreateRecipient(EmailRecipient Recipient)
        {
            _DataBaseContext.EmailRecipient.InsertOnSubmit(Recipient);
            _DataBaseContext.SubmitChanges();
            return Recipient.Id != 0;
        }

        public bool UpdateRecipient(EmailRecipient Recipient)
        {
            _DataBaseContext.SubmitChanges();
            return true;
        }
    }
}

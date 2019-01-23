using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpamTools.lib.Data;

namespace SpamTools.lib
{
    public class DataServiceDB : IDataService
    {
        private readonly DataBaseContext _DataBaseContext;

        public DataServiceDB(DataBaseContext Context) => _DataBaseContext = Context;

        public IEnumerable<Recipient> GetEmailRecipient()
        {
            return new ObservableCollection<Recipient>(_DataBaseContext.Recipients);
        }

        public bool CreateRecipient(Recipient Recipient)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecipient(Recipient Recipient)
        {
            _DataBaseContext.SaveChanges();
            return true;
        }
    }
}

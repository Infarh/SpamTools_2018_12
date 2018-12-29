using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.CommandWpf;
using SpamTools.lib;
using SpamTools.lib.Database;
using SpamTools.lib.MWWM;
using RelayCommand = GalaSoft.MvvmLight.Command.RelayCommand;

namespace SpamTools.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDataService _DataService;

        private string _Title = "Рассыльщик почты";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _Status = "Готов";
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        private EmailRecipient _CurrentRecipient;
        public EmailRecipient CurrentRecipient
        {
            get => _CurrentRecipient;
            set => Set(ref _CurrentRecipient, value);
        }

        //private readonly ObservableCollection<EmailRecipient> _Recipients = new ObservableCollection<EmailRecipient>();
        public ObservableCollection<EmailRecipient> Recipients { get; } = new ObservableCollection<EmailRecipient>();

        public ICommand UpdateRecipientsCommand { get; }

        private bool CanUpdateRecipientsCommandExecute() => true;

        private void OnUpdateRecipientsCommandExecuted()
        {
            Recipients.Clear();
            var db_recipients = _DataService.GetEmailRecipient();
            foreach (var recipient in db_recipients)
            {
                Recipients.Add(recipient);
            }
        }

        public ICommand CreateNewRecipientCommand { get; }

        private void OnCreateNewRecipientCommandExecuted()
        {
            var recipient = new EmailRecipient{ Name = "Получатель", EmailAddress = "user@server.ru" };
            if (_DataService.CreateRecipient(recipient))
            {
                CurrentRecipient = recipient;
                Recipients.Add(recipient);
            }
        }

        public ICommand UpdateRecipientCommand { get; }

        private bool UpdateRecipientCommandCanExecute(EmailRecipient Recipient)
        {
            return true; // Recipient != null || _CurrentRecipient != null;
        }

        private void OnUpdateRecipientCommandExecuted(EmailRecipient Recipient)
        {
            var recipient = Recipient ?? _CurrentRecipient;
            if(recipient is null) return;
            _DataService.UpdateRecipient(recipient);
        }

        public MainWindowViewModel(IDataService DataService)
        {
            UpdateRecipientsCommand = new RelayCommand(OnUpdateRecipientsCommandExecuted, CanUpdateRecipientsCommandExecute);
            CreateNewRecipientCommand = new RelayCommand(OnCreateNewRecipientCommandExecuted);
            UpdateRecipientCommand = new GalaSoft.MvvmLight.Command.RelayCommand<EmailRecipient>(OnUpdateRecipientCommandExecuted, UpdateRecipientCommandCanExecute);

           _DataService = DataService;
        }
    }
}

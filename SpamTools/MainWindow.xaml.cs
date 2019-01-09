using System;
using System.Security;
using System.Windows;
using SpamTools.lib;
using SpamTools.lib.Data;
using SpamTools.lib.Service;

namespace SpamTools
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnExitClick(object Sender, RoutedEventArgs E)
        {
            Close();
        }

        private void OnSendButtonClick(object Sender, RoutedEventArgs E)
        {
            var server = Servers.SelectedItem as MailServer;
            if (server == null)
            {
                MessageBox.Show("Сервер не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //var sender = Users.SelectedItem as Sender;
            //if (sender == null)
            //{
            //    MessageBox.Show("Отправитель не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //var password = new SecureString();
            //foreach (var password_cnar in PasswordService.Decode(sender.Password))
            //    password.AppendChar(password_cnar);


            //var send_mail_service = new MailSenderService(
            //    server.Address, server.Port, server.UseSSL,
            //    sender.Address, password);

            //send_mail_service.Send("Subject", "Email body", "email@server.com");
        }

        private void OnTabManagerBack(object Sender, EventArgs E)
        {
            if (MainTabControl.SelectedIndex > 0)
                MainTabControl.SelectedIndex--;
        }

        private void OnTabManagerForvard(object Sender, EventArgs E)
        {
            if (MainTabControl.SelectedIndex < MainTabControl.Items.Count - 1)
                MainTabControl.SelectedIndex++;
        }
    }
}

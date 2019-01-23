using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PolygonWPF
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

        private async void ButtonBase_OnClick(object Sender, RoutedEventArgs E)
        {
            //var thread = new Thread(ProcessLongWork);
            //thread.Start();

            var button = Sender as Button;
            button.IsEnabled = false;

            var task = Task.Run(() => Thread.Sleep(3000));
            await task.ConfigureAwait(true);

            ResultText.Text = "Результат готов!";
            button.IsEnabled = true;
        }

        private void ProcessLongWork()
        {
            Thread.Sleep(3000);

            ResultText.Dispatcher.Invoke(() =>
            {
                ResultText.Text = "Результат готов!";
            });

            //Application.Current.Dispatcher
        }
    }
}

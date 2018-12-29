/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SpamTools"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using SpamTools.lib;
using SpamTools.lib.Database;

namespace SpamTools.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default); // IoC + DI

            //SimpleIoc.Default.Register<Random>(() => new Random((int)DateTime.Now.Millisecond));

            if (!SimpleIoc.Default.IsRegistered<SpamDatabaseDataContext>())
                SimpleIoc.Default.Register(() => new SpamDatabaseDataContext());
            SimpleIoc.Default.Register<IDataService, DataServiceDB>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public MainWindowViewModel MainWindowModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

        public static void Cleanup()
        {
        }
    }
}
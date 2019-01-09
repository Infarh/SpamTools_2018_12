using System;
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
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (!SimpleIoc.Default.IsRegistered<Random>())
                SimpleIoc.Default.Register(() => new Random(DateTime.Now.Millisecond));

            if (!SimpleIoc.Default.IsRegistered<SpamDatabaseDataContext>())
                SimpleIoc.Default.Register(() => new SpamDatabaseDataContext());
            SimpleIoc.Default.Register<IDataService, DataServiceDB>();

            SimpleIoc.Default.Register<MainWindowViewModel>();
        }


        public MainWindowViewModel MainWindowModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

        public static void Cleanup() { }
    }
}
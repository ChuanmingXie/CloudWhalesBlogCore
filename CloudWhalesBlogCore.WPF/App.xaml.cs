using Autofac;
using CloudWhalesBlogCore.Services.Extensions;
using CloudWhalesBlogCore.Shared.Common;
using CloudWhalesBlogCore.Shared.Common.DataInterfaces;
using CloudWhalesBlogCore.WPF.Common;
using System;
using System.Windows;
using System.Windows.Threading;

namespace CloudWhalesBlogCore.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            NetCoreProvider.Resolve<ILog>()?.Warn(e.Exception, e.Exception.Message);
            e.Handled = true;
        }

        private void ConfigureServices()
        {
            var service = new ContainerBuilder();
            service.AddRepository<CloudBlogNLog, ILog>();
            NetCoreProvider.RegisterServiceLocator(service.Build());
        }
    }
}

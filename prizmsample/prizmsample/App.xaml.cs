using Prism.Ioc;
using prizmsample.Views;
using System;
using System.Windows;
using System.Threading;
using System.Reflection;
using Prism.Modularity;
using Modulesample;
using Prism.Mvvm;

namespace prizmsample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private Mutex mutex = new Mutex(false, "prizmsample");

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<ModulesampleModule>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            //ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(vt =>
            //{
            //    var viewName = vt.FullName;
            //    var asmName = vt.GetTypeInfo().Assembly.FullName;
            //    var vmName = $"{viewName}ViewModel, {asmName}";

            //    return Type.GetType(vmName);
            //});
        }

        private void PrismApplication_Startup(object sender, StartupEventArgs e)
        {
            if (this.mutex.WaitOne(0, false)) { return; }

            this.mutex.Close();
            this.mutex = null;
            this.Shutdown();
        }

        private void PrismApplication_Exit(object sender, ExitEventArgs e)
        {
            if (this.mutex == null) { return; }
            this.mutex.ReleaseMutex();
            this.mutex.Close();
        }
    }
}

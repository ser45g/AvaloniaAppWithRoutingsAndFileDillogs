using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.SimpleRouter;
using Material.Styles.Themes.Base;
using Material.Styles.Themes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestRoutingProject.ViewModels;
using TestRoutingProject.Views;

namespace TestRoutingProject
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public App()
        {
            _host = Host.CreateDefaultBuilder().
               ConfigureServices((hostContext, services) => {

                   services.AddSingleton<HistoryRouter<ViewModelBase>>(s => new HistoryRouter<ViewModelBase>(t => (ViewModelBase)s.GetRequiredService(t))); services.AddSingleton<HistoryRouter<UserControl2Page>>(s => new HistoryRouter<UserControl2Page>(t => (UserControl2Page)s.GetRequiredService(t)));
                   services.AddSingleton<MainViewModel>();
                   services.AddTransient<UserControl1ViewModel>();
                   services.AddTransient<UserControl2ViewModel>();
                   services.AddTransient<UserControl3ViewModel>();
                   services.AddTransient<UserControl4ViewModel>();


               }).Build();
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = _host.Services.GetRequiredService<MainViewModel>()
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = _host.Services.GetRequiredService<MainViewModel>()
                };
            }
           
            base.OnFrameworkInitializationCompleted();
        }
    }
}
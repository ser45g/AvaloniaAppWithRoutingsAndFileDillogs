using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.SimpleRouter;
using Material.Styles.Themes.Base;
using Material.Styles.Themes;

using TestRoutingProject.ViewModels;
using TestRoutingProject.Views;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HanumanInstitute.MvvmDialogs.Avalonia.Fluent;
using Splat;
using TestRoutingProject.Services;
using Microsoft.Extensions.Logging;
using TestRoutingProject.Models;
using System;
using Avalonia.Controls;

namespace TestRoutingProject
{
    public partial class App : Application
    {
     

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            var build = Locator.CurrentMutable;
            var loggerFactory = LoggerFactory.Create(builder => builder.AddFilter(logLevel => true).AddDebug());

            build.RegisterLazySingleton(() => (IDialogService)new DialogService(
                new DialogManager(
                    viewLocator: new ViewLocator(),
                    logger: loggerFactory.CreateLogger<DialogManager>(),
                    dialogFactory: new DialogFactory().AddFluent(messageBoxType: FluentMessageBoxType.ContentDialog)),
                viewModelFactory: x => Locator.Current.GetService(x)));

            build.RegisterLazySingleton(() => new MainViewModel(Locator.Current.GetService<HistoryRouter<ViewModelBase>>()));

            build.RegisterLazySingleton<CyphersManager>(()=>new CyphersManager());

            //build.Register<CeaserCypherSettingsViewModel>(() => new CeaserCypherSettingsViewModel(new Cyphers.CeaserCypher(2)));

            SplatRegistrations.Register<UserControl1ViewModel>();
            SplatRegistrations.Register<UserControl2ViewModel>();
            SplatRegistrations.Register<UserControl3ViewModel>();
            SplatRegistrations.Register<UserControl4ViewModel>();
            SplatRegistrations.Register<UserControl5ViewModel>();
            SplatRegistrations.Register<ChartsViewModel>();
            SplatRegistrations.Register<NoSettingsAvailableViewModel>();

            
            build.RegisterLazySingleton<HistoryRouter<ViewModelBase>>(() => new HistoryRouter<ViewModelBase>(t => (ViewModelBase)Locator.Current.GetService(t)));

            build.RegisterLazySingleton<HistoryRouter<UserControl2Page>>(() => new HistoryRouter<UserControl2Page>(t => (UserControl2Page)Locator.Current.GetService(t)));

            SplatRegistrations.Register<IStorageService, StorageService>();
           
            SplatRegistrations.SetupIOC();

        }
      
        public override void OnFrameworkInitializationCompleted()
        {

            DialogService.Show(null, Locator.Current.GetService<MainViewModel>());

            base.OnFrameworkInitializationCompleted();
        }

       

        public static MainViewModel MainViewModel => Locator.Current.GetService<MainViewModel>();
        private static IDialogService DialogService => Locator.Current.GetService<IDialogService>()!;
        public static StrongViewLocator ViewLocator { get; private set; } = default!;
    }
}
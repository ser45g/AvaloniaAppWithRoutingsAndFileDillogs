using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using System;
using TestRoutingProject.ViewModels;
using TestRoutingProject.Views;

namespace TestRoutingProject
{
    public class ViewLocator : StrongViewLocator
    {
        public ViewLocator() {
            ForceSinglePageNavigation = false;

            Register<MainViewModel, MainView, MainWindow>();
            Register<UserControl1ViewModel, UserControl1View, MainWindow>();
            Register<UserControl2ViewModel, UserControl2View, MainWindow>();
            Register<UserControl3ViewModel, UserControl3View, MainWindow>();
            Register<UserControl4ViewModel, UserControl4View,MainWindow>();
            Register<UserControl5ViewModel, UserControl5View,MainWindow>();

            Register<ChartsViewModel,ChartsView,ChartsWindow>();
            Register<NoSettingsAvailableViewModel, NoSettingsAvailableView, NoSettingsAvailableWindow>();
            Register<CeaserCypherSettingsViewModel,CeaserCypherSettingsView,CeaserCypherSettingsWindow>();
            Register<ReplacementCypherSettingsViewModel,ReplacementCypherSettingsView,ReplacementCypherSettingsWindow>();
        }
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FileSystem;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRoutingProject.Business;
using TestRoutingProject.Services;

namespace TestRoutingProject.ViewModels
{
    public partial class UserControl4ViewModel:UserControl2Page
    {
        private readonly IDialogService _dialogService;
        private readonly IStorageService _storage;

        public string Output { get; private set; }

        public UserControl4ViewModel(IDialogService dialogService, IStorageService storage)
        {
            _dialogService = dialogService;
            _storage = storage;

            IsLightTheme = App.Current.RequestedThemeVariant == Avalonia.Styling.ThemeVariant.Light;
        }

        [ObservableProperty]
        private bool _isLightTheme;

        [RelayCommand]
        private void ChangeTheme()
        {
            if (App.Current.RequestedThemeVariant == Avalonia.Styling.ThemeVariant.Dark) {
                App.Current.RequestedThemeVariant=Avalonia.Styling.ThemeVariant.Light;
            }
            else
            {
                App.Current.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Dark;
            }
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using System;


namespace TestRoutingProject.ViewModels
{
    public partial class SetKeySettingsViewModel : ViewModelBase, IModalDialogViewModel, ICloseable, IViewClosed
    {
        public bool? DialogResult { get; set; } = true;
        [ObservableProperty]
        private string _key;

        public SetKeySettingsViewModel(string key)
        {
            Key = key;
        }

        public event EventHandler? RequestClose;

        public void OnClosed()
        {
           
        }

        [RelayCommand]
        private void Ok()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        private void Close()
        {
            DialogResult = false;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}

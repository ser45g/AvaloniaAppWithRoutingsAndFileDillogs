using CommunityToolkit.Mvvm.Input;
using HanumanInstitute.MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRoutingProject.ViewModels
{
    public partial class NoSettingsAvailableViewModel : ViewModelBase, IModalDialogViewModel, ICloseable, IViewClosed
    {
        public bool? DialogResult => true;

        public event EventHandler? RequestClose;

        public void OnClosed()
        {
            
        }

        [RelayCommand]
        private void Close()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}

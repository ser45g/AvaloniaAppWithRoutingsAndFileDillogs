using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cyphers;
using HanumanInstitute.MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRoutingProject.ViewModels
{
    public partial class CeaserCypherSettingsViewModel: ViewModelBase, IModalDialogViewModel, ICloseable, IViewClosed
    {
        private readonly CeaserCypher _cypher;

        public bool? DialogResult { get; set; } = true;

        public event EventHandler? RequestClose;

        public CeaserCypherSettingsViewModel(CeaserCypher cypher)
        {
            this._cypher = cypher;
            Shift = cypher.Shift;
        }

        public void OnClosed()
        {

        }
        [ObservableProperty]
        private ushort _shift;

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

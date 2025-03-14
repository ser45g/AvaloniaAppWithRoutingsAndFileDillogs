﻿using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cyphers;
using HanumanInstitute.MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRoutingProject.ViewModels
{
    public partial class ReplacementCypherSettingsViewModel:ViewModelBase, IModalDialogViewModel, ICloseable, IViewClosed
    {
        private readonly ReplacementCypher _cypher;

        public bool? DialogResult { get; set; } = true;

        public event EventHandler? RequestClose;

        public ReplacementCypherSettingsViewModel(ReplacementCypher cypher)
        {
            this._cypher = cypher;
            Order= new ObservableCollection<int>(cypher.Order);
        }

        public void OnClosed()
        {

        }
        [ObservableProperty]
        private IList<int> _order;

        [ObservableProperty]
        private int _lengthOfBlock;


        partial void OnLengthOfBlockChanged(int oldValue, int newValue)
        {
            ChangeLengthOfBlock(newValue);
        }

        private void ChangeLengthOfBlock(int newValue)
        {
            
            Order.Clear();
            for(int i = 1; i < newValue+1; ++i)
            {
                Order.Add(i);
            }
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


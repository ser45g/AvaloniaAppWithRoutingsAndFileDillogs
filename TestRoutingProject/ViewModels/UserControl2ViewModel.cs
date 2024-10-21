using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TestRoutingProject.ViewModels
{
    public partial class UserControl2ViewModel : ViewModelBase
    {
        private readonly HistoryRouter<UserControl2Page> router;
        [ObservableProperty]
        private ViewModelBase _content = default!;


        public UserControl2ViewModel(HistoryRouter<UserControl2Page> router)
        {
            this.router = router;

            router.CurrentViewModelChanged += viewModel => Content = viewModel;

            // change to HomeView 
            router.GoTo<UserControl3ViewModel>();
        }
        [RelayCommand]
        private void GoUserControl3()
        {
            router.GoTo<UserControl3ViewModel>();
        }
        [RelayCommand]
        private void GoUserControl4()
        {
            router.GoTo<UserControl4ViewModel>();
        }
        [RelayCommand]
        private void Help()
        {
            router.GoTo<UserControl4ViewModel>();
        }
        [RelayCommand]
        private void GoTo(NavigationViewItemInvokedEventArgs args)
        {
            int id = 1;
            NavigationViewItem a = new NavigationViewItem();
            
        }
    }
        
}

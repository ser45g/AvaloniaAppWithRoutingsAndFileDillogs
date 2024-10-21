using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRoutingProject.ViewModels
{
    public partial class  UserControl1ViewModel: ViewModelBase
    {
        private readonly HistoryRouter<ViewModelBase> router;

        public UserControl1ViewModel(HistoryRouter<ViewModelBase> router)
        {
            this.router = router;
        }
        [RelayCommand]
        public void GoUserControl2()
        {
            router.GoTo<UserControl2ViewModel>();
        }
    }
}

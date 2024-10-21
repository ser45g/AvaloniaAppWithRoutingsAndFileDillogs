using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using FluentAvalonia.UI.Windowing;

namespace TestRoutingProject.Views
{
    public partial class MainWindow : AppWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            TitleBar.ExtendsContentIntoTitleBar = true;
            TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
            
        }
    }
}
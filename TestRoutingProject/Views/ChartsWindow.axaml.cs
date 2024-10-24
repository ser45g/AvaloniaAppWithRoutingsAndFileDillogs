using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Windowing;

namespace TestRoutingProject.Views;

public partial class ChartsWindow : AppWindow
{
    public ChartsWindow()
    {
        InitializeComponent();
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
    }
}
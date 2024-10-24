using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Windowing;

namespace TestRoutingProject.Views;

public partial class CeaserCypherSettingsWindow : AppWindow
{
    public CeaserCypherSettingsWindow()
    {
        InitializeComponent();
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
    }
}
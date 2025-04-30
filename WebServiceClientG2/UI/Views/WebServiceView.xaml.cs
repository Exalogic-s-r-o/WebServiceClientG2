using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Views;

public partial class WebServiceView : ContentView
{
    private ViewModels.WebServiceViewModel viewModel;
    public WebServiceView(Base.AppEngine appEngine,
                          IPopupService popupService)
    {
        InitializeComponent();

        viewModel = new ViewModels.WebServiceViewModel(appEngine, popupService);
        this.BindingContext = viewModel;
    }
}
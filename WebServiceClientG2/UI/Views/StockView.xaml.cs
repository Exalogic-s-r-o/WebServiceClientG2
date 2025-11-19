using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Views;

public partial class StockView : ContentView
{
    UI.ViewModels.StockViewModel viewModel;
    public StockView(Base.AppEngine appEngine,
                     IPopupService popupService)
    {
        InitializeComponent();

        this.viewModel = new UI.ViewModels.StockViewModel(appEngine, popupService);
        this.BindingContext = viewModel;
    }
}
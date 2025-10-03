using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Views;

public partial class Stock_StockMovementView : ContentView
{

	UI.ViewModels.Stock_StockMovementViewModel viewModel;
    public Stock_StockMovementView(Base.AppEngine appEngine,
							   IPopupService popupService)
	{
		InitializeComponent();

		this.viewModel = new UI.ViewModels.Stock_StockMovementViewModel(appEngine, popupService);
		this.BindingContext = viewModel;
    }
}
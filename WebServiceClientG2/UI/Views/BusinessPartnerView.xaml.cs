using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Views;

public partial class BusinessPartnerView : ContentView
{

	UI.ViewModels.BusinessPartnerViewModel viewModel;
    public BusinessPartnerView(Base.AppEngine appEngine,
							   IPopupService popupService)
	{
		InitializeComponent();

		this.viewModel = new UI.ViewModels.BusinessPartnerViewModel(appEngine, popupService);
		this.BindingContext = viewModel;
    }
}
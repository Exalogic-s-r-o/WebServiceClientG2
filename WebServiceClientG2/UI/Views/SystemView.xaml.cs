using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Views;

public partial class SystemView : ContentView
{
	ViewModels.SystemViewModel viewModel;
    public SystemView(Base.AppEngine appEngine,
                      IPopupService popupService)
	{
		InitializeComponent();

		this.viewModel = new ViewModels.SystemViewModel(appEngine, popupService);
        this.BindingContext = viewModel;
    }
}
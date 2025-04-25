using CommunityToolkit.Maui.Views;

namespace WebServiceClientG2.UI.Popups;

public partial class WebServicePopup : Popup
{
	public WebServicePopup(WebServicePopupViewModel astonPopupViewModel)
	{
		InitializeComponent();
		BindingContext = astonPopupViewModel;
	}
}
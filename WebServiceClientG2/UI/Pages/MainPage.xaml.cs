using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Pages;

public partial class MainPage : ContentPage
{
	UI.ViewModels.MainViewModel ViewModel;
	public MainPage(Base.AppEngine appEngine,
                    IPopupService popupService)
	{
		InitializeComponent();

        this.ViewModel = new UI.ViewModels.MainViewModel(appEngine, popupService);
        this.BindingContext = this.ViewModel;
    }

    private void ConsoleEditor_TextChanged(object sender, TextChangedEventArgs e)
    {

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await ConsoleScrollView.ScrollToAsync(0, ConsoleScrollView.ContentSize.Height, false);
        });
        
    }
}
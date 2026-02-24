using CommunityToolkit.Maui.Core;
using System;

namespace WebServiceClientG2.UI.Pages;

public partial class MainPage : ContentPage
{
	UI.ViewModels.MainViewModel ViewModel;
	private readonly Base.AppEngine _appEngine;
	private readonly IPopupService _popupService;

	public MainPage(Base.AppEngine appEngine,
	                IPopupService popupService)
	{
		InitializeComponent();

		_appEngine = appEngine;
		_popupService = popupService;

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

    private void OrdersReceived_Clicked(object sender, EventArgs e)
    {
        try
        {
            var ordersView = new UI.Views.OrdersReceivedView(_appEngine, _popupService);
            if (this.ViewModel != null)
            {
                this.ViewModel.CurrentTabContent = ordersView;
            }
        }
        catch
        {
        }
    }
}
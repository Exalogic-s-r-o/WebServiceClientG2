using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Views;

public partial class OrdersReceivedView : ContentView
{
    UI.ViewModels.OrdersReceivedViewModel viewModel;
    public OrdersReceivedView(Base.AppEngine appEngine,
                              IPopupService popupService)
    {
        InitializeComponent();

        this.viewModel = new UI.ViewModels.OrdersReceivedViewModel(appEngine, popupService);
        this.BindingContext = viewModel;
        
        // Load the XAML-based add view as child (OrdersReceivedAddView is a ContentView)
        try
        {
            var addView = new OrdersReceivedAddView();
            addView.BindingContext = viewModel;
            // set the content of this view to the add view
            this.Content = addView;
        }
        catch
        {
            // ignore and keep original XAML content
        }
    }
}


using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Views;

public partial class OrdersIssuedView : ContentView
{
    UI.ViewModels.OrdersIssuedViewModel viewModel;
    public OrdersIssuedView(Base.AppEngine appEngine,
                              IPopupService popupService)
    {
        InitializeComponent();

        this.viewModel = new UI.ViewModels.OrdersIssuedViewModel(appEngine, popupService);
        this.BindingContext = viewModel;
        
        // Load the XAML-based add view as child (OrdersIssuedAddView is a ContentView)
        try
        {
            //var addView = new OrdersIssuedAddView();
            //addView.BindingContext = viewModel;
            // set the content of this view to the add view
            //this.ctw_OrdersIssued_Add.Content = addView;

            var summaryView = new OrdersIssuedSummaryItemsView();
            summaryView.BindingContext = viewModel;
            // set the content of this view to the add view
            this.ctw_OrdersIssued_SummaryItems.Content = summaryView;

        }
        catch
        {
            // ignore and keep original XAML content
        }
    }
}


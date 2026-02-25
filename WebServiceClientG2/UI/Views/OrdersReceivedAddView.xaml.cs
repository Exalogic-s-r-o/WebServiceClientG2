using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using WebServiceClientG2.Base;

namespace WebServiceClientG2.UI.Views;

public partial class OrdersReceivedAddView : ContentView
{
    public OrdersReceivedAddView()
    {
        InitializeComponent();
    }

    private void OnAddItemClicked(object sender, EventArgs e)
    {
        if (this.BindingContext is WebServiceClientG2.UI.ViewModels.OrdersReceivedViewModel vm)
        {
            // call command if available
            if (vm.AddItemCommand.CanExecute(null)) vm.AddItemCommand.Execute(null);
        }
    }

    private void OnRemoveItemClicked(object sender, EventArgs e)
    {
        if (this.BindingContext is WebServiceClientG2.UI.ViewModels.OrdersReceivedViewModel vm)
        {
            if (sender is Button btn && btn.BindingContext != null)
            {
                var item = btn.BindingContext as Exa.OBERON.ServicesGen2.Client.Models.OrdersReceived.OrderReceivedItem;
                if (item != null)
                {
                    if (vm.RemoveItemCommand.CanExecute(item)) vm.RemoveItemCommand.Execute(item);
                }
            }
        }
    }
}

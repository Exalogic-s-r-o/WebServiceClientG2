using CommunityToolkit.Mvvm.Messaging;

namespace WebServiceClientG2.UI.Views;

public partial class StockView : ContentView
{
	public StockView()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage("Pridané zo stocku."));
    }
}
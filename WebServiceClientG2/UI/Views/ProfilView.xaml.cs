using CommunityToolkit.Mvvm.Messaging;

namespace WebServiceClientG2.UI.Views;

public partial class ProfilView : ContentView
{
	public ProfilView()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage("Pridané z profilu."));
    }
}
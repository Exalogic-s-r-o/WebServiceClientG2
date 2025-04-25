using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Pages
{
    public partial class MainPage : ContentPage
    {

        public MainPage(ViewModels.MainViewModel viewModel)
        {
            InitializeComponent();

            this.BindingContext = viewModel;
        }
    }

}

using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.Pages
{
    public partial class MainPage : ContentPage
    {
        private ViewModels.MainViewModel viewModel;
        public MainPage(Base.AppEngine appEngine,
                        IPopupService popupService)
        {
            InitializeComponent();

            viewModel = new ViewModels.MainViewModel(appEngine, popupService);
            this.BindingContext = viewModel;
        }
    }

}

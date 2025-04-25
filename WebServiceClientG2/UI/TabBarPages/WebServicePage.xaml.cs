using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2.UI.TabBarPages
{
    public partial class WebServicePage : ContentPage
    {
        private ViewModels.WebServiceViewModel viewModel;
        public WebServicePage(Base.AppEngine appEngine,
                              IPopupService popupService)
        {
            InitializeComponent();

            viewModel = new ViewModels.WebServiceViewModel(appEngine, popupService);
            this.BindingContext = viewModel;
        }
    }

}

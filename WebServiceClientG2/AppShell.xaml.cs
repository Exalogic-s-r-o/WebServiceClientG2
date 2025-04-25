namespace WebServiceClientG2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute($"{nameof(UI.TabBarPages.WebServicePage)}", typeof(UI.TabBarPages.WebServicePage));

            Routing.RegisterRoute($"{nameof(UI.TabBarPages.CompanyPage)}", typeof(UI.TabBarPages.CompanyPage));

            Routing.RegisterRoute($"{nameof(UI.TabBarPages.StockCardsPage)}", typeof(UI.TabBarPages.StockCardsPage));

            TabBar tabBar = new TabBar { Route = "tabs" };

            Tab webServiceTab = new Tab
            {
                Title = "Webová služba",
                Icon = "settings.png"
            };
            ShellContent webServiceContent = new ShellContent
            {
                Route = nameof(UI.TabBarPages.WebServicePage),
                ContentTemplate = new DataTemplate(typeof(UI.TabBarPages.WebServicePage))
            };
            webServiceTab.Items.Add(webServiceContent);

            Tab companyTab = new Tab
            {
                Title = "Firma",
                Icon = "settings.png"
            };
            ShellContent companyContent = new ShellContent
            {
                Route = nameof(UI.TabBarPages.CompanyPage),
                ContentTemplate = new DataTemplate(typeof(UI.TabBarPages.CompanyPage))
            };
            companyTab.Items.Add(companyContent);

            Tab stockCardsTab = new Tab
            {
                Title = "Skladové karty",
                Icon = "profile.png"
            };
            ShellContent stockCardsContent = new ShellContent
            {
                Route = nameof(UI.TabBarPages.StockCardsPage),
                ContentTemplate = new DataTemplate(typeof(UI.TabBarPages.StockCardsPage))
            };
            stockCardsTab.Items.Add(stockCardsContent);

            tabBar.Items.Add(webServiceTab);
            tabBar.Items.Add(companyTab);
            tabBar.Items.Add(stockCardsTab);

            this.Items.Add(tabBar);
        }
    }
}

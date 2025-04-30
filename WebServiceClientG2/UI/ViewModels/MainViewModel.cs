using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;


namespace WebServiceClientG2.UI.ViewModels
{
    public partial class MainViewModel : BaseViewModel,
                                         IRecipient<WebServiceClientG2.Messages.AddTextMessage>
    {

        #region MESSAGES

        void IRecipient<WebServiceClientG2.Messages.AddTextMessage>.
            Receive(WebServiceClientG2.Messages.AddTextMessage text) => ConsoleTextAdd(text.Value);

        #endregion

        #region CONSTRUCTOR
        public MainViewModel(Base.AppEngine appEngine,
                             IPopupService popupService) : base(appEngine, popupService)
        {
            WeakReferenceMessenger.Default.RegisterAll(this);

            this.webServiceView = new Views.WebServiceView(appEngine, popupService);
            this.systemView = new Views.SystemView(appEngine, popupService);

            this.webServiceTab = new Models.TabItem(name: "WebService", contentView: webServiceView, isSelected: true);
            this.systemTab = new Models.TabItem(name: "System", contentView: systemView);

            this.Tabs = new ObservableCollection<Models.TabItem>();
            this.SelectedTab = webServiceTab;

            CreateTabMenu();

            // Initialize console.
            ConsoleText = $"Testovacia aplikácia k OBERONGen2 webovej službe. Verzia z dňa: '{WebServiceClientG2.Base.AppEngine.CONST_APP_VERSION_DATE}'\n";
        }

        #endregion

        #region FIELDS

        private readonly UI.Views.WebServiceView webServiceView;
        private readonly UI.Views.SystemView systemView;

        private readonly Models.TabItem webServiceTab;
        private readonly Models.TabItem systemTab;

        #endregion

        #region PROPERTIES

        private ObservableCollection<Models.TabItem> tabs = new ObservableCollection<Models.TabItem>();
        /// <summary>
        /// Zoznam záložiek v liste.
        /// </summary>
        public ObservableCollection<Models.TabItem> Tabs
        {
            get { return tabs; }
            set
            {
                tabs = value;
                OnPropertyChanged("Tabs");
            }
        }

        private Models.TabItem selectedTab = new Models.TabItem("Test", new ContentView());
        /// <summary>
        /// Vybraná záložka.
        /// </summary>
        public Models.TabItem SelectedTab
        {
            get { return selectedTab; }
            set
            {
                if (selectedTab == value)
                {
                    return;
                }

                selectedTab = value;

                try
                {
                    ChangeView(value);
                }
                catch
                {
                }

                OnPropertyChanged("SelectedTab");
            }
        }

        private object currentTabContent = new object();
        /// <summary>
        /// Obsah aktuálnej záložky.
        /// </summary>
        public object CurrentTabContent
        {
            get { return currentTabContent; }
            set
            {
                currentTabContent = value;
                OnPropertyChanged("CurrentTabContent");
            }
        }

        private string consoleText = string.Empty;
        /// <summary>
        /// Text v konzole.
        /// </summary>
        public string ConsoleText
        {
            get { return consoleText; }
            set
            {
                consoleText = value;
                OnPropertyChanged("ConsoleText");
            }
        }

        #endregion

        #region METHODS

        private void ChangeView(Models.TabItem u_TabItem)
        {
            if (CurrentTabContent == u_TabItem.ContentView)
            {
                return;
            }

            CurrentTabContent = u_TabItem.ContentView;
        }

        /// <summary>
        /// Pridanie textu do konzoly.
        /// </summary>
        /// <param name="u_NewText"></param>
        public void ConsoleTextAdd(string u_NewText)
        {
            ConsoleText += $"{u_NewText}\n";
        }

        private void CreateTabMenu()
        {
            try
            {
                this.tabs.Add(webServiceTab);
                this.tabs.Add(systemTab);
            }
            catch
            {
            }
        }
        #endregion

    }
}

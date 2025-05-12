using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
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
            consoleText += JSONFilePath + "\n";

            this._AppEngine.WebServiceClient.System.JSONLog = SaveJSONLog;
            this._AppEngine.WebServiceClient.User.JSONLog = SaveJSONLog;
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

        /// <summary>
        /// Umiestnenie súboru s JSON logmi.
        /// </summary>
        public string JSONFilePath
        {
            get { return  $"Umiestnenie log súboru s JSON volaniami: {FileSystem.CacheDirectory}"; }
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

        /// <summary>
        /// Uloženie JSON logu do súboru.
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private void SaveJSONLog(string log)
        {
            try
            {
                string filePath = System.IO.Path.Combine(FileSystem.CacheDirectory, $"console_log.txt");
                System.IO.File.AppendAllText(filePath, log);
            }
            catch (System.Exception ex)
            {
                ConsoleText += "Error saving log file: " + ex.Message + "\n";
            }
        }

        /// <summary>
        /// Vyčistenie konzoly.
        /// </summary>
        [RelayCommand]
        public void ClearConsole()
        {
            ConsoleText = string.Empty;
        }

        [RelayCommand]
        private async Task OpenConsoleFile()
        {
            try
            {
                string filePath = System.IO.Path.Combine(FileSystem.CacheDirectory, $"console_log.txt");
                if (!System.IO.File.Exists(filePath))
                {
                    ConsoleText += "File not found: " + filePath + "\nPlease save the console first.\n";
                    return;
                }

                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            catch (System.Exception ex)
            {
                ConsoleText += "Error opening console file: " + ex.Message + "\n";
            }
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

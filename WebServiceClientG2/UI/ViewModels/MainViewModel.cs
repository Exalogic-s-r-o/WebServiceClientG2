using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.UI.ViewModels
{
    public partial class MainViewModel : BaseViewModel,
                                         IRecipient<WebServiceClientG2.Messages.AddTextMessage>
    {

        #region MESSAGES


        void IRecipient<WebServiceClientG2.Messages.AddTextMessage>.
            Receive(WebServiceClientG2.Messages.AddTextMessage text) => ConsoleTextAdd(text.Value);

        #endregion

        private object currentTabContent;
        private Color settingsTabColor;
        private Color profileTabColor;
        private string consoleText;

        public object CurrentTabContent
        {
            get { return currentTabContent; }
            set
            {
                currentTabContent = value;
                OnPropertyChanged("CurrentTabContent");
            }
        }

        public Color SettingsTabColor
        {
            get { return settingsTabColor; }
            set
            {
                settingsTabColor = value;
                OnPropertyChanged("SettingsTabColor");
            }
        }

        public Color ProfileTabColor
        {
            get { return profileTabColor; }
            set
            {
                profileTabColor = value;
                OnPropertyChanged("ProfileTabColor");
            }
        }

        public string ConsoleText
        {
            get { return consoleText; }
            set
            {
                consoleText = value;
                OnPropertyChanged("ConsoleText");
            }
        }

        private readonly UI.Views.ProfilView profilView;
        private readonly UI.Views.StockView stockView;
        private readonly UI.Views.WebServiceView webServiceView;

        public MainViewModel(Base.AppEngine appEngine,
                             IPopupService popupService) : base(appEngine, popupService)
        {
            WeakReferenceMessenger.Default.RegisterAll(this);

            webServiceView = new Views.WebServiceView(appEngine, popupService);
            profilView = new Views.ProfilView();
            stockView = new Views.StockView();

            // Initialize console
            ConsoleText = $"Testovacia aplikácia k OBERONGen2 webovej službe. Verzia z dňa: '{WebServiceClientG2.Base.AppEngine.CONST_APP_VERSION_DATE}'\n";

            // Default to Settings tab
            ShowWebService();
        }

        [RelayCommand]
        private void ShowWebService()
        {
            if (CurrentTabContent == webServiceView)
            {
                return;
            }

            CurrentTabContent = webServiceView;
            SettingsTabColor = Colors.DarkGray;
            ProfileTabColor = Colors.LightGray;
            ConsoleText += "Switched to webService Tab\n";
        }

        [RelayCommand]
        private void ShowSettings()
        {
            if (CurrentTabContent == profilView)
            {
                return;
            }

            CurrentTabContent = profilView;
            SettingsTabColor = Colors.DarkGray;
            ProfileTabColor = Colors.LightGray;
            ConsoleText += "Switched to Settings Tab\n";
        }

        [RelayCommand]
        private void ShowProfile()
        {
            if (CurrentTabContent == stockView)
            {
                return;
            }

            CurrentTabContent = stockView;
            SettingsTabColor = Colors.LightGray;
            ProfileTabColor = Colors.DarkGray;
            ConsoleText += "Switched to Profile Tab\n";
        }

        public void ConsoleTextAdd(string u_NewText)
        {
            ConsoleText += $"{u_NewText}\n";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

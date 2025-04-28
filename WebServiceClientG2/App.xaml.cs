using CommunityToolkit.Maui.Core;

namespace WebServiceClientG2
{
    public partial class App : Application
    {
        #region CONSTANTS

        private const string Pref_MainWindowX = "MainWindowX";
        private const string Pref_MainWindowY = "MainWindowY";
        private const string Pref_MainWindowWidth = "MainWindowWidth";
        private const string Pref_MainWindowHeight = "MainWindowHeight";

        #endregion

        #region FIELDS

        private int sizeChangedReq = 0;

        Base.AppEngine _AppEngine;
        IPopupService PopupService;

        #endregion

        public App(Base.AppEngine appEngine, 
                   IPopupService popupService)
        {
            InitializeComponent();

            this._AppEngine = appEngine;
            this.PopupService = popupService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {

            var window = new Window(new NavigationPage(new UI.Pages.MainPage(this._AppEngine, this.PopupService)));


            // nastav uloženú pozíciu okna. Niekedy sa uloží záporná hodnota. tu je ošetrenie ak bola uložená záporná pozícia
            var window_X = Preferences.Default.Get(Pref_MainWindowX, window.X);
            var window_Y = Preferences.Default.Get(Pref_MainWindowY, window.Y);

            if (window_X >= 0 || window_Y >= 0)
            {
                window.X = window_X;
                window.Y = window_Y;
                window.Width = Preferences.Default.Get(Pref_MainWindowWidth, window.Width);
                window.Height = Preferences.Default.Get(Pref_MainWindowHeight, window.Height);
            }

            // zavolaj pri zmene veľkosti a pozície okna
            window.SizeChanged += (s, e) => {

                // neopakuj volanie, pokiaľ nebolo spracované uloženie stavu
                if (Interlocked.CompareExchange(ref sizeChangedReq, 1, 0) == 0)
                {
                    _ = Window_SizeChangedAsync(s, e);
                }
            };

            return window;
        }

        /// <summary>
        /// Uloženie pozície okna.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task Window_SizeChangedAsync(object? sender, EventArgs e)
        {
            try
            {
                if (sender is Window window)
                {

                    // Počkaj na uloženie poslednej pozície a veľkosti okna
                    await Task.Delay(2000);

                    // Poznač že bol uložený posledný stav
                    sizeChangedReq = 0;

                    //System.Diagnostics.Debug.WriteLine($"DISP:{DeviceDisplay.Current.MainDisplayInfo}");
                    //System.Diagnostics.Debug.WriteLine($"POS[{window.X},{window.Y}], SIZE[{window.Width},{window.Height}]");

                    // neukladaj záporné hodnoty, nastane pri minimalizovaní okna
                    if (window.X < 0 && window.Y < 0)
                    {
                        return;
                    }

                    Preferences.Default.Set(Pref_MainWindowX, window.X);
                    Preferences.Default.Set(Pref_MainWindowY, window.Y);

                    Preferences.Default.Set(Pref_MainWindowWidth, window.Width);
                    Preferences.Default.Set(Pref_MainWindowHeight, window.Height);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
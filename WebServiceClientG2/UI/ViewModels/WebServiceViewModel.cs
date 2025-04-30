using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.UI.ViewModels
{
    public partial class WebServiceViewModel : BaseViewModel
    {

        #region CONSTRUCTOR

        /// <summary>
        /// Konštruktor.
        /// </summary>
        public WebServiceViewModel(Base.AppEngine appEngine, 
                                   IPopupService popupService) : base(appEngine, popupService)
        {
            this.IPAddress = appEngine.WebServiceSettings.IPAddress;
            this.UserName = appEngine.WebServiceSettings.UserName;
            this.Password = appEngine.WebServiceSettings.UserPassword;
            this.SSL = appEngine.WebServiceSettings.SSL;
        }
        #endregion

        #region PROPERTIES

        private string? prp_IPAddress;
        /// <summary>
        /// IP adresa webovej služby OBERON Center.
        /// </summary>
        public string? IPAddress
        {
            get => prp_IPAddress;
            set
            {
                prp_IPAddress = value;
                OnPropertyChanged("IPAddress");
            }
        }

        private string? prp_UserName;
        /// <summary>
        /// Používatelské meno.
        /// </summary>
        public string? UserName
        {
            get => prp_UserName;
            set
            {
                prp_UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string? prp_Password;
        /// <summary>
        /// Používatelské heslo.
        /// </summary>
        public string? Password
        {
            get => prp_Password;
            set
            {
                prp_Password = value;
                OnPropertyChanged("Password");
            }
        }

        private bool prp_SSL;
        /// <summary>
        /// Príznak či používateľ používa šifrovanú komunikáciu.
        /// </summary>
        public bool SSL
        {
            get => prp_SSL;
            set
            {
                prp_SSL = value;
                OnPropertyChanged("SSL");
            }
        }

        #endregion

        #region PRIVATE METHODS

        [RelayCommand]
        private async Task Back()
        {
            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                await GoBack();
            }
            catch (Exception ex)
            {
                await ShowPopup(EXC.Get(ex.Message));
            }
            finally
            {
                this.IsRunning = false;
            }
        }

        [RelayCommand]
        private async Task LoginSalt()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                if (this._AppEngine.WebServiceClient.IsInitialized == false)
                {
                    // Inicializácia webovej služby.
                    myEx = this._AppEngine.WebServiceClient.SetBaseAddress(this.IPAddress);
                    if (myEx.Result == false)
                    {
                        // Chyba.
                        await ShowPopup(myEx);
                        return;
                    }
                }

                // Uloží data zadané používateľom.
                myEx = SaveUserCredentials();
                if (myEx.Result == false)
                {
                    // Chyba.
                    await ShowPopup(myEx);
                }


                var result = await this._AppEngine.WebServiceClient.Login.LoginSalt(this.UserName);

            }
            catch (Exception ex)
            {
                await ShowPopup(EXC.Get(ex.Message));
            }
            finally
            {
                this.IsRunning = false;
            }
        }

        /// <summary>
        /// Úloha na prihlásenie používateľa.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task Login()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                if (string.IsNullOrEmpty(this.IPAddress) == true)
                {
                    // Chyba
                    await ShowPopup(EXC.Get("Nie je zadaná adresa webovej služby."));
                    return;
                }

                if (string.IsNullOrEmpty(this.UserName) == true)
                {
                    // Chyba
                    await ShowPopup(EXC.Get("Nie je zadané používateľské meno."));
                    return;
                }

                if (this._AppEngine.WebServiceClient.IsInitialized == false)
                {
                    // Inicializácia webovej služby.
                    myEx = this._AppEngine.WebServiceClient.SetBaseAddress(this.IPAddress);
                    if (myEx.Result == false)
                    {
                        // Chyba.
                        await ShowPopup(myEx);
                        return;
                    }
                }

                // Uloží data zadané používateľom.
                myEx = SaveUserCredentials();
                if (myEx.Result == false)
                {
                    // Chyba.
                    await ShowPopup(myEx);
                }

                var result = await this._AppEngine.WebServiceClient.Login.Login(this.UserName);

            }
            catch (Exception ex)
            {
                await ShowPopup(EXC.Get(ex.Message));
            }
            finally
            {
                this.IsRunning = false;
            }
        }

        /// <summary>
        /// Initializácia webovej služby.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task InitializeWebService()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                if (string.IsNullOrEmpty(this.IPAddress) == true)
                {
                    // Chyba
                    await ShowPopup(EXC.Get("Nie je zadaná adresa webovej služby."));
                    return;
                }

                if (string.IsNullOrEmpty(this.UserName) == true)
                {
                    // Chyba
                    await ShowPopup(EXC.Get("Nie je zadané používateľské meno."));
                    return;
                }

                // Uloží data zadané používateľom.
                myEx = SaveUserCredentials();
                if (myEx.Result == false)
                {
                    // Chyba.
                    await ShowPopup(myEx);
                    return;
                }

                // Inicializácia webovej služby.
                myEx = this._AppEngine.WebServiceClient.SetBaseAddress(this.IPAddress);
                if (myEx.Result == false)
                {
                    // Chyba.
                    await ShowPopup(myEx);
                    return;
                }

                // Odoslať správu do konzoly.
                WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"Webová služba bola inicializovaná na adrese: '{this.IPAddress}'"));
            }
            catch (Exception ex)
            {
                await ShowPopup(EXC.Get(ex.Message));
            }
            finally
            {
                this.IsRunning = false;
            }
        }

        /// <summary>
        /// Uloží všetky data, ktoré boli zadané používateľom.
        /// </summary>
        private EXC SaveUserCredentials()
        {
            EXC myEx = EXC.GetDefault();

            try
            {
                this._AppEngine.WebServiceSettings.IPAddress = IPAddress ?? string.Empty;
                this._AppEngine.WebServiceSettings.UserName = UserName ?? string.Empty;
                this._AppEngine.WebServiceSettings.UserPassword = Password ?? string.Empty;
                this._AppEngine.WebServiceSettings.SSL = SSL;

                this._AppEngine.WebServiceSettings.Save();

                // OK.
                myEx.Result = true;
                return myEx;
            }
            catch (Exception ex)
            {
                return EXC.Get($"Chyba pri uložení prihlasovacích údajov. '{ex.Message}'");
            }
        }

        #endregion
    }
}

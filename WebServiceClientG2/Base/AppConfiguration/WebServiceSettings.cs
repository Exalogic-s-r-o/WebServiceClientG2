using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.Base.AppConfiguration
{
    public class WebServiceSettings
    {

        #region CONSTANTS

        const string SECTION_NAME = "WebServiceSettings";

        #endregion

        #region PROPERTIES

        private string prp_IPAddress = string.Empty;
        /// <summary>
        /// IP adresa webovej služby.
        /// </summary>
        public string IPAddress
        {
            get
            {
                return prp_IPAddress;
            }

            set
            {
                prp_IPAddress = value;
            }
        }

        private string prp_UserPassword = string.Empty;
        /// <summary>
        /// Heslo do webovej služby.
        /// </summary>
        public string UserPassword
        {
            get
            {
                return prp_UserPassword;
            }

            set
            {
                prp_UserPassword = value;
            }
        }

        private string prp_UserName = string.Empty;
        /// <summary>
        /// Meno používateľa do webovej služby.
        /// </summary>
        public string UserName
        {
            get
            {
                return prp_UserName;
            }

            set
            {
                prp_UserName = value;
            }
        }

        private bool prp_SSL = false;
        /// <summary>
        /// Príznak či používateľ používa šifrovanú pomunikáciu.
        /// </summary>
        public bool SSL
        {
            get
            {
                return prp_SSL;
            }

            set
            {
                prp_SSL = value;
            }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Načíta nastavenia uložené pomocou 
        /// </summary>
        public void Load()
        {
            try
            {
                // IP adresa.
                prp_IPAddress = Preferences.Get($"{SECTION_NAME}.IPAddress", "");

                // Heslo používateľa.
                prp_UserPassword = Preferences.Get($"{SECTION_NAME}.UserPassword", "");

                // Meno používateľa.
                prp_UserName = Preferences.Get($"{SECTION_NAME}.UserName", "system");

                // SSL.
                prp_SSL = Preferences.Get($"{SECTION_NAME}.SSL", false);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Uloží nastavenia pomocou 
        /// </summary>
        public void Save()
        {
            try
            {
                // IP adresa.
                Preferences.Set($"{SECTION_NAME}.IPAddress", IPAddress);

                // Meno používateľa.
                Preferences.Set($"{SECTION_NAME}.UserPassword", UserPassword);

                // Heslo pouťívateľa.
                Preferences.Set($"{SECTION_NAME}.UserName", UserName);

                // SSL.
                Preferences.Set($"{SECTION_NAME}.SSL", SSL);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Vráti kópiu objektu.
        /// </summary>
        public WebServiceSettings Clone()
        {
            try
            {

                WebServiceSettings tmp_WebService;

                tmp_WebService = new WebServiceSettings();

                tmp_WebService.IPAddress = this.IPAddress;
                tmp_WebService.UserName = this.UserName;
                tmp_WebService.UserPassword = this.UserPassword;
                tmp_WebService.SSL = this.SSL;

                return tmp_WebService;
            }
            catch
            {
                return new WebServiceSettings();
            }
        }

        /// <summary>
        /// Odstráni hodnoty z tohto objektu.
        /// </summary>
        public void Clear()
        {
            try
            {
                this.IPAddress = string.Empty;
                this.UserName = string.Empty;
                this.UserPassword = string.Empty;
                this.SSL = this.SSL;

            }
            catch
            {
            }
        }

        #endregion

    }
}

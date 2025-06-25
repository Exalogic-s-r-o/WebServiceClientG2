using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.Base
{
    public class AppEngine : IDisposable
    {

        #region CONSTANTS

        /// <summary>
        /// Meno aplikácie.
        /// </summary>
        public const string CONST_APPNAME = "WebServiceClientG2 test projekt";

        /// <summary>
        /// Dátum vydania verzie.
        /// </summary>
        public const string CONST_APP_VERSION_DATE = "25.6.2025";

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Konštruktor.
        /// </summary>
        public AppEngine()
        {
            this.prp_WebServiceSettings = new WebServiceClientG2.Base.AppConfiguration.WebServiceSettings();
            this.prp_WebServiceClient = new Exa.OBERON.ServicesGen2.Client.WebServiceClient(CONST_APPNAME,
                                                                                            CONST_APP_VERSION_DATE);
        }

        #endregion

        #region PROPERTIES

        private Exa.OBERON.ServicesGen2.Client.WebServiceClient prp_WebServiceClient;
        /// <summary>
        /// Client webovej služby, obsahuje volania na webovú službu.
        /// </summary>
        public Exa.OBERON.ServicesGen2.Client.WebServiceClient WebServiceClient
        {
            get
            {
                return prp_WebServiceClient;
            }

            set
            {
                prp_WebServiceClient = value;
            }
        }

        private WebServiceClientG2.Base.AppConfiguration.WebServiceSettings prp_WebServiceSettings;
        /// <summary>
        /// Nastavenia aplikácie - obsahuje údaje o webovej službe. Tieto sa však priamo v runtime nepoužívajú,
        /// pretože údaje o pripojení je možné získať aj z QR kódu.
        /// </summary>
        public WebServiceClientG2.Base.AppConfiguration.WebServiceSettings WebServiceSettings
        {
            get
            {
                return prp_WebServiceSettings;
            }

            set
            {
                prp_WebServiceSettings = value;
            }
        }

        #endregion

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AppEngine()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}

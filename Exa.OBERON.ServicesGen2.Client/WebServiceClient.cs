using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Exa.OBERON.ServicesGen2.Client.Models;
using EXC = Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException;

namespace Exa.OBERON.ServicesGen2.Client
{
    public class WebServiceClient : IDisposable
    {

        #region DECLARATION

        public HttpClient HttpClient;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Konštuktor Http klienta - odporúča sa nastaviť maximálny http timeout daných volaní.
        /// </summary>
        /// <param name="defaultHttpTimeout">Maximálny Timeout Http volania. Pri každom volaní http requestu (GET, POST, PUT, DEL) sa nastavuje daný timout volania samostatne (musí byť totožný alebo menší ako maximálny timeout).</param>
        public WebServiceClient(uint defaultHttpTimeout = 60)
        {

            HttpClientHandler handler = new HttpClientHandler();

            // Attach custom certificate validation callback.
            handler.ServerCertificateCustomValidationCallback = ValidateCertificate;

            HttpClient = new HttpClient(handler);

            // Timeout Http Klienta - musí sa nastaviť pred prvým volaním, lebo potom sa už nedá zmeniť.
            // Pri každom volaní http requestu (GET, POST, PUT, DEL) sa nastavuje daný timout volania samostatne (musí byť totožný alebo menší ako maximálny timeout).            
            if (defaultHttpTimeout == 0)
            {
                defaultHttpTimeout = 60;
            }
            prp_HttpTimeout = defaultHttpTimeout;

            if (prp_HttpTimeout != 0)
            {
                HttpClient.Timeout = new TimeSpan(0, 0, (int)prp_HttpTimeout);
            }

            System = new Services.SystemService(this);

        }
        #endregion

        #region PROPERTIES


        /// <summary>
        /// Prihlasovací token pre komunikáciu s API. Používa sa len vtedy, ak server pre autentifikáciu používa daný typ prihlasovania a overovania.
        /// Ak si ho client po Login-e nastaví, bude automaticky použitý pri daných volaniach.
        /// (nastavuje Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.JWTToken));
        /// </summary>      
        public string JWTToken { get; set; }


        private uint prp_HttpTimeout;
        /// <summary>
        /// Maximálny timeout http volania a čakania na odpoveď (v sekundách). 
        /// Pri každom volaní http requestu (GET, POST, PUT, DEL) sa nastavuje daný timout volania samostatne (musí byť totožný alebo menší ako maximálny timeout). 
        /// </summary>
        public uint HttpTimeout
        {
            get
            {
                return prp_HttpTimeout;
            }
        }

        /// <summary>
        /// Obsahuje systémové funkcie bez prihlásenia (ping, version, atď.).
        /// </summary>
        public Exa.OBERON.ServicesGen2.Client.Services.SystemService System;

        #endregion

        #region PRIVATE METHODS

        public virtual bool ValidateCertificate(HttpRequestMessage requestMessage,
                                         X509Certificate2 certificate,
                                         X509Chain chain,
                                         SslPolicyErrors sslPolicyErrors)
        {
            // Check if there are SSL policy errors
            if (sslPolicyErrors != SslPolicyErrors.None)
            {

                // If untrusted certificate is acceptable, return true
                // Otherwise, return false to reject the connection
                // Example: You may add specific conditions to accept certain certificates
                // For testing purposes or when connecting to a known server with a self-signed certificate, you might return true
                // In production, carefully consider the security implications before accepting untrusted certificates
                return true; // Accept untrusted certificates (for demonstration purposes)
            }

            // If no SSL policy errors, certificate is considered valid
            return true;
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Nastavenie URL adresy pre komunikáciu s API.
        /// </summary>
        /// <param name="u_BaseAddress">URL adresa server - vytvára sa objekt Uri, nastavuje sa BaseAddress v objekte HttpClient.</param>
        public EXC SetBaseAddress(string u_BaseAddress)
        {
            EXC myEx = EXC.GetDefault();

            try
            {
                if (string.IsNullOrEmpty(u_BaseAddress) == true)
                {

                    myEx = EXC.Get("URL adresa nie je zadaná.", u_ErrNumber: Convert.ToInt32(Exa.OBERON.ServicesGen2.Client.Exceptions.enm_ErrNumbers.InputValueIsEmpty));
                    return myEx;
                }

                HttpClient.BaseAddress = new Uri(u_BaseAddress);

                myEx.Result = true;

            }
            catch (Exception ex)
            {
                myEx = EXC.Get($"SetBaseAddress error: {ex.Message}", ex);
            }

            return myEx;
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
                    try
                    {
                        if (HttpClient != null)
                        {
                            HttpClient.Dispose();
                            HttpClient = null;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

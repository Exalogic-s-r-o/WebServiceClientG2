using Exa.OBERON.ServicesGen2.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EXC = Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException;

namespace Exa.OBERON.ServicesGen2.Client.Services
{
    public class SystemService : BaseService
    {

        #region CONSTANTS

        private const string CONST_SYSTEM_PING = "/ping";
        private const string CONST_SYSTEM_VERSION = "/version";

        #endregion

        #region CONSTRUCTOR

        public SystemService(Exa.OBERON.ServicesGen2.Client.WebServiceClient webServiceClient) : base(webServiceClient)
        {

        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Informácie o verzii webovej služby.
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel<Models.System.Version>> Version()
        {

            ResultModel<Models.System.Version> result = new ResultModel<Models.System.Version>();

            try
            {
                var versionResult = await this.GetAsync<Models.System.Version>(u_Description: "Version",
                                                                               u_ApiPath: CONST_SYSTEM_VERSION);
                if (versionResult == null)
                {
                    result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_VERSION}'."));
                    return result;
                }

                result.result = true;
                result.data = versionResult;
                
            }
            catch (System.TimeoutException)
            {
                // Timeout - služba nie je dostupná.
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_PING}'. 'TIMEOUT' "));
            }
            catch (Exception ex)
            {
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_VERSION}'. '{ex.Message}' "));
            }
            return result;
        }

        /// <summary>
        /// Ping - test pripojenia na server.
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel<string>> Ping()
        {
            ResultModel<string> result = new ResultModel<string>();

            try
            {
                string pingresult = await this.WebServiceClient.HttpClient.GetStringAsync(CONST_SYSTEM_PING);

                if (pingresult == string.Empty)
                {
                    result.FromExaException(EXC.Get($"Odpoveď je prázdna '{CONST_SYSTEM_PING}'."));
                    return result;
                }

                result.data = pingresult;
                result.result = true;
            }
            catch (System.TimeoutException)
            {
                // Timeout - služba nie je dostupná.
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_PING}'. 'TIMEOUT' "));
            }
            catch (Exception ex)
            {
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_PING}'. '{ex.Message}' "));
            }
            return result;
        }

        #endregion
    }
}

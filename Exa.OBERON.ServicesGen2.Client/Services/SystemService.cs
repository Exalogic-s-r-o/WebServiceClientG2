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
            ResultModel<string> resultPing = new ResultModel<string>();

            try
            {
                resultPing = await this.WebServiceClient.System.GetAsync(u_Description: CONST_SYSTEM_PING, u_ApiPath: CONST_SYSTEM_PING, u_TimeOut: 3);

                if (resultPing == null)
                {
                    resultPing.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_PING}'."));
                    return resultPing;
                }
                                
            }
            catch (System.TimeoutException)
            {
                // Timeout - služba nie je dostupná.
                resultPing.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_PING}'. 'TIMEOUT' "));
            }
            catch (System.Net.Http.HttpRequestException)
            {
                // Timeout - služba nie je dostupná.
                resultPing.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_PING}'. 'TIMEOUT' "));
            }
            catch (Exception ex)
            {
                resultPing.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_SYSTEM_PING}'. '{ex.Message}' "));
            }
            return resultPing;
        }

        #endregion
    }
}

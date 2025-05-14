using Exa.OBERON.ServicesGen2.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EXC = Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException;

namespace Exa.OBERON.ServicesGen2.Client.Services
{
    public class BusinessPartnerService : BaseService
    {
        #region CONSTANTS

        private const string CONST_BUSINESSPARTNER_FIND = "/v1/business-partner/find";
        private const string CONST_BUSINESSPARTNER_ADD = "/v1/business-partner/add";


        #endregion

        #region CONSTRUCTOR

        public BusinessPartnerService(Exa.OBERON.ServicesGen2.Client.WebServiceClient webServiceClient) : base(webServiceClient)
        {

        }

        #endregion

        #region METHODS

        /// <summary>
        /// Vráti zoznam obchodných partnerov na základe zadaných parametrov.
        /// </summary>
        /// <param name="businessPartnerFindArg"></param>
        /// <returns></returns>
        public async Task<ResultModel<Models.BusinessPartner.BusinessPartnerFindData>> 
            BusinessPartner_Find(Models.BusinessPartner.BusinessPartnerFindArg businessPartnerFindArg)

        {

            ResultModel<Models.BusinessPartner.BusinessPartnerFindData> result = null;

            try
            {

                // Volanie.              
                result = await this.PostAsync<Models.BusinessPartner.BusinessPartnerFindData>(u_Description: "BusinessPartner_Find",
                                                                                              u_ApiPath: CONST_BUSINESSPARTNER_FIND,
                                                                                              u_Request: businessPartnerFindArg,
                                                                                              u_RequestRootName: "businessPartnerFindArg");
                if (result == null)
                {
                    result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_BUSINESSPARTNER_FIND}'."));
                    return result;
                }
                if (result.result == false)
                {
                    result.FromExaException(EXC.Get(result.description,
                                            u_ErrNumber: result.errNumber));
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_BUSINESSPARTNER_FIND}'. '{ex.Message}'"));
            }

            return result;
        }

        #endregion
    }
}

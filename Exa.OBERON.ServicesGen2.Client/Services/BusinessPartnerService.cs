using System;
using System.Collections.Generic;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Services
{
    public class BusinessPartnerService : BaseService
    {
        #region CONSTANTS

        private const string CONST_BUSINESSPARTNER_GET = "/v1/business-partner/get";
        private const string CONST_BUSINESSPARTNER_ADD = "/v1/business-partner/add";


        #endregion

        #region CONSTRUCTOR

        public BusinessPartnerService(Exa.OBERON.ServicesGen2.Client.WebServiceClient webServiceClient) : base(webServiceClient)
        {

        }

        #endregion
    }
}

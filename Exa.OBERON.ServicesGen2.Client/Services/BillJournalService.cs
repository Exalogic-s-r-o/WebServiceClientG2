using Exa.OBERON.ServicesGen2.Client.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EXC = Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException;

namespace Exa.OBERON.ServicesGen2.Client.Services
{
    /// <summary>
    /// Obsahuje metódy pre prácu s pokladničnými dokladmi, t.j. dokladmy, ktoré vznikajú používaním modulu Pokladnica OBERON.
    /// </summary>
    public class BillJournalService : BaseService
    {

        #region CONSTANTS

        private const string CONST_CashRegister_BillJournal_Summary_Items = "/v1/cashregister/bill-journal/summary/items";
    
        #endregion

        #region CONSTRUCTOR

        public BillJournalService(Exa.OBERON.ServicesGen2.Client.WebServiceClient webServiceClient) : base(webServiceClient)
        {

        }

        #endregion

        #region METHODS


        /// <summary>
        /// Vráti súhrn predaja - zosumarizované položky predaných na Pokladnici OBERON.
        /// </summary>
        public async Task<ResultModel<Models.BillJournal.BillJournalSummaryItemsData>> CashRegister_BillJournal_Summary_Items(Models.BillJournal.BillJournalSummaryItemsArg billJournalSummaryItemsArg)
     
        {

            ResultModel<Models.BillJournal.BillJournalSummaryItemsData> result = null;

            try
            {
               
                // Volanie login.               
                result = await this.PostAsync<Models.BillJournal.BillJournalSummaryItemsData>(u_Description: "CashRegister_BillJournal_Summary_Items",
                                                                              u_ApiPath: CONST_CashRegister_BillJournal_Summary_Items,
                                                                              u_Request: billJournalSummaryItemsArg,
                                                                              u_RequestRootName: "billJournalSummaryItemsArg");
                if (result == null)
                {
                    result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_CashRegister_BillJournal_Summary_Items}'."));
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
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_CashRegister_BillJournal_Summary_Items}'. '{ex.Message}'"));
            }

            return result;
        }
      
        #endregion
    }
}

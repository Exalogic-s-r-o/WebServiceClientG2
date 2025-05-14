using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.UI.ViewModels
{
    public partial class BusinessPartnerViewModel : BaseViewModel
    {

        public BusinessPartnerViewModel(Base.AppEngine appEngine,
                                        IPopupService popupService) : base(appEngine, popupService)
        {

        }

        #region PROPERTIES

        private string prp_BusinessPartnerFindValue = string.Empty;
        public string BusinessPartnerFindValue
        {
            get { return prp_BusinessPartnerFindValue; }
            set
            {
                prp_BusinessPartnerFindValue = value;
                OnPropertyChanged("BusinessPartnerFindValue");
            }
        }

        private string prp_BusinessPartnerFindMethod = string.Empty;
        public string BusinessPartnerFindMethod
        {
            get { return prp_BusinessPartnerFindMethod; }
            set
            {
                prp_BusinessPartnerFindMethod = value;
                OnPropertyChanged("BusinessPartnerFindMethod");
            }
        }

        #endregion

        #region METHODS

        [RelayCommand]
        private async Task BusinessPartner_Find()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner.BusinessPartnerFindArg businessPartnerFindArg =
                    new Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner.BusinessPartnerFindArg();

                if (string.IsNullOrEmpty(BusinessPartnerFindMethod))
                {
                    myEx = EXC.Get("Nebol zadaný typ vyhľadávania.");
                    await ShowPopup(myEx);
                    return;
                }

                if (string.IsNullOrEmpty(BusinessPartnerFindValue))
                {
                    myEx = EXC.Get("Nebol zadaný vyhľadávací reťazec.");
                    await ShowPopup(myEx);
                    return;
                }

                switch (BusinessPartnerFindMethod)
                {
                    case "GUID":
                        businessPartnerFindArg.FindMethod = 0;
                        break;
                    case "IČO":
                        businessPartnerFindArg.FindMethod = 1;
                        break;
                    case "Čiarový kód":
                        businessPartnerFindArg.FindMethod = 2;
                        break;
                    case "Názov obsahuje":
                        businessPartnerFindArg.FindMethod = 8;
                        break;
                    case "Názov":
                        businessPartnerFindArg.FindMethod = 9;
                        break;
                    default:
                        businessPartnerFindArg.FindMethod = 8;
                        break;
                }

                businessPartnerFindArg.FindValue = BusinessPartnerFindValue;
                businessPartnerFindArg.GetBranches = false;
                businessPartnerFindArg.GetFileRepository = false;
                businessPartnerFindArg.GetFileRepositoryBinaryDataMode = 0;
                businessPartnerFindArg.GetFileRepositoryBinaryDataFilterIDNums = null;

                var result = await this._AppEngine.WebServiceClient.BusinessPartner.BusinessPartner_Find(businessPartnerFindArg);
                if (result.result == false)
                {
                    myEx = EXC.Get($"Chyba pri volaní 'BusinessPartner_Find'. '{result.description}'");
                    await ShowPopup(myEx);
                    return;
                }



                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"BusinessPartner_Find:");
                sb.AppendLine();

                if (result.data.Items.Count == 0)
                {
                    sb.AppendLine($"Podľa hodnoty '{BusinessPartnerFindValue}' sa nenašli sa žiadne záznamy.");
                }

                foreach (BusinessPartner e_businessPartner in result.data.Items ?? Enumerable.Empty<BusinessPartner>())
                {
                    sb.AppendLine($"{e_businessPartner.Name} IČO: {e_businessPartner.IdentificationNumber} Mesto: {e_businessPartner.Address.City}");
                }

                WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"{sb}"));
            }
            catch(Exception ex)
            {
                await ShowPopup(EXC.Get(ex.Message));
            }
            finally
            {
                this.IsRunning = false;
            }

        }
        #endregion

    }
}

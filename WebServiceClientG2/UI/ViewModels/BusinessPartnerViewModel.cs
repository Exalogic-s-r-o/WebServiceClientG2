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

        #endregion

        #region METHODS

        [RelayCommand]
        private async Task BusinessPartner_Find()
        {
            EXC myEx = EXC.GetDefault();

            try
            {
                Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner.BusinessPartnerFindArg businessPartnerFindArg =
                    new Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner.BusinessPartnerFindArg();

                //if (string.IsNullOrEmpty(valueType))
                //{
                //    myEx = EXC.Get("Nebol zadaný typ vyhľadávania.");
                //    await ShowPopup(myEx);
                //    return;
                //}

                if (string.IsNullOrEmpty(BusinessPartnerFindValue))
                {
                    myEx = EXC.Get("Nebol zadaný vyhľadávací reťazec.");
                    await ShowPopup(myEx);
                    return;
                }


                businessPartnerFindArg.FindMethod = 8;
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

                if (result.data == null)
                {
                    WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"Podľa hodnoty '{BusinessPartnerFindValue}' sa nenašli sa žiadne záznamy."));
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"BusinessPartner_Find:");
                sb.AppendLine();

                foreach (BusinessPartner e_businessPartner in result.data.Items)
                {
                    sb.AppendLine($"{e_businessPartner.Name}");
                }

                WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"{sb}"));
            }
            catch
            {

                throw;
            }

        }
        #endregion

    }
}

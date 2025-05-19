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

        private string prp_BusinessPartnerAddGUID = string.Empty;
        public string BusinessPartnerAddGUID
        {
            get { return prp_BusinessPartnerAddGUID; }
            set
            {
                prp_BusinessPartnerAddGUID = value;
                OnPropertyChanged("BusinessPartnerAddGUID");
            }
        }

        private string prp_BusinessPartnerAddName = "Rudo";
        public string BusinessPartnerAddName
        {
            get { return prp_BusinessPartnerAddName; }
            set
            {
                prp_BusinessPartnerAddName = value;
                OnPropertyChanged("BusinessPartnerAddName");
            }
        }

        private string prp_BusinessPartnerAddICO = "123456789";
        public string BusinessPartnerAddICO
        {
            get { return prp_BusinessPartnerAddICO; }
            set
            {
                prp_BusinessPartnerAddICO = value;
                OnPropertyChanged("BusinessPartnerAddICO");
            }
        }

        private string prp_BusinessPartnerAddCity = "Buzica";
        public string BusinessPartnerAddCity
        {
            get { return prp_BusinessPartnerAddCity; }
            set
            {
                prp_BusinessPartnerAddCity = value;
                OnPropertyChanged("BusinessPartnerAddCity");
            }
        }

        private string prp_BusinessPartnerAddStreet = "Pečeňehova";
        public string BusinessPartnerAddStreet
        {
            get { return prp_BusinessPartnerAddStreet; }
            set
            {
                prp_BusinessPartnerAddStreet = value;
                OnPropertyChanged("BusinessPartnerAddStreet");
            }
        }

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

        /// <summary>
        /// Vyhľadanie obchodného partnera.
        /// </summary>
        /// <returns></returns>
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
                        businessPartnerFindArg.FindMethod = 20;
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
                sb.AppendLine($"BusinessPartnerFind:");
                sb.AppendLine();

                if (result.data.Items.Count == 0)
                {
                    sb.AppendLine($"Podľa hodnoty '{BusinessPartnerFindValue}' sa nenašli sa žiadne záznamy.");
                }

                foreach (BusinessPartner e_businessPartner in result.data.Items ?? Enumerable.Empty<BusinessPartner>())
                {
                    sb.AppendLine($"GUID:'{e_businessPartner.RecordGuid}' {e_businessPartner.Name} IČO: {e_businessPartner.IdentificationNumber} Mesto: {e_businessPartner.Address.City}");
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

        /// <summary>
        /// Pridanie obchodného partnera.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task BusinessPartner_Add()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner.BusinessPartnerAddArg businessPartnerAddArg =
                    new Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner.BusinessPartnerAddArg();

                if (string.IsNullOrEmpty(this.BusinessPartnerAddGUID))
                {
                    myEx = EXC.Get("Nebol zadaný jednoznačný identifikátor obchodného partnera.");
                    await ShowPopup(myEx);
                    return;
                }

                if (string.IsNullOrEmpty(this.BusinessPartnerAddName))
                {
                    myEx = EXC.Get("Nebol zadaný názov obchodného partnera.");
                    await ShowPopup(myEx);
                    return;
                }

                if (string.IsNullOrEmpty(this.BusinessPartnerAddCity))
                {
                    myEx = EXC.Get("Nebolo zadané mesto obchodného partnera.");
                    await ShowPopup(myEx);
                    return;
                }

                if (string.IsNullOrEmpty(this.BusinessPartnerAddStreet))
                {
                    myEx = EXC.Get("Nebola zadaná ulica obchodného partnera.");
                    await ShowPopup(myEx);
                    return;
                }

                businessPartnerAddArg.BusinessPartner.RecordGuid = this.BusinessPartnerAddGUID;
                businessPartnerAddArg.BusinessPartner.Name = this.BusinessPartnerAddName;
                businessPartnerAddArg.BusinessPartner.IdentificationNumber = this.BusinessPartnerAddICO;
                businessPartnerAddArg.BusinessPartner.Address.Street = this.BusinessPartnerAddStreet;
                businessPartnerAddArg.BusinessPartner.Address.City = this.BusinessPartnerAddCity;

                var result = await this._AppEngine.WebServiceClient.BusinessPartner.BusinessPartner_Add(businessPartnerAddArg);
                if (result.result == false)
                {
                    WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"Chyba pri volaní 'BusinessPartner_Add'. '{result.description}'"));
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"BusinessPartnerAdd:");
                sb.AppendLine();
                sb.AppendLine($"{result.data.GUID}");
                sb.AppendLine($"{result.data.Info}");
                sb.AppendLine($"{result.data.Value}");

                WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"{sb}"));
            }
            catch (Exception ex)
            {
                await ShowPopup(EXC.Get(ex.Message));
            }
            finally
            {
                this.IsRunning = false;
            }

        }

        /// <summary>
        /// Generovanie GUID.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task GUIDGenerate()
        {
            if (this.IsRunning == true)
            {
                return;
            }
            try
            {
                this.IsRunning = true;

                this.BusinessPartnerAddGUID = Guid.NewGuid().ToString();
            }
            catch (Exception ex)
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

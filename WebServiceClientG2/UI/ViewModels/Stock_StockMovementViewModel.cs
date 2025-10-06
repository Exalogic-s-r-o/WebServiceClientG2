using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Exa.OBERON.ServicesGen2.Client.Models.Common.BookSettings;
using Exa.OBERON.ServicesGen2.Client.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.UI.ViewModels
{
    public partial class Stock_StockMovementViewModel : BaseViewModel
    {

        public Stock_StockMovementViewModel(Base.AppEngine appEngine,
                                        IPopupService popupService) : base(appEngine, popupService)
        {

        }


        #region PROPERTIES

        private string prp_StockName = "Hlavný sklad";
        /// <summary>
        /// Názov skladu.
        /// </summary>
        public string StockName
        {
            get => this.prp_StockName;

            set
            {
                if (this.prp_StockName != value)
                {
                    this.prp_StockName = value;
                    OnPropertyChanged(nameof(StockName));
                }
            }
        }


        private string prp_StockMovementList_PageIndex = "0";
        /// <summary>
        /// Identifikátor aktuálnej strany pri stránkovaní zoznamu skladových pohybov.
        /// </summary>
        public string StockMovementList_PageIndex
        {
            get => this.prp_StockMovementList_PageIndex;

            set
            {
                if (this.prp_StockMovementList_PageIndex != value)
                {
                    this.prp_StockMovementList_PageIndex = value;
                    OnPropertyChanged(nameof(StockMovementList_PageIndex));
                }
            }
        }

        private string prp_StockMovementList_PageSize = "20";
        /// <summary>
        /// Veľkosť strany pri stránkovaní zoznamu skladových pohybov.
        /// </summary>
        public string StockMovementList_PageSize
        {
            get => this.prp_StockMovementList_PageSize;

            set
            {
                if (this.prp_StockMovementList_PageSize != value)
                {
                    this.prp_StockMovementList_PageSize = value;
                    OnPropertyChanged(nameof(StockMovementList_PageSize));
                }
            }
        }

        private DateTime prp_Date_From = DateTime.Now.Date.AddDays(-30) ;
        /// <summary>
        /// Dátum od - filter pre načítanie skladových pohybov. 
        /// </summary>
        public DateTime Date_From
        {
            get => this.prp_Date_From;

            set
            {
                if (this.prp_Date_From != value)
                {
                    this.prp_Date_From = value;
                    OnPropertyChanged(nameof(Date_From));
                }
            }
        }

        private DateTime prp_Date_To = DateTime.Now.Date;
        /// <summary>
        /// Dátum do - filter pre načítanie skladových pohybov. 
        /// </summary>
        public DateTime Date_To
        {
            get => this.prp_Date_To;

            set
            {
                if (this.prp_Date_To != value)
                {
                    this.prp_Date_To = value;
                    OnPropertyChanged(nameof(Date_To));
                }
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Načítanie zoznamu skladových pohybov.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task StockMovementsLoad()
        {
            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                Exa.OBERON.ServicesGen2.Client.Models.Stock.StockMovements.StockMovementListArg arg
                    = new Exa.OBERON.ServicesGen2.Client.Models.Stock.StockMovements.StockMovementListArg();

                arg.LoadSettings = new Exa.OBERON.ServicesGen2.Client.Models.Common.BookSettings.LoadSettingsArg()
                {
                    PageSize = Convert.ToInt32(this.StockMovementList_PageSize),
                    PageIndex = Convert.ToInt32(this.StockMovementList_PageIndex),
                    Filters = new List<Exa.OBERON.ServicesGen2.Client.Models.Common.BookSettings.LoadFilterItem>()
                };

                // Filter - dátum pohybu    
                Exa.OBERON.ServicesGen2.Client.Models.Common.BookSettings.LoadFilterItem FilterItem_DateMovement
                    = new Exa.OBERON.ServicesGen2.Client.Models.Common.BookSettings.LoadFilterItem();
                FilterItem_DateMovement.BookColumnID = (int)Exa.OBERON.ServicesGen2.Client.Models.Stock.StockMovements.StockMovement.enm_BookColumns.DateMovement;
                FilterItem_DateMovement.ConditionType = (int)BookColumn.enm_ConditionTypes.InRange;                
                FilterItem_DateMovement.Values = new List<string>([Exa.OBERON.ServicesGen2.Client.JsonConvert.DateToJsonString(this.Date_From), 
                                                                   Exa.OBERON.ServicesGen2.Client.JsonConvert.DateToJsonString(this.Date_To)]);
                arg.LoadSettings.Filters.Add(FilterItem_DateMovement);
                
                // Názav skladu
                arg.StockName = this.StockName;

                var StockMovementList = await this._AppEngine.WebServiceClient.Stock.StockMovements_List(arg);
                if (StockMovementList.result == false)
                {
                    // Chyba
                    WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"Chyba pri volaní metódy 'StockMovements_List'. '{StockMovementList.description}'."));
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"StockMovements_List");
                sb.AppendLine($"IsLastPage: {StockMovementList.data.IsLastPage.ToString()}");
                sb.AppendLine($"PageIndex: {StockMovementList.data.PageIndex.ToString()}");

                if (StockMovementList.data.Items == null || StockMovementList.data.Items.Count == 0)
                {
                    sb.AppendLine($"No items.");
                }
                else
                {
                    foreach (var item in StockMovementList.data.Items)
                    {
                        sb.AppendLine($"{item.DateMovement}  [{item.StockMovementType}]  {item.StockCardName} - {item.Amount} {item.Unit}, PriceWithVAT: {item.PriceWithVAT}  {item.BusinessPartner.Name}  ");
                    }
                }

                WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"{sb}"));
            }
            catch
            {
            }
            finally
            {
                this.IsRunning = false;
            }
        }

        #endregion

    }
}

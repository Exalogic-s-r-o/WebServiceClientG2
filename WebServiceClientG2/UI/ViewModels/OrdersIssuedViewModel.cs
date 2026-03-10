using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System;
using Exa.OBERON.ServicesGen2.Client.Models.OrdersIssued;
using Exa.OBERON.ServicesGen2.Client.Models;
using System.Text;
using CommunityToolkit.Mvvm.Messaging;

namespace WebServiceClientG2.UI.ViewModels
{
    public partial class OrdersIssuedViewModel : BaseViewModel
    {
        public OrdersIssuedViewModel(Base.AppEngine appEngine,
                                       IPopupService popupService) : base(appEngine, popupService)
        {

          //  this.prp_OrderIssuedItems = new System.Collections.ObjectModel.ObservableCollection<Exa.OBERON.ServicesGen2.Client.Models.OrdersIssued.OrderIssuedItem>();
        }

        #region PROPERTIES

        private string prp_OrderIssuedAddGUID = string.Empty;
        public string OrderIssuedAddGUID
        {
            get { return prp_OrderIssuedAddGUID; }
            set { prp_OrderIssuedAddGUID = value; OnPropertyChanged("OrderIssuedAddGUID"); }
        }

        private string prp_OrderIssuedNumber = string.Empty;
        public string OrderIssuedNumber
        {
            get { return prp_OrderIssuedNumber; }
            set { prp_OrderIssuedNumber = value; OnPropertyChanged("OrderIssuedNumber"); }
        }

        private string prp_OrderIssuedDateDelivery = string.Empty;
        public string OrderIssuedDateDelivery
        {
            get { return prp_OrderIssuedDateDelivery; }
            set { prp_OrderIssuedDateDelivery = value; OnPropertyChanged("OrderIssuedDateDelivery"); }
        }

        private DateTime prp_OrderIssuedDateDeliveryDate = DateTime.Now;
        public DateTime OrderIssuedDateDeliveryDate
        {
            get { return prp_OrderIssuedDateDeliveryDate; }
            set { prp_OrderIssuedDateDeliveryDate = value; OnPropertyChanged("OrderIssuedDateDeliveryDate"); }
        }

        private TimeSpan prp_OrderIssuedDateDeliveryTime = DateTime.Now.TimeOfDay;
        public TimeSpan OrderIssuedDateDeliveryTime
        {
            get { return prp_OrderIssuedDateDeliveryTime; }
            set { prp_OrderIssuedDateDeliveryTime = value; OnPropertyChanged("OrderIssuedDateDeliveryTime"); }
        }

        private DateTime prp_OrderIssuedDateReservationDate = DateTime.Now;
        public DateTime OrderIssuedDateReservationDate
        {
            get { return prp_OrderIssuedDateReservationDate; }
            set { prp_OrderIssuedDateReservationDate = value; OnPropertyChanged("OrderIssuedDateReservationDate"); }
        }

        private string prp_OrderIssuedBusinessPartnerName = string.Empty;
        public string OrderIssuedBusinessPartnerName
        {
            get { return prp_OrderIssuedBusinessPartnerName; }
            set { prp_OrderIssuedBusinessPartnerName = value; OnPropertyChanged("OrderIssuedBusinessPartnerName"); }
        }

        private string prp_OrderIssuedNotice = string.Empty;
        public string OrderIssuedNotice
        {
            get { return prp_OrderIssuedNotice; }
            set { prp_OrderIssuedNotice = value; OnPropertyChanged("OrderIssuedNotice"); }
        }

        //// Items for the new order
        //private System.Collections.ObjectModel.ObservableCollection<Exa.OBERON.ServicesGen2.Client.Models.OrdersIssued.OrderIssuedItem> prp_OrderIssuedItems;
        //public System.Collections.ObjectModel.ObservableCollection<Exa.OBERON.ServicesGen2.Client.Models.OrdersIssued.OrderIssuedItem> OrderIssuedItems
        //{
        //    get { return prp_OrderIssuedItems; }
        //    set { prp_OrderIssuedItems = value; OnPropertyChanged("OrderIssuedItems"); }
        //}

        private string prp_NewItemName = string.Empty;
        public string NewItemName
        {
            get { return prp_NewItemName; }
            set { prp_NewItemName = value; OnPropertyChanged("NewItemName"); }
        }

        private string prp_NewItemAmount = string.Empty;
        public string NewItemAmount
        {
            get { return prp_NewItemAmount; }
            set { prp_NewItemAmount = value; OnPropertyChanged("NewItemAmount"); }
        }

        private string prp_NewItemUnit = string.Empty;
        public string NewItemUnit
        {
            get { return prp_NewItemUnit; }
            set { prp_NewItemUnit = value; OnPropertyChanged("NewItemUnit"); }
        }

        private string prp_NewItemPrice = string.Empty;
        public string NewItemPrice
        {
            get { return prp_NewItemPrice; }
            set { prp_NewItemPrice = value; OnPropertyChanged("NewItemPrice"); }
        }

        private string prp_NewItemReserved = string.Empty;
        public string NewItemReserved
        {
            get { return prp_NewItemReserved; }
            set { prp_NewItemReserved = value; OnPropertyChanged("NewItemReserved"); }
        }


        private string prp_OrderIssuedSummaryItemsBranchName = string.Empty;
        public string OrderIssuedSummaryItemsBranchName
        {
            get { return prp_OrderIssuedSummaryItemsBranchName; }
            set { prp_OrderIssuedSummaryItemsBranchName = value; OnPropertyChanged("OrderIssuedSummaryItemsBranchName"); }
        }

        private bool  prp_OrderIssuedSummaryItemsHasVariants = false;
        public bool OrderIssuedSummaryItemsHasVariants
        {
            get { return prp_OrderIssuedSummaryItemsHasVariants; }
            set { prp_OrderIssuedSummaryItemsHasVariants = value; OnPropertyChanged("OrderIssuedSummaryItemsHasVariants"); }
        }

        #endregion

        #region METHODS

        [RelayCommand]
        private async Task GUIDGenerate()
        {
            if (this.IsRunning == true) return;

            try
            {
                this.IsRunning = true;
                this.OrderIssuedAddGUID = Guid.NewGuid().ToString();
            }
            catch
            {
            }
            finally
            {
                this.IsRunning = false;
            }
        }


        [RelayCommand]
        private async Task OrderIssued_SummaryItems()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true) return;

            try
            {
                this.IsRunning = true;

                // Argument volania
                OrderIssuedSummaryItemsArg arg = new OrderIssuedSummaryItemsArg();              
                if (string.IsNullOrEmpty(this.OrderIssuedSummaryItemsBranchName) == false)
                {
                    arg.BranchNames = new List<string>();
                    arg.BranchNames.Add(this.OrderIssuedSummaryItemsBranchName);
                }
                arg.SummaryByVariant  = this.OrderIssuedSummaryItemsHasVariants;

                var result = await this._AppEngine.WebServiceClient.Stock.Stock_OrderIssued_Summary_Items(arg);
                if (result.result == false)
                {
                    myEx = EXC.Get($"Chyba pri volaní 'OrderIssued_SummaryItems'. '{result.description}'");
                    await ShowPopup(myEx);
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(new string(Convert.ToChar("-"), 255));
                sb.AppendLine($"OrderIssued_SummaryItems");
                sb.AppendLine(new string( Convert.ToChar("-"), 255));
                if (result.data.Items == null || result.data.Items.Count == 0)
                {
                    sb.AppendLine($"No items.");
                }
                else
                {
                    sb.AppendLine($"Items count: {result.data.Items.Count - 1}");

                    foreach (var item in result.data.Items)
                    {
                        sb.AppendLine($"{item.Number} - {item.Name } -> AmountOrder {item.AmountOrder} {item.Unit}, AmountReserved: {item.AmountReserved }  {item.Unit},   [variant: {item.VariantName}]");
                    }
                }

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


        #endregion

        [RelayCommand]
        private async Task Load()
        {
            if (this.IsRunning) return;

            try
            {
                this.IsRunning = true;
                // Placeholder for future Orders Issued calls to web service
            }
            finally
            {
                this.IsRunning = false;
            }
        }
    }
}

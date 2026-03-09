using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System;
using Exa.OBERON.ServicesGen2.Client.Models.OrdersReceived;
using Exa.OBERON.ServicesGen2.Client.Models;
using System.Text;
using CommunityToolkit.Mvvm.Messaging;

namespace WebServiceClientG2.UI.ViewModels
{
    public partial class OrdersReceivedViewModel : BaseViewModel
    {
        public OrdersReceivedViewModel(Base.AppEngine appEngine,
                                       IPopupService popupService) : base(appEngine, popupService)
        {

            this.prp_OrderReceivedItems = new System.Collections.ObjectModel.ObservableCollection<Exa.OBERON.ServicesGen2.Client.Models.OrdersReceived.OrderReceivedItem>();
        }

        #region PROPERTIES

        private string prp_OrderReceivedAddGUID = string.Empty;
        public string OrderReceivedAddGUID
        {
            get { return prp_OrderReceivedAddGUID; }
            set { prp_OrderReceivedAddGUID = value; OnPropertyChanged("OrderReceivedAddGUID"); }
        }

        private string prp_OrderReceivedNumber = string.Empty;
        public string OrderReceivedNumber
        {
            get { return prp_OrderReceivedNumber; }
            set { prp_OrderReceivedNumber = value; OnPropertyChanged("OrderReceivedNumber"); }
        }

        private string prp_OrderReceivedDateDelivery = string.Empty;
        public string OrderReceivedDateDelivery
        {
            get { return prp_OrderReceivedDateDelivery; }
            set { prp_OrderReceivedDateDelivery = value; OnPropertyChanged("OrderReceivedDateDelivery"); }
        }

        private DateTime prp_OrderReceivedDateDeliveryDate = DateTime.Now;
        public DateTime OrderReceivedDateDeliveryDate
        {
            get { return prp_OrderReceivedDateDeliveryDate; }
            set { prp_OrderReceivedDateDeliveryDate = value; OnPropertyChanged("OrderReceivedDateDeliveryDate"); }
        }

        private TimeSpan prp_OrderReceivedDateDeliveryTime = DateTime.Now.TimeOfDay;
        public TimeSpan OrderReceivedDateDeliveryTime
        {
            get { return prp_OrderReceivedDateDeliveryTime; }
            set { prp_OrderReceivedDateDeliveryTime = value; OnPropertyChanged("OrderReceivedDateDeliveryTime"); }
        }

        private DateTime prp_OrderReceivedDateReservationDate = DateTime.Now;
        public DateTime OrderReceivedDateReservationDate
        {
            get { return prp_OrderReceivedDateReservationDate; }
            set { prp_OrderReceivedDateReservationDate = value; OnPropertyChanged("OrderReceivedDateReservationDate"); }
        }

        private string prp_OrderReceivedBusinessPartnerName = string.Empty;
        public string OrderReceivedBusinessPartnerName
        {
            get { return prp_OrderReceivedBusinessPartnerName; }
            set { prp_OrderReceivedBusinessPartnerName = value; OnPropertyChanged("OrderReceivedBusinessPartnerName"); }
        }

        private string prp_OrderReceivedNotice = string.Empty;
        public string OrderReceivedNotice
        {
            get { return prp_OrderReceivedNotice; }
            set { prp_OrderReceivedNotice = value; OnPropertyChanged("OrderReceivedNotice"); }
        }

        // Items for the new order
        private System.Collections.ObjectModel.ObservableCollection<Exa.OBERON.ServicesGen2.Client.Models.OrdersReceived.OrderReceivedItem> prp_OrderReceivedItems;
        public System.Collections.ObjectModel.ObservableCollection<Exa.OBERON.ServicesGen2.Client.Models.OrdersReceived.OrderReceivedItem> OrderReceivedItems
        {
            get { return prp_OrderReceivedItems; }
            set { prp_OrderReceivedItems = value; OnPropertyChanged("OrderReceivedItems"); }
        }

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


        private string prp_OrderReceivedSummaryItemsBranchName = string.Empty;
        public string OrderReceivedSummaryItemsBranchName
        {
            get { return prp_OrderReceivedSummaryItemsBranchName; }
            set { prp_OrderReceivedSummaryItemsBranchName = value; OnPropertyChanged("OrderReceivedSummaryItemsBranchName"); }
        }

        private bool  prp_OrderReceivedSummaryItemsHasVariants = false;
        public bool OrderReceivedSummaryItemsHasVariants
        {
            get { return prp_OrderReceivedSummaryItemsHasVariants; }
            set { prp_OrderReceivedSummaryItemsHasVariants = value; OnPropertyChanged("OrderReceivedSummaryItemsHasVariants"); }
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
                this.OrderReceivedAddGUID = Guid.NewGuid().ToString();
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
        private async Task AddItem()
        {
            try
            {
                decimal amount = 0;
                decimal price = 0;
                decimal.TryParse(this.NewItemAmount, out amount);
                decimal.TryParse(this.NewItemPrice, out price);

                var item = new Exa.OBERON.ServicesGen2.Client.Models.OrdersReceived.OrderReceivedItem()
                {
                    Name = this.NewItemName,
                    Amount = amount,
                    Unit = this.NewItemUnit,
                    PriceWithVATUnit = price
                };

                // parse reserved amount (nullable)
                if (!string.IsNullOrWhiteSpace(this.NewItemReserved))
                {
                    if (decimal.TryParse(this.NewItemReserved, out var reservedVal))
                    {
                        item.AmountReserved = reservedVal;
                    }
                    else
                    {
                        item.AmountReserved = null;
                    }
                }

                this.OrderReceivedItems.Add(item);

                // clear input
                this.NewItemName = string.Empty;
                this.NewItemAmount = string.Empty;
                this.NewItemUnit = string.Empty;
                this.NewItemPrice = string.Empty;
            }
            catch (Exception ex)
            {
                await ShowPopup(EXC.Get(ex.Message));
            }
        }

        [RelayCommand]
        private async Task RemoveItem(Exa.OBERON.ServicesGen2.Client.Models.OrdersReceived.OrderReceivedItem item)
        {
            if (item == null) return;
            try
            {
                this.OrderReceivedItems.Remove(item);
            }
            catch (Exception ex)
            {
                await ShowPopup(EXC.Get(ex.Message));
            }
        }

        [RelayCommand]
        private async Task OrderReceived_Add()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true) return;

            try
            {
                this.IsRunning = true;

                OrderReceivedAddArg arg = new OrderReceivedAddArg();

                arg.OrderReceived.DocumentType = "Objednávka";

                if (string.IsNullOrEmpty(arg.OrderReceived.BusinessPartner.Name) == true)
                { 
                    arg.OrderReceived.BusinessPartner.Name = "EXALOGIC, s.r.o.";
                    //arg.OrderReceived.BusinessPartner.Address = new Exa.OBERON.ServicesGen2.Client.Models.Common.Info.Address();
                    //arg.OrderReceived.BusinessPartner.Address.Street = "Jozefa Jureka 189/3";
                    //arg.OrderReceived.BusinessPartner.Address.City = "Bešenová";
                }

                arg.OrderReceived.BranchName = "Ružomberok";

                arg.OrderReceived.RecordGUID = this.OrderReceivedAddGUID;
                arg.OrderReceived.Number = this.OrderReceivedNumber;
                // combine date and time into delivery datetime
                try
                {
                    var dt = this.OrderReceivedDateDeliveryDate.Date + this.OrderReceivedDateDeliveryTime;
                    //arg.OrderReceived.DateDelivery = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    arg.OrderReceived.DateDelivery = "2026-02-25T13:45:00+01:00";
                }
                catch
                {
                    arg.OrderReceived.DateDelivery = this.OrderReceivedDateDelivery;
                }
                // set reservation date
                //arg.OrderReceived.DateReservation = this.OrderReceivedDateReservationDate.ToString("yyyy-MM-dd");

                arg.OrderReceived.DateReservation = "2026-02-25T13:45:00+01:00";
                arg.OrderReceived.Notice = this.OrderReceivedNotice;
                if (!string.IsNullOrEmpty(this.OrderReceivedBusinessPartnerName))
                {
                    arg.OrderReceived.BusinessPartner.Name = this.OrderReceivedBusinessPartnerName;
                }

                // Add items from UI
                if (this.OrderReceivedItems != null && this.OrderReceivedItems.Count > 0)
                {
                    foreach (var it in this.OrderReceivedItems)
                    {
                        arg.OrderReceived.Items.Add(it);
                    }
                }

                var result = await this._AppEngine.WebServiceClient.Stock.Stock_OrderReceived_Add(arg);
                if (result.result == false)
                {
                    myEx = EXC.Get($"Chyba pri volaní 'Stock_OrderReceived_Add'. '{result.description}'");
                    await ShowPopup(myEx);
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"OrderReceivedAdd:");
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


        [RelayCommand]
        private async Task OrderReceived_SummaryItems()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true) return;

            try
            {
                this.IsRunning = true;

                // Argument volania
                OrderReceivedSummaryItemsArg arg = new OrderReceivedSummaryItemsArg();              
                if (string.IsNullOrEmpty(this.OrderReceivedSummaryItemsBranchName) == false)
                {
                    arg.BranchNames = new List<string>();
                    arg.BranchNames.Add(this.OrderReceivedSummaryItemsBranchName);
                }
                arg.SummaryByVariant  = this.OrderReceivedSummaryItemsHasVariants;

                var result = await this._AppEngine.WebServiceClient.Stock.Stock_OrderReceived_Summary_Items(arg);
                if (result.result == false)
                {
                    myEx = EXC.Get($"Chyba pri volaní 'OrderReceived_SummaryItems'. '{result.description}'");
                    await ShowPopup(myEx);
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(new string(Convert.ToChar("-"), 255));
                sb.AppendLine($"OrderReceived_SummaryItems");
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
                // Placeholder for future Orders received calls to web service
            }
            finally
            {
                this.IsRunning = false;
            }
        }
    }
}

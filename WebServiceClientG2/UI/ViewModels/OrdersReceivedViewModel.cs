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

        private string prp_OrderReceivedBusinessPartnerGUID = string.Empty;
        public string OrderReceivedBusinessPartnerGUID
        {
            get { return prp_OrderReceivedBusinessPartnerGUID; }
            set { prp_OrderReceivedBusinessPartnerGUID = value; OnPropertyChanged("OrderReceivedBusinessPartnerGUID"); }
        }

        private string prp_OrderReceivedNotice = string.Empty;
        public string OrderReceivedNotice
        {
            get { return prp_OrderReceivedNotice; }
            set { prp_OrderReceivedNotice = value; OnPropertyChanged("OrderReceivedNotice"); }
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
        private async Task OrderReceived_Add()
        {
            EXC myEx = EXC.GetDefault();

            if (this.IsRunning == true) return;

            try
            {
                this.IsRunning = true;

                OrderReceivedAddArg arg = new OrderReceivedAddArg();
                arg.OrderReceived.RecordGUID = this.OrderReceivedAddGUID;
                arg.OrderReceived.Number = this.OrderReceivedNumber;
                arg.OrderReceived.DateDelivery = this.OrderReceivedDateDelivery;
                arg.OrderReceived.Notice = this.OrderReceivedNotice;
                if (!string.IsNullOrEmpty(this.OrderReceivedBusinessPartnerGUID))
                {
                    arg.OrderReceived.BusinessPartner.RecordGuid = this.OrderReceivedBusinessPartnerGUID;
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

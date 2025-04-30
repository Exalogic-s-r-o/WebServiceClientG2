using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace WebServiceClientG2.UI.ViewModels
{
    public partial class SystemViewModel : BaseViewModel
    {

        public SystemViewModel(Base.AppEngine appEngine,
                               IPopupService popupService) : base(appEngine, popupService)
        {

        }

        [RelayCommand]
        private async Task Ping()
        {
            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                var resultPing = await this._AppEngine.WebServiceClient.System.Ping();
                if (resultPing == null)
                {
                    // Chyba
                    await ShowPopup(EXC.Get($"Odpoveď neprišla {resultPing}."));
                    return;
                }

                WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"Ping: '{resultPing.data}'"));
            }
            catch
            {
            }
            finally
            {
                this.IsRunning = false;
            }
        }
    }
}

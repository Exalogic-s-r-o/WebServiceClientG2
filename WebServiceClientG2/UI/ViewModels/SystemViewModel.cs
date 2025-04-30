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

                var result = await this._AppEngine.WebServiceClient.System.Ping();
                if (result == null)
                {
                    // Chyba
                    await ShowPopup(EXC.Get($"Odpoveď neprišla {result}."));
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Ping");
                sb.AppendLine($"{result.data}");

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

        [RelayCommand]
        private async Task Version()
        {
            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                var result = await this._AppEngine.WebServiceClient.System.Version();
                if (result.result == false)
                {
                    // Chyba
                    await ShowPopup(EXC.Get($"Chyba pri volaní metódy 'version'. '{result.description}'."));
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Version");
                sb.AppendLine($"OBERONVersion:  '{result.data.OBERONVersion}'");
                sb.AppendLine($"ApiVersion:  '{result.data.ApiVersion}'");
                sb.AppendLine($"OBERONReleaseDate:  '{result.data.OBERONReleaseDate}'");
                sb.AppendLine($"ReleaseDate:  '{result.data.ReleaseDate}'");
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
    }
}

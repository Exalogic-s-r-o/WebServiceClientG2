using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.UI.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private readonly IPopupService popupService;

        public BaseViewModel(IPopupService popupService)
        {
            this.popupService = popupService;
        }

        /// <summary>
        /// Príznak pre activity tracker.
        /// </summary>
        protected bool prp_IsRunning = false;
        public bool IsRunning
        {
            get { return prp_IsRunning; }
            set
            {
                prp_IsRunning = value;
                OnPropertyChanged("IsRunning");
            }
        }

        #region PUBLIC METHODS

        /// <summary>
        /// Krok späť v navigácii.
        /// </summary>
        public virtual async Task GoBack()
        {
            try
            {
                // Štandardný BACK
                await Shell.Current.GoToAsync("..");

            }
            catch
            {

            }
        }

        public async Task ShowPopup(EXC myEx)
        {
            try
            {
                if (myEx.Result == false)
                {
                    // Chyba.
                    await this.popupService.ShowPopupAsync<WebServiceClientG2.UI.Popups.WebServicePopupViewModel>(onPresenting: viewModel =>
                    {
                        viewModel.Title = "ASTON";
                        viewModel.Message = myEx.Message;
                        viewModel.PopupType = 0;
                    });
                }


            }
            catch (Exception ex)
            {

            }
        }

        #endregion

    }
}

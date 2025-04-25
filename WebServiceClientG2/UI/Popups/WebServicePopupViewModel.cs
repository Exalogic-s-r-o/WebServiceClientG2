using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WebServiceClientG2.UI.Popups
{
    public partial class WebServicePopupViewModel : ObservableObject
    {

        readonly IPopupService popupService;

        public WebServicePopupViewModel(IPopupService popupService)
        {
            this.popupService = popupService;
        }

        private string _Title = string.Empty;
        /// <summary>
        /// Nadpis okna.
        /// </summary>
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(this.Title));
            }
        }

        private string _Message = string.Empty;
        /// <summary>
        /// Správa okna.
        /// </summary>
        public string Message
        {
            get => _Message;
            set
            {
                _Message = value;
                OnPropertyChanged(nameof(this.Message));
            }
        }

        private byte _PopupType = 0;
        /// <summary>
        /// Typ popupu, určuje farbu.
        /// </summary>
        public byte PopupType
        {
            get => _PopupType;
            set
            {
                _PopupType = value;
                OnPropertyChanged(nameof(this.PopupType));
            }
        }

        public Color PopupColor
        {
            get
            {
                return PopupType switch
                {
                    0 => Color.FromArgb("#ff4d4d"),
                    1 => Colors.Green,
                    2 => Colors.Blue,
                    _ => Colors.Red,
                };
            }
        }

        [RelayCommand]
        private void ClosePopup()
        {
            popupService.ClosePopup();
        }
    }
}

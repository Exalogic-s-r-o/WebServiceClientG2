using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace WebServiceClientG2.UI.ViewModels
{
    public class SystemViewModel : BaseViewModel
    {

        public SystemViewModel(Base.AppEngine appEngine,
                               IPopupService popupService) : base(appEngine, popupService)
        {

        }
    }
}

using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace WebServiceClientG2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("ExaCommonFont.ttf", "ExaCommonFont");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto_Medium.ttf", "RobotoMedium");
                    fonts.AddFont("Roboto_Regular.ttf", "RobotoRegular");
                });

            builder.Services.AddSingleton<WebServiceClientG2.Base.AppEngine>();

            builder.Services.AddTransientPopup<WebServiceClientG2.UI.Popups.WebServicePopup, WebServiceClientG2.UI.Popups.WebServicePopupViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

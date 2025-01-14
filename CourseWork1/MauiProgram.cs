using CourseWork1.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;


namespace CourseWork1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<IUserService, UserService>(); //dependency injection
            builder.Services.AddSingleton<ITransactions, TransactionsService>();
            builder.Services.AddSingleton<ITags, TagService>();
            builder.Services.AddSingleton<AuthenticationService>();
            builder.Services.AddMudServices(); //adding mudblazor service


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

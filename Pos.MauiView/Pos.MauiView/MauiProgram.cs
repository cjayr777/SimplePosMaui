using Microsoft.Extensions.Logging;
using Pos.MauiView.Service;

namespace Pos.MauiView;

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

        // 1. Determine the correct API Base URL
        // Android Emulator uses 10.0.2.2, Windows/iOS uses localhost
        string baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                         ? "https://10.0.2.2"
                         : "https://localhost:7109/";

#if DEBUG
        // 2. Setup a handler that ignores SSL certificate errors for local development
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

        builder.Services.AddScoped(sp => new HttpClient(handler)
        {
            BaseAddress = new Uri(baseUrl)
        });

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#else
            // 3. Production registration
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://your-production-url.com")
            });
#endif

        // 4. Register your application services
        builder.Services.AddScoped<ProductTypeService>();
        builder.Services.AddMauiBlazorWebView();

        return builder.Build();
    }
}


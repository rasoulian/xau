using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Gold.Pages;
using Gold.Pages.Popups;
using Gold.Services;
using Gold.ViewModels;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Polly;
using Refit;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gold;

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
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");


                fonts.AddFont("IRANSans.ttf", "IRANSans");
                fonts.AddFont("IRANSans_Bold.ttf", "IRANSansBold");
                fonts.AddFont("IRANSans_Light.ttf", "IRANSansLight");
                fonts.AddFont("IRANSans_Medium.ttf", "IRANSansMedium");
                fonts.AddFont("IRANSans_UltraLight.ttf", "IRANSansUltraLight");


                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons-Regular");
                fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIconsOutlined-Regular");
            });

#if DEBUG
        builder.Logging.AddDebug().SetMinimumLevel(LogLevel.Trace);
#endif

        RegisterServices(builder.Services);
        RegisterViews(builder.Services);
        RegisterViewModels(builder.Services);
        var app = builder.Build();

        ServiceHelper.Initialize(app.Services);
        //Expander.EnableAnimations();

        return app;
    }

    private static void RegisterServices(in IServiceCollection services)
    {
        services.AddSingleton<Plugin.Maui.ScreenSecurity.IScreenSecurity>(Plugin.Maui.ScreenSecurity.ScreenSecurity.Default);
        services.AddSingleton<Plugin.LocalNotification.INotificationService>(Plugin.LocalNotification.LocalNotificationCenter.Current);
        services.AddSingleton(new PersianService());
        services.AddTransient<IPopupService, PopupService>();
        services.AddSingleton<GetDeviceInfo>();

        ConfigureRefit(services);

    }

    private static void RegisterViews(in IServiceCollection services)
    {
        services.AddTransient<MainPage>();
        services.AddTransient<AboutPage>();
        services.AddTransient<NotificationPage>();
    }

    private static void RegisterViewModels(in IServiceCollection services)
    {
        services.AddTransient<MainViewModel>();
        services.AddTransient<AboutViewModel>();
        services.AddTransient<NotificationViewModel>();


        services.AddTransientPopup<RegisterPopup, RegisterViewModel>();
        services.AddTransientPopup<DepositPopup, DepositViewModel>();
        services.AddTransientPopup<UpdatePopup, UpdateViewModel>();

    }

    private static void ConfigureRefit(IServiceCollection services)
    {
        services.AddSingleton<IPlatformHttpMessageHandler>(_ =>
        {
#if ANDROID
            return new AndroidHttpMessageHandler();
#elif IOS
            return new IosHttpMessageHandler();
#endif
            return null;
        });

        services.AddSingleton<TokenService>();

        services.AddRefitClient<IApiService>(ConfigureRefitSettings)
            .ConfigureHttpClient(SetHttpClient)
            .AddStandardResilienceHandler(static options => options.Retry = new MobileHttpRetryStrategyOptions());


        static RefitSettings ConfigureRefitSettings(IServiceProvider sp)
        {
            var messageHandler = sp.GetRequiredService<IPlatformHttpMessageHandler>();
            var tokenService = sp.GetRequiredService<TokenService>();
            return new RefitSettings
            {
                HttpMessageHandlerFactory = () => messageHandler.GetHttpMessageHandler(),
                AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(tokenService.Token),
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
                {
                    //Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                }),


            };
        }

        static void SetHttpClient(HttpClient httpClient)
        {
            var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                               ? "https://core.rasoulian.ir"
                               : "https://core.rasoulian.ir";

            httpClient.BaseAddress = new Uri(baseUrl);
        }
    }
}

internal sealed class MobileHttpRetryStrategyOptions : HttpRetryStrategyOptions
{
    public MobileHttpRetryStrategyOptions()
    {
        BackoffType = DelayBackoffType.Exponential;
        MaxRetryAttempts = 3;
        UseJitter = true;
        Delay = TimeSpan.FromSeconds(2);
    }
}


/*
#f5853f
#f47633
#f99f3f
#f78e34
#fec23b
#fbad30
#fff
*/
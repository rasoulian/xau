using CommunityToolkit.Maui.Core;
using Gold.Pages;
using Gold.Resources.Languages;
using Gold.Services;

namespace Gold;

public partial class AppShell : Shell
{
    private readonly Plugin.Maui.ScreenSecurity.IScreenSecurity _screenSecurity;
    private readonly Plugin.LocalNotification.INotificationService _notificationService;
    private readonly IPopupService _popupService;
    private readonly PersianService _persianService;

    public AppShell()
    {
        InitializeComponent();
        _screenSecurity = ServiceHelper.GetService<Plugin.Maui.ScreenSecurity.IScreenSecurity>();
        _notificationService = ServiceHelper.GetService<Plugin.LocalNotification.INotificationService>();
        _persianService = ServiceHelper.GetService<PersianService>();
        _popupService = ServiceHelper.GetService<IPopupService>();



        Routing.RegisterRoute($"//{nameof(AboutPage)}", typeof(AboutPage));
        Routing.RegisterRoute($"//{nameof(NotificationPage)}", typeof(NotificationPage));

    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var status = PermissionStatus.Unknown;

        status = await Permissions.RequestAsync<Permissions.PostNotifications>();
        if (status != PermissionStatus.Granted)
        {
            //await DisplayAlert("Permission Required", "location permission is required!", "Ok");
        }

        status = await Permissions.RequestAsync<Permissions.LocationAlways>();
        if (status != PermissionStatus.Granted)
        {
            //await DisplayAlert("Permission Required", "location permission is required!", "Ok");
        }

#if ANDROID
        _screenSecurity.ActivateScreenSecurityProtection();
#endif

#if IOS
            _screenSecurity.ActivateScreenSecurityProtection(true, true, true);
#endif
        await Task.WhenAll(OnWelcome(), OnTips(), OnUpdates()).ConfigureAwait(false);
    }

    private Task OnWelcome()
    {
        return _notificationService.Show(new Plugin.LocalNotification.NotificationRequest
        {
            BadgeNumber = 1,
            NotificationId = 1,
            Title = AppResources.GoldApp,
            Subtitle = "ورود",
            Description = @$"مشتری عزیز خوش آمدید.
{_persianService.GetTime()}
{_persianService.GetDate().Replace('/', '.')}",
            CategoryType = Plugin.LocalNotification.NotificationCategoryType.Status,

            Schedule = new Plugin.LocalNotification.NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(1),
            },
            Sound = DeviceInfo.Platform == DevicePlatform.Android ? "notif" : "notif.mp3",
        });
    }

    private Task OnTips()
    {
        var tips = new List<string>
            {
                "خرید طلا",
                "فروش طلا",
                "پس انداز طلا",
                "طلای آب شده بدون اجرت بهترین انتخاب برای سرمایه‌گذازی و پس‌انداز طلا است",
                "با بررسی و تحلیل روند قیمتی طلا در بازار، می‌توانید در زمان و قیمت مناسب، اقدام به خرید و فروش آنی طلای خود کنید.ا",
            };
        int index = Random.Shared.Next(tips.Count);
        return _notificationService.Show(new Plugin.LocalNotification.NotificationRequest
        {
            BadgeNumber = 2,
            NotificationId = 2,
            Title = AppResources.GoldApp,
            Subtitle = "توصیه طلایی",
            Description = tips[index],
            CategoryType = Plugin.LocalNotification.NotificationCategoryType.Status,

            Schedule = new Plugin.LocalNotification.NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(30),
            },
            Sound = DeviceInfo.Platform == DevicePlatform.Android ? "notif" : "notif.mp3",
        });
    }

    private Task OnUpdates()
    {
        bool newVer = false;
        if (newVer) return Task.CompletedTask;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(3000);
            this._popupService.ShowPopup<ViewModels.UpdateViewModel>();
            Vibration.Default.Vibrate(TimeSpan.FromSeconds(1));
        });
        return Task.CompletedTask;
    }
}

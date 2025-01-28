using System.Globalization;

namespace Gold
{
    public partial class App : Application
    {
        public App()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fa-IR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fa-IR");
            Application.Current.UserAppTheme = AppTheme.Light;
            this.RequestedThemeChanged += (s, e) => { Application.Current.UserAppTheme = AppTheme.Light; };


            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1500);
                DeviceDisplay.KeepScreenOn = true;
                //DeviceService.Instance.SetScreenBrightness(1.0f);


            });

            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

    }
}

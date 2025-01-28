using Foundation;

namespace Gold;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

public partial class GetDeviceInfo
{
    public partial string GetDeviceID()
    {
        string deviceID = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        return deviceID;
    }
}

public class IosHttpMessageHandler : Services.IPlatformHttpMessageHandler
{
    public HttpMessageHandler GetHttpMessageHandler() =>
        new NSUrlSessionHandler
        {
            TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, Security.SecTrust trust) => url.StartsWith("https://core.rasoulian.ir")
        };
}

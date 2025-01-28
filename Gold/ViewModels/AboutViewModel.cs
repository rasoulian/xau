using CommunityToolkit.Maui.Core;

namespace Gold.ViewModels;


public partial class AboutViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    public AboutViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }
}



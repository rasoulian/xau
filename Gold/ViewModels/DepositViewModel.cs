using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Gold.Services;

namespace Gold.ViewModels;


public partial class DepositViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    public DepositViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    public void DisplayPopup()
    {
        //this.popupService.ShowPopup<UpdatingPopupViewModel>();
    }

    [ObservableProperty]
    private decimal _amount = 20_000_000m;


}



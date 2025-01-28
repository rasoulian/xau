using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Gold.Pages;
using Gold.Services;
using System.Windows.Input;

namespace Gold.ViewModels;


public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private bool _isBusy;


    private readonly IPopupService _popupService = ServiceHelper.GetService<IPopupService>();

    public ICommand OpenUrlCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public ICommand PhoneCallCommand => new Command<string>(PhoneDialer.Default.Open);



    public ICommand OnTermsCommand => new Command(async () =>
    {
        await Shell.Current.GoToAsync($"//{nameof(NotificationPage)}");
    });

    public ICommand OnNotificationCommand => new Command(async () =>
    {
        await Shell.Current.GoToAsync($"//{nameof(NotificationPage)}");
    });

    public ICommand OnRegisterCommand => new Command(async () =>
    {
        this._popupService.ShowPopup<RegisterViewModel>();
    });


    public ICommand OnDepositCommand => new Command(async () =>
    {
        this._popupService.ShowPopup<DepositViewModel>();
    });

    public ICommand OnUpdateCommand => new Command(async () =>
    {
        this._popupService.ShowPopup<UpdateViewModel>();
    });
}



using CommunityToolkit.Mvvm.ComponentModel;
using Gold.Services;
using System.Windows.Input;

namespace Gold.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly PersianService _persianService;

    public RegisterViewModel(PersianService persianService)
    {
        _persianService = persianService;
    }


    public ICommand RegisterCommand => new Command(async () =>
    {
        if (Sms.Default.IsComposeSupported)
        {
            string[] recipients = new[] { "10000011101010" };

            string text = $"{_mobileNumber}\n{_nationalId}\n{_cardNumber}\nIR{_ibanNumber}\n\n{_persianService.GetDate()} {_persianService.GetTime()}\n\n\n{Identifier}";

            var message = new SmsMessage(text, recipients);

            await Sms.Default.ComposeAsync(message);
        }
    });


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFilled))]
    private string _nationalId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFilled))]
    private string _mobileNumber;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFilled))]
    private string _ibanNumber;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFilled))]
    private string _cardNumber;

    public bool IsFilled =>
        !string.IsNullOrWhiteSpace(_nationalId) && _nationalId.Length == 10
        && !string.IsNullOrWhiteSpace(_mobileNumber) && _mobileNumber.Length == 11
        && !string.IsNullOrWhiteSpace(_cardNumber) && _cardNumber.Length == 16
        && !string.IsNullOrWhiteSpace(_ibanNumber) && _ibanNumber.Length == 26 - 2;

    public string Identifier => HashHelper.ComputeHash512(ServiceHelper.GetService<GetDeviceInfo>().GetDeviceID())[120..];

}



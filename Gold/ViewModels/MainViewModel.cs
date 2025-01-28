using CommunityToolkit.Maui.Core;
using Gold.Pages;
using Gold.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Gold.ViewModels;


public partial class MainViewModel : BaseViewModel
{
    private readonly IApiService _api;
    private readonly PersianService _persianService;

    public MainViewModel(IApiService api, PersianService persianService)
    {
        _api = api;
        _persianService = persianService;

        PrepareData();

    }

    void PrepareData()
    {
        var profile = _api.GetGolds();
        //var messages = new List<Notification>
        //{
        //    new Notification{MessageBody= "مشتری گرامی اطلاعات شما با موفقیت ثبت شد.", At=DateTime.Now.AddMinutes(-1)},
        //    new Notification{MessageBody= "مشتری گرامی اطلاعات شما با موفقیت دریافت نمودیم." , At=DateTime.Now.AddMinutes(-2)},
        //    new Notification{MessageBody= "مشتری گرامی خوش آمدید.", At = DateTime.Now.AddMinutes(-3)},
        //}.OrderByDescending(i => i.At).ToList();
        //messages.ForEach(Messages.Add);
    }

    public decimal Gold18 { get; set; }
    public int PersianMonthDay => _persianService.GetMonthDay();
    public string PersianMonthName => _persianService.GetMonthByName();




    public decimal Grams { get; set; } = 12.42m;
    public decimal Balance => Gold18 * Grams;
    public string OwnerName { get; set; } = "علی رسولیان";
    public string CardNumber { get; set; } = "504172xxxxxx7860";
    public string IbanNumber { get; set; } = "IR2507000100011xxxxxx4001";







    //public ObservableCollection<Notification> Messages { get; private set; } = new();
    //public ICommand RefreshCommand => new Command(async () => await RefreshDataAsync());



    //async Task RefreshDataAsync()
    //{
    //    IsRefreshing = true;
    //    await Task.Delay(TimeSpan.FromSeconds(3));
    //    var messages = new List<Notification>
    //    {
    //        new Notification{MessageBody= "پیام جدیدی ندارید."},
    //    }.OrderByDescending(i => i.At).ToList();
    //    messages.ForEach(Messages.Add);
    //    IsRefreshing = false;
    //}

}


public class Order
{
    public decimal Gold18 { get; set; } = 4_293_000m;
    public decimal Weight { get; set; }
    public decimal Amount { get; set; }
    public DateTime At { get; set; } = DateTime.Now;
}

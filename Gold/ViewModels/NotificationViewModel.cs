using Gold.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Gold.ViewModels;


public partial class NotificationViewModel : BaseViewModel
{
    public ObservableCollection<Notification> Messages { get; private set; } = new();
    public ICommand RefreshCommand => new Command(async () => await RefreshDataAsync());


    public NotificationViewModel()
    {
        PrepareData();
    }

    void PrepareData()
    {
        var messages = new List<Notification>
        {
            new Notification{MessageBody= "مشتری گرامی اطلاعات شما با موفقیت ثبت شد.", At=DateTime.Now.AddMinutes(-1)},
            new Notification{MessageBody= "مشتری گرامی اطلاعات شما با موفقیت دریافت نمودیم." , At=DateTime.Now.AddMinutes(-2)},
            new Notification{MessageBody= "مشتری گرامی خوش آمدید.", At = DateTime.Now.AddMinutes(-3)},
        }.OrderByDescending(i => i.At).ToList();
        messages.ForEach(Messages.Add);
    }

    async Task RefreshDataAsync()
    {
        IsRefreshing = true;
        await Task.Delay(TimeSpan.FromSeconds(3));
        var messages = new List<Notification>
        {
            new Notification{MessageBody= "پیام جدیدی ندارید."},
        }.OrderByDescending(i => i.At).ToList();
        messages.ForEach(Messages.Add);
        IsRefreshing = false;
    }

}


public class Notification
{
    public string MessageBody { get; set; }
    public DateTime At { get; set; } = DateTime.Now;
}

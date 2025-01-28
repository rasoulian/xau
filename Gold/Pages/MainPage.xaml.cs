using Gold.Services;
using Gold.ViewModels;

namespace Gold.Pages;

public partial class MainPage : ContentPage
{
    private readonly IApiService _api;

    public MainPage(MainViewModel viewModel, IApiService api)
    {
        InitializeComponent();
        _api = api;
        PrepareDate(viewModel);
        BindingContext = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
    }

    void PrepareDate(MainViewModel vm)
    {
        //var vm = (MainViewModel)BindingContext;
        var api = _api.GetGolds().GetAwaiter().GetResult();
        if (api.owner is null)
        {
            Shell.Current.GoToAsync($"//{nameof(AboutPage)}").GetAwaiter().GetResult();
        }

        vm.Gold18 = api.golds.gold18.rials*0.1m;
        vm.Grams = api.golds.gold18.grams;
        vm.CardNumber = api.owner.shetab;
        vm.IbanNumber = api.owner.sheba;
        vm.OwnerName = api.owner.fullname;
    }
}

using Gold.ViewModels;

namespace Gold.Pages;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

}

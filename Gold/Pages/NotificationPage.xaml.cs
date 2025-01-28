using Gold.ViewModels;

namespace Gold.Pages;

public partial class NotificationPage : ContentPage
{

    public NotificationPage(NotificationViewModel viewModel)
    {
		InitializeComponent();
        this.BindingContext = viewModel;

    }
}
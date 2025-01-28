using CommunityToolkit.Maui.Views;
using Gold.ViewModels;

namespace Gold.Pages.Popups;

public partial class UpdatePopup : Popup
{
    public UpdatePopup(UpdateViewModel viewModel)
    {
        InitializeComponent();
        base.BindingContext = viewModel;
    }
}
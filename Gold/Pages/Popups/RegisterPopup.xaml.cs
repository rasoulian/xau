using CommunityToolkit.Maui.Views;
using Gold.ViewModels;

namespace Gold.Pages.Popups;

public partial class RegisterPopup : Popup
{
	public RegisterPopup(RegisterViewModel viewModel)
	{
		InitializeComponent();
        base.BindingContext = viewModel;
    }


}
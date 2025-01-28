using CommunityToolkit.Maui.Views;
using Gold.ViewModels;

namespace Gold.Pages.Popups;

public partial class DepositPopup : Popup
{
	public DepositPopup(DepositViewModel viewModel)
	{
		InitializeComponent();
        base.BindingContext = viewModel;
    }
    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        //lets the Entry be empty
        if (string.IsNullOrWhiteSpace(e.NewTextValue)) return;

        decimal.TryParse(e.NewTextValue.Replace("٬", ""), out decimal value);
        ((Entry)sender).Text = value.ToString("N0");
    }
}
namespace BugTracker.View;

public partial class TicketPage : ContentPage
{
	public TicketPage(TicketViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
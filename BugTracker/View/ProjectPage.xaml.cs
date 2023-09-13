namespace BugTracker.View;

public partial class ProjectPage : ContentPage
{
	public ProjectPage(ProjectViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
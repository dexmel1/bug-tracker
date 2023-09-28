namespace BugTracker.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    public void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (App.Current.UserAppTheme == AppTheme.Light)
        {
            App.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            App.Current.UserAppTheme = AppTheme.Light;
        }
    }
}
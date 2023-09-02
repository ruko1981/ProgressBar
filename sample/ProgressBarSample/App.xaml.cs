namespace ProgressBarSample;

public partial class App : Application
{
	public App(AppShell appShell)
	{
		// TODO: Please give feedback on the inclusion of styles defined in XAML at https://github.com/mrlacey/MauiAppAccelerator/issues/10
		InitializeComponent();

		MainPage = appShell;
	}
}

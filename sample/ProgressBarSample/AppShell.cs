namespace ProgressBarSample;

public class AppShell : Shell
{
	public AppShell(SamplePage Sample)
	{
		Items.Add(new ShellContent { Title = "Sample", Icon = ImageSource.FromFile("iconsample.png"), Content = Sample });
	}
}

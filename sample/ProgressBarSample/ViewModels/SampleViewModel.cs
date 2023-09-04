namespace ProgressBarSample.ViewModels;

public partial class SampleViewModel : BaseViewModel
{
	[ObservableProperty]
	private float progress = 0.0f;

	[RelayCommand]
	private void IncrementProgress()
	{
		Debug.WriteLine("IncrementProgress()");
		Progress += 0.1f;
	}

	[RelayCommand]
	private void UpdateProgressBar (ProgressBar.Maui.ProgressBar progressBar)
	{
		progressBar.Update();
	}
}

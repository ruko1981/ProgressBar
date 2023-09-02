namespace ProgressBarSample.ViewModels;

public partial class SampleViewModel : BaseViewModel
{
	[ObservableProperty]
	private float progress = 0.01f;

	[RelayCommand]
	private void IncrementProgress()
	{
		Progress +=0.01f;
	}
}

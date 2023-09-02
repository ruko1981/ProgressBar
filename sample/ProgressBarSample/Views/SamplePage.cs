
namespace ProgressBarSample.Views;

public partial class SamplePage : ContentPage
{
	public SamplePage(SampleViewModel viewModel)
	{
		BindingContext = viewModel;

		Content = new VerticalStackLayout
		{
			Spacing = 30,
			Padding = new Thickness(30, 0),
			VerticalOptions = LayoutOptions.Center,

			Children =
			{
				new ProgressBar.Maui.ProgressBar()
				{
					WidthRequest = 300,
					HeightRequest = 5,
					Progress = 0.4f,
					ProgressColor = Colors.DeepSkyBlue,
				}
			}
		};
		
	}
}

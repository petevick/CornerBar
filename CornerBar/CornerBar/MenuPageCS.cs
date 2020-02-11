using CarouselPageNavigation;
using CornerBar.PinchPanGesture;
using Xamarin.Forms;

namespace CornerBar
{
	public class MenuPageCS : CarouselPage
	{
		public MenuPageCS()
		{
			ItemTemplate = new DataTemplate (() => {
				var nameImage = new PinchToZoomContainer() {
					HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = App.ScreenSize.Height,
                    WidthRequest = App.ScreenSize.Width-16,
                   
				};
                nameImage.SetBinding (Label.TextProperty, "Source");

				return new ContentPage {
					Padding = new Thickness (0, Device.OnPlatform (40, 40, 0), 0, 0),
					Content = new StackLayout {
						Children = { nameImage}
					}
				};
			});

			ItemsSource = MenusDataModel.All;
		}
	}
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using CornerBar;
using Xamarin.Forms;

namespace CarouselPageNavigation
{
	public class MenusDataModel
	{
		public string Name { get; set; }

		public static IList<MenusDataModel> All { get; set; }

		static MenusDataModel()
		{
			All = new ObservableCollection<MenusDataModel> {
				new MenusDataModel {
					Name = App.menuFilename.Replace(".","1.")
                    },
				new MenusDataModel {
					Name = App.menuFilename.Replace(".","2.")
                },
                new MenusDataModel {
                    Name = App.menuFilename.Replace(".","3.")
                },
                new MenusDataModel {
					Name = App.menuFilename.Replace(".","4.")
                }
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornerBar.Models
{
    public class MainPageViewModel : BaseViewModel
    {
        private TimeSpan bookingtime;

        public TimeSpan BookingTime
        {
            get { return bookingtime; }
            set
            {
                //set the new value of the message
                bookingtime = value;
                //notify the view that this has changed so it knows to refresh.
                //this comes from the base view model we created
                OnPropertyChanged("BookingTime");
            }
        }
        private int blurbcount;

        public int BlurbCount
        {
            get { return blurbcount; }
            set
            {
                //set the new value of the message
                blurbcount = value;
                //notify the view that this has changed so it knows to refresh.
                //this comes from the base view model we created
                OnPropertyChanged("BlurbCount");
            }
        }
        private string  news;

        public string News
        {
            get { return news; }
            set
            {
                //set the new value of the message
                news = value;
                //notify the view that this has changed so it knows to refresh.
                //this comes from the base view model we created
                OnPropertyChanged("News");
            }
        }
        public MainPageViewModel()
        {
            //set the default message to show
           
            BlurbCount = 1;
            bookingtime = new TimeSpan(12,00,00);
            News = "Any news will appear here";
        }
    }
    public class Staff
    {
        public Staff(string name, string image1, string blurb)
        {
            this.Name = name;
            this.Image1 = image1;
            this.Blurb = blurb;
        }

        public string Name { private set; get; }

        public string Image1 { private set; get; }
        public string Blurb { private set; get; }
    };
    public class MenuNames
    {
        public MenuNames(string name)
        {
            this.Name = name;
        }

        public string Name { private set; get; }
    };
}


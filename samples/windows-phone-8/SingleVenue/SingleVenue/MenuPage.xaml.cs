using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SingleVenue.Resources;
using System.Windows.Media;

namespace SingleVenue
{
    public partial class MenuPage : PhoneApplicationPage
    {
        public MenuPage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.DataContext = App.CurrentMenu;
        }

        private void phoneButton_Click(object sender, EventArgs e)
        {
            OnPhoneCall();
        }

        private void calendarButton_Click(object sender, EventArgs e)
        {
            OnAddToCalendar();
        }

        private void shareButton_Click(object sender, EventArgs e)
        {
            OnShare();
        }

        private void directionsButton_Click(object sender, EventArgs e)
        {
            OnMapsDirections();
        }

        private void OnMapsDirections()
        {
            if (App.MainViewModel != null)
                App.MainViewModel.GetDirections();
        }

        private void OnPhoneCall()
        {
            if (App.MainViewModel != null)
                App.MainViewModel.Call();
        }

        private void OnAddToCalendar()
        {
            if (App.MainViewModel != null)
                App.MainViewModel.AddToCalendar();
        }

        private void OnShare()
        {
            if (App.MainViewModel != null)
                App.MainViewModel.Share();
        }

        private void BuildApplicationBar()
        {
            var appBarBackgroundBrush = Application.Current.Resources["appBarBackgroundBrush"] as SolidColorBrush;
            var appBarForegroundBrush = Application.Current.Resources["appBarForegroundBrush"] as SolidColorBrush;

            ApplicationBar = new ApplicationBar();

            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = false;
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.BackgroundColor = appBarBackgroundBrush.Color;
            ApplicationBar.ForegroundColor = appBarForegroundBrush.Color;

            var appBarCallButton = new ApplicationBarIconButton()
            {
                Text = AppResources.AppBarButtonTextCall,
                IconUri = new Uri("/Assets/Images/phone.png", UriKind.RelativeOrAbsolute)
            };
            appBarCallButton.Click += phoneButton_Click;

            ApplicationBar.Buttons.Add(appBarCallButton);

            var appBarCalendarButton = new ApplicationBarIconButton()
            {
                Text = AppResources.AppBarButtonTextCalendar,
                IconUri = new Uri("/Assets/Images/calendar.png", UriKind.RelativeOrAbsolute)
            };
            appBarCalendarButton.Click += calendarButton_Click;

            ApplicationBar.Buttons.Add(appBarCalendarButton);

            var appBarDirectionsButton = new ApplicationBarIconButton()
            {
                Text = AppResources.AppBarButtonTextDirections,
                IconUri = new Uri("/Assets/Images/directions.png", UriKind.RelativeOrAbsolute)
            };
            appBarDirectionsButton.Click += directionsButton_Click;

            ApplicationBar.Buttons.Add(appBarDirectionsButton);

            var appBarShareButton = new ApplicationBarIconButton()
            {
                Text = AppResources.AppBarButtonTextShare,
                IconUri = new Uri("/Assets/Images/share.png", UriKind.RelativeOrAbsolute)
            };
            appBarShareButton.Click += shareButton_Click;

            ApplicationBar.Buttons.Add(appBarShareButton);
        }
    }
}
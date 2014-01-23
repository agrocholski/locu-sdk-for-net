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
using SingleVenue.ViewModels;
using Windows.Phone.Speech.VoiceCommands;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Tasks;

namespace SingleVenue
{
    public partial class HomePage : PhoneApplicationPage
    {
        private VenueViewModel viewModel = null;

        public HomePage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            viewModel = await VenueViewModel.Create();
            this.DataContext = viewModel;
            App.MainViewModel = viewModel;
            addMapOverlay();

            await viewModel.Save();
            await viewModel.Refresh();
            await viewModel.Save();
            addMapOverlay();

            App.NavigationService = this.NavigationService;

            if (!AppSettings.Instance.VoiceCommandInitialized)
            {
                await VoiceCommandService.InstallCommandSetsFromFileAsync(new Uri("ms-appx:///VoiceCommands.xml"));

                AppSettings.Instance.VoiceCommandInitialized = true;
            }

            if (NavigationContext.QueryString.ContainsKey("voiceCommandName"))
            {
                var voiceCommandName = NavigationContext.QueryString["voiceCommandName"];
                OnVoiceCommand(voiceCommandName);
            }
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
            if (viewModel != null)
                viewModel.GetDirections();
        }

        private void OnPhoneCall()
        {
            if (viewModel != null)
                viewModel.Call();
        }

        private void OnAddToCalendar()
        {
            if (viewModel != null)
                viewModel.AddToCalendar();
        }

        private void OnShare()
        {
            if (viewModel != null)
                viewModel.Share();
        }

        private void OnVoiceCommand(string voiceCommandName)
        {
            switch (voiceCommandName)
            {
                case "callVenue":
                    OnPhoneCall();
                    break;

                case "getDirections":
                    OnMapsDirections();
                    break;

                case "shareVenue":
                    OnShare();
                    break;

                case "addToCalendar":
                    OnAddToCalendar();
                    break;

                case "getHours":
                    pagePanorama.DefaultItem = pagePanorama.Items[1];
                    break;

                case "viewMenu":
                    pagePanorama.DefaultItem = pagePanorama.Items[2];
                    break;


                default:
                    break;
            }
        }

        private void phoneTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            OnPhoneCall();
        }

        private void venueMap_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Keys.MapsApplicationId) && !string.IsNullOrEmpty(Keys.MapsAuthenticationToken))
            {
                Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = Keys.MapsApplicationId;
                Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = Keys.MapsAuthenticationToken;
            }
        }

        private void venueMap_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            OnMapsDirections();
        }

        private void addMapOverlay()
        {
            if (viewModel != null)
            {
                var primaryAccentBrush = Application.Current.Resources["primaryAccentBrush"] as SolidColorBrush;
                var secondaryAccentBrush = Application.Current.Resources["secondaryAccentBrush"] as SolidColorBrush;

                venueMap.Layers.Clear();

                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.Background = new SolidColorBrush(Colors.Transparent);

                var outerEllipse = new Ellipse();
                outerEllipse.Height = 20;
                outerEllipse.Width = 20;
                outerEllipse.Fill = secondaryAccentBrush;

                grid.Children.Add(outerEllipse);

                var innerEllipse = new Ellipse();
                innerEllipse.Height = 10;
                innerEllipse.Width = 10;
                innerEllipse.Fill = primaryAccentBrush;

                grid.Children.Add(innerEllipse);

                var overlay = new MapOverlay();
                overlay.Content = grid;

                overlay.GeoCoordinate = viewModel.Location;
                overlay.PositionOrigin = new Point(0, 0.5);

                var layer = new MapLayer();
                layer.Add(overlay);

                venueMap.Layers.Add(layer);
            }
        }

        private void poweredByLocuImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri("http://locu.com", UriKind.Absolute);
            webBrowserTask.Show();
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
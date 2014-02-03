using Locu.VenueDetails;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using Microsoft.Phone.Tasks;
using MultiVenue.Resources;
using MultiVenue;

namespace MultiVenue.ViewModels
{
    public class VenueViewModel : ViewModelBase
    {
        public VenueViewModel()
        {
            _openHours = new ObservableCollection<OpenHoursViewModel>();
            _menus = new ObservableCollection<MenuViewModel>();
        }

        private string _locuVenueId;
        public string LocuVenuId
        {
            get { return _locuVenueId; }
            set
            {
                if(_locuVenueId != value)
                {
                    _locuVenueId = value;
                    OnPropertyChanged("LocuVenueId");
                }
            }
        }

        private string _addressLine1;
        public string AddressLine1
        {
            get { return _addressLine1; }
            set
            {
                if (_addressLine1 != value)
                {
                    _addressLine1 = value;
                    OnPropertyChanged("AddressLine1");
                }
            }
        }

        private string _addressLine2;
        public string AddressLine2
        {
            get { return _addressLine2; }
            set
            {
                if (_addressLine2 != value)
                {
                    _addressLine2 = value;
                    OnPropertyChanged("AddressLine2");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        private GeoCoordinate _location;
        public GeoCoordinate Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private ObservableCollection<OpenHoursViewModel> _openHours;
        public ObservableCollection<OpenHoursViewModel> OpenHours
        {
            get { return _openHours; }
            set
            {
                if (_openHours != value)
                {
                    _openHours = value;
                    OnPropertyChanged("OpenHours");
                }
            }
        }

        private ObservableCollection<MenuViewModel> _menus;
        public ObservableCollection<MenuViewModel> Menus
        {
            get { return _menus; }
            set
            {
                if (_menus != value)
                {
                    _menus = value;
                    OnPropertyChanged("Menus");
                }
            }
        }

        private MenuViewModel _defaultMenu;
        public MenuViewModel DefaultMenu
        {
            get { return _defaultMenu; }
            set
            {
                if (_defaultMenu != value)
                {
                    _defaultMenu = value;
                    OnPropertyChanged("DefaultMenu");
                }
            }
        }

        private string _json;
        public string Json
        {
            get { return _json; }
            set
            {
                if (_json != value)
                {
                    _json = value;
                    OnPropertyChanged("Json");
                }
            }
        }

        public void GetDirections()
        {
            var mapsDirectionsTask = new MapsDirectionsTask();

            var venueLML = new LabeledMapLocation(Name, Location);
            mapsDirectionsTask.End = venueLML;

            mapsDirectionsTask.Show();
        }

        public void Share()
        {
            var shareStatusTask = new ShareStatusTask();
            shareStatusTask.Status = string.Format("{0} {1}", AppResources.DiningAt, Name);
            shareStatusTask.Show();
        }

        public void Call()
        {
            if (!string.IsNullOrEmpty(Phone))
            {
                var phoneCallTask = new PhoneCallTask();

                phoneCallTask.PhoneNumber = Phone;
                phoneCallTask.DisplayName = Name;

                phoneCallTask.Show();
            }
        }

        public void AddToCalendar()
        {
            var saveAppointmentTask = new SaveAppointmentTask();

            saveAppointmentTask.StartTime = DateTime.Now.AddHours(1);
            saveAppointmentTask.EndTime = DateTime.Now.AddHours(2);
            saveAppointmentTask.Subject = string.Format("{0} {1}", AppResources.DiningAt, Name);
            saveAppointmentTask.Location = string.Format("{0} {1}", AddressLine1, AddressLine2);
            saveAppointmentTask.IsAllDayEvent = false;
            saveAppointmentTask.Reminder = Reminder.FifteenMinutes;
            saveAppointmentTask.AppointmentStatus = Microsoft.Phone.UserData.AppointmentStatus.Busy;

            saveAppointmentTask.Show();
        }

        public async Task<bool> Save()
        {
            var success = false;

            try
            {
                byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(this.Json.ToCharArray());

                var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

                var dataFolder = await localFolder.CreateFolderAsync("data", Windows.Storage.CreationCollisionOption.OpenIfExists);

                var dataFile = await dataFolder.CreateFileAsync(string.Format("venue-{0}.json", this.LocuVenuId), Windows.Storage.CreationCollisionOption.ReplaceExisting);

                using (var stream = await dataFile.OpenStreamForWriteAsync())
                {
                    stream.Write(dataBytes, 0, dataBytes.Length);
                }

                success = true;
            }
            catch { }

            return success;
        }

        public async Task<bool> Refresh()
        {
            var success = false;

            try
            {
                var response = await GetDataFromWeb();

                if (response.Json != this.Json)
                {
                    this.Name = response.Venues[0].Name;
                    this.Phone = response.Venues[0].Phone;
                    this.AddressLine1 = response.Venues[0].StreeAddress;
                    this.AddressLine2 = string.Format("{0}, {1} {2}", response.Venues[0].Locality, response.Venues[0].Region, response.Venues[0].PostalCode);
                    this.Location = new GeoCoordinate(response.Venues[0].Latitude, response.Venues[0].Longitude);
                    this.Json = response.Json;
                }

                success = true;
            }
            catch { }

            return success;
        }

        public static async Task<VenueViewModel> Create(string locuVenueId)
        {
            var cachedData = "";
            VenueDetailsResponse response = null;
            VenueViewModel viewModel = null;

            //Try and get a cached copy of the data from isolated storage
            cachedData = await GetDataFromIsolatedStorage();

            //If data does still not exist, load from the Locu's web API
            if (string.IsNullOrEmpty(cachedData))
                response = await GetDataFromWeb();
            else
                response = VenueDetailsResponse.CreateFromString(cachedData);

            viewModel = CreateFromVenueDetailsResponse(response);
            viewModel.LocuVenuId = locuVenueId;

            return viewModel;
        }

        private async static Task<string> GetDataFromIsolatedStorage(string locuVenueId)
        {
            string data = "";

            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var dataFolder = await localFolder.CreateFolderAsync("data", Windows.Storage.CreationCollisionOption.OpenIfExists);

            try
            {
                var dataFile = await dataFolder.OpenStreamForReadAsync(string.Format("venue-{0}.json", locuVenueId));

                using (var streamReader = new StreamReader(dataFile))
                {
                    data = streamReader.ReadToEnd();
                }
            }
            catch { }

            return data;
        }

        private async static Task<VenueDetailsResponse> GetDataFromWeb(string locuVenueId)
        {
            var venueIds = new List<string>() { locuVenueId };
            var request = new VenueDetailsRequest(Keys.LocuApiKey, venueIds);
            var client = new VenueDetailsClient();
            var response = await client.SendAsync(request);

            return response[0];
        }

        private static VenueViewModel CreateFromVenueDetailsResponse(VenueDetailsResponse response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            if (response.Venues == null)
                throw new ArgumentNullException("response.Venues");

            if (response.Venues.Count != 1)
                throw new ArgumentOutOfRangeException("response.Venues");

            var viewModel = new VenueViewModel();

            viewModel.Name = response.Venues[0].Name;
            viewModel.Phone = response.Venues[0].Phone;
            viewModel.AddressLine1 = response.Venues[0].StreeAddress;
            viewModel.AddressLine2 = string.Format("{0}, {1} {2}", response.Venues[0].Locality, response.Venues[0].Region, response.Venues[0].PostalCode);
            viewModel.Location = new GeoCoordinate(response.Venues[0].Latitude, response.Venues[0].Longitude);

            if (response.Venues[0].OpenHours != null)
                viewModel.OpenHours = GetOpenHours(response.Venues[0].OpenHours);

            if (response.Venues[0].Menus != null)
            {
                foreach (var menu in response.Venues[0].Menus)
                    viewModel.Menus.Add(new MenuViewModel(menu));
            }

            if (viewModel.Menus != null && viewModel.Menus.Count >= 1)
                viewModel.DefaultMenu = viewModel.Menus[0];

            viewModel.Json = response.Json;

            return viewModel;
        }

        private static ObservableCollection<OpenHoursViewModel> GetOpenHours(VenueDetailsOpenHours source)
        {
            var result = new ObservableCollection<OpenHoursViewModel>();

            var day1 = new OpenHoursViewModel() { Day = DaysOfWeek.Today };
            var day2 = new OpenHoursViewModel() { Day = DaysOfWeek.Tomorrow };
            var day3 = new OpenHoursViewModel();
            var day4 = new OpenHoursViewModel();
            var day5 = new OpenHoursViewModel();
            var day6 = new OpenHoursViewModel();
            var day7 = new OpenHoursViewModel();

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    day1.Hours = CreateHoursFromList(source.Monday);
                    day2.Hours = CreateHoursFromList(source.Tuesday);
                    day3.Day = DaysOfWeek.Wednesday;
                    day3.Hours = CreateHoursFromList(source.Wednesday);
                    day4.Day = DaysOfWeek.Thursday;
                    day4.Hours = CreateHoursFromList(source.Thursday);
                    day5.Day = DaysOfWeek.Friday;
                    day5.Hours = CreateHoursFromList(source.Friday);
                    day6.Day = DaysOfWeek.Saturday;
                    day6.Hours = CreateHoursFromList(source.Saturday);
                    day7.Day = DaysOfWeek.Sunday;
                    day7.Hours = CreateHoursFromList(source.Sunday);
                    break;

                case DayOfWeek.Tuesday:
                    day1.Hours = CreateHoursFromList(source.Tuesday);
                    day2.Hours = CreateHoursFromList(source.Wednesday);
                    day3.Day = DaysOfWeek.Thursday;
                    day3.Hours = CreateHoursFromList(source.Thursday);
                    day4.Day = DaysOfWeek.Friday;
                    day4.Hours = CreateHoursFromList(source.Friday);
                    day5.Day = DaysOfWeek.Saturday;
                    day5.Hours = CreateHoursFromList(source.Saturday);
                    day6.Day = DaysOfWeek.Sunday;
                    day6.Hours = CreateHoursFromList(source.Sunday);
                    day7.Day = DaysOfWeek.Monday;
                    day7.Hours = CreateHoursFromList(source.Monday);
                    break;

                case DayOfWeek.Wednesday:
                    day1.Hours = CreateHoursFromList(source.Wednesday);
                    day2.Hours = CreateHoursFromList(source.Thursday);
                    day3.Day = DaysOfWeek.Friday;
                    day3.Hours = CreateHoursFromList(source.Friday);
                    day4.Day = DaysOfWeek.Saturday;
                    day4.Hours = CreateHoursFromList(source.Saturday);
                    day5.Day = DaysOfWeek.Sunday;
                    day5.Hours = CreateHoursFromList(source.Sunday);
                    day6.Day = DaysOfWeek.Monday;
                    day6.Hours = CreateHoursFromList(source.Monday);
                    day7.Day = DaysOfWeek.Tuesday;
                    day7.Hours = CreateHoursFromList(source.Tuesday);
                    break;

                case DayOfWeek.Thursday:
                    day1.Hours = CreateHoursFromList(source.Thursday);
                    day2.Hours = CreateHoursFromList(source.Friday);
                    day3.Day = DaysOfWeek.Saturday;
                    day3.Hours = CreateHoursFromList(source.Saturday);
                    day4.Day = DaysOfWeek.Sunday;
                    day4.Hours = CreateHoursFromList(source.Sunday);
                    day5.Day = DaysOfWeek.Monday;
                    day5.Hours = CreateHoursFromList(source.Monday);
                    day6.Day = DaysOfWeek.Tuesday;
                    day6.Hours = CreateHoursFromList(source.Tuesday);
                    day7.Day = DaysOfWeek.Wednesday;
                    day7.Hours = CreateHoursFromList(source.Wednesday);
                    break;

                case DayOfWeek.Friday:
                    day1.Hours = CreateHoursFromList(source.Friday);
                    day2.Hours = CreateHoursFromList(source.Saturday);
                    day3.Day = DaysOfWeek.Sunday;
                    day3.Hours = CreateHoursFromList(source.Sunday);
                    day4.Day = DaysOfWeek.Monday;
                    day4.Hours = CreateHoursFromList(source.Monday);
                    day5.Day = DaysOfWeek.Tuesday;
                    day5.Hours = CreateHoursFromList(source.Tuesday);
                    day6.Day = DaysOfWeek.Wednesday;
                    day6.Hours = CreateHoursFromList(source.Wednesday);
                    day7.Day = DaysOfWeek.Thursday;
                    day7.Hours = CreateHoursFromList(source.Thursday);
                    break;

                case DayOfWeek.Saturday:
                    day1.Hours = CreateHoursFromList(source.Saturday);
                    day2.Hours = CreateHoursFromList(source.Sunday);
                    day3.Day = DaysOfWeek.Monday;
                    day3.Hours = CreateHoursFromList(source.Monday);
                    day4.Day = DaysOfWeek.Tuesday;
                    day4.Hours = CreateHoursFromList(source.Tuesday);
                    day5.Day = DaysOfWeek.Wednesday;
                    day5.Hours = CreateHoursFromList(source.Wednesday);
                    day6.Day = DaysOfWeek.Thursday;
                    day6.Hours = CreateHoursFromList(source.Thursday);
                    day7.Day = DaysOfWeek.Friday;
                    day7.Hours = CreateHoursFromList(source.Friday);
                    break;

                case DayOfWeek.Sunday:
                    day1.Hours = CreateHoursFromList(source.Sunday);
                    day2.Hours = CreateHoursFromList(source.Monday);
                    day3.Day = DaysOfWeek.Tuesday;
                    day3.Hours = CreateHoursFromList(source.Tuesday);
                    day4.Day = DaysOfWeek.Wednesday;
                    day4.Hours = CreateHoursFromList(source.Wednesday);
                    day5.Day = DaysOfWeek.Thursday;
                    day5.Hours = CreateHoursFromList(source.Thursday);
                    day6.Day = DaysOfWeek.Friday;
                    day6.Hours = CreateHoursFromList(source.Friday);
                    day7.Day = DaysOfWeek.Saturday;
                    day7.Hours = CreateHoursFromList(source.Saturday);
                    break;
            }

            result.Add(day1);
            result.Add(day2);
            result.Add(day3);
            result.Add(day4);
            result.Add(day5);
            result.Add(day6);
            result.Add(day7);

            return result;
        }

        private static ObservableCollection<string> CreateHoursFromList(List<string> source)
        {
            var result = new ObservableCollection<string>();

            foreach (var item in source)
            {
                var hours = ConvertHours(item);
                result.Add(hours);
            }

            return result;
        }

        private static string ConvertHours(string source)
        {

            if (source.Contains('-'))
            {
                //Verify source format: xx:xx:xx - xx:xx:xx
                var hoursSplit = source.Split(new char[] { '-' });

                if (hoursSplit.Length != 2)
                    return source;

                var hour1 = hoursSplit[0];
                var hour2 = hoursSplit[1];

                if (!hour1.Contains(':') || !hour2.Contains(':'))
                    return source;

                var hour1Split = hour1.Split(new char[] { ':' });

                if (hour1Split.Length < 2)
                    return source;

                var hour2Split = hour2.Split(new char[] { ':' });

                if (hour2Split.Length < 2)
                    return source;

                //Convert source
                var hour1Converted = ConvertHour(hour1Split[0].Trim());
                var hour1Minute = hour1Split[1].Trim();
                var hour1Meridiem = GetMeridiem(hour1Split[0].Trim());

                var hour2Converted = ConvertHour(hour2Split[0].Trim());
                var hour2Minute = hour2Split[1].Trim();
                var hour2Meridiem = GetMeridiem(hour2Split[0]).Trim();

                var hours = string.Format("{0}:{1} {2} - {3}:{4} {5}", hour1Converted, hour1Minute, hour1Meridiem,
                    hour2Converted, hour2Minute, hour2Meridiem);

                return hours;
            }

            return source;



        }

        private static string GetMeridiem(string source)
        {
            if (source == "0" || source == "24" || source == "1"
                || source == "2" || source == "3" || source == "4"
                || source == "5" || source == "6" || source == "7"
                || source == "8" || source == "9" || source == "10"
                || source == "11")
                return DaysOfWeek.AM;
            else
                return DaysOfWeek.PM;
        }

        private static string ConvertHour(string source)
        {
            if (source == "0" || source == "24")
                return "12";

            if (source == "13")
                return "1";
            else if (source == "14")
                return "2";
            else if (source == "15")
                return "3";
            else if (source == "16")
                return "4";
            else if (source == "17")
                return "5";
            else if (source == "18")
                return "6";
            else if (source == "19")
                return "7";
            else if (source == "20")
                return "8";
            else if (source == "21")
                return "9";
            else if (source == "22")
                return "10";
            else if (source == "23")
                return "11";
            else
                return source;
        }
    }
}

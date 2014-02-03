using MultiVenue.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiVenue.ViewModels
{
    public class VenueListViewModel : ViewModelBase
    {
        private ObservableCollection<VenueViewModel> _venues;
        public ObservableCollection<VenueViewModel> Venues
        {
            get { return _venues; }
            set
            {
                if (_venues != value)
                {
                    _venues = value;
                    OnPropertyChanged("Venues");
                }
            }
        }

        public static async Task<VenueListViewModel> Create()
        {
            VenueListViewModel viewModel = null;

            foreach(var locuVenueId in Keys.LocuVenueIds)
            {
                var venue = await VenueViewModel.Create(locuVenueId);
                viewModel.Venues.Add(venue);
            }

            return viewModel;
        }
    }
}

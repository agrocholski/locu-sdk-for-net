using Locu.VenueDetails;
using MultiVenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiVenue.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private Menu menu;
        private ICommand viewMenuCommand;

        public MenuViewModel(Menu menu)
        {
            this.menu = menu;
            this.viewMenuCommand = new DelegateCommand(this.ViewMenuAction);
        }

        public string Name
        {
            get { return menu.Name; }
            set
            {
                if (menu.Name != value)
                {
                    menu.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public List<MenuSection> Sections
        {
            get { return menu.Sections; }
            set
            {
                if (menu.Sections != value)
                {
                    menu.Sections = value;
                    OnPropertyChanged("Sections");
                }
            }
        }

        public ICommand ViewMenu
        {
            get { return viewMenuCommand; }
        }

        private void ViewMenuAction(object o)
        {
            App.CurrentMenu = this.menu;

            App.NavigationService.Navigate(new Uri("/MenuPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}

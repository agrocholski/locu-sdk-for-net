using MultiVenue.Resources;
using MultiVenue.ViewModels;
using Locu.VenueDetails;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SingleVenue.Converters
{
    public class MenuItemVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is MenuContent)
            {
                var content = value as MenuContent;

                if (content.Type == "ITEM")
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SectionTextVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is MenuContent)
            {
                var content = value as MenuContent;

                if (content.Type == "SECTION_TEXT")
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultipleMenuVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ObservableCollection<MenuViewModel>)
            {
                var menus = value as ObservableCollection<MenuViewModel>;

                if (menus.Count > 1)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SingleMenuVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ObservableCollection<MenuViewModel>)
            {
                var menus = value as ObservableCollection<MenuViewModel>;

                if (menus.Count == 1)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SectionNameVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                var converted = value as string;

                if (string.IsNullOrEmpty(converted))
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SubsectionNameVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                var converted = value as string;

                if (string.IsNullOrEmpty(converted))
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuHeaderTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ObservableCollection<MenuViewModel>)
            {
                var menus = value as ObservableCollection<MenuViewModel>;

                if (menus.Count == 1)
                    return AppResources.MenuHeaderText;
                else
                    return AppResources.MenusHeaderText;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuSectionNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                var sectionName = value as string;

                return sectionName.ToUpper();
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuSubsectionNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                var subsectionName = value as string;

                return subsectionName.ToUpper();
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuItemNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is MenuContent)
            {
                var content = value as MenuContent;


                if (!string.IsNullOrEmpty(content.Name) && !string.IsNullOrEmpty(content.Price))
                {
                    return string.Format("{0} ({1})", content.Name.ToUpper(), content.Price);
                }
                else if (!string.IsNullOrEmpty(content.Name))
                {
                    return content.Name.ToUpper();
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuItemDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                var menuItemDescription = value as string;

                return menuItemDescription.ToLower();
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PriceDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                var price = value as string;

                return string.Format("({0})", price);
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

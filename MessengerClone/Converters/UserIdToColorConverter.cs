using MessengerClone.Store;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
namespace MessengerClone.Converters;

public class UserIdToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int senderId)
        {
            return senderId == UserStore.Instance.CurrentUser.ID ? "#2a5fff" : "#303030";  // Customize colors as needed
        }
        

        return "Gray";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

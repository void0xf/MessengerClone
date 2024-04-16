using MessengerClone.Store;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MessengerClone.Converters
{
    public class UserIdToAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int messageSenderId = (int)value;
            int currentUserId = UserStore.Instance.CurrentUser.ID; // Directly access here

            return messageSenderId == currentUserId ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

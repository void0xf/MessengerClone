using MessengerClone.DbModels;
using MessengerClone.Services;
using MessengerClone.Store;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MessengerClone.Converters
{

    public class UserIdToLastMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int userId)
            {
                MessageServices messageServices = new MessageServices();
                Message LastMessage = messageServices.GetLastMessage(UserStore.Instance.CurrentUser.ID, userId);
                string Youstring = LastMessage.SenderId == UserStore.Instance.CurrentUser.ID ? "You:" : "";
                string LastMessageString = $"{Youstring} {CropStringWithEllipsis(LastMessage.Content, 10)} {GetFormattedDate(LastMessage.Timestamp, "en-US")}";
                return LastMessageString;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("This converter only works for one-way binding.");
        }

        public string GetFormattedDate(DateTime timestamp, string culture)
        {
            CultureInfo cultureInfo = new CultureInfo(culture);
            return timestamp.ToString("MMMM d", cultureInfo);
        }
        public static string CropStringWithEllipsis(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            if (input.Length > maxLength)
            {
                return input.Substring(0, maxLength) + "...";
            }

            return input;
        }

    }
}

using System;
using System.Globalization;
using Xamarin.Forms;

namespace XrnCourse.BucketList.Converters
{
    public class ItemCompletionToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "item.png";
            }
            else
            {
                return "item_done.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

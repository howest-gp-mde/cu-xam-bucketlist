using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace XrnCourse.BucketList.Converters
{
    public class BucketCompletionToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value.GetType() == typeof(bool))
            {
                bool completed = (bool)value;
                return completed ? "bucket_done.png" : "bucket_default.png";
            }
            else
            {
                throw new ArgumentException("Expected bool as value", "value");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

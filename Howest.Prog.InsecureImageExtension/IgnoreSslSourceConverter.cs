using System;
using System.Globalization;
using Xamarin.Forms;

namespace Howest.Prog.InsecureImageExtension
{
    //public class IgnoreSslSourceConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if(value is string)
    //        {
    //            var originalSource = (string)value;

    //            string token = null;
    //            if(parameter is string)
    //            {
    //                token = (string)parameter;
    //            }

    //            if (Uri.TryCreate(originalSource, UriKind.Absolute, out var uri))
    //            {
    //                var imageSource = ImageSource.FromStream((cancellation) => HttpHelpers.GetImageStreamAsync(uri, true, token));
    //                return imageSource;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }

    //        return null;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }


       
    //}
}

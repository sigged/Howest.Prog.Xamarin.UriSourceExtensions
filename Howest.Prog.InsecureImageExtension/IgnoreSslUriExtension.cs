using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Howest.Prog.InsecureImageExtension
{

    [ContentProperty("Source")]
    public class IgnoreSslUriExtension : IMarkupExtension
    {
        public string Source { get; set; }
        public string BearerToken { get; set; }

        protected static IValueConverter converter = new IgnoreSslSourceConverter();

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            if (Uri.TryCreate(Source, UriKind.Absolute, out var uri))
            {
                var imageSource = ImageSource.FromStream((token) => HttpHelpers.GetImageStreamAsync(uri, true, BearerToken));
                return imageSource;
            }
            else
            {
                return null;
            }
        }
    }

    [ContentProperty("Path")]
    public class IgnoreSslUriBindingExtension : BindableObject, IMarkupExtension, IMarkupExtension<BindingBase>
    {
        public string Path { get; set; }

        //public static readonly BindableProperty TokenProperty =
        //    BindableProperty.CreateAttached("Token", typeof(string), typeof(IgnoreSslUriBindingExtension), null);

        //public string Token
        //{
        //    get { return (string) GetValue(TokenProperty); }
        //    set { SetValue(TokenProperty, value); }
        //}

        //public static readonly BindableProperty IgnoreTlsCertificateErrorsProperty =
        //    BindableProperty.CreateAttached("IgnoreTlsCertificateErrors", typeof(bool), typeof(IgnoreSslUriBindingExtension), false);

        //public bool IgnoreTlsCertificateErrors
        //{
        //    get { return (bool)GetValue(IgnoreTlsCertificateErrorsProperty); }
        //    set { SetValue(IgnoreTlsCertificateErrorsProperty, value); }
        //}


        protected static IValueConverter converter = new IgnoreSslSourceConverter();

        BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            BindableObject targetObject;
            BindableProperty targetProperty;

            if (target != null && target.TargetObject is BindableObject && target.TargetProperty is BindableProperty)
            {
                targetObject = (BindableObject)target.TargetObject;
                targetProperty = (BindableProperty)target.TargetProperty;

                var element = targetObject as Image;
                var src = element.GetValue(Image.SourceProperty);
            }

            string token = null;

            var sourceBinding = new Binding(Path, BindingMode.OneWay, converter, token);
            return sourceBinding;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
        }
    }
}

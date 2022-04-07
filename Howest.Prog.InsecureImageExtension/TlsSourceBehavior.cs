using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Howest.Prog.InsecureImageExtension
{
    public class TlsSourceBehavior : Behavior<Image>
    {
        protected Image attachedImage;

        public static readonly BindableProperty TokenProperty =
            BindableProperty.CreateAttached(nameof(Token), typeof(string), typeof(TlsSourceBehavior), null);


        /// <summary>
        /// Gets or sets a value containing the access token for the remote resource.
        /// The token is expected to use the Bearer authentication scheme.
        /// </summary>
        /// <remarks>
        /// Leave null for unauthenticated access.
        /// </remarks>
        public string Token
        {
            get
            {
                return (string)GetValue(TokenProperty);
            }
            set
            {
                SetValue(TokenProperty, value);
            }
        }

        public static readonly BindableProperty IgnoreCertificateErrorsProperty =
            BindableProperty.CreateAttached(nameof(IgnoreCertificateErrors), typeof(bool), typeof(TlsSourceBehavior), null);

        /// <summary>
        /// Gets or sets a value to ignore TLS certificate errors such as self-signed, expired or untrusted certificates.
        /// This is especially useful while debugging a locally hosted web service.
        /// </summary>
        /// <remarks>
        /// Certificates are only ignored in Debug builds for security reasons.
        /// </remarks>
        public bool IgnoreCertificateErrors
        {
            get
            {
                return (bool)GetValue(IgnoreCertificateErrorsProperty);
            }
            set
            {
                SetValue(IgnoreCertificateErrorsProperty, value);
            }
        }

        protected override void OnAttachedTo(Image image)
        {
            attachedImage = image;
            SubscribeEvents(true);
        }

        protected override void OnDetachingFrom(Image image)
        {
            SubscribeEvents(false);
            attachedImage = null;
        }
        private void SubscribeEvents(bool subscribe)
        {
            if (subscribe)
            {
                attachedImage.PropertyChanged += AttachedImage_PropertyChanged;
                attachedImage.BindingContextChanged += AttachedImage_BindingContextChanged;
            }
            else
            {
                attachedImage.PropertyChanged -= AttachedImage_PropertyChanged;
                attachedImage.BindingContextChanged -= AttachedImage_BindingContextChanged;
            }
        }

        private void AttachedImage_BindingContextChanged(object sender, EventArgs e)
        {
            if (attachedImage == null)
                return;

            SwapImageSource();
        }

        private void AttachedImage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (attachedImage == null)
                return;

            if (e.PropertyName == nameof(Image.Source))
            {
                SwapImageSource();
            }
        }

        private void SwapImageSource()
        {
            if (attachedImage.Source is UriImageSource)
            {
                bool useCustomHandler = IgnoreCertificateErrors || Token != null;
                if (useCustomHandler)
                {
                    var originalUriSource = (UriImageSource)attachedImage.Source;
                    SubscribeEvents(false);
                    attachedImage.Source = ImageSource.FromStream((cancellation) => HttpHelpers.GetImageStreamAsync(originalUriSource.Uri, IgnoreCertificateErrors, Token));
                    SubscribeEvents(true);
                }

            }
        }

    }
}

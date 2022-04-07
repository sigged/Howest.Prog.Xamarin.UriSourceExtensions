using Howest.Prog.InsecureImageExtension;
using SampleApp.Models;
using SampleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SampleApp
{
    public class SlideShowPage : ContentPage
    {
        protected Image swappableImage;
        protected bool isActive;
        protected IList<ImageDto> images;
        protected readonly ApiClient _client;
        protected int imageIndex = -1;

        public SlideShowPage()
        {
            this.Disappearing += (sender, e) => { isActive = false; };
            this.Appearing += (sender, e) => { isActive = true; };
            this.Appearing += SwapperPage_Appearing;

            _client = new ApiClient();

            swappableImage = new Image();

            swappableImage.Behaviors.Add(new TlsSourceBehavior
            {
                IgnoreCertificateErrors = true
            });
            

            Content = new StackLayout
            {
                Children = {
                    swappableImage
                }
            };

        }

        private async void SwapperPage_Appearing(object sender, EventArgs e)
        {
            images = (await _client.GetPublicImagesAsync()).ToList();


            if(images.Count() > 0)
            {
                imageIndex = 0;
                swappableImage.Source = images[imageIndex].Url;


                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    imageIndex++;
                    if(imageIndex >= images.Count())
                        imageIndex = 0;

                    swappableImage.Source = images[imageIndex].Url;

                    return isActive;
                });

            }

        }
    }
}
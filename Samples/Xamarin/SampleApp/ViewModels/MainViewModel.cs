using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand LoadPublicPage => new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PublicPage());
        });

        public ICommand LoadProtectedPage => new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProtectedPage());
        });
        public ICommand LoadSlideshowPage => new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SlideShowPage());
        });
    }
}

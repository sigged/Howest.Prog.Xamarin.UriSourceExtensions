using SampleApp.Models;
using SampleApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class PublicViewModel : ViewModelBase
    {

        protected readonly ApiClient _client;

        public PublicViewModel()
        {
            _client = new ApiClient();
        }

        public ICommand LoadImagesCommand => new Command(async () =>
        {
            var images = await _client.GetPublicImagesAsync();
            Images = new ObservableCollection<ImageDto>(images);
        });

        public ObservableCollection<ImageDto> images = new ObservableCollection<ImageDto>();

        public ObservableCollection<ImageDto> Images
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
                OnPropertyChanged();
            }
        }
    }
}

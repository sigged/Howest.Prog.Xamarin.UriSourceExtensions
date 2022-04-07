using SampleApp.Models;
using SampleApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class ProtectedViewModel : ViewModelBase
    {

        protected readonly ApiClient _client;

        public ProtectedViewModel()
        {
            _client = new ApiClient();
        }

        public ICommand AuthenticateCommand => new Command(async () =>
        {
            try
            {
                var authresult = await _client.LoginAsync();
                IsAuthenticated = authresult.Success;

                if (IsAuthenticated)
                {
                    await SecureStorage.SetAsync("token", authresult.Token);
                    UserName = "Jeftje";
                    AuthenticationToken = authresult.Token;
                }
                else
                {
                    SecureStorage.Remove("token");
                    UserName = null;
                    AuthenticationToken = null;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            
        });

        public ICommand LoadImagesCommand => new Command(async () =>
        {
            try {
                string token = await SecureStorage.GetAsync("token");
                var images = await _client.GetProtectedImagesAsync(token);
                Images = new ObservableCollection<ImageDto>(images);
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        });

        public bool isAuthenticated;
        public bool IsAuthenticated
        {
            get
            {
                return isAuthenticated;
            }
            set
            {
                isAuthenticated = value;
                OnPropertyChanged();
            }
        }

        public string authenticationToken;
        public string AuthenticationToken
        {
            get
            {
                return authenticationToken;
            }
            set
            {
                authenticationToken = value;
                OnPropertyChanged();
            }
        }


        public string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

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

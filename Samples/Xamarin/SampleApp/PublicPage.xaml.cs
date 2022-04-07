using SampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PublicPage : ContentPage
    {
        public PublicPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            (BindingContext as PublicViewModel).LoadImagesCommand.Execute(null);
        }
    }
}
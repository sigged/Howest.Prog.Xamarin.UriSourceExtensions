using SampleApp.ViewModels;
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
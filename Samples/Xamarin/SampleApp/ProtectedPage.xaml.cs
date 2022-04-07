using SampleApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProtectedPage : ContentPage
    {
        public ProtectedPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            (BindingContext as ProtectedViewModel).RefreshAuthenticationState.Execute(null);
        }
    }
}
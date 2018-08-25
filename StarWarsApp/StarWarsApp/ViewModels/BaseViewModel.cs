using System.Threading.Tasks;
using System.Windows.Input;
using StarWarsApp.Services;
using Xamarin.Forms;

namespace StarWarsApp.ViewModels
{
    public abstract class BaseViewModel : BindableObject
    {
        public StarWarsApi Api => new StarWarsApi();
        public INavigation Navigation => Application.Current.MainPage.Navigation;

        public ICommand RefreshCommand { get; set; }
        public ICommand ItemClickCommand { get; set; }

        bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public BaseViewModel(string title)
        {
            Title = title;
        }

        public async Task ShowAlertAsync(string title, string msg, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, msg, cancel);
        }

        public virtual Task Initialize(object parameters = null) => Task.FromResult(true);
    }
}

using StarWarsApp.Models;
using StarWarsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarWarsApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        ObservableCollection<People> people = new ObservableCollection<People>();
        public ObservableCollection<People> Peoples
        {
            get { return people;  }
            set { people = value; OnPropertyChanged(); }
        }

        public MainViewModel() : base("StarWarsForms")
        {
            RefreshCommand = new Command(async () => await LoadData(), () => !IsBusy);
            ItemClickCommand = new Command<People>(async (item) => await ItemClickCommandExecuteAsync(item));
        }

        public override async Task Initialize(object parameters = null) => await LoadData();

        async Task ItemClickCommandExecuteAsync(People people)
        {
            if (IsBusy)
                return;

            Exception error = null;

            try
            {
                IsBusy = true;
                var peoplePage = new PeoplePage();
                await peoplePage.ViewModel.Initialize(people);

                await Navigation.PushAsync(peoplePage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }

            if (error != null)
                await ShowAlertAsync("Error!", error.Message, "OK");
        }

        async Task LoadData()
        {
            if (IsBusy)
                return;

            Exception error = null;

            try
            {
                IsBusy = true;

                var peoples = await Api.GetPeoplesAsync();

                Peoples.Clear();
                foreach (var people in peoples)
                    Peoples.Add(people);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }

            if (error != null)
                await ShowAlertAsync("Error!", error.Message, "OK");
        }

    }
}

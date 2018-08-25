using StarWarsApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApp.ViewModels
{
    public class PeopleViewModel : BaseViewModel
    {
        private People _people;

        public override async Task Initialize(object parameters)
        {
            _people = parameters as People;
            Title = _people.name;

            await LoadData();
        }

        public People People
        {
            get
            {
                return _people;
            }
            set
            {
                _people = value; OnPropertyChanged();
            }
        }

        public PeopleViewModel() : base("")
        {
        }

        async Task LoadData()
        {
            if (IsBusy)
                return;

            Exception error = null;

            try
            {
                IsBusy = true;

                var people = await Api.GetPeopleAsync(_people.url);
                People = people;
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

using StarWarsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarWarsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeoplePage : ContentPage
    {
        private PeopleViewModel _vm;
        public PeopleViewModel ViewModel
        {
            get
            {
                if (_vm == null)
                    _vm = new PeopleViewModel();

                BindingContext = _vm;

                return (BindingContext as PeopleViewModel);
            }
        }

        public PeoplePage()
        {
            InitializeComponent();
        }
    }
}
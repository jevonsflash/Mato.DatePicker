using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mato.Sample.ViewModel;
using Xamarin.Forms;

namespace Mato.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
            this.Container.TranslateTo(0, -1000);
        }

        private string _current = "";

        private void CalendarView_OnOnFinishedSelected(object sender, IList<DateTime> e)
        {
            _current = string.Join(" - ", e.Select(c=>c.ToString("d")));
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var calv = MainCalendarView;
            calv.InitDate = InitStringEntry.Text;
            calv.IsMultipleSelect = IsMutiSelButton.IsToggled;
            this.Container.TranslateTo(0, 0);


        }

        private void ConfirmButton_OnClicked(object sender, EventArgs e)
        {
          var vm=  this.BindingContext as MainPageViewModel;
            vm.Result.Add(_current);
            this.Container.TranslateTo(0, 1000);
        }
    }
}

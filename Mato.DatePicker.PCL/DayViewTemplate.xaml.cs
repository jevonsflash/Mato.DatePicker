using System;
using Mato.DatePicker.Model;
using Xamarin.Forms;

namespace Mato
{
    public partial class DayViewTemplate : ContentView
    {
        public event EventHandler<DayModel> OnSelected;

        public DayViewTemplate(DayModel currentDay,DateTime initDate)
        {
            InitializeComponent();

            DayViewModel vm = new DayViewModel(currentDay,initDate);

            this.BindingContext = vm;
            
            vm.OnSelected += Vm_OnSelected;

            this.lblDay.AutomationId = string.Format("LabelDay_{0}", currentDay.Date.ToString("dd"));
        }

        private void Vm_OnSelected(object sender, DayModel e)
        {
            OnSelected?.Invoke(this, e);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mato.DatePicker.Model;
using Xamarin.Forms;

namespace Mato
{
    public partial class CalendarView : ContentView
    {
        public event EventHandler<IList<DateTime>> OnFinishedSelected;


        public CalendarView()
        {
            InitializeComponent();

        }

        private DateTime GetDate(string dateString)
        {
            var date = DateTime.Now;
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime.TryParse(InitDate, out date);
            }
            return date;
        }

        private void Init()
        {
           

                CalendarViewModel vm = new CalendarViewModel(IsMultipleSelect, GetDate(InitDate));
                BindingContext = vm;

                vm.OnMakeCalendar += Vm_OnMakeCalendar;

                vm.Draw();

                SetDaysOfWeekNames();

            

        }

        private bool _isMultipleSelect;

        public bool IsMultipleSelect
        {
            get { return _isMultipleSelect; }
            set
            {
                _isMultipleSelect = value;
                Init();
            }
        }



        public static readonly BindableProperty InitDateProperty =
            BindableProperty.Create(nameof(InitDate),
                typeof(string), typeof(CalendarView),
                "", propertyChanged: OnInitDatePropertyChanged);

        private static void OnInitDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CalendarView)?.Init();
        }

        public string InitDate
        {
            get { return (string)GetValue(InitDateProperty); }
            set { SetValue(InitDateProperty, value); }
        }

        

        private void Vm_OnMakeCalendar(object sender, DayModel[,] e)
        {
            StklSun.Children.Clear();
            StklMon.Children.Clear();
            StklTue.Children.Clear();
            StklWed.Children.Clear();
            StklThu.Children.Clear();
            StklFri.Children.Clear();
            StklSat.Children.Clear();

            for (int i = 0; i < e.GetLength(0); i++)
            {
                SetItem(StklSun, i, 0, e);
                SetItem(StklMon, i, 1, e);
                SetItem(StklTue, i, 2, e);
                SetItem(StklWed, i, 3, e);
                SetItem(StklThu, i, 4, e);
                SetItem(StklFri, i, 5, e);
                SetItem(StklSat, i, 6, e);
            }
        }

        private void SetItem(StackLayout stk, int line, int col, DayModel[,] calendar)
        {
            DayViewTemplate tpl = null;

            if (calendar[line, col] != null)
                tpl = new DayViewTemplate(calendar[line, col], GetDate(InitDate));
            else
                tpl = new DayViewTemplate(DayModel.GetDayNothing(), GetDate(InitDate));

            tpl.OnSelected += Tpl_OnSelected;

            stk.Children.Add(tpl);
        }

        private void Tpl_OnSelected(object sender, DayModel e)
        {
            DayClickAction(e);
            var vm = (CalendarViewModel)BindingContext;
            vm.UpdateSelectedDates();
            if (vm.SelectedDates != null)
            {
                this.SelectedDates = vm.SelectedDates.Select(c => c.Date).ToList();
            }

            this.OnFinishedSelected?.Invoke(this, this.SelectedDates);
        }
        private void DayClickAction(DayModel _dayModel)
        {
            if (IsMultipleSelect)
            {
                if (_dayModel.Date != DateTime.MinValue)
                    _dayModel.Selected = !_dayModel.Selected;
            }
            else
            {
                if (_dayModel.Date != DateTime.MinValue)
                {


                    _dayModel.Selected = !_dayModel.Selected;

                    var vm = (CalendarViewModel)BindingContext;
                    var oldselects = vm.SelectedDates;
                    foreach (var item in oldselects)
                    {
                        item.Selected = false;
                    }
                    _dayModel.Selected = true;

                }
            }
        }


        private IList<DateTime> _selectedDates;

        public IList<DateTime> SelectedDates
        {
            get
            {
                if (_selectedDates == null)
                {
                    _selectedDates = new List<DateTime>();
                }
                return _selectedDates;
            }
            private set
            {
                _selectedDates = value;

            }
        }
        private void SetDaysOfWeekNames()
        {
            LblSun.Text = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[0];
            LblMon.Text = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[1];
            LblTue.Text = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[2];
            LblWed.Text = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[3];
            LblThu.Text = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[4];
            LblFri.Text = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[5];
            LblSat.Text = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[6];
        }
    }
}

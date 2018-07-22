using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using Mato.DatePicker.Model;
using Xamarin.Forms;

namespace Mato
{
    public class CalendarViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DayModel[,]> OnMakeCalendar;

        private readonly CalendarModel _calendar;

        public CalendarViewModel(bool isMultipleSelect, DateTime initDate)
        {
            _calendar = new CalendarModel(initDate, isMultipleSelect);
            
            PriorCommand = new Command(() =>
            {
                _calendar.PriorMonth();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(YearMonthLabel)));

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDates)));

                RefreshChanges();
            });

            NextCommand = new Command(() =>
            {
                _calendar.NextMonth();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(YearMonthLabel)));

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDates)));


                RefreshChanges();
            });

        }

        #region [ Commands ]

        public ICommand NextCommand { get; protected set; }

        public ICommand PriorCommand { get; protected set; }

        #endregion

        public ObservableCollection<DayModel> SelectedDates
        {
            get
            {
                return _calendar.GetSelectedDates();
            }
        }

        public void Draw()
        {
            RefreshChanges();
        }

        public string YearMonthLabel
        {
            get { return $"{CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedMonthNames[_calendar.CurrentMonth - 1]} / {_calendar.CurrentYear} "; }
        }

        public void UpdateSelectedDates()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDates)));

        }
        public DayModel[,] GetCurrentDayModels()
        {
           return _calendar.CurrentCalendar;
        }
        private void RefreshChanges()
        {
            OnMakeCalendar?.Invoke(this, GetCurrentDayModels());
        }
    }
}

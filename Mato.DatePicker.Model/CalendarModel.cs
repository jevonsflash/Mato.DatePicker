using System;
using System.Collections.ObjectModel;

namespace Mato.DatePicker.Model
{
    public class CalendarModel
    {
        #region [ Constants ]

        private const int MAX_WEEKS = 6;
        private const int MAX_DAYS = 7;

        #endregion

        public bool IsMultipleSelect { get; set; }

        private DateTime _currentDate;

        private DayModel[,] currentCalendar;



        public DayModel[,] CurrentCalendar
        {
            get
            {
                return currentCalendar;
            }
        }

        public int CurrentYear
        {
            get
            {
                return _currentDate.Year;
            }
        }

        public int CurrentMonth
        {
            get
            {
                return _currentDate.Month;
            }
        }

        public ObservableCollection<DayModel> GetSelectedDates()
        {
            ObservableCollection<DayModel> selectedDates = new ObservableCollection<DayModel>();

            for (int i = 0; i < currentCalendar.GetLength(0); i++)
            {
                for (int j = 0; j < currentCalendar.GetLength(1); j++)
                {
                    if (currentCalendar[i, j] != null && currentCalendar[i, j].Selected)
                    {
                        selectedDates.Add(currentCalendar[i, j]);
                    }
                }
            }

            return selectedDates;
        }

        public ObservableCollection<DayModel> GetDates()
        {
            ObservableCollection<DayModel> selectedDates = new ObservableCollection<DayModel>();

            for (int i = 0; i < currentCalendar.GetLength(0); i++)
            {
                for (int j = 0; j < currentCalendar.GetLength(1); j++)
                {
                    if (currentCalendar[i, j] != null)
                    {
                        selectedDates.Add(currentCalendar[i, j]);
                    }
                }
            }

            return selectedDates;
        }

        public CalendarModel(DateTime baseDate, bool isMultipleSelect)
        {
            _currentDate = baseDate;
            this.IsMultipleSelect = isMultipleSelect;
            this.MakeCurrentCalendar();
        }

        public void NextMonth()
        {
            _currentDate = _currentDate.AddMonths(1);
            this.MakeCurrentCalendar();
        }

        public void PriorMonth()
        {
            _currentDate = _currentDate.AddMonths(-1);
            this.MakeCurrentCalendar();
        }

        private void MakeCurrentCalendar()
        {
            currentCalendar = new DayModel[MAX_WEEKS, MAX_DAYS];

            int lastDay = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

            DateTime cDate = new DateTime(_currentDate.Year, _currentDate.Month, 1);

            DayModel dayCellAux = null;

            for (int i = 1, line = 0; i <= lastDay; i++)
            {
                dayCellAux = new DayModel(cDate);

                currentCalendar[line, Convert.ToInt32(cDate.DayOfWeek)] = dayCellAux;

                if (cDate.DayOfWeek == DayOfWeek.Saturday)
                    line++;

                cDate = cDate.AddDays(1);
                //SetCurrentDate(cDate, dayCellAux);
            }


        }

        private void SetCurrentDate(DateTime cDate, DayModel dayCellAux)
        {
            if (cDate.Date == _currentDate.Date)
            {
                dayCellAux.Selected = true;
            }
        }
    }

}

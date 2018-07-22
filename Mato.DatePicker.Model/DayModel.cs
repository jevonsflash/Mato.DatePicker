using System;
using System.ComponentModel;

namespace Mato.DatePicker.Model
{
    public class DayModel: INotifyPropertyChanged
    {


        private bool _selected;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }

        public DateTime Date { get; protected set; }

        public DayModel(DateTime date,bool isSelected=false)
        {
            this.Date = date;
            Selected = isSelected;
        }

        public static DayModel GetDayNothing()
        {
            return new DayModel(DateTime.MinValue);
        }
    }
}

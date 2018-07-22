using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Mato.DatePicker.Model;
using Xamarin.Forms;

namespace Mato
{
    public class DayViewModel : INotifyPropertyChanged
    {
        private DayModel _dayModel;
        private readonly DateTime _initDate;
        private Color _selectedColor;
        private IChinaDateServer ChinaDateServer => DependencyService.Get<IChinaDateServer>();

        public event EventHandler<DayModel> OnSelected;

        public DayViewModel(DayModel dayModel, DateTime initDate)
        {
            _dayModel = dayModel;
            _initDate = initDate;
            _dayModel.PropertyChanged += _dayModel_PropertyChanged;
            DayClick = new Command(DayClickAction);
            this.SelectedColor = Color.Transparent;
            InitCurrentDate(dayModel);
            

        }

        private async void InitCurrentDate(DayModel dayModel)
        {
            if (dayModel.Date == _initDate)
            {
                await Task.Delay(200);
                DayClickAction();
            }
        }

        private void _dayModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
            {
                var accentBrush = Color.FromHex("3984E0");

                if (_dayModel.Selected)
                {
                    this.SelectedColor = accentBrush;
                }
                else
                {
                    this.SelectedColor = Color.Transparent;
                }
            }
        }

        private void DayClickAction()
        {
            if (OnSelected != null)
                OnSelected(this, this._dayModel);
        }


        public ICommand DayClick { get; protected set; }

        public string Text
        {
            get
            {
                if (_dayModel.Date != DateTime.MinValue)
                    return _dayModel.Date.Day.ToString();
                return " ";
            }
        }

        public string ChinaDateText
        {
            get
            {
                if (_dayModel.Date != DateTime.MinValue)
                    return ChinaDateServer.GetDay(_dayModel.Date);
                return " ";
            }
        }

        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                if (value != _selectedColor)
                {
                    _selectedColor = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedColor)));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

    }
}

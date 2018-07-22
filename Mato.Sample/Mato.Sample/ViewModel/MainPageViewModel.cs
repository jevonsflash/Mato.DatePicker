using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mato.Sample.Model;

namespace Mato.Sample.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public MainPageViewModel()
        {
            Result=new ObservableCollection<string>();

        }

        private ObservableCollection<string> _result;

        public ObservableCollection<string> Result
        {
            get { return _result; }
            set
            {
                _result = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));

            }
        }

     



        public event PropertyChangedEventHandler PropertyChanged;
    }
}

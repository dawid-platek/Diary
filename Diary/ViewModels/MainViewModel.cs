using Diary.Commands;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
        }

        public ICommand RefreshStudentsCommand { get; set; }

        private string _test = "XXX";

        public string Test
        {
            get { return _test; }
            set
            {
                _test = value;
                OnPropertyChanged();
            }
        }

        private void RefreshStudents(object obj)
        {
            MessageBox.Show(Test);
        }


    }
}

using Diary.Commands;
using Diary.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);

            Students = new ObservableCollection<Student>
            {
                new Student { FirstName = "Tomasz", LastName = "Kowalski", Group = new Group {Id = 1 } },
                new Student { FirstName = "Marek", LastName = "Nowak", Group = new Group {Id = 2 } },
                new Student { FirstName = "Stanisław", LastName = "Nowakowski", Group = new Group {Id = 1 } }
            };

            InitGroups();
        }

        public ICommand RefreshStudentsCommand { get; set; }



        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }


        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }


        private void RefreshStudents(object obj)
        {

        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group { Id = 0, Name = "Wszystkie" },
                new Group { Id = 1, Name = "1A" },
                new Group { Id = 2, Name = "2A" },
            };

            SelectedGroupId = 0;
        }

    }
}

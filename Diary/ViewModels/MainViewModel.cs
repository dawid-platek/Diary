﻿using Diary.Commands;
using Diary.Models;
using Diary.Models.Wrappers;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context.Students.ToList();
            }

            AddStudentsCommand = new RelayCommand(AddEditStudent);
            EditStudentsCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentsCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            RefreshDiary();

            InitGroups();
        }

        

        public ICommand AddStudentsCommand { get; set; }
        public ICommand EditStudentsCommand { get; set; }
        public ICommand DeleteStudentsCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }


        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
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

        private ObservableCollection<GroupWrapper> _group;

        public ObservableCollection<GroupWrapper> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<GroupWrapper>
            {
                new GroupWrapper { Id = 0, Name = "Wszystkie" },
                new GroupWrapper { Id = 1, Name = "1A" },
                new GroupWrapper { Id = 2, Name = "2A" },
            };

            SelectedGroupId = 0;
        }
        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {
           return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
           var metroWindow = Application.Current.MainWindow as MetroWindow;
           var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Tak",
                NegativeButtonText = "Nie"
            };

            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            //uswanie ucznia z bazy

            RefreshDiary(); 
        }

        private void AddEditStudent(object obj)
        {
           var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>
            {
                new StudentWrapper { FirstName = "Tomasz", LastName = "Kowalski", Group = new GroupWrapper {Id = 1 } },
                new StudentWrapper { FirstName = "Marek", LastName = "Nowak", Group = new GroupWrapper {Id = 2 } },
                new StudentWrapper { FirstName = "Stanisław", LastName = "Nowakowski", Group = new GroupWrapper {Id = 1 } }
            };
        }
    }
}

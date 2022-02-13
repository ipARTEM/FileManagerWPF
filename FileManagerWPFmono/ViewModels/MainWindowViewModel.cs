using FileManagerWPFmono.Commands;
using FileManagerWPFmono.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FileManagerWPFmono.Models;

namespace FileManagerWPFmono.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string _title= "FileManager";
        public string Title
        {
            get { return _title; }
            set=>Set(ref _title, value);
        }


        private ObservableCollection<string> _collList;
        public ObservableCollection<string> CollList
        {
            get { return _collList; }
            set => Set(ref _collList, value);
        }


        private List<DriveInfo> _pathLine;
        public List<DriveInfo> PathLine
        {
            get { return _pathLine; }
            set => Set(ref _pathLine, value);
        }





        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            CollList = new ObservableCollection<string>()
            {
                "df",
                "sdf"
            };

            PathLine=PathProcess.GetDrives();
        }

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        public static List<DriveInfo> GetDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<DriveInfo> newAllDrives = new List<DriveInfo>();
            foreach (var driveInfo in allDrives)
            {
                if (driveInfo.IsReady)
                {
                    newAllDrives.Add(driveInfo);
                }
            }
            return newAllDrives;
        }
    }
}

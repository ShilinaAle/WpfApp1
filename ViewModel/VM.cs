using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    class VM : INotifyPropertyChanged
    {
        private UrlTags utags;
        public UrlTags Utags
        {
            get { return utags; }
            set
            {
                utags = value; OnPropertyChanged("Utags");
            }
        }
        private ObservableCollection<UrlTags> _urlWithTagsList;
        public ObservableCollection<UrlTags> UrlWithTagsList
        {
            get
            {
                return _urlWithTagsList;
            }
            set
            {
                _urlWithTagsList = value;
                OnPropertyChanged();
            }
        }

        public VM()
        {
            UrlWithTagsList = new ObservableCollection<UrlTags>();
            //Generating();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new MyCommand(SubmitExecute, CanSubmitExecute);
                }
                return _SubmitCommand;
            }
        }


        private void SubmitExecute(object parameter)
        {
            UrlWithTagsList.Clear();
            //foreach (var i in Enumerable.Range(1, 10))
            //{
            //    UrlWithTagsList.Add(new Model.UrlWithTags("some url", i));
            //}
            Start();
        }
        //#region
        public void Start()
        {
            // Read the file and display it line by line.  
            foreach (string line in File.ReadLines(@"..\..\text.txt"))
            {
                //if (IsUrlValid(line))
                //{
                Process.Start("explorer.exe", line);
                UrlWithTagsList.Add(new UrlTags(line, 1));
                //}
            }
        }

        private bool IsUrlValid(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                string HTMLSource = wc.DownloadString(url);
                //Console.WriteLine($"url {url} is correct");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show($"url {url} is not correct");
                return false;
            }
        }

        //#endregion





        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }

        private ICommand _StopCommand;
        public ICommand StopCommand
        {
            get
            {
                if (_StopCommand == null)
                {
                    _StopCommand = new MyCommand(StopExecute, CanStopExecute);
                }
                return _StopCommand;
            }
        }


        private void StopExecute(object parameter)
        {
            UrlWithTagsList.Clear();
        }

        private bool CanStopExecute(object parameter)
        {
            if (UrlWithTagsList.Count == 0)
                return false;
            return true;
        }

        //public static void Generating()
        //{
        //    foreach (var i in Enumerable.Range(1, 10))
        //    {
        //        UrlWithTagsList.Add(new Model.UrlWithTags { Tags = i, Url = "some url" });
        //    }
        //}
    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WpfApp1.Command;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    class VM : INotifyPropertyChanged
    {
        private UrlTags utags;
        private ObservableCollection<UrlTags> _urlWithTagsList;
        public Dispatcher _dispatcher;
        private ICommand _StartCommand;
        private ICommand _StopCommand;
        bool _cancel = false;
        bool _canStart = true;


        public UrlTags Utags
        {
            get { return utags; }
            set
            {
                utags = value; 
                OnPropertyChanged("Utags");
            }
        }
        
        public ObservableCollection<UrlTags> UrlWithTagsList
        {
            get { return _urlWithTagsList; }
            set
            {
                _urlWithTagsList = value;
                OnPropertyChanged();
            }
        }

        public VM()
        {
            UrlWithTagsList = new ObservableCollection<UrlTags>();
            _dispatcher = Dispatcher.CurrentDispatcher;
        }
        
        public async Task StartAsync()
        {
            int i = 0;
            foreach (string line in File.ReadLines(@"..\..\text.txt"))
            {
                if (_cancel)
                    break;

                i++;
                await Task.Run(() =>
                {
                    if (IsUrlValid(line))
                    {
                        _dispatcher.Invoke(new Action(() =>
                        {
                            Process.Start("explorer.exe", line);
                            _urlWithTagsList.Add(new UrlTags(line, i));
                        }));
                    }
                });
            }
        }

        private bool IsUrlValid(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                string HTMLSource = wc.DownloadString(url);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show($"url {url} is not correct");
                return false;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand StartCommand
        {
            get
            {
                if (_StartCommand == null)
                {
                    _StartCommand = new MyCommand(StartExecute, CanStartExecute);
                }
                return _StartCommand;
            }
        }


        private async void StartExecute(object parameter)
        {
            UrlWithTagsList.Clear();
            _cancel = false;
            _canStart = false;
            await StartAsync();
        }

        private bool CanStartExecute(object parameter)
        {
            if (_canStart == false)
                return false;
            return true;
        }

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
            _cancel = true;
            _canStart = true;
        }

        private bool CanStopExecute(object parameter)
        {
            if (UrlWithTagsList.Count == 0 || _cancel == true)
                return false;
            return true;
        }
    }
}

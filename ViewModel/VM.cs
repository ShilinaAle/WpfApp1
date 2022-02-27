﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        int _countOfUrl = 0;
        string _maxUrl = "";
        int _maxTags = -1;
        int _invalidTags = 0;
        
        public VM()
        {
            UrlWithTagsList = new ObservableCollection<UrlTags>();
            _dispatcher = Dispatcher.CurrentDispatcher;
        }
        
        public async Task StartAsync()
        {
            foreach (string line in File.ReadLines(@"..\..\text.txt"))
            {
                
                if (_cancel)
                    break;

                await Task.Run(() =>
                {
                    CountOfUrl++;
                    if (IsUrlValid(line))
                    {
                        _dispatcher.Invoke(new Action(() =>
                        {
                            int count = CountTeg(line);
                            if (count > _maxTags)
                            {
                                MaxUrl = line;
                                _maxTags = count;
                            }
                            Process.Start(line);
                            _urlWithTagsList.Add(new UrlTags(line, count));
                        }));
                    }
                });
            }
        }

        public int CountTeg(string url)
        {
            var hc = new HttpClient();
            var result = hc.GetStringAsync(url).Result;

            Regex regex = new Regex(@"<a ");
            MatchCollection matches = regex.Matches(result);
            if (matches.Count > 0)
            {
                return matches.Count;
            }
            Console.WriteLine("Совпадений не найдено");
            return 0;
        }

        public bool IsUrlValid(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                string HTMLSource = wc.DownloadString(url);
                return true;
            }
            catch (Exception)
            {
                InvalidTags++;
                return false;
            }
        }

        
        public int InvalidTags
        {
            get { return _invalidTags; }
            set
            {
                _invalidTags = value;
                OnPropertyChanged();
            }
        }

        public string MaxUrl
        {
            get { return _maxUrl; }
            set
            {
                _maxUrl = value;
                OnPropertyChanged();
            }
        }

        public int CountOfUrl
        {
            get { return _countOfUrl; }
            set
            {
                _countOfUrl = value;
                OnPropertyChanged();
            }
        }
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
            CountOfUrl = 0;
            InvalidTags = 0;
            await StartAsync();
            _cancel = true;
            _canStart = true;
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

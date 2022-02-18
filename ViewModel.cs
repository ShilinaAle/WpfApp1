using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    class ViewModel
    {
        private MainWindow window;
        string maxUrl = "-1";
        int maxTegs = -1;
        public ViewModel(MainWindow _window)
        {
            window = _window;
        }
        
        public void MainViewModel()
        {
            foreach (var i in Enumerable.Range(1, 10))
            {
                window.listView.Items.Add(new Model { Count = i, Url = "some url" });
            }

            //try
            //{
            //    foreach (string line in File.ReadLines(@"..\..\..\text.txt"))
            //    {

            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Авторизация не пройдена");
            //}

        }
    }
}

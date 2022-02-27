using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ViewModel
{
    class UrlProcessing
    {
        public static bool IsUrlValid(string url)
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

        public static int CountTeg(string url)
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
    }
}

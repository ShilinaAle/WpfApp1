using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    class UrlTags
    {
        private string url;
        private int tags;
        public UrlTags(string url, int count)
        {
            this.url = url;
            tags = count;
        }
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                //OnPropertyChange("Url");
            }
        }
        public int Tags
        {
            get
            {
                return tags;
            }
            set
            {
                tags = value;
                //OnPropertyChange("Tags");
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChange([CallerMemberName]string propertyName = null)
        //{
        //    //if (propertyName != null)
        //    //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}

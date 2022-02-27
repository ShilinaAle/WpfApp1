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
            }
        }
    }
}

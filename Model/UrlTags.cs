namespace WpfApp1.Model
{
    /// <summary>
    /// Количество тэгов на конкретный url
    /// </summary>
    class UrlTags
    {
        string _url;
        int _tags;

        public UrlTags(string url, int count)
        {
            _url = url;
            _tags = count;
        }

        public string Url
        {
            get
            { return _url; }
            set
            { _url = value; }
        }

        public int Tags
        {
            get
            { return _tags; }
            set
            { _tags = value; }
        }
    }
}

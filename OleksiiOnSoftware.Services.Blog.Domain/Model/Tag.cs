namespace OleksiiOnSoftware.Services.Blog.Domain.Model
{
    class Tag
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public Tag(string url, string title)
        {
            Url = url;
            Title = title;
        }

        protected bool Equals(Tag other)
        {
            return string.Equals(Url, other.Url);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Tag) obj);
        }

        public override int GetHashCode()
        {
            return Url?.GetHashCode() ?? 0;
        }
    }
}

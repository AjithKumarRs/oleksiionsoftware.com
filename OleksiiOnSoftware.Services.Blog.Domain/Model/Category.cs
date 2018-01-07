using System.Collections.Generic;

namespace OleksiiOnSoftware.Services.Blog.Domain.Model
{
    class Category
    {
        public string Url { get; }

        public string Title { get; set; }

        public Categories Categories { get; set; }

        public List<Post> Posts { get; set; }

        public Category(string url)
        {
            Url = url;
            Categories = new Categories();
            Posts = new List<Post>();
        }

        public Category(string url, string title) : this(url)
        {
            Title = title;
        }

        protected bool Equals(Category other)
        {
            return string.Equals(Url, other.Url);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Category)obj);
        }

        public override int GetHashCode()
        {
            return Url?.GetHashCode() ?? 0;
        }
    }
}
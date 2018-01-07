namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class BlogStartedEvent : Event
    {
        public string Brand { get; }
        public string Copyright { get; }
        public string Avatar { get; }
        public string Github { get; }
        public string Linkedin { get; }
        public string Twitter { get; }

        public BlogStartedEvent(string aggregateId, string brand, string copyright, string avatar, string github, string linkedin, string twitter) : base(aggregateId)
        {
            Brand = brand;
            Copyright = copyright;
            Avatar = avatar;
            Github = github;
            Linkedin = linkedin;
            Twitter = twitter;
        }

        protected bool Equals(BlogStartedEvent other)
        {
            return base.Equals(other) && string.Equals(Brand, other.Brand) && string.Equals(Copyright, other.Copyright) && string.Equals(Avatar, other.Avatar) && string.Equals(Github, other.Github) && string.Equals(Linkedin, other.Linkedin) && string.Equals(Twitter, other.Twitter);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BlogStartedEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Brand != null ? Brand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Copyright != null ? Copyright.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Avatar != null ? Avatar.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Github != null ? Github.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Linkedin != null ? Linkedin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Twitter != null ? Twitter.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}

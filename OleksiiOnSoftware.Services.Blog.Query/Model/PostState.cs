namespace OleksiiOnSoftware.Services.Blog.Query.Model
{
    using System;
    using System.Collections.Generic;

    public class PostState
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string BodyShort { get; set; }

        public DateTime PublishAt { get; set; }

        public string CategoryTitle { get; set; }

        public string CategoryUrl { get; set; }

        public bool Infobar { get; set; }

        public bool Comments { get; set; }

        public bool IsHidden { get; set; }

        public List<TagState> Tags { get; set; }

        public PostState()
        {
            Tags = new List<TagState>();
        }
    }
}

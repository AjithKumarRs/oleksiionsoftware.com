namespace OleksiiOnSoftware.Services.Blog.Client.Impl
{
    using Dto;
    using Newtonsoft.Json;
    using OleksiiOnSoftware.Services.Blog.Query.Views;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public class BlogApiClient : IBlogClient
    {
        private readonly HttpClient _httpClient;

        public BlogApiClient(string url)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(url) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<string>> GetBlogs()
        {
            var json = await _httpClient.GetStringAsync("/api/blogs");
            return JsonConvert.DeserializeObject<List<string>>(json);
        }

        public async Task<HomeView> GetBlog(string blogId)
        {
            var json = await _httpClient.GetStringAsync($"/api/blogs/{blogId}");
            return JsonConvert.DeserializeObject<HomeView>(json);
        }

        public async Task CreateBlog(BlogDto blog)
        {
            var json = JsonConvert.SerializeObject(blog);
            var resp = await _httpClient.PostAsync($"/api/blogs", new StringContent(json, Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
        }

        public async Task CreatePost(PostDto post)
        {
            var json = JsonConvert.SerializeObject(post);
            var resp = await _httpClient.PostAsync($"/api/blogs/{post.BlogId}/posts",
                new StringContent(json, Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
        }

        public async Task CreateCategory(CategoryDto category)
        {
            var json = JsonConvert.SerializeObject(category);
            var resp = await _httpClient.PostAsync($"/api/blogs/{category.BlogId}/categories",
                new StringContent(json, Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
        }

        public async Task CreateTag(TagDto tag)
        {
            var json = JsonConvert.SerializeObject(tag);
            var resp = await _httpClient.PostAsync($"/api/blogs/{tag.BlogId}/tags",
                new StringContent(json, Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
        }

        public async Task CreateLink(LinkDto link)
        {
            var json = JsonConvert.SerializeObject(link);
            var resp = await _httpClient.PostAsync($"/api/blogs/{link.BlogId}/links", new StringContent(json, Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteBlog(string blogId)
        {
            var resp = await _httpClient.DeleteAsync($"/api/blogs/{blogId}");
            resp.EnsureSuccessStatusCode();
        }
    }
}

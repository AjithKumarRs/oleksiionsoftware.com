using System;
using System.Collections.Generic;
using System.Linq;

namespace OleksiiOnSoftware.Services.Blog.Domain.Model
{
    class Categories : List<Category>
    {
        public void AddCategory(string url, string title)
        {
            var categories = GetCategories(url);
            var categoryUrl = GetPathItems(url).Last();
            categories.Add(new Category(categoryUrl, title));
        }

        public Category GetCategory(IList<string> path)
        {
            var firstSegment = path.FirstOrDefault();

            var category = this.FirstOrDefault(_ => string.Equals(_.Url, firstSegment, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                return null;
            }

            foreach (var segment in path.Skip(1))
            {
                category = category.Categories.FirstOrDefault(_ => string.Equals(_.Url, segment, StringComparison.OrdinalIgnoreCase));
                if (category == null)
                {
                    return null;
                }
            }

            return category;
        }

        public Category GetCategory(string path)
        {
            var items = GetPathItems(path);
            return GetCategory(items);
        }

        public Category GetParentCategory(string path)
        {
            var items = GetPathItems(path);
            return GetCategory(new ArraySegment<string>(items, 0, items.Length - 1));
        }

        public Categories GetCategories(string categoryPath)
        {
            var items = GetPathItems(categoryPath);
            if (items.Length == 1)
            {
                return this;
            }

            var childPath = items
                .Skip(1)
                .Take(categoryPath.Length - 1)
                .ToList();

            var category = this.FirstOrDefault(_ => string.Equals(_.Url, items[0], StringComparison.OrdinalIgnoreCase));
            return category?.Categories.GetCategories(childPath);
        }

        public Categories GetCategories(List<string> categoryPath)
        {
            if (categoryPath.Count == 1)
            {
                return this;
            }

            var childPath = categoryPath
                .Skip(1)
                .Take(categoryPath.Count - 1)
                .ToList();

            return GetCategories(childPath);
        }

        private static string[] GetPathItems(string path)
        {
            return path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
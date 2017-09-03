using Gec.Models.Gec;
using Gec.Models.Playground;
using Gec.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gec.Helpers.Blogs
{
    public static class BlogsMapper
    {
        public static Feed newBlog(this BlogViewModel feed)
        {
            var newFeed = new Feed
            {
                
                Name = feed.Name,
                Title = feed.Title,
                FeedType =feed.FeedType,
                Article = feed.Article

                //        public int Stars { get; set; }
                //public string Name { get; set; }
                //public string Title { get; set; }
                //public string Subtitle { get; set; }
                //public string Article { get; set; }
                //public DateTime DateCreated { get; set; }
                //public DateTime? DateArchived { get; set; }
                //public int PictureId { get; set; }
                //public Picture Picture { get; set; }
                //public int? Id { get; set; }
                //public User User { get; set; }
                //public bool IsArchived { get; set; }
                //public string FeedType { get; set; }
                //public int Likes { get; set; }
                //public int UnLikes { get; set; }
                //public ICollection<Comment> Comments { get; set; }

            };
            return newFeed;
        }
        public static BlogViewModel BlogVM(this Feed newFeed)
        {
            var feed = new BlogViewModel
            {

                Name = newFeed.Name,
                Title = newFeed.Title,
                FeedType = newFeed.FeedType,
                Article = newFeed.Article

            };
            return feed;
        }
        public static List<Feed> ListBlogs(this List<BlogViewModel> blogs)
        {
            var listBlogs = new List<Feed>();
            foreach (var blog in blogs)
            {
                listBlogs.Add(new Feed
                {
                    Name = blog.Name,
                    Title = blog.Title,
                    FeedType = blog.FeedType,
                    Article = blog.Article

                });
            }
            return listBlogs;
        }
        public static List<BlogViewModel> ListBlogsVM(this List<Feed> blogs)
        {
            var listBlogs = new List<BlogViewModel>();
            foreach (var blog in blogs)
            {
                listBlogs.Add(new BlogViewModel
                {
                    Name = blog.Name,
                    Title = blog.Title,
                    FeedType = blog.FeedType,
                    Article = blog.Article
                });
            }
            return listBlogs;
        }

    }
}
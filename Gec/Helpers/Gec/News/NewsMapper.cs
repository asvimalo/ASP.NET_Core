using Gec.Models.Gec;
using Gec.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Helpers.Gec.News
{
    public static class NewsMapper
    {
        public static Feed newNews(this NewsViewModel news)
        {
            var newNews = new Feed
            {
                Article = news.Article,
                Title = news.Title,
                Name = news.Name,
                FeedType = news.FeedType

            };
            return newNews;
        }
        public static NewsViewModel NewsVM(this Feed newNews)
        {
            var news = new NewsViewModel
            {
                Article = newNews.Article,
                Title = newNews.Title,
                Name = newNews.Name,
                FeedType = newNews.FeedType

            };
            return news;
        }
        public static List<Feed> ListNews(this List<NewsViewModel> newsList)
        {
            var listNews = new List<Feed>();
            foreach (var news in newsList)
            {
                listNews.Add(new Feed
                {
                    Article = news.Article,
                    Title = news.Title,
                    Name = news.Name,
                    FeedType = news.FeedType
                });
            }
            return listNews;
        }
        public static List<NewsViewModel> ListNewsVM(this List<Feed> listNews)
        {
            var newsList = new List<NewsViewModel>();
            foreach (var news in listNews)
            {
                newsList.Add(new NewsViewModel
                {
                    Article = news.Article,
                    Title = news.Title,
                    Name = news.Name,
                    FeedType = news.FeedType

                });
            }
            return newsList;
        }
    }
}

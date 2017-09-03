using Gec.EF.IRepo;
using Gec.Helpers.Gec.News;
using Gec.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Controllers.Api
{
    
    [Route("/api/news")]
    public class NewsController : Controller
    {
        private ILogger<NewsController> _logger;
        private IFeedRepo _repo;

        public NewsController(IFeedRepo repo, ILogger<NewsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                //var result = _repo.GetTripsByUsername(this.User.Identity.Name);
                var allNewsVm = NewsMapper.ListNewsVM(_repo.GetAll("news").ToList());
                return Ok(allNewsVm);
            }
            catch (Exception ex)
            {
                // TODO LOGGING
                _logger.LogError($"Failed to get all news: {ex}");
                return BadRequest("Error occured");
            }
            //var news = _repo.GetAll("blog");
            //return Ok(news);
        }
        [HttpGet("{id}", Name = "NewsGet")]
        public IActionResult Get(int id)
        {
            try
            {
                var feed = _repo.Get(id);
                if (feed == null)
                {
                    return NotFound($"News {id} was not found");

                }
                else
                {
                    return Ok(feed);
                }
            }
            catch (Exception)
            {


            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]NewsViewModel news)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Creating a new News");
                    //ClaimsPrincipal currentUser = this.User;

                    var newNews = NewsMapper.newNews(news);

                    //newNews.User.UserName = User.Identity.Name;
                    _repo.Add(newNews);

                    if (await _repo.SaveChangesAsync())
                    {
                        var newUri = Url.Link("NewsGet", new { id = newNews.FeedId });
                        return Created(newUri, NewsMapper.NewsVM(newNews));
                        //return Created($"api/news/{newNews.FeedId}", NewsMapper.NewsVM(newNews));
                    }
                    else
                    {
                        _logger.LogWarning("Couldn't save news to the database");
                    }

                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Threw exception while saving News: {ex}");
                return BadRequest("Error ocurred");
            }

            return BadRequest("Failed to save the trip");
        }
        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] NewsViewModel news)
        {
            try
            {
                _logger.LogInformation($"Updating blog {news.Title}");
                var oldNews = _repo.Get(id);
                oldNews.Article = news.Article ?? oldNews.Article;
                oldNews.Title = news.Title ?? oldNews.Article;
                oldNews.Name = news.Name ?? oldNews.Name;
                oldNews.FeedType = news.FeedType ?? oldNews.FeedType;

                var updatedNews = _repo.Update(oldNews);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(updatedNews);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Threw exception updating blog: {ex}");
                return BadRequest("Could not update blog");
            }
            return BadRequest("Couldn't update Blog");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _repo.Delete(id);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok("News deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception deleting news: {ex}");

            }
            return BadRequest("Could not delete News");
        }

    }
}

using Gec.EF.IRepo;
using Gec.Helpers.Blogs;
using Gec.Models.Account;
using Gec.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Controllers.Api
{

    [Route("/api/blogs")]
    
    public class BlogsController : Controller
    {
        private ILogger<BlogsController> _logger;
        private IFeedRepo _repo;
        private UserManager<User> _userManager;

        public BlogsController(IFeedRepo repo,
            ILogger<BlogsController> logger,
            UserManager<User> userManager)
        {
            _repo = repo;
            _logger = logger;
            _userManager = userManager;
        }
        [Authorize]
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                //var result = _repo.GetTripsByUsername(this.User.Identity.Name);
                var allBlogsVm = BlogsMapper.ListBlogsVM(_repo.GetAll("blog").ToList());
                return Ok(allBlogsVm);
            }
            catch (Exception ex)
            {
                // TODO LOGGING
                _logger.LogError($"Failed to get all blogs: {ex}");
                return BadRequest("Error occured");
            }
            //var news = _repo.GetAll("blog");
            //return Ok(news);
        }
        [HttpGet("{id}", Name = "BlogGet")]
        public IActionResult Get(int id)
        {
            try
            {
                var feed = _repo.Get(id);
                if (feed == null)
                {
                    return NotFound($"Blog {id} was not found");

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
        
        [Authorize(Policy ="SuperUsers")]
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]BlogViewModel blog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Creating a new blog");
                    var newBlog = BlogsMapper.newBlog(blog);
                    var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                    //And not...
                    //newBlog.User.UserName = User.Identity.Name;
                    if (user != null)
                    {
                        newBlog.User = user;


                        _repo.Add(newBlog);

                        if (await _repo.SaveChangesAsync())
                        {
                            var newUri = Url.Link("BlogGet", new { id = newBlog.FeedId });
                            return Created(newUri, BlogsMapper.BlogVM(newBlog));
                        }
                        else
                        {
                            _logger.LogWarning("Couldn't save blog to the database");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"THrew exception while saving blog: {ex}");
                return BadRequest("Error occured");

            }
            return BadRequest("Failed to save the trip");
        }
        [Authorize]
        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BlogViewModel blog)
        {
            try
            {

                _logger.LogInformation($"Updating blog {blog.Title}");
                var oldBlog = _repo.Get(id);
                oldBlog.Article = blog.Article ?? oldBlog.Article;
                oldBlog.Title = blog.Title ?? oldBlog.Article;
                oldBlog.Name = blog.Name ?? oldBlog.Name;
                oldBlog.FeedType = blog.FeedType ?? oldBlog.FeedType;
                //var newBlog = BlogsMapper.newBlog(blog);
                if (oldBlog.User.UserName != this.User.Identity.Name) return Forbid();

                var updatedBlog = _repo.Update(oldBlog);
                if (await _repo.SaveChangesAsync())
                {
                    var newUri = Url.Link("BlogGet", new { id = updatedBlog.FeedId });
                    return Created(newUri, BlogsMapper.BlogVM(updatedBlog));
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Threw exception updating blog: {ex}");
                return BadRequest("Could not update blog");
            }
            return BadRequest("Couldn't update Blog");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Double object request, here and in repo
                var blogToDel = _repo.Get(id);
                if (blogToDel.User.UserName != this.User.Identity.Name) return Forbid();
                _repo.Delete(id);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok("Blog deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Threw exception deleting blog: {ex}");

            }
            return BadRequest("Could not delete Blog");
        }

    }
}

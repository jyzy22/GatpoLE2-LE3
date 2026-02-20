using BlogDataLibrary;
using BlogDataLibrary.Data;
using BlogAPI.Models;
using BlogDataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private ISqlData _db;

        public PostController(ISqlData db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult ListPosts()
        {
            try
            {
                var posts = _db.ListPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult ShowPostDetails(int id)
        {
            try
            {
                var post = _db.ShowPostDetails(id);
                if (post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddPost([FromBody] PostForm postForm)
        {
            try
            {
                var currentUserId = GetCurrentUserId();

                var post = new PostModel
                {
                    UserId = currentUserId,
                    Title = postForm.Title,
                    Content = postForm.Body,
                    CreatedAt = DateTime.Now
                };

                _db.AddPost(post);
                return Ok(new { message = "Post added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            throw new Exception("User ID not found in token");
        }
    }
}
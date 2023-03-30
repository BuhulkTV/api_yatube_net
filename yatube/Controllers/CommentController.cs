using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using yatube.Models;
using yatube.Repositories;

namespace yatube.Controllers
{
    [Route("api/post/{post_id}/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICommentRepositorie _commentRepositorie;
        public CommentController(IConfiguration configuration, ICommentRepositorie commentRepositorie)
        {
            _configuration = configuration;
            _commentRepositorie = commentRepositorie;
        }
        [HttpGet]
        public List<CommentForGet> Get(int post_id)
        {
            return _commentRepositorie.GetComments(post_id);
        }
        [HttpPost]
        public string Post(CommentForCreate comment, int post_id)
        {
            _commentRepositorie.AddComment(comment, post_id);
            return "Added Successfully";
        }
        [HttpPut]
        public string Put(CommentForReplace com, int post_id)
        {
            _commentRepositorie.ReplaceComment(com, post_id);
            return "Updated Successfully";
        }
        [HttpDelete("{id}")]
        public string Delete(int id, int post_id)
        {
            _commentRepositorie.RemoveComment(id, post_id);
            return "Delete Successfully";
        }
    }
}

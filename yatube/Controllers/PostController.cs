using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using yatube.Models;
using yatube.Repositories;

namespace yatube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPostRepositorie _postRepositorie;
        public PostController(IConfiguration configuration, IPostRepositorie postRepositorie)
        {
            _configuration = configuration;
            _postRepositorie = postRepositorie;
        }
        [HttpGet]
        public List<PostForGet> Get()
        {
            return _postRepositorie.GetPosts();
        }
        [HttpPost]
        public string Post(PostForCreate post) 
        {
            _postRepositorie.AddPost(post);
            return "Added Successfully";
        }
        [HttpPut]
        public string Put(Post post)
        {
            _postRepositorie.ReplacePost(post);
            return "Updated Successfully";
        }
        [HttpDelete("{id}")]
        public string Delete(int id)
        {

            _postRepositorie.RemovePost(id);
            return "Delete Successfully";
        }
    }
}

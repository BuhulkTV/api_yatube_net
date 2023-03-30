using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using yatube.Models;
using yatube.Repositories;

namespace yatube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthorRepositorie _authorRepositorie;
        public AuthorController(IConfiguration configuration, IAuthorRepositorie authorRepositorie)
        {
            _configuration = configuration;
            _authorRepositorie = authorRepositorie;
        }
        [HttpGet]
        public List<Author> Get()
        {
            return _authorRepositorie.GetAuthors();
        }
        [HttpPost]
        public string Post(AuthorForCreate aut)
        {
            _authorRepositorie.AddAuthor(aut);
            return "Added Successfully";
        }
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            _authorRepositorie.RemoveAuthor(id);
            return "Delete Successfully";
        }
    }
}

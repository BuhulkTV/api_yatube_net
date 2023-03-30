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
    public class GroupsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IGroupRepositorie _groupRepositorie;
        public GroupsController(IConfiguration configuration, IGroupRepositorie groupRepositorie)
        {
            _configuration = configuration;
            _groupRepositorie = groupRepositorie;
        }
        [HttpGet]
        public List<GroupsForGet> Get()
        {
            return _groupRepositorie.GetGroups();
        }
        [HttpGet("{id}")]
        public GroupForGet Get(int id)
        {
            return _groupRepositorie.GetGroup(id);
        }
        [HttpPost]
        public string Post(GroupsForCreate group)
        {
            _groupRepositorie.AddGroup(group);
            return "Added Successfully";
        }
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            _groupRepositorie.RemoveGroup(id);
            return "Delete Successfully";
        }
    }
}

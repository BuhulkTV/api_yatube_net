using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using yatube.Models;

namespace yatube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PostController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                SELECT post_id, text, pub_date, name, title
                FROM post
                JOIN author ON post.author_id = author.author_id
                LEFT JOIN groups ON post.group_id = groups.group_id
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                SELECT text, pub_date, name, title
                FROM post
                JOIN author ON post.author_id = author.author_id
                LEFT JOIN groups ON post.group_id = groups.group_id
                WHERE post_id = @post_id
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@post_id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Post pos) 
        {
            string query = @"
                INSERT INTO post(text,pub_date,author_id,group_id)
                VALUES (@text,@pub_date,@author_id,@group_id)
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@post_id", pos.PostId);
                    myCommand.Parameters.AddWithValue("@text", pos.Text);
                    myCommand.Parameters.AddWithValue("@pub_date", Convert.ToDateTime(pos.PubDate));
                    myCommand.Parameters.AddWithValue("@author_id", pos.AuthorId);
                    myCommand.Parameters.AddWithValue("@group_id", pos.GroupId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(Post pos)
        {
            string query = @"
                update post
                set text = @text,
                pub_date = @pub_date,
                author_id = @author_id,
                group_id = @group_id
                where post_id = @post_id
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@post_id", pos.PostId);
                    myCommand.Parameters.AddWithValue("@text", pos.Text);
                    myCommand.Parameters.AddWithValue("@pub_date", Convert.ToDateTime(pos.PubDate));
                    myCommand.Parameters.AddWithValue("@author_id", pos.AuthorId);
                    myCommand.Parameters.AddWithValue("@group_id", pos.GroupId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                DELETE FROM post
                WHERE post_id = @post_id
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@post_id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Delete Successfully");
        }
    }
}

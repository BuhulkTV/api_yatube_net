using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using yatube.Models;

namespace yatube.Controllers
{
    [Route("api/post/{post_id}/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CommentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get(int post_id)
        {
            string query = @"
                SELECT comment_id, text, created, name, post_id
                FROM comment
                JOIN author ON comment.author_id = author.author_id
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
                    myCommand.Parameters.AddWithValue("@post_id", post_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Comment com, int post_id)
        {
            string query = @"
                INSERT INTO comment(text,created,author_id,post_id)
                VALUES (@text,@created,@author_id,@post_id)
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@comment_id", com.CommentId);
                    myCommand.Parameters.AddWithValue("@text", com.Text);
                    myCommand.Parameters.AddWithValue("@created", Convert.ToDateTime(com.Created));
                    myCommand.Parameters.AddWithValue("@author_id", com.AuthorId);
                    myCommand.Parameters.AddWithValue("@post_id", post_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(Comment com, int post_id)
        {
            string query = @"
                update comment
                set text = @text,
                created = @created,
                author_id = @author_id
                where comment_id = @comment_id and post_id = @post_id
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@comment_id", com.CommentId);
                    myCommand.Parameters.AddWithValue("@text", com.Text);
                    myCommand.Parameters.AddWithValue("@created", Convert.ToDateTime(com.Created));
                    myCommand.Parameters.AddWithValue("@author_id", com.AuthorId);
                    myCommand.Parameters.AddWithValue("@post_id", post_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id, int post_id)
        {
            string query = @"
                DELETE FROM comment
                WHERE post_id = @post_id and comment_id = @comment_id
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@comment_id", id);
                    myCommand.Parameters.AddWithValue("@post_id", post_id);
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

using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using yatube.Models;

namespace yatube.Repositories
{
    public class CommentRepositorie : ICommentRepositorie
    {
        private readonly IConfiguration _configuration;

        public CommentRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<CommentForGet> GetComments(int postId)
        {
            string query = @"
                SELECT comment_id, text, created, name, post_id
                FROM comment
                JOIN author ON comment.author_id = author.author_id
                WHERE post_id = @post_id
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            var comments = new List<CommentForGet>();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@post_id", postId);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        comments.Add(new CommentForGet() { 
                            CommentId = myReader.GetInt32(0),
                            Text = myReader.GetString(1),
                            Created = myReader.GetDateTime(2).ToString(),
                            Name = myReader.GetString(3),
                            PostId = myReader.GetInt32(4) });
                    }

                    myReader.Close();
                    myCon.Close();
                }
            }
            return comments;
        }
        public void AddComment(CommentForCreate comment, int postId)
        {
            string query = @"
                INSERT INTO comment(text,created,author_id,post_id)
                VALUES (@text,@created,@author_id,@post_id)
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@text", comment.Text);
                    myCommand.Parameters.AddWithValue("@created", Convert.ToDateTime(comment.Created));
                    myCommand.Parameters.AddWithValue("@author_id", comment.AuthorId);
                    myCommand.Parameters.AddWithValue("@post_id", postId);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
        public void ReplaceComment(CommentForReplace comment, int postId) 
        {
            string query = @"
                update comment
                set text = @text,
                created = @created,
                author_id = @author_id
                where comment_id = @comment_id and post_id = @post_id
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@comment_id", comment.CommentId);
                    myCommand.Parameters.AddWithValue("@text", comment.Text);
                    myCommand.Parameters.AddWithValue("@created", Convert.ToDateTime(comment.Created));
                    myCommand.Parameters.AddWithValue("@author_id", comment.AuthorId);
                    myCommand.Parameters.AddWithValue("@post_id", postId);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
        public void RemoveComment(int id, int postId)
        {
            string query = @"
                DELETE FROM comment
                WHERE post_id = @post_id and comment_id = @comment_id
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@comment_id", id);
                    myCommand.Parameters.AddWithValue("@post_id", postId);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }

        }
    }
}

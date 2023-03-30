using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using yatube.Models;

namespace yatube.Repositories
{
    public class PostRepositorie : IPostRepositorie
    {
        private readonly IConfiguration _configuration;
        public PostRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<PostForGet> GetPosts()
        {
            string query = @"
                SELECT post_id, text, pub_date, name, title
                FROM post
                JOIN author ON post.author_id = author.author_id
                LEFT JOIN groups ON post.group_id = groups.group_id
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            var posts = new List<PostForGet>();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        if(myReader.IsDBNull(4))
                        {
                            posts.Add(new PostForGet
                            {
                                PostId = myReader.GetInt32(0),
                                Text = myReader.GetString(1),
                                PubDate = myReader.GetDateTime(2).ToString(),
                                Name = myReader.GetString(3),
                                Title = "None"
                            });
                        }
                        else
                        {
                            posts.Add(new PostForGet
                            {
                                PostId = myReader.GetInt32(0),
                                Text = myReader.GetString(1),
                                PubDate = myReader.GetDateTime(2).ToString(),
                                Name = myReader.GetString(3),
                                Title = myReader.GetString(4)
                            });
                        }
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }
            return posts;
        }
        public void AddPost(PostForCreate post)
        {
            string query = @"
                INSERT INTO post(text,pub_date,author_id,group_id)
                VALUES (@text,@pub_date,@author_id,@group_id)
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@text", post.Text);
                    myCommand.Parameters.AddWithValue("@pub_date", Convert.ToDateTime(post.PubDate));
                    myCommand.Parameters.AddWithValue("@author_id", post.AuthorId);
                    myCommand.Parameters.AddWithValue("@group_id", post.GroupId);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
        public void ReplacePost(Post post)
        {
            string query = @"
                update post
                set text = @text,
                pub_date = @pub_date,
                author_id = @author_id,
                group_id = @group_id
                where post_id = @post_id
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@post_id", post.PostId);
                    myCommand.Parameters.AddWithValue("@text", post.Text);
                    myCommand.Parameters.AddWithValue("@pub_date", Convert.ToDateTime(post.PubDate));
                    myCommand.Parameters.AddWithValue("@author_id", post.AuthorId);
                    myCommand.Parameters.AddWithValue("@group_id", post.GroupId);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
        public void RemovePost(int postId)
        {
            string query = @"
                DELETE FROM post
                WHERE post_id = @post_id
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@post_id", postId);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
    }
}

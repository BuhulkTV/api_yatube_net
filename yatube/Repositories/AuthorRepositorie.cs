using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using yatube.Models;

namespace yatube.Repositories
{
    public class AuthorRepositorie : IAuthorRepositorie
    {
        private readonly IConfiguration _configuration;

        public AuthorRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Author> GetAuthors() 
        {
            string query = @"
                SELECT author_id, name
                FROM author
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            var authors = new List<Author>();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        authors.Add(new Author { AuthorId = myReader.GetInt32(0), Name = myReader.GetString(1) });
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }
            return authors;
        }
        
        public void AddAuthor(AuthorForCreate author)
        {
            string query = @"
                INSERT INTO author(name)
                VALUES (@name)
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@name", author.Name);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
        public void RemoveAuthor(int authorId)
        {
            string query = @"
                DELETE FROM author
                WHERE author_id = @author_id
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@author_id", authorId);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
    }
}

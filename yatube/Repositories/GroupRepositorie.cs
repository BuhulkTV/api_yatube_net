using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using yatube.Models;

namespace yatube.Repositories
{
    public class GroupRepositorie : IGroupRepositorie
    {
        private readonly IConfiguration _configuration;

        public GroupRepositorie(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<GroupsForGet> GetGroups()
        {
            string query = @"
                SELECT group_id, title
                FROM groups
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            var groups = new List<GroupsForGet>();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        groups.Add(new GroupsForGet { GroupId = myReader.GetInt32(0), Title = myReader.GetString(1) });
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }
            return groups;
        }
        public GroupForGet GetGroup(int groupId)
        {
            string query = @"
                SELECT title, slug, description
                FROM groups
                WHERE group_id = @group_id
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            var group = new GroupForGet();
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@group_id", groupId);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        group = new GroupForGet { Title = myReader.GetString(0), Slug = myReader.GetString(1), Description = myReader.GetString(2) };
                    }
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return group;
        }
        public void AddGroup(GroupsForCreate groups)
        {
            string query = @"
                INSERT INTO groups(title,slug,description)
                VALUES (@title,@slug,@description)
            ";
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@title", groups.Title);
                    myCommand.Parameters.AddWithValue("@slug", groups.Slug);
                    myCommand.Parameters.AddWithValue("@description", groups.Description);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
        public void RemoveGroup(int groupId)
        {
            string query = @"
                DELETE FROM groups
                WHERE group_id = @group_id
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("YatubeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@group_id", groupId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
        }
    }
}

using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDataLibrary.Data
{
    public class SqlData : ISqlData
    {
        private ISqlDataAccess _db;
        private const string connectionStringName = "Default";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public UserModel Authenticate(string username, string password)
        {
            List<UserModel> results = _db.LoadData<UserModel, dynamic>(
                "dbo.spUsers_Authenticate",
                new { username, password },
                connectionStringName,
                true);

            return results.Count > 0 ? results[0] : null;
        }

        public void Register(string username, string firstName, string lastName, string password)
        {
            _db.SaveData<dynamic>(
                "dbo.spUsers_Register",
                new { username, firstName, lastName, password },
                connectionStringName,
                true);
        }

        public void AddPost(PostModel post)
        {
            // Validate input
            if (string.IsNullOrEmpty(post.Content))
            {
                throw new ArgumentException("Post content cannot be empty");
            }

            _db.SaveData(
                "dbo.spPosts_Insert",
                new
                {
                    userId = post.UserId,
                    title = post.Title,
                    content = post.Content,
                    dateCreated = post.CreatedAt  // Make sure this is included
                },
                connectionStringName,
                true);
        }

        public List<ListPostModel> ListPosts()
        {
            return _db.LoadData<ListPostModel, dynamic>(
                "dbo.spPosts_List",
                new { },
                connectionStringName,
                true);
        }

        public PostModel ShowPostDetails(int postId)
        {
            List<PostModel> results = _db.LoadData<PostModel, dynamic>(
                "dbo.spPosts_Detail",
                new { postId },
                connectionStringName,
                true);

            return results.Count > 0 ? results[0] : null;
        }
    }
}

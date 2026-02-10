using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;



namespace BlogTestUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlData db = GetConnection();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Blog Management System");
                Console.WriteLine("1) Register");
                Console.WriteLine("2) Login");
                Console.WriteLine("3) Add Post");
                Console.WriteLine("4) List Posts");
                Console.WriteLine("5) Show Post Details");
                Console.WriteLine("0) Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Register(db);
                        break;
                    case "2":
                        Authenticate(db);
                        break;
                    case "3":
                        AddPost(db);
                        break;
                    case "4":
                        ListPosts(db);
                        break;
                    case "5":
                        ShowPostDetails(db);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static SqlData GetConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();
            ISqlDataAccess dbAccess = new SqlDataAccess(config);
            SqlData db = new SqlData(dbAccess);

            return db;
        }

        private static UserModel GetCurrentUser(SqlData db)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            UserModel user = db.Authenticate(username, password);
            return user;
        }

        public static void Authenticate(SqlData db)
        {
            Console.Clear();
            Console.WriteLine("=== LOGIN ===");
            UserModel user = GetCurrentUser(db);
            if (user == null)
            {
                Console.WriteLine("Invalid credentials.");
            }
            else
            {
                Console.WriteLine($"Welcome, {user.FirstName} {user.LastName}!");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void Register(SqlData db)
        {
            Console.Clear();
            Console.WriteLine("=== REGISTER ===");
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();
            Console.Write("Enter first name: ");
            var firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            var lastName = Console.ReadLine();

            db.Register(username, firstName, lastName, password);
            Console.WriteLine("User registered successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void AddPost(SqlData db)
        {
            Console.Clear();
            Console.WriteLine("=== ADD POST ===");

           
            UserModel user = GetCurrentUser(db);
            if (user == null)
            {
                Console.WriteLine("Invalid credentials. Cannot add post.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Title: ");
            string title = Console.ReadLine();

            
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Write body: ");
            string body = Console.ReadLine();

            
            if (string.IsNullOrWhiteSpace(body))
            {
                Console.WriteLine("Body cannot be empty!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            PostModel post = new PostModel
            {
                Title = title.Trim(),
                Content = body.Trim(),
                CreatedAt = DateTime.Now,
                UserId = user.Id,
            };


            try
            {
                db.AddPost(post);
                Console.WriteLine("Post added successfully!");

                
                Console.WriteLine("Verifying post was saved...");
                System.Threading.Thread.Sleep(1000); 

                var posts = db.ListPosts();
                if (posts != null && posts.Count > 0)
                {
                    Console.WriteLine($"Total posts in database: {posts.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding post: {ex.Message}");
                Console.WriteLine($"Exception type: {ex.GetType()}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static void ListPosts(SqlData db)
        {
            Console.Clear();
            Console.WriteLine("=== ALL POSTS ===");

            List<ListPostModel> posts = db.ListPosts();

            if (posts == null || posts.Count == 0)
            {
                Console.WriteLine("No posts found.");
            }
            else
            {
                foreach (var post in posts)
                {
                    Console.WriteLine($"ID: {post.Id}");
                    Console.WriteLine($"Title: {post.Title}");

                    // Show first 20 characters of content
                    string preview = post.Content.Length > 20 ? post.Content.Substring(0, 20) + "..." : post.Content;
                    Console.WriteLine($"Content: {preview}");

                    Console.WriteLine($"Author: {post.FirstName} {post.LastName} ({post.UserName})");
                    Console.WriteLine($"Date: {post.CreatedAt}");
                    Console.WriteLine("------------------------");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void ShowPostDetails(SqlData db)
        {
            Console.Clear();
            Console.WriteLine("=== POST DETAILS ===");
            Console.Write("Enter Post ID: ");

            if (int.TryParse(Console.ReadLine(), out int postId))
            {
                PostModel post = db.ShowPostDetails(postId);
                if (post != null)
                {
                    Console.WriteLine($"\nTitle: {post.Title}");
                    Console.WriteLine($"Author: {post.FirstName} {post.LastName} ({post.UserName})");
                    Console.WriteLine($"Date: {post.CreatedAt}");
                    Console.WriteLine($"Content: {post.Content}");
                }
                else
                {
                    Console.WriteLine("Post not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid Post ID!");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

}
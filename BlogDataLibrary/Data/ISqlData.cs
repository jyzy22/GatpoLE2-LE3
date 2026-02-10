using BlogDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISqlData
{
    UserModel Authenticate(string username, string password);
    void Register(string username, string firstName, string lastName, string password);
    void AddPost(PostModel post);  // Changed from PostModels to PostModel
    List<ListPostModel> ListPosts();  // Changed from ListPostModels to ListPostModel
    PostModel ShowPostDetails(int postId);  // Changed from PostModels to PostModel

}
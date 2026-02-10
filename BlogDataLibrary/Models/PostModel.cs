using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlogDataLibrary.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

/* LE2
public class PostModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }  
    public DateTime DateCreated { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
}
*/
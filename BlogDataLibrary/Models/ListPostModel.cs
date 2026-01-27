namespace BlogDataLibrary.Models
{
    public class ListPostModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }
    }
}

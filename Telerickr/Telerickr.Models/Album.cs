namespace Telerickr.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}

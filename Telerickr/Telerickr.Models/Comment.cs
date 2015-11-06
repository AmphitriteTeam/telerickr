namespace Telerickr.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
    }
}

namespace Telerickr.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Content { get; set; }

        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
    }
}

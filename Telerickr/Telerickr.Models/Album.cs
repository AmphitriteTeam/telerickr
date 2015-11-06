namespace Telerickr.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}

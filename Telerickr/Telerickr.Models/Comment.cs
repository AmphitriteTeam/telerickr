namespace Telerickr.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CommentContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}

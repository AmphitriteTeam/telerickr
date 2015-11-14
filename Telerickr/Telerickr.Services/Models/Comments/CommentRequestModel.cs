namespace Telerickr.Services.Models.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentRequestModel
    {
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
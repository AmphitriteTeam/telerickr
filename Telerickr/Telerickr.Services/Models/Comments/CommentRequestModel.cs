namespace Telerickr.Services.Models.Comments
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class CommentRequestModel
    {
        [Required]
        [MaxLength(ValidationConstants.CommentContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
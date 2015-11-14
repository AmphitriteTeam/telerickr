namespace Telerickr.Services.Models.Photos
{
    using System.ComponentModel.DataAnnotations;

    public class PhotoRequestModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(4)]
        public string FileExtension { get; set; }

        [Required]
        public int AlbumId { get; set; }
    }
}
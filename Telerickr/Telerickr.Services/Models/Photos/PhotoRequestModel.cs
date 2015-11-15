namespace Telerickr.Services.Models.Photos
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class PhotoRequestModel
    {
        [Required]
        [MinLength(ValidationConstants.TitleMinLength)]
        [MaxLength(ValidationConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ValidationConstants.UrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(ValidationConstants.FileExtensionMaxLength)]
        public string FileExtension { get; set; }

        [Required]
        public int AlbumId { get; set; }
    }
}
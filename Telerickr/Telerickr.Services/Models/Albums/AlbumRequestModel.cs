namespace Telerickr.Services.Models.Albums
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class AlbumRequestModel
    {
        [Required]
        [MinLength(ValidationConstants.TitleMinLength)]
        [MaxLength(ValidationConstants.TitleMaxLength)]
        public string Title { get; set; }
    }
}
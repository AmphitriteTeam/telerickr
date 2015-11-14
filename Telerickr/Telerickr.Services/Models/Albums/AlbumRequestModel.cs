namespace Telerickr.Services.Models.Albums
{
    using System.ComponentModel.DataAnnotations;

    public class AlbumRequestModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
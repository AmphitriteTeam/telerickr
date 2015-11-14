namespace Telerickr.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Photo
    {
        public int Id { get; set; }

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

        public DateTime? UploadDate { get; set; }

        public int Likes { get; set; }
        
        [Required]
        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}

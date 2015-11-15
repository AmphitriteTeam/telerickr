namespace Telerickr.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class Photo
    {
        private ICollection<Comment> comments;

        public Photo()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

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

        public DateTime UploadDate { get; set; }

        public int Likes { get; set; }
        
        [Required]
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}

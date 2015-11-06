namespace Telerickr.Models
{
    using System;
    using System.Collections.Generic;

    public class Photo
    {
        private ICollection<Comment> comments;

        public Photo()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string FileExtension { get; set; }

        public DateTime UploadDate { get; set; }

        public int Likes { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}

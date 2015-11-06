namespace Telerickr.Models
{
    using System.Collections.Generic;

    public class User
    {
        private ICollection<Photo> photos;
        private ICollection<Album> albums;

        public User()
        {
            this.photos = new HashSet<Photo>();
            this.albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Photo> Photos
        {
            get { return this.photos; }
            set { this.photos = value; }
        }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }
            set { this.albums = value; }
        }
    }
}

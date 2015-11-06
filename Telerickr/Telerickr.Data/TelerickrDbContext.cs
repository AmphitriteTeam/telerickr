namespace Telerickr.Data
{
    using System.Data.Entity;
    using Telerickr.Models;

    public class TelerickrDbContext : DbContext
    {
        public TelerickrDbContext()
            : base("Telerickr")
        {
        }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Photo> Photos { get; set; }

        public virtual IDbSet<Album> Albums { get; set; }

        public virtual IDbSet<Comment> comments { get; set; }
    }
}

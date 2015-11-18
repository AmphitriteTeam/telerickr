namespace Telerickr.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Telerickr.Models;

    public class TelerickrDbContext : IdentityDbContext<User>, ITelerickrDbContext
    {
        public TelerickrDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Photo> Photos { get; set; }

        public virtual IDbSet<Album> Albums { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public static TelerickrDbContext Create()
        {
            return new TelerickrDbContext();
        }
    }
}

using Telerickr.Data.Migrations;

namespace Telerickr.Data
{
    using System.Data.Entity;
    using Telerickr.Models;

    public class TelerickrDbContext : DbContext
    {
        public TelerickrDbContext(string pass)
            : base(string.Format("Server=telerickr.crannfpfmxvs.us-west-2.rds.amazonaws.com,1433;Database=telerickr;User Id=amphitrite;Password={0};", pass))
        {
        }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Photo> Photos { get; set; }

        public virtual IDbSet<Album> Albums { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }
    }
}

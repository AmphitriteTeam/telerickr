namespace Telerickr.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Telerickr.Models;

    public interface ITelerickrDbContext
    {
        IDbSet<Photo> Photos { get; set; }

        IDbSet<Album> Albums { get; set; }

        IDbSet<Comment> Comments { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}

namespace Telerickr.Services
{
    using System.Data.Entity;
    using Telerickr.Data;
    using Telerickr.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TelerickrDbContext, Configuration>());
            TelerickrDbContext.Create().Database.Initialize(true);
        }
    }
}
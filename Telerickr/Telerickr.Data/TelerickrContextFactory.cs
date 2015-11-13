namespace Telerickr.Data
{
    public class TelerickrContextFactory
    {
        public TelerickrDbContext Create()
        {
            return new TelerickrDbContext();
        }
    }
}

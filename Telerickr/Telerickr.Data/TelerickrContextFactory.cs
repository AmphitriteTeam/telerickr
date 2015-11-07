namespace Telerickr.Data
{
    public class TelerickrContextFactory
    {
        public TelerickrDbContext Create(string pass)
        {
            return new TelerickrDbContext(pass);
        }
    }
}

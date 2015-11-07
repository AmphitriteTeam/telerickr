using System;
using Telerickr.Data;
using Telerickr.Models;

namespace Telerickr.ConsoleClient
{
    public class Program
    {
        public static void Main()
        {
            var contextFactory = new TelerickrContextFactory();
            var pass = Console.ReadLine();
            var db = contextFactory.Create(pass);

            db.Database.CreateIfNotExists();

            var user = new User()
            {
                Username = "ivaylo",
                Password = "123456"
            };

            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}

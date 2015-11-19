namespace Telerickr.Services.Tests.TestObjects
{
    using System;
    using Telerickr.Models;
    using Common;

    public static class ObjectsFactory
    {
        public static InMemoryRepository<Album> GetEmptyAlbumRepository()
        {
            return new InMemoryRepository<Album>();
        }

        public static InMemoryRepository<User> GetEmptyUserRepository()
        {
            return new InMemoryRepository<User>();
        }

        public static InMemoryRepository<Photo> GetEmptyPhotoRepository()
        {
            return new InMemoryRepository<Photo>();
        }

        public static User GetValidUser()
        {
            return new User()
            {
                UserName = "Test User0",
                Email = "TestUser0@abv.bg"
            };
        }

        public static InMemoryRepository<Album> GetAlbumRepository(int numberOfAlbums = TestConstants.DefaultNumberOfModels)
        {
            var repo = new InMemoryRepository<Album>();

            for (int i = 0; i < numberOfAlbums; i++)
            {
                repo.Add(new Album
                {
                  Id= i,
                  Title = "Test Title " + i,
                  User = new User()
                  {
                      UserName = "Test User" + i,
                      Email = "TestUser" + i + "@abv.bg"
                  },
                  UserId = "test id " + i
                });
            }

            return repo;
        }
    }
}

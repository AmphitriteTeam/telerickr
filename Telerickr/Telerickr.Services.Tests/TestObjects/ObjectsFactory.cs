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

        public static User GetValidUser(int i = 0)
        {
            return new User()
            {
                UserName = "Test User" + i,
                Email = "TestUser0@abv.bg"
            };
        }

        public static Album GetValidAlbum(int i = 0)
        {
            return new Album
            {
                Id = i,
                Title = "Test Title " + i,
                User = new User()
                {
                    UserName = "Test User" + i,
                    Email = "TestUser" + i + "@abv.bg"
                },
                UserId = "test id " + i
            };
        }

        public static InMemoryRepository<Album> GetAlbumRepository(int numberOfAlbums = TestConstants.DefaultNumberOfModels)
        {
            var repo = new InMemoryRepository<Album>();

            for (int i = 0; i < numberOfAlbums; i++)
            {
                repo.Add(GetValidAlbum(i));
            }

            return repo;
        }

        public static InMemoryRepository<User> GetUserRepository(int numberOfUsers = TestConstants.DefaultNumberOfModels)
        {
            var repo = new InMemoryRepository<User>();

            for (int i = 0; i < numberOfUsers; i++)
            {
                repo.Add(new User
                {
                    UserName = "Test User" + i,
                    Email = "TestUser" + i + "@abv.bg"
                });
            }

            return repo;
        }
    }
}

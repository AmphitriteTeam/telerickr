namespace Telerickr.Services.Tests.Mocks
{
    using Data;
    using System.Security.Principal;
    using Telerickr.Models;
    using Controllers;
    using Common;

    public static class ControllerMockFactory
    {
        public static AlbumsController GetAlbumsController(IRepository<Album> albums, IRepository<User> users, IRepository<Photo> photos, bool withUser = true, bool validUser = true)
        {
            var controller = new AlbumsController(albums, users, photos);
            if (withUser)
            {
                var username = TestConstants.InvalidUsername;
                if (validUser)
                {
                    username = TestConstants.ValidUsername;
                }

                controller.User = new GenericPrincipal(new GenericIdentity(username, "Passport"), new[] { "tester" });
            }
           
            return controller;
        }

    }
}

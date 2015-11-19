namespace Telerickr.Services.Tests.Mocks
{
    using System.Security.Principal;

    using Common;
    using Controllers;
    using Data;
    using Telerickr.Models;

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

        public static PhotosController GetPhotosController(IRepository<Photo> photos, IRepository<Album> albums, bool withUser = true, bool validUser = true)
        {
            var controller = new PhotosController(photos, albums);
            if (withUser)
            {
                var username = TestConstants.InvalidUsername;
                if (validUser)
                {
                    username = TestConstants.ValidUsername;
                }

                controller.User = new GenericPrincipal(new GenericIdentity(username, "Passport"), new[] { "tester" });
            }
            else
            {
                controller.User = null;
            }

            return controller;
        }

        public static CommentsController GetCommentsController(IRepository<Comment> comments, IRepository<Photo> photos, bool withUser = true, bool validUser = true)
        {
            var controller = new CommentsController(comments, photos);
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

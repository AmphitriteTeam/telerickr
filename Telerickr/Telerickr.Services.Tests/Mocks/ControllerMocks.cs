namespace Telerickr.Services.Tests.Mocks
{
    using Data;
    using Moq;
    using System.Security.Principal;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Telerickr.Models;
    using Telerickr.Services.Controllers;

    public static class ControllerMocks
    {
        public static AlbumsController GetAlbumsController(IRepository<Album> albums, IRepository<User> users, IRepository<Photo> photos, string username)
        {
            var controller = new AlbumsController(albums, users, photos);

            controller.User = new GenericPrincipal(new GenericIdentity(username, "Passport"), new[] { "tester" });

            return controller;
        }

    }
}

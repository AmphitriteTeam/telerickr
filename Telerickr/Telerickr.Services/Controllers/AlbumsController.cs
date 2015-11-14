namespace Telerickr.Services.Controllers
{
    using System.Web.Http;
    using System.Linq;
    using Data;
    using Telerickr.Models;
    using Models;

    public class AlbumsController : ApiController
    {
        private readonly IRepository<Album> albums;
        private readonly IRepository<User> users;

        public AlbumsController()
        {
            var db = new TelerickrDbContext();
            this.albums = new GenericRepository<Album>(db);
            this.users = new GenericRepository<User>(db);
        }

        public IHttpActionResult Get()
        {
            var result = this.albums
                .All()
                .Select(AlbumResponceModel.FromModel)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.albums
                .All()
                .Where(a => a.Id == id)
                .Select(AlbumResponceModel.FromModel)
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(AlbumRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUser = this.User.Identity.Name;
            var creator = this.users
                .All()
                .FirstOrDefault(u => u.UserName == currentUser);

            var newAlbum = new Album
            {
                Title = model.Title,
                User = creator
            };

            this.albums.Add(newAlbum);
            this.albums.SaveChanges();

            return this.Ok(newAlbum.Id);
        }
    }
}

namespace Telerickr.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Telerickr.Models;
    using Models.Albums;

    public class AlbumsController : ApiController
    {
        private readonly IRepository<Album> albums;
        private readonly IRepository<User> users;
        private readonly IRepository<Photo> photos;

        public AlbumsController()
        {
            var db = new TelerickrDbContext();
            this.albums = new GenericRepository<Album>(db);
            this.users = new GenericRepository<User>(db);
            this.photos = new GenericRepository<Photo>(db);
        }

        public IHttpActionResult Get()
        {
            var result = this.albums
                .All()
                .Select(AlbumResponseModel.FromModel)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.albums
                .All()
                .Where(a => a.Id == id)
                .Select(AlbumResponseModel.FromModel)
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Put(int id, AlbumRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var album = this.albums
                .All()
                .FirstOrDefault(a => a.Id == id);

            if (albums == null)
            {
                return this.NotFound();
            }

            if (album.User.UserName != this.User.Identity.Name)
            {
                return this.Unauthorized();
            }

            album.Title = model.Title;

            this.albums.Update(album);
            this.albums.SaveChanges();

            return this.Ok("Album updated");
        }

        [Authorize]
        public IHttpActionResult Post(AlbumRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUser = this.users
                .All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var newAlbum = new Album
            {
                Title = model.Title,
                User = currentUser
            };

            this.albums.Add(newAlbum);
            this.albums.SaveChanges();

            return this.Ok(newAlbum.Id);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var currentUser = this.users
                .All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var result = this.albums
                .All()
                .Where(a => a.Id == id)
                .FirstOrDefault();

            if (result.User.UserName != currentUser.UserName)
            {
                return this.Unauthorized();
            }

            if (result == null)
            {
                return this.NotFound();
            }

            var numberOfPhotos = result.Photos.Count;
            var allPhotos = result.Photos.ToList();

            for (int i = 0; i < numberOfPhotos; i++)
            {
                this.photos.Delete(allPhotos[i]);
            }

            this.albums.Delete(result);
            this.albums.SaveChanges();
            this.photos.SaveChanges();

            return this.Ok("Album deleted with all pictures inside.");
        }
    }
}

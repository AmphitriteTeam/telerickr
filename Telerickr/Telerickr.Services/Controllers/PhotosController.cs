namespace Telerickr.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Telerickr.Data;
    using Telerickr.Models;
    using Telerickr.Services.Models.Photos;

    public class PhotosController : ApiController
    {
        private readonly IRepository<Photo> photos;

        public PhotosController()
        {
            var db = new TelerickrDbContext();
            this.photos = new GenericRepository<Photo>(db);
        }

        public IHttpActionResult Get()
        {
            var result = this.photos
                .All()
                .OrderByDescending(p => p.UploadDate)
                .Take(10)
                .Select(PhotoResponceModel.FromModel)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.photos
                .All()
                .Where(p => p.Id == id)
                .Select(PhotoResponceModel.FromModel)
                .FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(PhotoRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newPhoto = new Photo
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                FileExtension = model.FileExtension,
                UploadDate = DateTime.Now,
                Likes = 0,
                AlbumId = model.AlbumId
            };

            this.photos.Add(newPhoto);
            this.photos.SaveChanges();

            return this.Ok(newPhoto.Id);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var result = this.photos
                .All()
                .FirstOrDefault(p => p.Id == id);

            if (result == null)
            {
                return this.NotFound();
            }

            // Currently only the Album owner can delete photos.
            if (this.User.Identity.Name != result.Album.User.UserName)
            {
                return this.Unauthorized();
            }

            this.photos.Delete(result);
            this.photos.SaveChanges();

            return this.Ok("Photo deleted");
        }
    }
}

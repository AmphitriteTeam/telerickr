namespace Telerickr.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    
    using Data;
    using Models.Comments;
    using Telerickr.Models;
    
    public class CommentsController : ApiController
    {
        private readonly IRepository<Photo> photos;
        private readonly IRepository<Comment> comments;

        public CommentsController(IRepository<Comment> comments, IRepository<Photo> photos)
        {
            this.comments = comments;
            this.photos = photos;
        }

        public IHttpActionResult Get()
        {
            var result = this.comments
                .All()
                .OrderBy(p => p.Id)
                .Take(10)
                .Select(CommentResponseModel.FromModel)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.comments
                .All()
                .Where(c => c.Id == id)
                .Select(CommentResponseModel.FromModel)
                .FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(int photoId, CommentRequestModel comment)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var photo = this.photos
                .All()
                .FirstOrDefault(p => p.Id == photoId);

            if (photo == null)
            {
                return this.NotFound();
            }

            var commentToAdd = new Comment()
            {
                Content = comment.Content,
                UserId = comment.UserId
            };

            photo.Comments.Add(commentToAdd);
            this.photos.SaveChanges();

            return this.Ok("Comment succesfully added.");
        }

        [Authorize]
        public IHttpActionResult Put(int id, CommentRequestModel comment)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentComment = this.comments
                .All()
                .FirstOrDefault(c => c.Id == id);

            if (currentComment == null)
            {
                return this.NotFound();
            }

            if (currentComment.User.UserName != this.User.Identity.Name)
            {
                return this.Unauthorized();
            }

            currentComment.Content = comment.Content;

            this.comments.Update(currentComment);
            this.comments.SaveChanges();

            return this.Ok("Comment updated");
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var currentComment = this.comments
                .All()
                .FirstOrDefault(c => c.Id == id);

            if (currentComment == null)
            {
                return this.NotFound();
            }

            if (currentComment.User.UserName != this.User.Identity.Name)
            {
                return this.Unauthorized();
            }

            this.comments.Delete(currentComment);
            this.comments.SaveChanges();

            return this.Ok("Comment deleted");
        }
    }
}
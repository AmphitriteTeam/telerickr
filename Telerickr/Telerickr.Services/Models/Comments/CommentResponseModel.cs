namespace Telerickr.Services.Models.Comments
{
    using System;
    using System.Linq.Expressions;

    using Telerickr.Models;

    public class CommentResponseModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public static Expression<Func<Comment, CommentResponseModel>> FromModel
        {
            get
            {
                return c => new CommentResponseModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserId = c.UserId
                };
            }
        }
    }
}
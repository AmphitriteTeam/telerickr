namespace Telerickr.Services.Models.Photos
{
    using System;
    using System.Linq.Expressions;
    using Telerickr.Models;

    public class PhotoResponseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime UploadDate { get; set; }

        public int Likes { get; set; }

        public static Expression<Func<Photo, PhotoResponseModel>> FromModel
        {
            get
            {
                return p => new PhotoResponseModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    ImageUrl = p.ImageUrl,
                    UploadDate = p.UploadDate,
                    Likes = p.Likes
                };
            }
        }
    }
}
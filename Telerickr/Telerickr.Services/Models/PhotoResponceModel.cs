using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerickr.Models;

namespace Telerickr.Services.Models
{
    public class PhotoResponceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime UploadDate { get; set; }

        public int Likes { get; set; }

        public static Expression<Func<Photo, PhotoResponceModel>> FromModel
        {
            get
            {
                return p => new PhotoResponceModel
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
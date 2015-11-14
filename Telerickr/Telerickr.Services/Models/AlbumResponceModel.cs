using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerickr.Models;

namespace Telerickr.Services.Models
{
    public class AlbumResponceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public static Expression<Func<Album, AlbumResponceModel>> FromModel
        {
            get
            {
                return a => new AlbumResponceModel
                {
                    Id = a.Id,
                    Title = a.Title
                };
            }
        }
    }
}
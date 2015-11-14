namespace Telerickr.Services.Models.Albums
{
    using System;
    using System.Linq.Expressions;
    using Telerickr.Models;

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
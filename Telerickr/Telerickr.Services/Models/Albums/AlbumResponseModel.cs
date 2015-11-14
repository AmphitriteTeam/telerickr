namespace Telerickr.Services.Models.Albums
{
    using System;
    using System.Linq.Expressions;
    using Telerickr.Models;

    public class AlbumResponseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public static Expression<Func<Album, AlbumResponseModel>> FromModel
        {
            get
            {
                return a => new AlbumResponseModel
                {
                    Id = a.Id,
                    Title = a.Title
                };
            }
        }
    }
}
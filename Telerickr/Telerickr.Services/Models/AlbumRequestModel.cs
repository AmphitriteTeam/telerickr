using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Telerickr.Models;

namespace Telerickr.Services.Models
{
    public class AlbumRequestModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
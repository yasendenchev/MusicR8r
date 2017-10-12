using MusicR8r.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicR8r.Models.Home
{
    public class HomeViewModel
    {
        public ICollection<GenreViewModel> Genres { get; set; }
    }
}
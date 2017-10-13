using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicR8r.Areas.Admin.Models
{
    public class AddArtistViewModel
    {
        public string Name { get; set; }

        public string CountryOfOrigin { get; set; }

        public string Bio { get; set; }
    }
}
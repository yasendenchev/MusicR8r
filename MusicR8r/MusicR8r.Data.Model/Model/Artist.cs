﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Data.Model.Models
{
    public class Artist : DataModel
    {
        public Artist()
        {
            this.Albums = new HashSet<Album>();
        }

        public string Name { get; set; }

        public string CountryOfOrigin { get; set; }

        public string Bio { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}

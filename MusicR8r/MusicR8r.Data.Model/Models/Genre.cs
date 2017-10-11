﻿using System.Collections.Generic;

namespace MusicR8r.Data.Model.Models
{
    public class Genre : DataModel
    {
        public Genre()
        {
            this.Songs = new HashSet<Song>();
        }

        public Genre(string name)
        {
            this.Name = name;
            this.Songs = new HashSet<Song>();
        }
        
        public string Name { get; set; }

        public virtual ICollection<Song> Songs { get; set; }


    }
}
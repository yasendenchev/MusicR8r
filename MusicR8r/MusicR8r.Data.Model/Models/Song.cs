using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Data.Model.Models
{
    public class Song : DataModel
    {
        public Song()
        {
            this.Genres = new HashSet<Genre>();
        }

        public string Name { get; set; }

        public int Duration { get; set; }

        public Artist Artist { get; set; }

        public Album Album { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}

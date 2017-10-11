using System.Collections.Generic;

namespace MusicR8r.Data.Model.Models
{
    public class Genre : DataModel
    {
        public string Name { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
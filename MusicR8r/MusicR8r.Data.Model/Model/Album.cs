using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Data.Model.Models
{
    public class Album : DataModel
    {
        public Album()
        {
            this.Songs = new HashSet<Song>();
        }

        public string Name { get; set; }

        public int Year { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual ICollection<User> Users {get; set;}
    }
}

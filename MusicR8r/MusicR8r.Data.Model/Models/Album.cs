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
            this.Users = new HashSet<User>();
        }

        //public Album(string name, int year, Artist artist, ICollection<Song> songs, ICollection<User> users)
        //{
        //    this.Name = name;
        //    this.Year = year;
        //    this.Artist = artist;
        //    this.Songs = songs;
        //    this.Users = users;
        //}

        public string Name { get; set; }

        public int Year { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public virtual ICollection<User> Users {get; set;}
    }
}

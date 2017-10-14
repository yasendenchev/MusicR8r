using System.Collections.Generic;

namespace MusicR8r.Data.Model.Models
{
    public class Song : DataModel
    {
        public Song()
        {
            this.Genres = new HashSet<Genre>();
        }

        public Song(string name, int duration, Artist artist, Album album)
        {
            this.Genres = new HashSet<Genre>();
            this.Name = name;
            this.Duration = duration;
            this.Artist = artist;
            this.Album = album;
        }
        
        public string Name { get; set; }

        public int Duration { get; set; }

        public Artist Artist { get; set; }

        public Album Album { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}

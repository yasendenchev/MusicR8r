using System.Collections.Generic;

namespace MusicR8r.Data.Model.Models
{
    public class Song : DataModel
    {
        public Song()
        {
        }

        public Song(string name, int duration, Artist artist, Album album, Genre genre)
        {
            this.Name = name;
            this.Duration = duration;
            this.Artist = artist;
            this.Album = album;
            this.Genre = genre;
        }
        
        public string Name { get; set; }

        public int Duration { get; set; }

        public Artist Artist { get; set; }

        public Album Album { get; set; }

        public virtual Genre Genre { get; set; }
    }
}

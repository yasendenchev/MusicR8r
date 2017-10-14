using System.Collections.Generic;

namespace MusicR8r.Data.Model.Models
{
    public class Artist : DataModel
    {
        public Artist()
        {
            this.Albums = new HashSet<Album>();
        }

        public Artist(string name, string countryOfOrigin, string bio)
        {
            this.Name = name;
            this.CountryOfOrigin = countryOfOrigin;
            this.Bio = bio;
            this.Albums = new HashSet<Album>();
        }

        public string Name { get; set; }

        public string CountryOfOrigin { get; set; }

        public string Bio { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}

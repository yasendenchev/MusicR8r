using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression("^[A-Z][-a-zA-Z]+$", ErrorMessage ="Name is not valid!")]
        public string Name { get; set; }

        public virtual ICollection<Song> Songs { get; set; }


    }
}
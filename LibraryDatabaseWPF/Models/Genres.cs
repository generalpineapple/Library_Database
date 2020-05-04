using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class Genres
    {
        public int GenreId { get; }
        public string GenreName { get; }

        public Genres(int genreId, string genreName)
        {
            GenreId = genreId;
            GenreName = genreName;
        }
    }
}

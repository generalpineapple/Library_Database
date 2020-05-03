using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models; 


namespace LibraryDatabaseWPF
{
    public interface IAuthorRepository
    {
        IReadOnlyList<Authors> RetrieveAuthor(int AuthorId);

        void SaveAuthor(int id, string name);
    }
}

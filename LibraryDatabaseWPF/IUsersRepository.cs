﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public interface IUsersRepository
    {
        /// <summary>
        /// Retrieves all persons in the database.
        /// </summary>
        /// <returns>
        /// <see cref="IReadOnlyList{Person}"/> containing all persons.
        /// </returns>
        IReadOnlyList<Users> RetrieveUsers();

        /// <summary>
        /// Fetches the person with the given <paramref name="personId"/> if it exists.
        /// </summary>
        /// <param name="personId">Identifier of the person to fetch.</param>
        /// <returns>
        /// An instance of <see cref="Person"/> containing the information of the requested person.
        /// </returns>
        /// <exception cref="DataAccess.RecordNotFoundException">
        /// Thrown if <paramref name="personId"/> does not exist.
        /// </exception>
        Users FetchInventory(int userId);

        /// <summary>
        /// Creates a new person in the repository.
        /// </summary>
        /// <param name="firstName">First name of the person to create.</param>
        /// <param name="lastName">Last name of the person to create.</param>
        /// <param name="email">Email of the person to create.</param>
        /// <returns>
        /// The resulting instance of <see cref="Person"/>.
        /// </returns>
        Users CreateInventory(int userId, string name, int totalCheckouts, string phoneNumber, string email, int lateReturns);
    }
}
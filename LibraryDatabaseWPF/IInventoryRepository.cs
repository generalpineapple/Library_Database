﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public interface IInventoryRepository
    {
        void CreateInventory(string isbn);

        Inventory GetInventoryByISBN(string isbn);
    }
}

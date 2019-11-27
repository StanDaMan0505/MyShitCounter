using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyShitCounter.DB
{
    public interface IDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

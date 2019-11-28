using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using MyShitCounter.DB;
using Xamarin.Forms;

[assembly: Dependency(typeof(DBNull))]

namespace MyShitCounter.Droid.DB
{
    public class DB : IDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySqlite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}
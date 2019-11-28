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
using SQLite;
using Xamarin.Forms;
using MyShitCounter.DB;
using System.IO;
using MyShitCounter.Droid.DB;

[assembly: Dependency(typeof(DB))]

namespace MyShitCounter.Droid.DB
{
    public class DB : IDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            //var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "MySqlite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}
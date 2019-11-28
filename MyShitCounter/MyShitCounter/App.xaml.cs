using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyShitCounter
{
    public partial class App : Application
    {

        //static ShitDatabase database;

        //public static ShitDatabase Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            database = new ShitDatabase(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyShitSQLite.db3"));
        //        }
        //        return database;
        //    }
        //}


        public App()
        {
            InitializeComponent();
            CreateShitDatabase();
            MainPage = new MainPage();
        }

        private void CreateShitDatabase()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "shitdb1.db2");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Entry>();
            if(db.Table<Entry>().Count() == 0)
            {
                var newEntry = new Entry();
                newEntry.Value = 0; //0 = schlecht, 1=gut
                newEntry.DateTime = DateTime.Now;
            }
        }

        public class Entry
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [MaxLength(8)]
            public Int16 Value { get; set; }
            public DateTime DateTime { get; set; }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

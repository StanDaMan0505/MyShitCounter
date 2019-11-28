using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyShitCounter
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Button upButton;
        Button downButton;
        ListView list;
        ObservableCollection<string> data = new ObservableCollection<string>();
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Entry> _entries;
        private ListView LVMain;

        public MainPage()
        {
            InitializeComponent();
            SQLiteAsyncConnection sQLiteAsyncConnection = _connection = DependencyService.Get<DB.IDb>().GetConnection();

            //KOMENTAR
            this.Padding = new Thickness(20, 20, 20, 20);
            StackLayout panel = new StackLayout { Spacing = 15 };

            panel.Children.Add(upButton = new Button
            {
                Text = "The Good"
            });

            panel.Children.Add(downButton = new Button
            {
                Text = "The Bad"
            });

            panel.Children.Add(LVMain = new ListView
            {
                Header = "& The Ugly Truth",
                SelectionMode = ListViewSelectionMode.None,
                ItemsSource = _entries
            }); 

            upButton.Clicked += UpButton_Clicked;
            downButton.Clicked += DownButton_Clicked;

            this.Content = panel;
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Entry>();
            var entries = await _connection.Table<Entry>().ToListAsync();
            _entries = new ObservableCollection<Entry>(entries);
            LVMain.ItemsSource = _entries;

            base.OnAppearing();
        }

        async private void DownButton_Clicked(object sender, EventArgs e)
        {
            //data.Add(string.Concat("down", DateTime.Now));

            var entry = new Entry() { DateTime = DateTime.Now, GoodBad = false };
            await _connection.InsertAsync(entry);
            _entries.Add(entry);

        }

        async private void UpButton_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //data.Add(string.Concat("up", DateTime.Now));

            var entry = new Entry() { DateTime = DateTime.Now, GoodBad = true };
            await _connection.InsertAsync(entry);
            _entries.Add(entry);
        }
    }

    public class Entry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }        
        public DateTime DateTime { get; set; }
        public bool GoodBad { get; set; }
    }
}

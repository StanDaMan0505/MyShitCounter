using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;

namespace MyShitCounter
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private Button upButton;
        private Button downButton;
        private Label counterLabel;

        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Entry> _entries;
        private ListView LVMain;

        public MainPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<DB.IDb>().GetConnection();

            this.Padding = new Thickness(20, 20, 20, 20);
            StackLayout panel = new StackLayout { Spacing = 15 };

            var entryTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });

                var stateLabel = new Label { FontAttributes = FontAttributes.Bold };
                var dateTimeLabel = new Label();

                stateLabel.SetBinding(Label.TextProperty, "GoodBad");
                dateTimeLabel.SetBinding(Label.TextProperty, "DateTime");

                grid.Children.Add(stateLabel);
                grid.Children.Add(dateTimeLabel, 1, 0);

                return new ViewCell
                {
                    View = grid
                };
            });

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
                ItemTemplate = entryTemplate,
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

        public class Entry
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public DateTime DateTime { get; set; }
            public bool GoodBad { get; set; }
        }

        async private void DownButton_Clicked(object sender, EventArgs e)
        {
            //data.Add("down");
            var entry = new Entry { DateTime = DateTime.Now, GoodBad = false };
            await _connection.InsertAsync(entry);
            _entries.Add(entry);
        }

        async private void UpButton_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //data.Add("Up");
            var entry = new Entry() { DateTime = DateTime.Now, GoodBad = true };
            await _connection.InsertAsync(entry);
            _entries.Add(entry);
        }
    }
}

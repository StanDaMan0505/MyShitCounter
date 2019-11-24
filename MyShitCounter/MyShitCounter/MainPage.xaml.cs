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

        public MainPage()
        {
            // InitializeComponent();
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

            panel.Children.Add(list = new ListView
            {
                Header = "& The Ugly Truth",
                SelectionMode = ListViewSelectionMode.None,
                ItemsSource = data
            });

            upButton.Clicked += UpButton_Clicked;
            downButton.Clicked += DownButton_Clicked;

            this.Content = panel;
        }

        private void DownButton_Clicked(object sender, EventArgs e)
        {
            data.Add("down");
        }

        private void UpButton_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            data.Add("Up");
        }
    }
}

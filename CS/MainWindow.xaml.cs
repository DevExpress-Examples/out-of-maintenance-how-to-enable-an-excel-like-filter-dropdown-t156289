using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DXGridSample {
    public partial class MainWindow : Window {
        public ObservableCollection<Item> Items { get; set; }
        public MainWindow() {
            Items = new ObservableCollection<Item>();
            for (int i = 0; i < 100; i++)
                Items.Add(new Item { Id = i, Name = "Name" + i, IsEnabled = i % 2 == 0 });
            DataContext = this;
            InitializeComponent();
        }
    }
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
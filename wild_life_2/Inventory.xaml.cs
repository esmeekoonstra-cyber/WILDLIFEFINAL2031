using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace wild_life_2
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    /*public partial class Inventory : Window
    {

        private List<string> inventory;
        private string selectedItem;

        public Inventory(List<string> inventory)
        {
            InitializeComponent();

            this.inventory = inventory;

            RefreshInventory();
        }

        private void RefreshInventory()
        {
            InventoryList.ItemsSource = null;
            InventoryList.ItemsSource = inventory;
        }

        //Om vrucht te eten
        private void EatButton_Click(object sender, RoutedEventArgs e)
        {
            string item = InventoryList.SelectedItem as string;

            if (item == null)
                return;

            if (item == "Appel")
            {
                inventory.Remove(item);

                MessageBox.Show("Appel gegeten!");

                RefreshInventory();
            }
        }

        private void InventoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = InventoryList.SelectedItem as string;

            if (selectedItem == null)
                return;

            ItemNameText.Text = selectedItem;

            if (selectedItem == "Appel")
            {
                ItemInfoText.Text = "Een verse appel. Herstelt 10 gezondheid.";
                EatButton.IsEnabled = true;
            }
        }
    }*/

    public partial class Inventory : Window
    {
        private List<string> inventory;
        private string selectedItem;

        public bool AppelGegeten { get; private set; } = false;
        public Inventory(List<string> inventory)
        {
            InitializeComponent();

            this.inventory = inventory;

            RefreshInventory();
        }

        private void RefreshInventory()
        {
            InventoryGrid.Children.Clear();

            foreach (string item in inventory)
            {
                Button button = new Button();

                button.Content = item;
                button.Margin = new Thickness(5);
                button.Click += InventoryItem_Click;

                InventoryGrid.Children.Add(button);
            }
        }

        private void InventoryItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            selectedItem = button.Content.ToString();

            ItemNameText.Text = selectedItem;

            if (selectedItem == "Appel")
            {
                ItemInfoText.Text =
                    "Een verse appel.\nHerstelt 10 gezondheid.";

                EatButton.IsEnabled = true;
            }
            else
            {
                ItemInfoText.Text = "";
                EatButton.IsEnabled = false;
            }
        }

        private void EatButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null)
                return;

            if (selectedItem == "Appel")
            {
                inventory.Remove(selectedItem);
                AppelGegeten = true;


                MessageBox.Show( 
                "Goed! Sluit je inventory om naar het volgende level te gaan.",
                "Goed gedaan!",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

                selectedItem = null;

                ItemNameText.Text = "Selecteer een item";
                ItemInfoText.Text = "";
                EatButton.IsEnabled = false;

                RefreshInventory();
            }
        }
    }

}

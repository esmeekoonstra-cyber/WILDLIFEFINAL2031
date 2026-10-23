using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private List<string> inventory = new();

        public GameWindow()
        {
            InitializeComponent();

            KeyDown += GameWindow_KeyDown;
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            double x = Canvas.GetLeft(Player);
            double y = Canvas.GetTop(Player);

            if (e.Key == Key.Left)
                Canvas.SetLeft(Player, x - 10);

            if (e.Key == Key.Right)
                Canvas.SetLeft(Player, x + 10);

            if (e.Key == Key.Up)
                Canvas.SetTop(Player, y - 10);

            if (e.Key == Key.Down)
                Canvas.SetTop(Player, y + 10);

            
            //3. Naar level 2. Als de appel is gegeten, wordt GameWindow2 geopend en wordt het eerste level gesloten. Is de appel niet gegeten? Dan blijf je gewoon in level 1.
            
            if (e.Key == Key.I)
            {
                Inventory inventoryWindow = new Inventory(inventory);

                inventoryWindow.Closed += (s, args) => //1. Inventory sluiten. De Closed-gebeurtenis wacht totdat het inventoryvenster daadwerkelijk gesloten is.
                {
                    if (inventoryWindow.AppelGegeten) //2.Controleren of de appel is gegeten, 'if (inventoryWindow.AppelGegeten)' controleert de status die je in stap 1 hebt toegevoegd.

                    {
                        GameWindow2 volgendLevel = new GameWindow2();  //3. Naar level 2. Als de appel is gegeten, wordt GameWindow2 geopend en wordt het eerste level gesloten. Is de appel niet gegeten? Dan blijf je gewoon in level 1.

                        volgendLevel.Show();

                        this.Close();
                    }
                };

                inventoryWindow.Show();
            }

            CheckFruitPickup();
        }

        private void CheckFruitPickup()
        {
            Rect playerRect = new Rect(
                Canvas.GetLeft(Player),
                Canvas.GetTop(Player),
                Player.Width,
                Player.Height);

            Rect fruitRect = new Rect(
                Canvas.GetLeft(Vrucht),
                Canvas.GetTop(Vrucht),
                Vrucht.Width,
                Vrucht.Height);

            if (playerRect.IntersectsWith(fruitRect))
            {
                inventory.Add("Appel");

                GameCanvas.Children.Remove(Vrucht);
                InstructionText.Text = "Je hebt de vrucht opgepakt! Klik op I om je inventory te openen.";
                MessageBox.Show("Appel opgepakt!");
            }
        }
    }
}

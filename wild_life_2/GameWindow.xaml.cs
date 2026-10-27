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

        private CharacterAnimator animator;

        public GameWindow()
        {
            InitializeComponent();
            KeyUp += GameWindow_KeyUp;
            animator = new CharacterAnimator(
                  Player,
                 "images/FemaleCharacter"
                );
            InventoryPanel.InventoryItems = inventory;
            InventoryPanel.VolgendLevelEvent += Inventory_VolgendLevelEvent;

            KeyDown += GameWindow_KeyDown;
        }

      
        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            double x = Canvas.GetLeft(Player);
            double y = Canvas.GetTop(Player);

            double snelheid = 10;

            // Nieuwe positie berekenen
            double nieuweX = x;
            double nieuweY = y;

            // Bewegen met pijltjestoetsen en WASD
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                nieuweX -= snelheid;
                animator.SetDirection("Left");
                animator.SetMoving(true);
            }

            if (e.Key == Key.Right || e.Key == Key.D)
            {
                nieuweX += snelheid;
                animator.SetDirection("Right");
                animator.SetMoving(true);
            }

            if (e.Key == Key.Up || e.Key == Key.W)
            {
                nieuweY -= snelheid;
                animator.SetDirection("Back");
                animator.SetMoving(true);
            }

            if (e.Key == Key.Down || e.Key == Key.S)
            {
                nieuweY += snelheid;
                animator.SetDirection("Front");
                animator.SetMoving(true);
            }
            // Grenzen speelveld controleren
            nieuweX = Math.Max(0, Math.Min(nieuweX,
                GameCanvas.Width - Player.Width));

            nieuweY = Math.Max(0, Math.Min(nieuweY,
                GameCanvas.Height - Player.Height));

            // Nieuwe positie toepassen
            Canvas.SetLeft(Player, nieuweX);
            Canvas.SetTop(Player, nieuweY);


            //3. Naar level 2. Als de appel is gegeten, wordt GameWindow2 geopend en wordt het eerste level gesloten. Is de appel niet gegeten? Dan blijf je gewoon in level 1.

            /*if (e.Key == Key.I)
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
            }*/
            if (e.Key == Key.I)
            {
                InventoryPanel.UpdateInventory();

                InventoryPanel.Visibility =
                    InventoryPanel.Visibility == Visibility.Visible
                    ? Visibility.Collapsed
                    : Visibility.Visible;

                return;
            }

            CheckFruitPickup();
        }

        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W ||
                e.Key == Key.A ||
                e.Key == Key.S ||
                e.Key == Key.D ||
                e.Key == Key.Up ||
                e.Key == Key.Down ||
                e.Key == Key.Left ||
                e.Key == Key.Right)
            {
                animator.SetMoving(false);
            }
        }

        private void CheckFruitPickup()
        {
            if (!GameCanvas.Children.Contains(Vrucht))
                return;

            // Kleinere hitbox speler
            double x = Canvas.GetLeft(Player);
            double y = Canvas.GetTop(Player);

            Rect playerRect = new Rect(
                x + 12,
                y + 10,
                Player.Width - 24,
                Player.Height - 15);
            // hitbos fruit
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
                ApplePopup.Visibility = Visibility.Visible;
            }

            
        }
        private void CloseApplePopup(object sender, RoutedEventArgs e)
        {
            ApplePopup.Visibility = Visibility.Collapsed;
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Inventory_VolgendLevelEvent(object sender, EventArgs e)
        {
            GoedGedaanPopup.Visibility = Visibility.Visible;
        }
        private void VerderButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow2 volgendLevel = new GameWindow2();
            volgendLevel.Show();

            this.Close();
        }
    }
}

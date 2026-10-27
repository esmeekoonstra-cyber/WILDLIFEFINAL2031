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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow2 : Window
    {
        private List<string> inventory = new();
        private int houtAantal = 0;
        private CharacterAnimator animator;
        public GameWindow2()
        {
            InitializeComponent();

            KeyUp += GameWindow2_KeyUp;

            animator = new CharacterAnimator(
             Player,
             "images/FemaleCharacter"
             );

            InventoryPanel.InventoryItems = inventory;
            
            KeyDown += GameWindow2_KeyDown;
        }

        private void GameWindow2_KeyDown(object sender, KeyEventArgs e)
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


            CheckHoutPickup();


            if (e.Key == Key.I)
            {
                InventoryPanel.UpdateInventory();

                InventoryPanel.Visibility =
                    InventoryPanel.Visibility == Visibility.Visible
                    ? Visibility.Collapsed
                    : Visibility.Visible;

                return;
            }
        }

        private void GameWindow2_KeyUp(object sender, KeyEventArgs e)
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

        private void CheckHoutPickup()
        {
            Image[] houtBundels =
            {
              Hout1,
              Hout2,
              Hout3,
              Hout4,
              Hout5
            };

            Rect playerRect = new Rect(
                Canvas.GetLeft(Player),
                Canvas.GetTop(Player),
                Player.Width,
                Player.Height);

            foreach (Image hout in houtBundels)
            {
                if (hout.Visibility != Visibility.Visible)
                    continue;

                Rect houtRect = new Rect(
                    Canvas.GetLeft(hout),
                    Canvas.GetTop(hout),
                    hout.Width,
                    hout.Height);

                if (playerRect.IntersectsWith(houtRect))
                {
                    hout.Visibility = Visibility.Collapsed;
                    houtAantal++;
                    HoutTeller.Text = $"Hout: {houtAantal}/5";

                    if (houtAantal >= 5)
                    {
                        Huis.Visibility = Visibility.Visible;
                        Huis.Opacity = 1.0;
                        Canvas.SetLeft(Huis, 350);
                        Canvas.SetTop(Huis, 150);
                        Panel.SetZIndex(Huis, 10);

                        HoutPopup.Visibility = Visibility.Visible;
                    }


                    break;
                }
            }
        }
        private void CloseHoutPopup(object sender, RoutedEventArgs e)
        {
            HoutPopup.Visibility = Visibility.Collapsed;
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
       

    }
}

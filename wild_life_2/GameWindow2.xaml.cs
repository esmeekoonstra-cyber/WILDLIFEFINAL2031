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
        public GameWindow2()
        {
            InitializeComponent();

            KeyDown += GameWindow2_KeyDown;
        }

        private void GameWindow2_KeyDown(object sender, KeyEventArgs e)
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

            CheckHoutPickup();


            if (e.Key == Key.I)
            {
                Inventory inventoryWindow =
                    new Inventory(inventory);

                inventoryWindow.Show();
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

                        MessageBox.Show(
                            "Je hebt genoeg hout verzameld! " +
                            "Ga naar het huis om het te bouwen.");
                    }


                    break;
                }
            }
        }

    }
}

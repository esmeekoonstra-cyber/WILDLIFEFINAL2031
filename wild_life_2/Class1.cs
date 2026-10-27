using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace wild_life_2
{
    public class CharacterAnimator
    {
        private readonly Image player;
        private readonly string characterFolder;
        private readonly DispatcherTimer timer;

        private string direction = "Front";
        private bool isMoving = false;
        private int walkFrame = 0;

        public CharacterAnimator(Image player, string characterFolder)
        {
            this.player = player;
            this.characterFolder = characterFolder;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Animation_Tick;

            ShowIdle();
        }

        public void SetDirection(string newDirection)
        {
            direction = newDirection;

            if (!isMoving)
                ShowIdle();
        }

        public void SetMoving(bool moving)
        {
            isMoving = moving;

            if (isMoving)
            {
                if (!timer.IsEnabled)
                {
                    // Begin meteen met het idle-frame
                    walkFrame = 0;
                    ShowFrame(direction + "_Idle.png");
                    timer.Start();
                }
            }
            else
            {
                timer.Stop();
                ShowIdle();
            }
        }

        private void Animation_Tick(object sender, EventArgs e)
        {
            if (!isMoving)
                return;

            walkFrame = (walkFrame + 1) % 3;

            if (walkFrame == 0)
                ShowFrame(direction + "_Idle.png");
            else if (walkFrame == 1)
                ShowFrame(direction + "_Walk1.png");
            else
                ShowFrame(direction + "_Walk2.png");
        }

        private void ShowIdle()
        {
            ShowFrame(direction + "_Idle.png");
        }

        private void ShowFrame(string fileName)
        {
            player.Source = new BitmapImage(
                new Uri(
                    $"/wild_life_2;component/{characterFolder}/{fileName}",
                    UriKind.Relative
                )
            );
        }
    }
}
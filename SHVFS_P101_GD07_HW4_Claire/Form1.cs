using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakoutGame
{
    public partial class Form1 : Form
    {
        int score;
        int ballPositionX;
        int ballPositionY;
        int playerSpeed;

        bool moveLeft;
        bool moveRight;

        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            setupGame();
        }

        private void gameTimeEvent(object sender, EventArgs e)
        {
            gameScore.Text = "Score: " + score;
            //platform moving range
            if (moveLeft == true && platform.Left > 0)
            {
                platform.Left -= playerSpeed;
            }
            if (moveRight == true && platform.Left < 494)
            {
                platform.Left += playerSpeed;
            }

            ball.Left += ballPositionX;
            ball.Top += ballPositionY;

            //ball moving range
            if (ball.Left < 0 || ball.Left > 575)
            {
                ballPositionX = -ballPositionX;
            }
            if (ball.Top < 0)
            {
                ballPositionY = -ballPositionY;
            }
            if (ball.Bounds.IntersectsWith(platform.Bounds))
            {
                ballPositionY = 7 * (-1);
                if (ballPositionX < 0)
                {
                    ballPositionX = 7 * (-1);
                }
                else
                {
                    ballPositionX = 7;
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        ballPositionY = -ballPositionY;
                        this.Controls.Remove(x);
                    }
                }
            }

            if (score == 18) //number of blocks
            {
                gameTime.Stop();
                MessageBox.Show("You win!" + "\nYour score is " + score);
            }
            if (ball.Top > 480)
            {
                gameTime.Stop();
                MessageBox.Show("You lose..." + "\nYour score is " + score);
                MessageBox.Show("Restart the game?");
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }

        }

        private void setupGame()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
        {
            score = 0;
            ballPositionX = 5;
            ballPositionY = 5;
            playerSpeed = 10;
            gameScore.Text = "Score: " + score;
            gameTime.Start();
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(80, 80, 80);
                }
            }
        }

    }
}

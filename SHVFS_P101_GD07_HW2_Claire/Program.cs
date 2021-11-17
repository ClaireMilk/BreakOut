using System;
using System.Collections.Generic;
using System.Threading;

namespace Game
{
    internal class Program
    {
        private static Player PlayerOne = new Player()
        {
            Direction = new Vector2(1, 0),
            InputDirections = new Dictionary<ConsoleKey, Vector2>()
            {
                { ConsoleKey.W, Vector2.Up },
                { ConsoleKey.S, Vector2.Down },
                { ConsoleKey.A, Vector2.Left },
                { ConsoleKey.D, Vector2.Right },
             }
        };

        private static Player PlayerTwo = new Player()
        {
            Direction = new Vector2(-1, 0),
            InputDirections = new Dictionary<ConsoleKey, Vector2>()
            {
                { ConsoleKey.UpArrow, Vector2.Up },
                { ConsoleKey.DownArrow, Vector2.Down },
                { ConsoleKey.LeftArrow, Vector2.Left },
                { ConsoleKey.RightArrow, Vector2.Right },
             }
        };

        public const int timeScale = 100;
        //2d array
        private static bool[,] usedGridPositions;

        public static void Main(string[] args)
        {
            #region Draw the start screen
            var title = "Snake on a Train";

            Console.CursorLeft = Console.BufferWidth / 2 - title.Length / 2;
            Console.WriteLine(title);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Player One's Controls: \n\n" +
                "W - Up\n" + "S - Down\n" + "A - Left\n" + "D - Right");

            var playerTwoControlTitle = "Player Two's Controls: \n";
            var palyerTwoControlTitleCursorLeftPosition = Console.BufferWidth - playerTwoControlTitle.Length;
            Console.CursorTop = 1;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorLeft = palyerTwoControlTitleCursorLeftPosition;

            Console.WriteLine(playerTwoControlTitle);
            playerTwoControlTitle = "Up - Up";
            Console.CursorLeft = palyerTwoControlTitleCursorLeftPosition;
            Console.WriteLine(playerTwoControlTitle);
            playerTwoControlTitle = "Down - Down";
            Console.CursorLeft = palyerTwoControlTitleCursorLeftPosition;
            Console.WriteLine(playerTwoControlTitle);
            playerTwoControlTitle = "Left - Left";
            Console.CursorLeft = palyerTwoControlTitleCursorLeftPosition;
            Console.WriteLine(playerTwoControlTitle);
            playerTwoControlTitle = "Right - Right";
            Console.CursorLeft = palyerTwoControlTitleCursorLeftPosition;
            Console.WriteLine(playerTwoControlTitle);
            Console.ReadKey();
            Console.Clear();
            #endregion

            // Set up play area...
            Console.WindowHeight = 30;
            Console.BufferHeight = 30;
            Console.WindowWidth = 100;
            Console.BufferWidth = 100;
            usedGridPositions = new bool[Console.WindowWidth, Console.WindowHeight];

            PlayerOne.Direction = Vector2.Right;
            PlayerOne.Position.x = 0;
            PlayerOne.Position.y = Console.WindowHeight / 2;

            PlayerTwo.Direction = Vector2.Left;
            PlayerTwo.Position.x = Console.WindowWidth - 1;
            PlayerTwo.Position.y = Console.WindowHeight / 2;

            // Let's loop...
            //int i = 0;
            while (true)
            {
                //check player input and set directions
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.W && PlayerOne.Direction != Vector2.Down)
                    {
                        PlayerOne.Direction = Vector2.Up;
                    }

                    if (key.Key == ConsoleKey.S && PlayerOne.Direction != Vector2.Up)
                    {
                        PlayerOne.Direction = Vector2.Down;
                    }

                    if (key.Key == ConsoleKey.A && PlayerOne.Direction != Vector2.Right)
                    {
                        PlayerOne.Direction = Vector2.Left;
                    }

                    if (key.Key == ConsoleKey.D && PlayerOne.Direction != Vector2.Left)
                    {
                        PlayerOne.Direction = Vector2.Right;
                    }

                    if (key.Key == ConsoleKey.UpArrow && PlayerTwo.Direction != Vector2.Down)
                    {
                        PlayerTwo.Direction = Vector2.Up;
                    }

                    if (key.Key == ConsoleKey.DownArrow && PlayerTwo.Direction != Vector2.Up)
                    {
                        PlayerTwo.Direction = Vector2.Down;
                    }

                    if (key.Key == ConsoleKey.RightArrow && PlayerTwo.Direction != Vector2.Left)
                    {
                        PlayerTwo.Direction = Vector2.Right;
                    }

                    if (key.Key == ConsoleKey.LeftArrow && PlayerTwo.Direction != Vector2.Right)
                    {
                        PlayerTwo.Direction = Vector2.Left;
                    }
                }

                //set player position, based on their directions

                PlayerOne.Position += PlayerOne.Direction;
                PlayerTwo.Position += PlayerTwo.Direction;

                //check if a player has lost
                //draw the players
                //This code is Not DRY ->Don't repeat Yourself

                //Console.WriteLine(i++);

                //simple1dArray[PlayerOne.Position.x] = true;
                //simple1dArray[PlayerTwo.Position.x] = true;
                //usedGridPositions[PlayerOne.Position.x, PlayerOne.Position.y] = true;
                //usedGridPositions[PlayerTwo.Position.x, PlayerTwo.Position.y] = true;

                DrawPlayer(PlayerOne.Position.x, PlayerOne.Position.y, '*', ConsoleColor.Yellow);
                DrawPlayer(PlayerTwo.Position.x, PlayerTwo.Position.y, '*', ConsoleColor.Blue);

                //Draw player 1 as a yellow asterisk *
                //Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.SetCursorPosition(PlayerOne.Position.x, PlayerOne.Position.y);
                //Console.Write('*');

                //Console.ForegroundColor = ConsoleColor.Blue;
                //Console.SetCursorPosition(PlayerTwo.Position.x, PlayerTwo.Position.y);
                //Console.Write('*');

                //the game must check win, loss, or draw
                //win loss or draw happens when players hit each other, or leave the 
                //the game must track scores
                // the game must allow players to continue after each round

                Thread.Sleep(timeScale);

                if (PlayerOne.Position.x < 0 || PlayerOne.Position.x > Console.WindowWidth - 1 || PlayerOne.Position.y < 0 || PlayerOne.Position.y > Console.WindowHeight - 1)
                {
                    PlayerTwo.Score ++;
                    ResetConsole();
                }
                if (PlayerTwo.Position.x < 0 || PlayerTwo.Position.x > Console.WindowWidth - 1 || PlayerTwo.Position.y < 0 || PlayerTwo.Position.y > Console.WindowHeight - 1)
                {
                    PlayerOne.Score ++;
                    ResetConsole();
                }
                if (usedGridPositions[PlayerOne.Position.x, PlayerOne.Position.y] == true && usedGridPositions[PlayerTwo.Position.x, PlayerTwo.Position.y] == true)
                {
                    PlayerOne.Score ++;
                    PlayerTwo.Score ++;
                    ResetConsole();
                }
                else
                {
                    if (usedGridPositions[PlayerOne.Position.x, PlayerOne.Position.y] == false)
                        usedGridPositions[PlayerOne.Position.x, PlayerOne.Position.y] = true;
                    else
                    {
                        PlayerTwo.Score ++;
                        ResetConsole();
                    }
                    if (usedGridPositions[PlayerTwo.Position.x, PlayerTwo.Position.y] == false)
                        usedGridPositions[PlayerTwo.Position.x, PlayerTwo.Position.y] = true;
                    else
                    {
                        PlayerOne.Score ++;
                        ResetConsole();
                    }
                }
            }

            Console.ReadLine();         
        }

        public static void ResetConsole()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2, 1);
            Console.WriteLine("Players' scores: "+ PlayerOne.Score+ " : "+ PlayerTwo.Score +"\n");

            Console.SetCursorPosition(Console.WindowWidth / 2, 5);
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
            Console.Clear();

            for(int i=0;i<Console.WindowWidth;i++)
                for(int j = 0; j < Console.WindowHeight; j++)
                {
                    usedGridPositions[i, j] = false;
                }

            PlayerOne.Position.x = 0;
            PlayerOne.Position.y = Console.WindowWidth / 2;
            PlayerOne.Direction = Vector2.Right;

            PlayerTwo.Position.x = Console.WindowWidth - 1;
            PlayerTwo.Position.y = Console.WindowHeight / 2;
            PlayerTwo.Direction = Vector2.Left;
        }

        private static void DrawPlayer(int positionX, int positionY, char playerSymbol, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(playerSymbol);
        }

    public struct Vector2
        {
            public int x;
            public int y;

            public Vector2(int x,int y)
            {
                // this refers to the fields on THIS object, not the parameters/arguements in the constructor or method
                this.x = x;
                this.y = y;
            }

            public static readonly Vector2 Up = new Vector2(0, 1);
            public static readonly Vector2 Down = new Vector2(0, -1);
            public static readonly Vector2 Left = new Vector2(-1, 0);
            public static readonly Vector2 Right = new Vector2(1, 0);

            //operator overloads -> We must tell the compiler how to add, subtract, and compare our new, custom type
            // the computer doesn't know how to

            //operateor overloads
            public static Vector2 operator +(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x + v2.x, v1.y + v2.y);
            }

            public static Vector2 operator -(Vector2 v)
            {
                return new Vector2(-v.x, -v.y);
            }
            public static bool operator == (Vector2 v1, Vector2 v2) 
            {
                return (v1.x ==v2.x && v1.y ==v2.y);
            }
            public static bool operator != (Vector2 v1, Vector2 v2)
            {
                return (v1.x != v2.x || v1.y != v2.y);
            }
        }

        public class Player
        {
            public Vector2 Direction;
            public Vector2 Position;
            public int Score = 0;

            // Input direction
            public Dictionary <ConsoleKey, Vector2> InputDirections;
        }
    }
}

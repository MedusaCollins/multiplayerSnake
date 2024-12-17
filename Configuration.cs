using System;
using Raylib_cs;
using System.Numerics;

namespace SnakeGame
{
    public class GameConfig
    {
        public const int COLS = 25;
        public const int ROWS = 25;
        public const int screenWidth = 600;
        public const int screenHeight = 600;
        public const int cellWidth = screenWidth / COLS;
        public const int cellHeight = screenHeight / ROWS;

        public static bool isMenu = true;
        public static bool isGameOver = false;
        public static string winnerMessage = "";
        public static string winnerTitle = "";

        public struct Cell
        {
            public int x;
            public int y;
            public bool haveFood;
        }

        public struct Snake
        {
            public Vector2[] body;
            public Vector2 direction;
            public int length;
            public int capacity;
        }

        public static Cell[,] grid = new Cell[COLS, ROWS];
        public static Snake snake1 = new Snake
        {
            body = new Vector2[5],
            direction = new Vector2(1, 0),
            length = 1,
            capacity = 5
        };
        public static Snake snake2 = new Snake
        {
            body = new Vector2[5],
            direction = new Vector2(-1, 0),
            length = 1,
            capacity = 5
        };
    }
}

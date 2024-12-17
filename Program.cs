using System;
using Raylib_cs;
using System.Numerics;

namespace SnakeGame
{
    public class Program
    {
        const int COLS = 20;
        const int ROWS = 20;
        const int screenWidth = 400;
        const int screenHeight = 400;
        const int cellWidth = screenWidth / COLS;
        const int cellHeight = screenHeight / ROWS;

        struct Cell
        {
            public int x;
            public int y;
            public bool haveFood;
        }

        struct Position
        {
            public int x;
            public int y;
        }

        struct Snake
        {
            public Vector2[] body;   // Dinamik dizi
            public Vector2 direction;
            public int length;
            public int capacity;
        }

        static Cell[,] grid = new Cell[COLS, ROWS];
        static Snake snake = new Snake
        {
            body = new Vector2[5],
            direction = new Vector2(1, 0),
            length = 1,
            capacity = 5
        };

        static void Main(string[] args)
        {
            // Yılanın başlangıç pozisyonunu ata
            snake.body[0] = new Vector2(2, 2);

            // Raylib ile pencereyi başlat
            Raylib.InitWindow(screenWidth, screenHeight, "Classic snake game with raylib");
            Raylib.SetTargetFPS(15);

            // Grid yapısını başlat
            for (int x = 0; x < COLS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    grid[x, y] = new Cell { x = x, y = y };
                }
            }

            while (!Raylib.WindowShouldClose())
            {
                // Girişleri oku ve yılanı hareket ettir
                HandleInput();

                MoveSnake();

                // Yiyecekleri kontrol et ve oluştur
                if (!CheckFood())
                {
                    int randX, randY;
                    do
                    {
                        randX = new Random().Next(0, ROWS);
                        randY = new Random().Next(0, COLS);
                    } while (grid[randX, randY].haveFood);

                    grid[randX, randY].haveFood = true;
                }

                // Ekranı çiz
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);

                for (int x = 0; x < COLS; x++)
                {
                    for (int y = 0; y < ROWS; y++)
                    {
                        CellDraw(grid[x, y]);
                    }
                }

                Raylib.EndDrawing();
            }

            // Pencereyi kapat
            Raylib.CloseWindow();
        }

        static void CellDraw(Cell cell)
        {
            Color cellColor = Color.White; // Default to white (0)

            if (cell.haveFood)
            {
                cellColor = Color.Blue;
            }

            if (snake.body[0].X == cell.x && snake.body[0].Y == cell.y && cell.haveFood)
            {
                cellColor = Color.Blue;
                cell.haveFood = false;
                snake.length++;
            }

            // Eğer yılanın bir parçasına denk gelirse
            for (int i = 0; i < snake.length; i++)
            {
                if (cell.x == (int)snake.body[i].X && cell.y == (int)snake.body[i].Y)
                {
                    cellColor = Color.Orange;
                }
            }

            Raylib.DrawRectangle(cell.x * cellWidth, cell.y * cellHeight, cellWidth, cellHeight, cellColor);
            Raylib.DrawRectangleLines(cell.x * cellWidth, cell.y * cellHeight, cellWidth, cellHeight, Color.Black);
        }

        static void MoveSnake()
        {
            // Yılanın vücut kısmını kaydır
            for (int i = snake.length - 1; i > 0; i--)
            {
                snake.body[i] = snake.body[i - 1];
            }

            // Yılanın kafasını yeni pozisyona ekle
            snake.body[0].X += snake.direction.X;
            snake.body[0].Y += snake.direction.Y;

            // Kenarlarda tekrar dönmesi
            if (snake.body[0].X < 0) snake.body[0].X = COLS - 1;
            if (snake.body[0].X >= COLS) snake.body[0].X = 0;
            if (snake.body[0].Y < 0) snake.body[0].Y = ROWS - 1;
            if (snake.body[0].Y >= ROWS) snake.body[0].Y = 0;
        }

        static bool CheckFood()
        {
            for (int x = 0; x < ROWS; x++)
            {
                for (int y = 0; y < COLS; y++)
                {
                    if (grid[x, y].haveFood)
                        return true;
                }
            }
            return false;
        }

        static void HandleInput()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Up) && snake.direction.Y != 1)
            {
                snake.direction = new Vector2(0, -1);
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.Down) && snake.direction.Y != -1)
            {
                snake.direction = new Vector2(0, 1);
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.Left) && snake.direction.X != 1)
            {
                snake.direction = new Vector2(-1, 0);
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.Right) && snake.direction.X != -1)
            {
                snake.direction = new Vector2(1, 0);
            }
        }
    }
}


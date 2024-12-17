using System;
using Raylib_cs;
using System.Numerics;

namespace SnakeGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameConfig.snake1.body[0] = new Vector2(2, 2);
            GameConfig.snake2.body[0] = new Vector2(17, 17);
            
            Image iconImage = Raylib.LoadImage("icon.png");
            Raylib.SetWindowIcon(iconImage);

            Raylib.InitWindow(GameConfig.screenWidth, GameConfig.screenHeight, "Multiplayer Snake Game");
            Raylib.SetTargetFPS(14);

            for (int x = 0; x < GameConfig.COLS; x++)
                for (int y = 0; y < GameConfig.ROWS; y++)
                    GameConfig.grid[x, y] = new GameConfig.Cell { x = x, y = y };

            while (!Raylib.WindowShouldClose())
            {
                if (GameConfig.isMenu)
                    GameUI.HandleMenu();
                else if (GameConfig.isGameOver)
                    GameUI.GameOverMenu();

                else
                {
                    GameLogic.HandleInput();
                    GameLogic.MoveSnake(ref GameConfig.snake1, ref GameConfig.snake2);
                    GameLogic.MoveSnake(ref GameConfig.snake2, ref GameConfig.snake1);

                    if (!GameLogic.CheckFood())
                        GameLogic.SpawnFood();

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.RayWhite);

                    for (int x = 0; x < GameConfig.COLS; x++)
                        for (int y = 0; y < GameConfig.ROWS; y++)
                            GameLogic.CellDraw(GameConfig.grid[x, y]);

                    Raylib.EndDrawing();
                }
            }

            Raylib.UnloadImage(iconImage);
            Raylib.CloseWindow();
        }
    }
}

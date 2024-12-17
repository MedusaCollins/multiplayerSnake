using Raylib_cs;
using System.Numerics;

namespace SnakeGame
{
    public class GameLogic
    {
        public static void CellDraw(GameConfig.Cell cell)
        {
            Color cellColor = Color.White;

            if (cell.haveFood)
                cellColor = Color.Blue;

            if (GameConfig.snake1.body[0].X == cell.x && GameConfig.snake1.body[0].Y == cell.y && cell.haveFood)
            {
                cellColor = Color.Blue;
                GameConfig.grid[cell.x, cell.y].haveFood = false;
                GameConfig.snake1.length++;
                GrowSnake(ref GameConfig.snake1);
                GameLogic.SpawnFood();
            }

            if (GameConfig.snake2.body[0].X == cell.x && GameConfig.snake2.body[0].Y == cell.y && cell.haveFood)
            {
                cellColor = Color.Green;
                GameConfig.grid[cell.x, cell.y].haveFood = false;
                GameConfig.snake2.length++;
                GrowSnake(ref GameConfig.snake2);
                GameLogic.SpawnFood();
            }

            for (int i = 0; i < GameConfig.snake1.length; i++)
                if (cell.x == (int)GameConfig.snake1.body[i].X && cell.y == (int)GameConfig.snake1.body[i].Y)
                    cellColor = Color.Orange;

            for (int i = 0; i < GameConfig.snake2.length; i++)
                if (cell.x == (int)GameConfig.snake2.body[i].X && cell.y == (int)GameConfig.snake2.body[i].Y)
                    cellColor = Color.Red;

            Raylib.DrawRectangle(cell.x * GameConfig.cellWidth, cell.y * GameConfig.cellHeight, GameConfig.cellWidth, GameConfig.cellHeight, cellColor);
            Raylib.DrawRectangleLines(cell.x * GameConfig.cellWidth, cell.y * GameConfig.cellHeight, GameConfig.cellWidth, GameConfig.cellHeight, Color.RayWhite);
        }


        // Food
        public static void SpawnFood()
        {
            int randX, randY;
            bool foodPlaced = false;

            while (!foodPlaced)
            {
                randX = new Random().Next(0, GameConfig.COLS);
                randY = new Random().Next(0, GameConfig.ROWS);

                bool isOccupied = false;
                for (int i = 0; i < GameConfig.snake1.length; i++)
                {
                    if (GameConfig.snake1.body[i].X == randX && GameConfig.snake1.body[i].Y == randY)
                    {
                        isOccupied = true;
                        break;
                    }
                }
                for (int i = 0; i < GameConfig.snake2.length; i++)
                {
                    if (GameConfig.snake2.body[i].X == randX && GameConfig.snake2.body[i].Y == randY)
                    {
                        isOccupied = true;
                        break;
                    }
                }

                if (!isOccupied)
                {
                    GameConfig.grid[randX, randY].haveFood = true;
                    foodPlaced = true;
                }
            }
        }

        public static bool CheckFood()
        {
            for (int x = 0; x < GameConfig.ROWS; x++)
                for (int y = 0; y < GameConfig.COLS; y++)
                    if (GameConfig.grid[x, y].haveFood)
                        return true;

            return false;
        }


        // Snake 
        public static void MoveSnake(ref GameConfig.Snake snake, ref GameConfig.Snake otherSnake)
        {
            for (int i = snake.length - 1; i > 0; i--)
                snake.body[i] = snake.body[i - 1];

            snake.body[0].X += snake.direction.X;
            snake.body[0].Y += snake.direction.Y;

            if (snake.body[0].X < 0) snake.body[0].X = GameConfig.COLS - 1;
            if (snake.body[0].X >= GameConfig.COLS) snake.body[0].X = 0;
            if (snake.body[0].Y < 0) snake.body[0].Y = GameConfig.ROWS - 1;
            if (snake.body[0].Y >= GameConfig.ROWS) snake.body[0].Y = 0;

            for (int i = 1; i < snake.length; i++)
            {
                if (snake.body[0].X == snake.body[i].X && snake.body[0].Y == snake.body[i].Y)
                {
                    GameConfig.winnerMessage = $"{((snake.Equals(GameConfig.snake1)) ? "Orange" : "Red")} snake collided with itself!";
                    GameConfig.winnerTitle = $"{((snake.Equals(GameConfig.snake1)) ? "Red" : "Orange")} is the winner!";
                    GameConfig.isGameOver = true;
                    return;
                }
            }

            if (snake.body[0].X == otherSnake.body[0].X && snake.body[0].Y == otherSnake.body[0].Y)
            {
                GameConfig.winnerMessage = "Game Over!";
                if(GameConfig.snake1.length > GameConfig.snake2.length)
                    GameConfig.winnerTitle = "Orange is the winner!";
                else if(GameConfig.snake1.length < GameConfig.snake2.length)
                    GameConfig.winnerTitle = "Red is the winner!";
                else if(GameConfig.snake1.length == GameConfig.snake2.length)
                    GameConfig.winnerTitle = "It's a tie!";
                GameConfig.isGameOver = true;
                return;
            }

            for (int i = 0; i < otherSnake.length; i++)
            {
                if (snake.body[0].X == otherSnake.body[i].X && snake.body[0].Y == otherSnake.body[i].Y)
                {
                    GameConfig.winnerMessage = $"{((snake.Equals(GameConfig.snake1)) ? "Orange" : "Red")} snake collided with the {((otherSnake.Equals(GameConfig.snake1)) ? "Orange" : "Red")} snake!";
                    GameConfig.winnerTitle = $"{((snake.Equals(GameConfig.snake1)) ? "Red" : "Orange")} is the winner!";
                    GameConfig.isGameOver = true;
                    return;
                }
            }

        }

        static void GrowSnake(ref GameConfig.Snake snake)
        {
            if (snake.length >= snake.capacity)
            {
                snake.capacity *= 2;
                Array.Resize(ref snake.body, snake.capacity);
            }
        }

        public static void HandleInput()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Up) && GameConfig.snake1.direction.Y != 1)
                GameConfig.snake1.direction = new Vector2(0, -1);
            else if (Raylib.IsKeyPressed(KeyboardKey.Down) && GameConfig.snake1.direction.Y != -1)
                GameConfig.snake1.direction = new Vector2(0, 1);
            else if (Raylib.IsKeyPressed(KeyboardKey.Left) && GameConfig.snake1.direction.X != 1)
                GameConfig.snake1.direction = new Vector2(-1, 0);
            else if (Raylib.IsKeyPressed(KeyboardKey.Right) && GameConfig.snake1.direction.X != -1)
                GameConfig.snake1.direction = new Vector2(1, 0);

            if (Raylib.IsKeyPressed(KeyboardKey.W) && GameConfig.snake2.direction.Y != 1)
                GameConfig.snake2.direction = new Vector2(0, -1);
            else if (Raylib.IsKeyPressed(KeyboardKey.S) && GameConfig.snake2.direction.Y != -1)
                GameConfig.snake2.direction = new Vector2(0, 1);
            else if (Raylib.IsKeyPressed(KeyboardKey.A) && GameConfig.snake2.direction.X != 1)
                GameConfig.snake2.direction = new Vector2(-1, 0);
            else if (Raylib.IsKeyPressed(KeyboardKey.D) && GameConfig.snake2.direction.X != -1)
                GameConfig.snake2.direction = new Vector2(1, 0);
        }
    }
}

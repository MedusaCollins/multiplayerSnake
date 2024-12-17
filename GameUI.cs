using Raylib_cs;
using System.Numerics;

namespace SnakeGame
{
    public class GameUI
    {
        public static void HandleMenu()
        {
            Rectangle startButton = new Rectangle(200, 280, 200, 40);

            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePos = Raylib.GetMousePosition();
                if (Raylib.CheckCollisionPointRec(mousePos, startButton))
                    GameConfig.isMenu = false;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            int titleWidth = Raylib.MeasureText("Snake Game", 30);
            int titlePosX = (GameConfig.screenWidth - titleWidth) / 2;
            int titlePosY = 100;

            int buttonWidth = (int)startButton.Width;
            int buttonHeight = (int)startButton.Height;
            int buttonPosX = (GameConfig.screenWidth - buttonWidth) / 2;
            int buttonPosY = (GameConfig.screenHeight - buttonHeight) / 2;

            Raylib.DrawText("Snake Game", titlePosX, titlePosY, 30, Color.Black);
            Raylib.DrawRectangleRec(startButton, Color.LightGray);
            Raylib.DrawText("Start The Game", buttonPosX + 20, buttonPosY + 10, 20, Color.Black);

            Raylib.EndDrawing();
        }

        public static void GameOverMenu()
        {
            int titleWidth = Raylib.MeasureText(GameConfig.winnerTitle, 30);
            int titleHeight = 30;

            int messageWidth = Raylib.MeasureText(GameConfig.winnerMessage, 20);
            int messageHeight = 20;

            int screenCenterX = GameConfig.screenWidth / 2;
            int screenCenterY = GameConfig.screenHeight / 2;

            int titlePosX = screenCenterX - titleWidth / 2;
            int titlePosY = screenCenterY - (titleHeight + messageHeight) / 2 - 10;

            int messagePosX = screenCenterX - messageWidth / 2;
            int messagePosY = titlePosY + titleHeight + 10;

            Color titleColor = GameConfig.winnerTitle == "Red is the winner!" ? Color.Red :
                               GameConfig.winnerTitle == "Orange is the winner!" ? Color.Orange : Color.Black;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            Raylib.DrawText(GameConfig.winnerTitle, titlePosX, titlePosY, 30, titleColor);
            Raylib.DrawText(GameConfig.winnerMessage, messagePosX, messagePosY, 20, Color.Black);

            int scorePosY = messagePosY + messageHeight + 30;
            Raylib.DrawText($"Orange Snake Skor: {GameConfig.snake1.length - 1}", screenCenterX - Raylib.MeasureText($"Orange Snake Skor: {GameConfig.snake1.length - 1}", 20) / 2, scorePosY, 20, Color.Black);
            Raylib.DrawText($"Red Snake Skor: {GameConfig.snake2.length - 1}", screenCenterX - Raylib.MeasureText($"Red Snake Skor: {GameConfig.snake2.length - 1}", 20) / 2, scorePosY + 30, 20, Color.Black);

            Raylib.DrawText("Press space to restart", screenCenterX - Raylib.MeasureText("Press any key to restart", 20) / 2, scorePosY + 60, 20, Color.Black);

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                GameConfig.snake1.body = new Vector2[5];
                GameConfig.snake2.body = new Vector2[5];
                GameConfig.snake1.direction = new Vector2(1, 0);
                GameConfig.snake2.direction = new Vector2(-1, 0);
                GameConfig.snake1.length = 1;
                GameConfig.snake2.length = 1;
                GameConfig.snake1.capacity = 5;
                GameConfig.snake2.capacity = 5;

                GameConfig.snake1.body[0] = new Vector2(2, 2);
                GameConfig.snake2.body[0] = new Vector2(17, 17);

                for (int x = 0; x < GameConfig.COLS; x++)
                    for (int y = 0; y < GameConfig.ROWS; y++)
                        GameConfig.grid[x, y].haveFood = false;

                GameLogic.SpawnFood();

                GameConfig.isGameOver = false;
                GameConfig.winnerMessage = "";
                GameConfig.isMenu = false;
            }

            Raylib.EndDrawing();
        }
    }
}

using Raylib_cs;

class Program
{
    static void Main()
    {
        // Initialize Raylib window
        Raylib.InitWindow(800, 600, "Hello World - Raylib C#");

        // Set the target FPS (frames per second)
        Raylib.SetTargetFPS(60);

        // Main game loop
        while (!Raylib.WindowShouldClose())
        {
            // Start drawing
            Raylib.BeginDrawing();

            // Clear the background to a color
            Raylib.ClearBackground(Color.DarkGray);

            // Draw "Hello, World!" text at position (10, 10)
            Raylib.DrawText("Hello, World!", 10, 10, 20, Color.White);

            // End drawing
            Raylib.EndDrawing();
        }

        // Close the window and OpenGL context
        Raylib.CloseWindow();
    }
}

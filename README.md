# Snake Game

This project is a version of the classic **Snake** game. The player controls a snake that moves across the screen, and with each piece of food it eats, its length increases. The objective is to survive as long as possible without colliding with the walls or the snake's own body.

## Requirements

- **.NET 8.0**: The **.NET 8.0 SDK** must be installed to run this project.
- **Raylib 6.1.1**: **Raylib** is used for graphics in this project.

## Project Structure

Here's an overview of the project's structure and the purpose of key files and directories:

```
SnakeGame/
│
├── Program.cs/               # Main game loop and window initialization code.
│
├── GameUI.cs                 # Game UI and menu management.
│
├── GameLogic.cs              # All game logics.
│
├── Configuration.cs          # Game settings, structs and configuration.
│
└── multiplayerSnake.cs.proj  # Contains a list of all the files to be compiled as part of the project.
```


## Running the Project

### 1. Install .NET 8.0 and Raylib

Ensure that **.NET 8.0 SDK** is installed on your system. If not, you can download it from the [official .NET website](https://dotnet.microsoft.com/download).

### 2. Add Raylib to the Project

To add Raylib to the project, follow these steps:

1. **Install the Raylib NuGet Package**:
   
   Open the terminal and run the following command to add the **Raylib** package to your project:

   ```bash
   dotnet add package Raylib-cs --version 6.1.1

This will add Raylib 6.1.1 to your project.
    
## Run the Project:

To start the game, run the following command:

    dotnet run

This will launch the game, and you should be able to play it in a terminal window.


## Contribution Guideline

We welcome contributions! Here's how you can contribute:

1. Fork the repository.

2. Create a new branch with a descriptive name:

   ```bash
   git checkout -b feature/your-feature-name
   ```

3. Make your changes and commit them:

   ```bash
   git commit -m "Add a detailed description of your changes"
   ```

4. Push to your branch:

   ```bash
   git push origin feature/your-feature-name
   ```

5. Create a pull request on GitHub, explaining the purpose of your changes.

Please make sure to follow our coding standards and provide clear descriptions in your pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

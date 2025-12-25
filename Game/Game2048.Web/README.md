# Game 2048 Web Application

A web-based implementation of the classic 2048 game built with ASP.NET MVC.

## Project Structure

This project follows the same architecture as the FifteenGame.Web example:

- **Game2048.Business**: Contains the game logic and models
- **Game2048.Web**: Contains the web interface, controllers, and views

## How to Play

1. Use arrow keys (↑, ↓, ←, →) to move tiles
2. When two tiles with the same number touch, they merge into one
3. The goal is to create a tile with the number 2048
4. The game ends when you can't make any more moves

## Features

- Classic 2048 gameplay
- Score tracking
- Game state persistence in session
- Keyboard controls
- Responsive design
- Win/lose detection

## Technical Details

- **Framework**: ASP.NET MVC 5
- **Language**: C#
- **Target Framework**: .NET Framework 4.7.2
- **Architecture**: MVC pattern with separate business logic layer

## Game Logic

The game implements the following core mechanics:

- 4x4 grid
- Random tile generation (2 or 4)
- Tile sliding and merging
- Score calculation
- Game over detection
- Win condition (reaching 2048)

## How to Run

1. Open the solution in Visual Studio
2. Restore NuGet packages
3. Build and run the project
4. Navigate to the application URL in your browser

## Controls

- **Arrow Keys**: Move tiles in the corresponding direction
- **New Game Button**: Start a fresh game

## Project Files

### Business Layer
- `GameModel.cs`: Game board model
- `GameService.cs`: Game logic implementation

### Web Layer
- `GameController.cs`: Handles game actions and state
- `GameViewModel.cs`: View model for the game
- `CellViewModel.cs`: Individual cell representation
- `Views/Game/Index.cshtml`: Main game interface

The project structure mirrors the FifteenGame.Web example but implements 2048 game logic instead of a match-3 game.

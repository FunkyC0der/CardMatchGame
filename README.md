# Card match game

In this project, I used a DI container (Zenject) and a service-oriented model. I wrote the code following the KISS principle and, in my opinion, the most important SOLID principle: Single Responsibility. Additionally, I implemented a single entry point approach.

## Single entry point
The game must be launched from the Boot scene. In `ProjectInstaller` I bind `BootService`, it transitions into the initial game state, `BootGameState`. It handles all the necessary logic to set up the game and then loads either the `MainMenu` or `GameLevel` scene.

Additionally, I created a tool editor class called `BootSceneAutoLoader`, which automatically loads the Boot scene if any other scene is opened.

## High level architecture
The project is organized using Zenject installers:

* `ProjectInstaller` - Manages all project-wide services.
* `LevelInstaller` - Manages the GameLevel scene and contains all gameplay-related services.

## Game States
To control the game, I use a Game State Machine. I want my state machine to work with game states declared not only in the project context but also in the scene context. To achieve this, I created two helper classes—`GameStateFactory` and `GameStateChanger` — using the Zenject attribute `CopyIntoAllSubContainers`, ensuring they use the correct `DiContainer`.

Project-wide states (ProjectInstaller):
* `BootGameState` – Sets up the game during boot.
* `LoadLevelGameState` – Prepares the game for the GameLevel and loads it.
* `MainMenuGameState` – Loads and displays the main menu.

Level/Gameplay states (LevelInstaller):
* `LevelStartGameState` – Sets up the level at the beginning.
* `LevelLoopGameState` – Waits for the level to end (either when the timer finishes or a win condition is met).
* `LevelOverGameState` – Stops gameplay, updates progress data, and shows the game over popup.

## Game Notes
* The game features a list of levels that can be opened from the Main Menu.
* The game tracks player progress, including completed levels and best times, using the `ProgressService`.
  * For saving data, I implemented multiple formats through `ISerializer` and different save destinations via `ISaveLoadService`. These can be configured in the `ProjectConfig` ScriptableObject asset.
* The `SettingsService`, with a placeholder `SettingsData`, demonstrates how to manage game settings and integrate with the saving system.

## Gameplay Level Notes
* Levels are configured using the `LevelsData` ScriptableObject, allowing you to set:
  * Grid size
  * Timer duration
  * Time duration for showing card hints
  * Number of card matches required
* The level includes control buttons: Show Hint, Restart, and Menu.
* A hint is shown at the start of each level, indicating the number of cards to match.
* A game-over pop-up appears at the end of each level.
* I used the PrimeTween plugin to animate various elements and ensure code execution in the correct order.

## Services Notes
* `IAssetsService` – Loads and provides assets (e.g., `LevelsData` and UI data) for the game.
* `UIFactory` – Creates UI elements and windows on demand, and injects dependencies into them using `DiContainer`.
* `GameQuitSaver` – Saves the game upon quitting.

## Future improvements
* Add a specialized window service to manage more complex menus with multiple windows, allowing navigation back and forth using a stack of opened windows.
* Add real working settings.

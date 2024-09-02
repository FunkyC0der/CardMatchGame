# Card match game

In this project, I used a DI container (Zenject) and a service-oriented model. I wrote the code following the KISS principle and, in my opinion, the most important SOLID principle: Single Responsibility. Additionally, I implemented a single entry point approach.

## Single entry point
I used the multiple scene feature to implement this. The game must be launched from the Boot scene, which contains its own `BootInstaller` and a small `BootService`. This service handles game save loading and then uses the `SceneLoader` service to load the first scene. The `SceneLoader` loads either the MainMenu or GameLevel scene using `LoadSceneMode.Additive`. 

Additionally, I created a tool editor class called `BootSceneAutoLoader`, which automatically loads the Boot scene if any other scene is opened.

## High level architecture
The project is organized using Zenject installers:

* `ProjectInstaller` - Manages all project-wide services.
* `BootInstaller` - Manages the Boot scene dependencies.
* `LevelInstaller` - Manages the GameLevel scene and contains all gameplay-related services.

## Game Notes
* The game features a list of levels that can be opened from the Main Menu.
* The game tracks player progress, including completed levels and best times, using the `ProgressService`.
  * For saving data, I implemented multiple formats through `ISerializer` and different save destinations via `ISaveLoadService`. These can be configured in the `ProjectConfig` ScriptableObject asset.
* The `SettingsService`, with a placeholder `SettingsData`, demonstrates how to manage game settings and integrate with the saving system.

## Gameplay Level Notes
* Levels are controlled by the `LevelProgress` service.
* Levels are configured using the `LevelsData` ScriptableObject, allowing you to set:
  * Grid size
  * Timer duration
  * Time duration for showing card hints
  * Number of card matches required
* The level includes control buttons: Show Hint, Restart, and Menu.
* A hint is shown at the start of each level, indicating the number of cards to match.
* A game-over pop-up appears at the end of each level.
* I used the PrimeTween plugin to animate various elements and ensure code execution in the correct order.

## Future improvements
* `BootService` can load saves asynchronously.
* Add a loading curtain at game start and during scene transitions.
* Add a specialized window service to manage more complex menus with multiple windows, allowing navigation back and forth using a stack of opened windows.
* Create a new implementation of `ILevelInput` to support touch controls, or integrate the New Input System.

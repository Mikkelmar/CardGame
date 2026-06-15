# Card Game

<img width="1830" height="997" alt="card game" src="https://github.com/user-attachments/assets/a17d7f62-3a74-4b59-ac8a-3e2373b5650d" />
<img width="473" height="486" alt="card4" src="https://github.com/user-attachments/assets/0500de2b-f7e0-43d7-b2ef-ba02134f131b" />


A networked digital card game client built in C# with MonoGame. The project includes a deck collection screen, deck builder, card rendering system, hero powers, drag-and-drop card play, board interactions, WebSocket multiplayer client code, and a queue-based animation system for match events.

The game is inspired by Hearthstone and uses AI generated images using stable diffusion.

## Features

- MonoGame DesktopGL client targeting `netcoreapp3.1`.
- Collection and deck-slot screen with 15 local deck slots.
- Deck builder with search, mana-cost filtering, faction pages, hero-power selection, share/load deck buttons, and "only can add" filtering.
- Deck rules for 30-card decks, duplicate limits, legendary limits, and faction/power-card requirements.
- Card catalog data with 248 main card definitions and 36 token card definitions in the checked-in build output.
- Card factions represented in the UI: Arcane, Necrotic, Nature, Wild, Order, Discord, and Outworlders.
- Hero powers including fire blast, card draw, armor, damaged summon, nature heal, damage, daggers, and skeleton creation.
- Match board with player hands, minion board rows, heroes, hero powers, weapons, action history, targeting, and an end-turn button.
- Mulligan UI with keep/remove card selection and confirmation.
- Drag-and-drop card play from hand to board, including target selection for cards that require targets.
- Minion combat with board targeting, taunt validation, frozen/sleeping feedback, rush/charge checks, and attack animations.
- Visual status indicators for taunt, poison, lifesteal, deathrattle, aura, frozen, spell damage, and power shield.
- WebSocket multiplayer client that sends deck choices, hero powers, card plays, attacks, mulligan choices, hero-power targets, and end-turn events.
- Queue-based animation system for draw, discard, summon, attack, death, spell, hero power, fireball, frostbolt, magic missile, board burn, and trigger effects.
- MonoGame content pipeline for card art, faction frames, icons, fonts, and sound effects.

<img width="542" height="467" alt="discard" src="https://github.com/user-attachments/assets/42a88be8-3373-4abf-b244-ca1f4c48bf75" />
<img width="434" height="468" alt="card2" src="https://github.com/user-attachments/assets/5f21a068-7624-4a1e-8711-8e039c0837a7" />
<img width="306" height="469" alt="card1" src="https://github.com/user-attachments/assets/c8720b57-e305-4caf-a6bc-e17ec3e0c9e3" />

## Controls

| Input | Action |
| --- | --- |
| Mouse left click | Select deck slots, buttons, cards, minions, hero powers, and targets |
| Drag card | Move a playable card from hand toward the board |
| Release card on board | Attempt to play the card |
| Right click | Cancel targeting or close a hidden option selection |
| `Enter` in search box | Apply the collection search filter |
| `Esc` | Exit the application |

## Requirements

- Windows, macOS, or Linux support through MonoGame DesktopGL, though the current publish profile is Windows-focused.
- Visual Studio 2019 or newer, or the .NET CLI.
- .NET SDK capable of building SDK-style projects.
- .NET Core 3.1 runtime.
- The sibling shared project expected at `../CardGame.Shared/CardGame.Shared.projitems`.
- The sibling server project expected at `../Server/Server.csproj` when building `CardGame.sln`.
- A running compatible WebSocket game server. The client currently connects to `ws://51.175.74.234:7000`.

The project uses these NuGet packages:

- `MonoGame.Framework.DesktopGL`
- `MonoGame.Content.Builder.Task`
- `MonoGame.Extended`
- `Xamarin.Essentials`

> Note: .NET Core 3.1 is out of support. This project still targets `netcoreapp3.1`, so a newer SDK alone is not always enough.

## Build and Run

Expected sibling-folder layout:

```text
Documents/
  CardGame/
  CardGame.Shared/
  Server/
```

From the `CardGame` folder:

```powershell
dotnet restore CardGame.sln
dotnet build CardGame.sln
dotnet run --project CardGame.csproj
```

You can also open `CardGame.sln` in Visual Studio and run the `CardGame` project.

If you only want to build the client, the shared project is still required because `CardGame.csproj` imports `../CardGame.Shared/CardGame.Shared.projitems`.

## Current Build Notes

This checkout was tested with the .NET 8 SDK installed. The build does not currently complete from this repository by itself:

- `dotnet build CardGame.sln` fails because `../Server/Server.csproj` is missing.
- `dotnet build CardGame.csproj` fails because `../CardGame.Shared/CardGame.Shared.projitems` is missing.
- The project also targets `netcoreapp3.1`, so the .NET Core 3.1 runtime may be needed after the sibling projects are present.

## Project Layout

```text
Client/             WebSocket client, server action queue, and card serialization helpers
Content/            Card art, faction frames, icons, fonts, sounds, and Content.mgcb
Engine/             Program entry point, Game1, drawing setup, sound loading, and helpers
Graphics/           Texture loading, sprite drawing, and text helpers
Managers/           Page, mouse, object, sound, deck, collection, and visual board/hand managers
Objects/            Card actors, board actors, hero power UI, collection UI, buttons, popups, and game objects
Pages/              Collection page, game board, game interface, and weapon actor
PanimaionSystem/    Queueable game actions and visual animation classes
Properties/         Launch settings and publish profile
```

## Data and Saves

Deck saves are stored outside the repository in:

```text
%APPDATA%/MyCardGame/Saves
```

Each deck save stores:

- deck name
- selected hero power id
- comma-separated deck contents

The checked-in build output also includes card definition JSON files under:

```text
bin/Debug/netcoreapp3.1/Cards/
bin/Release/netcoreapp3.1/Cards/
```

## Development Notes

- `Game1` starts on the collection page and switches to the game board when a deck starts a battle.
- `Game1.isClient` defaults to `true`, so creating a `NetworkHandler` attempts to connect to the configured WebSocket server.
- `Content/Content.mgcb` builds the art, fonts, and sound effects used by the MonoGame content pipeline.
- The repository currently contains checked-in `bin` and `obj` folders. Consider adding a `.gitignore` and removing generated build output from source control if you want a cleaner repository.
- You currently need to configure the server IP manually in the client (TODO feature).


# Slot Machine — Unity Developer Internship Assignment

## 1. Game Overview

This project is a 2D slot machine game developed using Unity. Players can spin three reels, spend coins on each spin, and earn rewards by matching symbols across all three reels.

The game includes a starting coin balance, a fixed bet amount, randomized reel symbols, win detection, and a temporary win notification.

## 2. Instructions to Run the Game

### Run in Unity Editor
1. Install a compatible version of Unity.
2. Clone or download this repository.
3. Open the project through Unity Hub.
4. Open `Assets/Scenes/SlotMachine_Main.unity`.
5. Press the **Play** button in the Unity Editor.
6. Press **SPIN** to play.

### WebGL Build
The WebGL build is included in `Build/WebGL/`.

To test it locally, serve that folder using a local web server and open the provided local address in your browser. Opening `index.html` directly may not work correctly.

**Note:** The WebGL build currently has a rendering issue in the browser. The game has been tested in the Unity Editor, where its core gameplay works.

## 3. Features

- Three spinning reels
- Randomized slot symbols
- Starting balance of 1,000 coins
- Fixed bet of 50 coins per spin
- Win detection when all three symbols match
- Symbol-based payouts
- Balance and bet display
- Temporary win notification
- Prevention of spinning while a spin is in progress
- Insufficient-balance handling

## 4. Thought Process and Approach

The game was designed around a simple slot machine gameplay loop. Each spin deducts the bet from the player's balance, animates the reels by rapidly changing their symbols, and selects the final symbols randomly.

After the reels stop, the game checks whether all three symbols match. Winning combinations award coins according to the symbol's payout multiplier, while non-winning combinations display a retry message. The interface is updated to reflect the player's current balance.

The implementation separates gameplay logic from the visual interface through Inspector-assigned UI references, making the game easier to configure and maintain.

## 5. Technologies Used

- Unity
- C#
- TextMeshPro
- Unity UI
- WebGL

## 6. Project Structure

- `Assets/` — game scenes, scripts, artwork, and other project assets
- `Packages/` — Unity package dependencies
- `ProjectSettings/` — Unity project configuration
- `Build/WebGL/` — exported WebGL build

## 7. Author

Developed as part of the Unity Developer Internship assignment.

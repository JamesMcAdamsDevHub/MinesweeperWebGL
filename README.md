# Minesweeper

## Browser-Based Minesweeper

A classic Minesweeper game developed in Unity and C#, playable directly in desktop and mobile browsers through WebGL.

**[Play Minesweeper](https://www.jamesmcadams.com/games/minesweeper)**

---

## Description

This Unity project recreates the classic Minesweeper game with three difficulty levels, randomized bomb placement, and a scalable interface designed for browser-based gameplay.

The game uses a state-driven architecture to manage gameplay, tile interactions, and win/loss conditions. Bombs are generated after the first valid reveal to guarantee first-click safety, while a breadth-first search (BFS) algorithm automatically reveals connected regions of empty tiles.

Players can switch between revealing and flagging tiles, with a counter tracking the number of remaining flags. At the end of each game, bombs are revealed with visual indicators distinguishing correctly flagged bombs, unflagged bombs, and the bomb responsible for a loss.

---

## Features

- Three difficulty levels with configurable board sizes and bomb counts
- Randomized bomb placement with first-click protection
- Breadth-first search for revealing adjacent empty tiles
- Tile flagging with remaining flag counter
- Separate Reveal and Flag interaction modes
- Automatic win/loss detection
- Visual feedback for bombs, flags, and explosions
- Scalable interface with desktop and mobile browser support
- In-game restart and difficulty selection

---

## Difficulty Levels

| Difficulty | Board Size | Bombs |
| ---------- | ---------- | ----- |
| Easy       | 8 × 8      | 8     |
| Normal     | 10 × 10    | 16    |
| Challenge  | 12 × 12    | 30    |

---

## Technical Implementation

### Game Architecture

The project separates gameplay responsibilities across three primary components:

- **GameController:** Manages difficulty selection, menus, and gameplay UI controls.
- **BoardController:** Handles board generation, bomb placement, tile interactions, flagging, and game state.
- **Tile:** Manages individual tile properties, visual states, and player interactions.

### Breadth-First Search

When a tile with no adjacent bombs is revealed, a queue-based breadth-first search identifies and reveals connected empty tiles.

Numbered tiles bordering these regions are also revealed, while flagged tiles remain protected from automatic reveals.

### Game State Management

Gameplay is managed through separate game and interaction states.

Game states control initialization, active gameplay, reveal animations, and game completion. Interaction states determine whether selecting a tile reveals its contents or toggles a flag.

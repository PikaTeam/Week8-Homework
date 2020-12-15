# A* Implementation
## Game link (itch.io)
https://pikateam.itch.io/chasetile-week8hm

## Balloon
![Balloon](./github_media/balloon.png)
use the balloon to enter the next scene which contains a new tilemap with tiles based on the game of SPACE FUNERAL 2: BLOOD BLOOD
Gamplay of the original game can be found here:
https://www.youtube.com/watch?v=w8lbZOqNmIM&ab_channel=atumsk
The scene is based on the level that can be found at time of 2:32~

## Movement
Mouse only (left button on a tile)

## Weighted nodes/vertices/tiles/cells
Each tile/cell has now a weight attached to it:
![weighted tile](./github_media/weighted-tile.png)

## A* Heuristics
Using the A* heuristic pathfinding algorithm (with distance herusitic of L1-norm or "Manhatten") to move with the mouse by clicking at the cell to move the player to there.

## Character movement speed by tile
The movement speed in a tile depends on the tile's weights factor, the time delay between steps (in seconds) is calculated by the following formula:
```
1/(speed * 1/WeightOfTile) = WeightOfTile/speed
```
## Tile weights:
- Grass: 1
- Bushes: 2
- Hills: 4
- Swamp: 3

## Links
Link to A*: [CLICK HERE](./Assets/Scripts/0-bfs/AStar.cs)
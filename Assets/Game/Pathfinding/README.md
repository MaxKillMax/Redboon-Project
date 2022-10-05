Store - the second part of the task.
____
### Dots
- Dot is a visual display of the marked path. Dummy
- DotFactory dummy factory
~~Also, there is a LineRenderer, but it is currently in the PlayerInput~~
### Inputs
- PlayerInput I think it's clear. It contains all the control of the player's input. Acts as a controller.
### Pathfinding
- Pathfinding - an algorithm for creating a path. Regardless of the result, I would like to see how you created your version (if you have one) of the pathfinder. My algorithm supports mazes of rectangles. The only limitation is that rectangles cannot be rotated.
### UI
- PathfinderWindow interface to exit to the main menu.
### View
I decided to create a small view (the logic does not depend on the display). You can quickly assemble something from different shapes of rectangles in the scene, and then launch Pathfinding along this entire path. Works in semi-automatic mode. That is, each added Edge will need to be added to the PlayerInput array. And also, you need to calculate the structures of the Edge manually (for the rectangle, I created an automatic system for calculating their positions)
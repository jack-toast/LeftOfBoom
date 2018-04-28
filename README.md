# Left of Boom repository

## TODO:
* Destructible asteroids
* Ammo system
* Show wear on ship
* Time warp more fluid
* Implement non linear drag: Makes it feel more 'snappy'
* Loading from image:
  * not all aligned to grid
  * find center of mass of pixels, put larger asteroid there
  * sort of a packing solution for this... no idea where to start
  * talk to Chris about it.


## TO-DONE:
* Have the reset functionality reset the position of the magnetic asteroids
* Cannon particle effect
* Ship health
  * Damage taken scales with momentum exchange of collision
* Load obstacles from image


Reading the level from an image file:

Look further into:
* https://www.wikiwand.com/en/Tessellation
* https://www.wikiwand.com/en/Packing_problems
* https://www.wikiwand.com/en/Polyomino
* Division of shape with Voronoi structure: https://www.wikiwand.com/en/Voronoi_diagram
* ding ding ding: https://mathematica.stackexchange.com/questions/40334/generating-visually-pleasing-circle-packs
* JS solution: https://gist.github.com/gouldingken/8d0b7a05b0b0156da3b8
* Python solution: https://www.youtube.com/watch?v=HLUqDIOng80


Have a different image for mag asteroids, asteroids, and checkpoints

Pseudo-pseudo code
```
maxAttempts = 100
load in a binary image

attempt = 0
radius = maxRadius

while(radius > minRadius):
  attempts = 0
  while(attempts < maxAttempts):
    put a circle at random coordinate
    if the circle intersects another:
      attempts++
      delete circle
    if the circle intersects the perimeter:
      attempts++
      delete circle
  radius--

```

try to place circles at max radius,
when that stops working, decrease the radius,
do this until you've reached the minimum radius

At the end of this the circles should fully populate the play space

Need to calculate the density in order to spawn the correct number of circles

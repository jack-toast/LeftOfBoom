# Left of Boom repository

## Milestone 2 notes (Apr. 30th 2018):
1. Finalized art: I have made a significant effort in terms of art for the player's ship. For this I modeled the ship in Cinema 4D and modeled the cannon in Fusion 360. For the final milestone, I hope to have modeled the ship in Fusion 360 as well.

2. Working level: I have made it so that you can make a level by simply supplying a PNG image to the SceneController. I have created a few levels for the game because of this simplicity.

3. Level Configuration: The SceneController holds my *LoadAsteroidsFromImage.cs* script. This script generates the level according the image provided.

4. Physics and colliders:  Currently, all of the game objects are represented by spheres, so the collision system is simplified. I may change this in the future, but it works for now. If you point the cannon towards an asteroid and shoot, the asteroid will be pushed away. You will notice that this only works when asteroid is within a sort of cone or area of effect.

5. Reaction to collisions: The player's health will decrease when it collides with an asteroid. This decrease is proportional to the momentum lost in the exchange. The player's health is shown via the health bar and can be replenished by colliding with a checkpoint. Speaking of the checkpoints, you'll see that a counter increments whenever you collide with a checkpoint (trigger, not collider, but whatever).

6. Player input: The player input works just as it did before. Aim with your mouse and rotate with A and D.

7. Enemy AI: If moving slowly towards the player counts as AI, then the magnetic asteroids have AI functionality. Enemy ships are not part of this game in its current form or in my current vision for the game. I may implement this in the future, but given that the game focuses on your isolation, this is unlikely.


### Milestone 2 Achievements!:

**Reading from file:** As I talked about above, I spawn the asteroids and magnetic asteroids from an image called Level1.png. This image has a white background with black and red markings on it. The red markings denote where magnetic asteroids should spawn. The black markings denote where the regular asteroids should spawn. This image is sized at 512 x 512, but this size should not matter. Please note that one pixel corresponds to a distance unit in Unity.

**Level up!**: I have made a second level. I can de-spawn and respawn everything, but I haven't figured out the checkpoints sub-system. I will do this before the next checkpoint. 

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


### Notes regarding loading the level from an image file:

I have since completed this goal. I will keep the notes below for reference or for anyone else who might be interested.


Look further into:
* https://www.wikiwand.com/en/Tessellation
* https://www.wikiwand.com/en/Packing_problems
* https://www.wikiwand.com/en/Polyomino
* Division of shape with Voronoi structure: https://www.wikiwand.com/en/Voronoi_diagram
* ding ding ding: https://mathematica.stackexchange.com/questions/40334/generating-visually-pleasing-circle-packs
* JS solution: https://gist.github.com/gouldingken/8d0b7a05b0b0156da3b8
* Python solution: https://www.youtube.com/watch?v=HLUqDIOng80



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

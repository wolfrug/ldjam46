// Ombrascope hint system in full
VAR tribesmenHints = 0
VAR warriorsHints = 0
VAR foodHints = 0
VAR courageHints = 0
VAR clayHints = 0
VAR finalLocationHints = 0


==ombrascope_hint

->leave
=tribesmenHint
// The dig/the bunker
After moment, the image resolves into something familiar. Buildings, rising towards the skies, filled to the brim with people in white shirts and ties; they crowd together around screens that show images of destruction. This is a memory of a world long lost.

Then the image changes. A woman, her hair wrapped in a purple scarf, is hurrying downwards, using the stairs. Others follow her. She looks distraught, and tries to stop several times - but the men wearing dark clothes with the demeanour of warriors pick her up and force her to keep going.

The final image is of a great vault door, lit only by artificial light. In front of it, the woman and her retinue. The door opens, and they enter. Behind them, the room crumbles, and as it does, so does the image.
~tribesmenHints++

->leave
=warriorsHint
// The war golem
The image the ombrascope shows you is clearly more recent. A war golem is being outfitted for battle inside a massive stone hall, surrounded by hooded figures forming the golem's body with their minds. 

You see the golem, giant next to the hooded figures it is accompanying, head towards what appears to be a dust storm in the distance: the Moving City. 

The last image is fuzzy and confused. It is from inside the city. Explosive shots streak in from defensive turrets, tearing a hole in the golem's body. A Hunter-Killer appears suddenly, tearing apart one of the hooded figures. Then the image ends abruptly; the memory-bearer suddenly blinded.
~warriorsHints++

->leave
=foodHint
// The eating engine
The first image is fuzzy and shaky. An arm is pointing at the City's veins, the ever-present growths that take root anywhere there is arable ground. The image moves closer, and the arm points with increasing distress at the roots.

Another image, as fuzzy as the first: this one shows a machine like a giant furnace, its maw open. Inside it are cut veins of growth - as if fed to it. As you watch, an arm closes the maw, and the machine begins to glow.

The final image is different; it is not a memory, but a prophecy. The Veins are spreading, overtaking the City, soon overtaking the mountains around the city. The view moves back, showing more and more of the world - all of it being consumed by the growth.
~foodHints++

->leave
=courageHint
// Disassembler
The image you are shown is from the point of view of a person, yet it is not fuzzy or imprecise, but entirely clear - a recorded memory. It shows the world outside the City; trees, grass, an untainted sky. Your shamans murmur among themselves. In the distance you see the violent maelstrom that is the City; your destination.

The image fades and reappears. This time, you can see Clay being formed into shapes in front of your eyes in a way your Clayworkers could only dream of. The image is from inside the City - how the memory-bearer got inside, you have no idea. But they are forming golems, weapons, tools you have no idea what they are for out of Clay without as much as touching them.

And moments later, you see everything begin to melt away, disintegrate, sparks flying as the matter is sucked upwards. The image goes fuzzy for the first time as it looks up at whatever it is flying above, whatever it is that is causing all this damage. A vague shape, a bright light. And then it goes dark.

->leave
=clayHint
// powerplant
The first image is distinct, but you can immediately tell it is not a recent memory. The City is not yet the City - and the images are also strange. Strange glyphs and odd graphics line the sides and top as the image flies over a newly-constructed city district. The image of a man behind a desk is superimposed on the image in the lower corner.

It continues with a series of images that are clearly of the inside of the district - a machine, of some kind. No humans are present aside from a few that are clearly not there to do work. Someone flips a switch while others watch and applaud. Moments later, a light turns on.

The final image is from afar, and much more recent: in the distance, familiar shapes loom against the Veil, except now they are lumpen and mishapen, half-built, half-cannibalized by the City. And, you note, dark.

->leave
=finalLocationHint
// control room
Control room hint

->leave

=nonsenseHint
// happens now and against
Nonsense hint.

->leave

=leave
{debug:
->camp
}
->camp.leave
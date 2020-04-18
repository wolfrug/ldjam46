// Keep this include while working/testing, I will comment out when integrating
//INCLUDE clay_functions.ink
// Change the 'locationName' of all of these to correspond to the knot name below
// Set in the 'leave' stitch
VAR intro_battlefield_last_visit = -1
// this is set to true when the location is spawned. Needs to be set to false manually
VAR intro_battlefield_new = true
// this is set when spawned.
VAR intro_battlefield_creationTime = 0
// this is set when the object is very close to the edge
VAR intro_battlefield_dangerClose = false
// set this to true if you want it to be hidden
VAR intro_battlefield_hidden = false


==intro_battlefield
{intro_battlefield_new:
~intro_battlefield_new = false
->corpse
}
// needs to be set manually - could also be used when e.g. disabling repeatable loot

You survey the battlefield and its scars. One thing is certain: the Disassemblers will not leave anything they find here intact. If you wish to scavenge anything else, now is the time to do it.

* [Attempt to salvage the fallen Hunter-Killer {displayCheck(_clayworker, medium)}]
{skillCheck(_clayworker, medium):
The formidable steel bird lies dormant, although you curiously enough find no damage on it aside from what it suffered when it crashed. Finding an intact one is quite rare. You personally open up its cerebral carapace and remove the little nugget of Clay that was its mind before you let your Clayworkerks loose on it.

Soon enough, you have amassed a nice little pile of Clay, ready to be repurposed.
{alter(_clay, 10)}
->leave
- else:
You watch your Clayworkers pry at the machine's steel carapace, when suddenly, with an ominous whirr, the bird stirs. Before anyone has time to react, it twists and turns, razor talons raking open the Clayworkers swarming over it. Your tribesmen scatter as the Hunter-Killer's scorch ray begins to build up, but something is broken in the mechanism, and with a final shudder the bird explodes. {alter(_tribesmen, -5)} {alter(_courage, -10)}

Nothing useful remains afterwards. Grimly, you gather your dead and wounded.
->leave
}
// always use the leave stitch for leaving. Also be careful that it's a + not a *
+ [Leave]->leave

=corpse
You carefully make your way across the battlefield, your warriors tense and ready for anything, while your tribesmen stay at a safe distance. The air itself smells burnt, beyond the smoke: evidence of the Hunter-Killers' scorch-rays being used.

The battle was clearly intense. You see a dead Hunter-Killer wreck, fresh craters, and several crumbled golems. The tableau of destruction all points towards the smoking ruin of a man, lying where the Hunter-Killers left him in the middle of the road. Even at this distance, you can see he is not dressed like a Tribesman.

How could one man cause such mayhem? You sense the unease from your warriors.

* [You are not afraid. Approach the corpse yourself. {displayCheck(warchief, 2)}]
{skillCheck(_warchief, easy):
Your warriors are encouraged by your bravery, their hesitation forgotten.
{alter(_courage, 1)}
-else:
Despite attempting to put on a brave face, your warriors can sense how ill at ease you are.
{alter(_courage, -1)}
}
* [Send a tracker to give you the all-clear.{displayCheck(trickster, 2)}]
{skillCheck(_trickster, easy):
At your command, one of your trackers prowls on ahead while the rest of you wait. You are the chieftain, and your safety is more important than theirs.
- else:
The trackers look surprised when you command one of them to move forward. After a brief, whispered discussion, one of them takes the plunge.
{alter(_courage, -1)}
}
 You watch as he approaches the corpse, poking at it with his spear. After a few tense moments, the tracker waves at you to approach.
- You approach the corpse. Its charred skin is stretched taut over skull and flesh, leaving you with no notion of what the man - or, for that matter, woman - might have looked like. Their clothes, what remains of them, are of strange make, but definitely some type of armor.

That is when you see it. It lies next to his open palm, catching the light of the sun on its spotless golden skin. You recognize what it is immediately: an <i>ombrascope</i>. You reach for it without stopping to think; a golden ombrascope, held by an outsider, is unheard of.

The ombrascope fits perfectly in your palm, and feels smooth and warm. By shining a light through its center, one can project glyphs onto surfaces both big and small; glyphs that can open doors, chase away Hunter-Killers, or protect against Disassemblers.

But this one feels different. You can see the thin filaments that create the intricate shadows move, as if by themselves. Rearranging themselves into new shapes. New glyphs. You have to get this to your Shamans, at once.

* [Attempt to decipher the ombrascope. {displayCheck(_shaman, hard)}]
{skillCheck(_shaman, 6):
As you shine a light through the ombrascope, you see not a static glyph, but shifting images. You stare at them, transfixed, as they shift. You notice your shamans have joined you when you try to start making sense of them. 

->camp.ombrascope_guidance
- else:
You try to understand the vague images you can see reflected off the sand, but are soon frustrated. The shamans will be able to interpret them better, you hope.
->intro_battlefield
}
* [Give the ombrascope to your shamans, while you attend to other matters.]
->intro_battlefield


=leave
// always use this when finishing
// this sets the last visit time to current time, if this is needed for checks
~intro_battlefield_last_visit = currentTime
{debug:
->intro_battlefield
}
->DONE
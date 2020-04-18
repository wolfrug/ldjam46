// Keep this include while working/testing, I will comment out when integrating
//INCLUDE clay_functions.ink
// Change the 'locationName' of all of these to correspond to the knot name below
// Set in the 'leave' stitch
VAR hospital_last_visit = -1
// this is set to true when the location is spawned. Needs to be set to false manually
VAR hospital_new = true
// this is set when spawned.
VAR hospital_creationTime = 0
// this is set when the object is very close to the edge
VAR hospital_dangerClose = false
// set this to true if you want it to be hidden
VAR hospital_hidden = true


==hospital
{hospital == 1:
Your tribesmen balk at the idea of coming any closer to the House of Murmurs. Even out here you can hear the unnatural sounds emanating from behind the cracked walls. Whatever magic keeps the Disassemblers from destroying the building has also left it in a terrible state.
}

As you approach the rusted chain-link fence, it starts to swing open, apparently welcoming you. You step through, but not everyone is quite as brave.

+ [Encourage your whole tribe to pass through. {displayCheck(_courage, 100)}]
{skillCheck(_courage, 100):
After a bit of hesitation, the rest of your tribe pass through the gate. You hope you made the right call.
- else:
Most pass through, but you notice some who slink away, preferring to abandon the tribe rather than risk this cursed place. You wonder if they might not be the smarter ones. {alter(_tribesmen, -RANDOM(1,5))}
}
->insideHospital(true)
+ [Continue with just your warriors.]
You tell the rest of your tribe to wait by the gate. They seem grateful to do so. Your warriors, too proud to show fear, follow you in.
->insideHospital(false)
+ [Leave for now.]
You turn around and leave - for now.
->leave

=insideHospital(fulltribe)
The second door opens without hands as you approach. It takes you a moment to understand what you are seeing inside. Despite the dirt on the floor and the vines growing up the walls, you realize this building has never been looted.

Chairs sit where they are supposed to, monitors are placed on tables, couches and chairs line the walls next to flower pots with the dried-out husks of plants in them. As you slowly make your way into the building, you remember the old stories the Shamans used to tell of the world Before.

That is when you hear the sound. Not a murmur any more - no, a screech. The door to the outside closes behind you with a bang, shutting you in with whatever inhabits this cursed place.

{fulltribe:
Your people, all trapped in there with you, look to you for leadership. The door is shut securely. It will not budge.
- else:
Your warriors crowd around you, gripping their weapons. Unsure of if they will be any use.
}

You turn to look in the direction of your shuffling doom - even as you feel something vibrate in pouch you keep the ombrascope in. You touch it, and find that the whole pouch is indeed shaking.

But you do not have time to think about that. From the darkness steps a monstrosity; a machine of the ancients, oozing from every pore black, living clay.

"It is time for your examination." It hums merrily, lifting a sawblade.

->endDemo

=leave
// always use this when finishing
// this sets the last visit time to current time, if this is needed for checks
~hospital_last_visit = currentTime
->DONE
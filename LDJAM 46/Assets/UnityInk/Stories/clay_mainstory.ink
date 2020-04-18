INCLUDE clay_functions.ink
INCLUDE clay_locationTemplate.ink
INCLUDE clay_battlefield.ink
INCLUDE clay_camp.ink
INCLUDE clay_hospital.ink
INCLUDE clay_mainEncounters.ink
INCLUDE clay_ombrascope.ink




VAR random_encounter_last_visit = -1
VAR random_encounter_new = true
VAR random_encounter_creationTime = 0
VAR random_encounter_hidden = false
VAR debug = true
{debug:
~trickster = 3
~hasOmbrascope = true
->camp
}
==start
Many generations ago, the world outside the bounds of the City ended, but the City did not know that. It kept assembling and disassembling itself forever, trapping you and your tribe inside.

The City is alive, and to many - including many in your tribe - the City is God, who provides, but also punishes those who do not follow His will.

You are the Chieftain of your tribe, and it is your responsibility to see to the well-being of your people, and to lead them in your never-ending pilgrimage through the City, ever Windward.

You look upon them now, once more gathering their belongings to continue your trek, even as the Wind picks up.

{alter(_warriors, 10)}
{alter(_tribesmen, 50)}
* [Your warriors stand proud, front and center, always ready to defend their fellow tribesmen. (+Warriors) (+Warchief)]
{alter(_warriors, 15)}
{alter(_warchief, 1)}
* [A populous tribe is yours, and skilled too in many things: the trades, claywork, leatherwork, herdwork. (+Tribesmen) (+Clayworker) ]
{alter(_tribesmen, 25)}
{alter(_clayworker, 1)}
* [It is a sad sight. Once you were mighty, but the years have taken their toll. (+Difficulty)]
{alter(_tribesmen, -25)}
- It is time to decide where you are headed next. But a good chieftain does not make decisions without consulting their advisors. As always, you look to...

* [Your shamans (+Shaman)]
{alter(_shaman, 1)}
Knowing the City and its moods and secrets is the purvey of the Shaman. Reading the Wind, communing with the <i>eidolons</i> of the City, and remembering the history of the tribe; all these things the Shamans do, and you have learned to listen to their advice.
* [Your scouts (+Trickster)]
{alter(_trickster, 1)}
Although the Shamans think they can read the  City's intentions by listening to the Wind, your Scouts can tell you what they saw with their own two eyes. You have always trusted more in what you can see and touch than the secretive world of the <i>eidolons</i> the Shamans commune with.
* [Your warchiefs (+Warchief)]
{alter(_warchief, 1)}
Without your warriors, your tribesmen would be at the mercy of the City - or rather its inhabitants. They are what stands between you and the predations of outcasts, scavengers and tribeless raiders - not to mention the monsters of the City itself.
- However, you already know what they will advise you to do. Last night, you saw the Hunter-Killers swarm an area just Windward of you. You heard the patter of their weapons echoing off the buildings, and then silence. Whatever they hunted, and killed, would be there. It could also be an unfortunate fellow tribe that caught the ire of the City for some reason, but usually when the Hunter-Killers are found this far from the Veil, it is because of an intrusion. 

From time to time, things trickle in from beyond the Veil; things the Disassemblers missed, or ignored. That was what happened to your tribe, many generations ago, although in most cases it is not living things, but objects. But the interest of the Hunter-Killers suggest something that was alive. You consider your own motivations for going.

* [Information about the world beyond the Veil is a valuable commodity when trading with other tribes. (+Pactmaker)]
{alter(_pactmaker, 1)}
* [You must always learn more of how the City works, and thinks. Your survival depends on it. (+Shaman)]
{alter(_shaman, 1)}
* [Legend says, some Intruders in the past brought with them wondrous weapons. The tribe could use such artefacts. (+Warchief)]
{alter(_warchief, 1)}
* [Your curiosity must be sated, that is all. (+Trickster)]
{alter(_trickster, 1)}

- The tribe is all but ready to head out. As chieftain, you know the content of every pack and cart. How much surplus food you have. How much unmoulded Clay remains to fashion into tools when necessary. But beyond that, you also know the heart of your people: you know when their hearts are strong and when they falter.
{alter(_food, 25)}
{alter(_clay, 10)}
{alter(_courage, 50)} 

* [You observe your herd of rustled cattle with pride. Their former brands are all but healed. (+Food)(+Trickster)]
{alter(_food, 10)}
{alter(_trickster, 1)}
* [The clayworkers' carts are laden heavy. The City has been kind to you. (+Clay)(+Clayworker)]
{alter(_clay, 5)}
{alter(_clayworker, 1)}
* [You can hear the laughter in the air, see the trust in their eyes as they look upon you. (+Courage)(+Pactmaker)]
{alter(_courage, 25)}
{alter(_pactmaker, 1)}
* [Alas, there is little to be proud of now. Your herd is thin, your supplies running out. And it shows. (+Difficulty)]
{alter(_food, -5)}
{alter(_clay, -5)}
{alter(_courage, -25)}
- Even as you prepare to give the order to move, you think back to what your {~father|mother}, Chieftain before you, always told you.

* ["Remember: most things can be solved by words, not blood." (+Pactmaker)]
{alter(_pactmaker, 1)}
* ["Be like the Clay we use for our tools: always ready to change." (+Clayworker)]
{alter(_clayworker, 1)}
* ["Listen to the City and learn its language, and all doors will open for you." (+Shaman)]
{alter(_shaman, 1)}
- You tell your people to start moving. Soon, the caravan of your tribe begins walking down the cracked and dusty pavement, between the looming half-built houses of the City, towards your destiny.

->DONE

==touch_edge
You are sucked up into the void and die, oh no.
#endgame
The end.
->END

==endDemo
<color=red>This is the end of the demo! More to come soon! Thanks for playing!</color>
#endgame
The end.
->END
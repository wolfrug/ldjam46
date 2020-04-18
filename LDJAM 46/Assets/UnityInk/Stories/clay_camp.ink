// Keep this include while working/testing, I will comment out when integrating
//INCLUDE clay_functions.ink
// Change the 'locationName' of all of these to correspond to the knot name below
// Set in the 'leave' stitch
VAR camp_last_visit = -1
// this is set to true when the location is spawned. Needs to be set to false manually
VAR camp_new = true
// this is set when spawned.
VAR camp_creationTime = 0
// this is set when the object is very close to the edge
VAR camp_dangerClose = false
// set this to true if you want it to be hidden
VAR camp_hidden = false
// this is set by what ground you are standing on
VAR camp_zone = "default"
// coefficients = coeff/2 - coeff*2 min/max. So 5 = 2-10.
VAR camp_zone_clayCoeff = 5
VAR camp_zone_foodCoeff = 5
VAR camp_zone_peopleCoeff = 1
VAR camp_zone_dangerCoeff = 0

==camp
{~You set up camp in the shadow of a half-built apartment building, using the plastic sheets that cover the sides to reinforce your tents.|Your tribesmen contentedly begin settling down, shaking their tired legs.|You make camp, like you have done so many times before. The Wind howls as you work in silence.|The order to set up camp propagates down your convoy. Mothers lift their children from their backs, sighing with relief.|You order your warriors to secure the perimeter as your tribesmen begin erecting the camp tents.|Erecting the camp is practiced action; soon, the smell of cooking begins drifting towards you.}

Your people are <> 
{
- exhausted: 
exhausted. <>
- rested:
well rested. <>
- else
in need of rest. <>
}
The mood in the camp is <>
{courage > 0:
good.
- else:
 despairing.
}

+ {nightTime} [Rest for the night {displayTime(80)}]->nightRest
+ {not nightTime} [Set up camp and rest {displayTime(80)}]->dayRest
+ {hasOmbrascope} [Decipher the Golden Ombrascope {displayTime(20)}] ->ombrascope_guidance
+ [Scout the area({displayCheck(_trickster, medium)}{displayTime(10)}]
{startTimer(0, 10, "camp.scout")}
You send out the scouts. ->leave
+ [Scavenge for supplies.]->scavenge
+ {tribesmen > 0} [Train warriors.] ->train
+ [Continue your journey]
{~Your tribesmen get to work and quickly disassemble the camp. Within minutes, it is like you were never there.|Your tribesmen set to work pulling down tents and packing the carts. In no time at all, you are ready to go.|Once the order is given, it takes mere minutes for the camp to disappear into packs and carts. Soon, your tribe is ready to move again.|You give the order and watch as children are coralled, tents pulled down and draught animals hitched to carts. Soon, you are ready to go.|Decamping takes mere minutes, your tribesmen working in effective lockstep. Everyone knows speed is of the essence in the City.}
->leave


=nightRest
{startTimer(0, 80, "camp.restDone")}
{exhausted:
{~Your tribesmen, practically sleeping on their feet, collapse into their tents. Only the night watchmen stay awake, peering into the dark by the campfires.|Exhausted men,  women and children gratefully crawl into their tents. Within minutes, the only movement in the camp are the night watchmen stirring the flames of the campfires.|You can hear sighs of relief throughout your people, as they are finally given a chance to rest. Soon, the whole camp is still.} 
- else:
{~Your tribesmen settle in for the long night by their campfires, singing songs to their children to put them to sleep.|Soon, your camp is lit up by multiple campfires, as your tribe prepares for the night.|Once the night watch is set up, there is little else to do but tuck in and rest your wearied bones.}
}
->leave
=dayRest
{startTimer(0, 80, "camp.restDone")}
{exhausted:
{~Your tribesmen, delirous with exhaustion, collapse into their tents. A silence falls over the camp within minutes.|Exhausted men,  women and children gratefully crawl into their tents. The guards left outside stare with bleary eyes into the Wind.|You can hear sighs of relief throughout your people, as they are finally given a chance to rest. Soon, the whole camp is still.} 
- else:
{~Your tribesmen settle in for a rest from the never-ending walk, happy to rest their legs.|No matter the time of day, rest is always welcome. Soon, the camp goes quiet.|The camp settles in for a rest, although small groups of those not quite weary enough chat quietly by tent openings.}
}
->leave

=restDone
{exhausted:
{~The people crawling from their tents are like new; revived once more after a good rest.|You can tell from the atmosphere in the camp that the rest did more than good. Your tribe is once more ready to face the City.|Despite some groaning, most seem to have recovered well. Soon, the camp is bustling with energy.|After a long rest, your tribesmen seem fully revived.}
}
Your tribe is once again rested and ready to head out.
~rested = true
~exhausted = false
->leave
=ombrascope_guidance
{LIST_COUNT(knownLocations)<1:
There is a sense of reverence as you settle down around the golden ombrascope. The moment you shine a light through it, you can see the image projected through it is no mere glyph: it moves.

This is something different, and interpreting it will take time.
}
{startTimer(0,20, "camp.ombrascope_guidance_done")}
{nightTime:
You and your shamans huddle around a campfire, the shadows of the ombrascope especially lively from the flickering flames.
- else:
Finding a place where the sun shines through unrestricted, you and your shamans settle down to read what the ombrascope tells you.
}
{debug:
->ombrascope_guidance_done
}
->leave

->leave
=ombrascope_guidance_done

{!~->ombrascope_hint.tribesmenHint|->ombrascope_hint.warriorsHint|->ombrascope_hint.foodHint|->ombrascope_hint.courageHint|->ombrascope_hint.clayHint|->ombrascope_hint.finalLocationHint|->ombrascope_hint.nonsenseHint}
->ombrascope_hint.nonsenseHint

{debug:
->camp
}
->leave

=rationing
It is time to declare the day's rations. As always, it is up to you to decide how.

~temp totalNeed = warriors + tribesmen + 1
~temp fullRations = totalNeed
~temp halfRations = totalNeed/2
~temp thirdRations = totalNeed/3

+ {food>=fullRations} [Give everyone full rations (-{fullRations} food)]
{alter(_food, -fullRations)} {alter(_courage, 5)}
You distribute the rations evenly to everyone. No-one goes hungry today.
+ {food >=halfRations} [Give everyone regular rations (-{halfRations} food]
{alter(_food, -halfRations)}
Your tribe understands food is at a premium. No-one complains, but the mood is slightly less jovial.
+ {food>=thirdRations} [Give everyone a third rations (-{thirdRations} food]
{alter(_food, -thirdRations)} {alter(_courage, -5)}
The mood is grim as the ration sizes for the day are revealed. The march ahead will be tough.
+ {food < thirdRations && food > 0} [There isn't enough food for everyone, but you distribute what you have.]
~temp courageLoss = totalNeed - food
{alter(_courage, -courageLoss)} {alter(_food, -totalNeed)}
The announcement is met with quiet shock. Not that anyone was truly surprised, but no-one is happy.
+ {food == 0}[There are no rations to give.]
To escape every day from the Veil, to walk endless miles across cracked asphalt, requires energy. To do so on an empty stomach is torture. You will not be able to continue like this for long.
{alter(_courage, -totalNeed)}

-Once the rationing for the day has been declared, the tribe continues.
->leave

=scavenge
You have {print_num(tribesmen)} able-bodied tribesmen. What would you like them to look for?
~temp foodDifficultyModifier = (tribesmen*2)-warriors-(warriors*(warchief-1))
+ [Clay. {displayCheck(_clayworker, 4)}{displayTime(20)}]
{startTimer(0, 20, "camp.scavenge_done_supplies")}
{skillCheck(_clayworker, medium):
Your skilled clayworkers head out together with the tribesmen to help them look.
- else:
Your tribesmen will have to tend to themselves and find what they can.
{alter(camp_zone_clayCoeff, -1)}
}
+ [Food. {displayCheck(tribesmen, foodDifficultyModifier)}{displayTime(20)}]
{startTimer(0, 20, "camp.scavenge_done_food")}
{skillCheck(_tribesmen, foodDifficultyModifier):
Your tribesmen begin foraging, safe in the knowledge a warrior is always nearby to defend them if need be.
- else:
You do not have enough warriors to defend everyone. As a result, the area you can forage becomes smaller.
{alter(camp_zone_foodCoeff, -1)}
}
+ [People. {displayCheck(_pactmaker, 4)}{displayTime(20)}]
{startTimer(0, 20, "camp.scavenge_done_people")}
{skillCheck(_pactmaker, medium):
Your tribe is renowned for its hospitality. You are sure many will be interested.
- else:
Your tribesmen are a surly lot, and you wonder how much new blood they will be able to attract.
{alter(camp_zone_peopleCoeff, -1)}
}
- ->leave

// no proper thingamajog given, hmm
->leave
=scavenge_done_supplies
~temp result = RANDOM(camp_zone_clayCoeff/2,camp_zone_clayCoeff*2) + RANDOM(0, tribesmen/6)
{alter(_clay, result)}
Your tribesmen return with what they find, for your clayworkers to melt down.
->leave

=scavenge_done_food
~temp result = RANDOM(camp_zone_foodCoeff/2,camp_zone_foodCoeff*2) + RANDOM(0, tribesmen/3)
{alter(_food, result)}
Your tribesmen return with what they find, growing in cracks or hidden in strange crevices.
->leave

=scavenge_done_people
~temp result = RANDOM(camp_zone_peopleCoeff/2,camp_zone_peopleCoeff*2) + RANDOM(0, tribesmen/12)
{alter(_tribesmen, result)}
{result > 0:
Your tribesmen return with stragglers, eager to join a new tribe.
- else:
Your tribesmen return with no-one.
}
->leave

=farming_done
~temp result = RANDOM(camp_zone_foodCoeff/2,camp_zone_foodCoeff*2)
{alter(_food, result)}
Your seedlings grow with monstrous quickness. Once they are ripe, you harvest them quickly, before they grow out of hand.
->leave

=train
All of your tribesmen can take up arms to defend the caravan - but that means they will not be available to scavenge or do other kinds of work. You will also need Clay to fashion into weapons and armour for them, and time to give them the training they need.

+ {tribesmen >= 5 && clay >=5} [Train a small warband (+5 Warriors, -5 Tribesmen, -5 Clay) {displayCheck(warchief, 4)}]
{skillCheck(_warchief, medium):
{startTimer(0, 10, "camp.training_done_small")}
You set an example for the recruits to follow, and the volunteers you get are eager to get started. Training them will be short work.
- else:
{startTimer(0, 20, "camp.training_done_small")}
You fail to garner much enthusiasm among your people, and the warriors you finally get are slow to learn.
}
+ {tribesmen >=10 && clay >=10} [Train a larger warband (+10 Warriors, -10 Tribesmen, -10 Clay){displayCheck(warchief, 6)}]
{skillCheck(_warchief, hard):
{startTimer(0, 20, "camp.training_done_big")}
You set an example for the recruits to follow, and the volunteers you get are eager to get started. Training them will be short work.
- else:
{startTimer(0, 40, "camp.training_done_big")}
You fail to garner much enthusiasm among your people, and the warriors you finally get are slow to learn.
}
+ [Never mind.]
- ->leave

=training_done_small
{alter(_warriors, 5)} {alter(_tribesmen, -5)} {alter(_clay, -5)}
Your new warriors go to join their new comrades, ready to protect the life of their fellow tribesmen.
->leave
=training_done_big
{alter(_warriors, 10)} {alter(_tribesmen, -10)} {alter(_clay, -10)}
Your new warriors go to join their new comrades, ready to protect the life of their fellow tribesmen.
->leave

=scout
{not task_success: ->DONE}
{skillCheck(_trickster, medium):
You send out your best scouts to find out more about the area you are in. They are soon back with information
Clay: {camp_zone_clayCoeff}
Food: {camp_zone_foodCoeff}
People: {camp_zone_peopleCoeff}
Danger: {camp_zone_dangerCoeff}
- else:
You send out your scouts, but they return with limited information:
{RANDOM(0,100)>50:
Clay: {camp_zone_clayCoeff}
- else:
Clay: ?
}
{RANDOM(0,100)>50:
Food: {camp_zone_foodCoeff}
- else:
Food: ?
}
{RANDOM(0,100)>50:
People: {camp_zone_peopleCoeff}
- else:
People: ?
}
{RANDOM(0,100)>50:
Danger: {camp_zone_dangerCoeff}
- else:
Danger: ?
}
}
->leave

=leave
// always use this when finishing
// this sets the last visit time to current time, if this is needed for checks
~camp_last_visit = currentTime
->DONE
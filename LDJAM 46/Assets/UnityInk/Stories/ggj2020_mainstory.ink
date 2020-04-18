INCLUDE ggj2020_functions.ink

VAR debug = true
{debug:
~insight = 1
~token = 8
~honey = 8
~nacre = 8
~theurgy = 2
~obols = 8
~snippet = 8
~mettle = 3

->start
}
== start

#changebackground #spawn.background.church #play.earthquake
An Un-God has emerged from the cthonic bowls of the city, ripping apart the sacred halls of the Church of Marrow, and breaking the seal of the Sacred Dead which kept the unquiet hordes at bay.

The city is in a state of panic. 

Is it a sign? Is it the end of days? Is it that pesky heretical cult trying to wake up the Elder Gods again?

Without the seal, the city lies defenceless, and none shall be safe until the Church is re-sanctified, and the seal reforged. 

You have been tasked with overseeing the repairs. 

But in order to do that, you'll need to convince everyone to work together.

*[Easier said than done.]-> professions 

== professions
You are the Arbiter, but before that you were...

*[a Hunter.]
Many things in the underground needed hunting. And not all hunting was after food.
~mettle = 4
~insight = 3
~artifice = 2
~candour = 1
~theurgy = 0
*[an Academic.]
You spent most of your time behind a cushy desk at the Osseus University, learning all there is to know about the Boneworks.
~mettle = 0
~insight = 4
~artifice = 3
~candour = 2
~theurgy = 1
*[an Inquisitor.]
You are the one who asked the pointed questions. Uncovered the heretics, the cultists, the un-godly believers.
~mettle = 1
~insight = 0
~artifice = 4
~candour = 3
~theurgy = 2
*[a Warrior Cleric.]
You trained with the finest warriors of the clergy, honing your deadly arts in service of the Church. ‘So that the sleeping gods may never wake. Amen.’
~mettle = 2
~insight = 1
~artifice = 0
~candour = 4
~theurgy = 3
*[a Necromancer.]
Someone needs to make sure the dead stay dead. And skeletons make for excellent manual laborers.
~mettle = 3
~insight = 2
~artifice = 1
~candour = 0
~theurgy = 4

- But there was always something extraordinary about you. A reason you caught the Arbiters’ attention.

* [You aren’t averse to spilling a little blood]
You were a duellist, exceptionally skilled with a blade. You carved out a reputation for yourself through violence, and those who knew you learned to fear your name.
~mettle += 1
* [You learned to survive at an early age.]
An urchin, you were forced to make your own way in the world, and that made you cunning. You learned to observe others, and used the knowledge you gleaned to your own advantage.
~insight += 1
* [You were always a liar.]
Able to deceive and manipulate. You played the people around you as though they were strings on a harp, using charm and half-truths to get what you wanted. 
~artifice += 1
* [You grew up in a big family.]
You had more sisters, brothers, uncles, aunts, grandmothers and grandfathers than you could shake a stick at. And somehow you ended up being the lubricant that kept the whole thing together.
~candour += 1
* [You were born with magic.]
Where most must study for decades to foster the spark of magic, to you it came at an early age. ‘Born one foot in the Nether’, you were never in danger of being bullied on the schoolyard.
~theurgy +=1

- But your past is behind you now; it is forbidden to refer to an Arbiter’s background, on pain of death. It remains a part of you, but it is not you. Not any more.

*[You have a job to do.] Time is of the essence. ->intro

= intro
The Church of the Marrow stands before you - or at least what's left of it. 

When the Un-God breached the earth, it brought down most of the east wing with it.

The creature now lolls amidst the remains, a somnolent worm, its maw lined with concentric rings of translucent teeth. 

As you watch a Nun from the Mellliferous Order of Mercy strides forth and impales the beasts single albescent eye with her sword. The creature shudders, then grows still. 

You look away. 

Your business lies elsewhere - for now.

*[Keep walking.] ->church

== church
Bone-dust drifts through the air like snow, filtered through the light of the jagged fibula that arch precariously overhead.

The Priest is waiting for you in the remains of the apsis, his lips moving in quiet prayer as he genuflects before the shattered altar. 

*[You cough politely to alert him of your presence.]
*[You wait for him to finish his prayers, listening for interesting morsels he might divulge.]
*[You tap him on the shoulder, interrupting his worship. After all you're very busy and important, and you don't have all day.]

-“Arbiter!” The Priest exclaims, and rushes forward to shake your hand. “Glad to make your acquaintance.”

*[”Likewise.”]
*[Naturally.”]
*[“Charmed, I’m sure. ”]

-Together you inspect the damage that has been wrought by the great beast. Shattered bones lie everywhere, empty skulls glowering at them accusingly as they pass.
->breach_help

= breach_help

*[Ask him how the seal was broken.]->breach
*[Ask him if there’s any way you can assist the Church with repairs.]->help

= help
“Well now that you mention it--”

*[“--there is the matter of the Intercostal Brotherhood.”]->engineers

*[“--the Church does have a problem that could use a more delicate touch.”]->heresy

= engineers
The priest goes on to describe the great saga of the Intercostal Brotherhood of Engineers, and how they refuse to listen to reason.

“It’s their JOB to repair this sort of thing, but they flat out refuse to lift a finger. I don’t suppose you could find a way to convince them to assist us once more with the repairs to the Church?”->breach_help

= heresy

“It’s those filthy heretics with their filthy notions.” The priest explains. “They’re on the rampage, stirring up public sentiment against the holy orders, making unreasonable demands, distributing their sordid little pamphlets.” He shudders in what you only assume is abject horror.

“Perhaps you could poke around a little? Find a way to...silence, them?” He smiles knowingly.->breach_help

= breach

“The seal is meant to keep the Undead and the Other from breaching the city walls, so why did it fail now?”

The priest sighs, and wipes his brow. “My superiors have numerous ideas on the subject. It’s possible the beast managed to find a weakness in the spell, and merely exploited it. Or perhaps heretics were involved. But for my own part, I’m not so sure.”->options
=options
*[Ask him about the worm.]->the_worm_itself
*[Ask him about the heretics.]->those_pesky_heretics
*[Ask him about his own suspicions.]->discord_and_disarray
*[You think you have enough information to go on.]->endconversation
= the_worm_itself
“The truth of the matter,’ he tells you with some reluctance, ‘is that the worm itself could never have breached the threshold of the city on its own.”

“The ancient magic that binds us is too ancient, too complex. No, the great beast merely revealed what was already broken.
->options

= those_pesky_heretics

“Heretics?” You ask, somewhat amused. You’ve seen their pamphlets around the city and hardly credit them as a threat.

The priest clearly senses your disbelief.

“Oh they are quite dangerous, I assure you.” He says. “But I suspect this is beyond even their dark learning. No, the cause lies elsewhere.”

He almost seems disappointed.
->options
= discord_and_disarray

“What do you think is the true cause?”

The Priest shuffles his feet, uncomfortable in the face of your open scrutiny.

“I suspect the city itself is the cause. The ancient magics that bound the seal together where based on the strength of the city itself, and the ties that bind us all together. It’s no secret that there has been much discord of late - neighbour fighting with neighbour, religious disagreements, strife and hardship throughout the city.”

“In truth I suspect this to be the root cause. It’s our fault. We are the ones who broke the seal.”
->options

=endconversation
You promise to return once you’ve made some progress. #changebackground

->DONE

== atheneum
{enterLocation(loc_atheneum)}
{atheneum<2: 
The Atheneum sits, stout and sombre, in the sacrum, before you. Outside, dour-looking Inquisitors congregate, truncheons in hand, looking more like thugs than church officials. Inside the so-called Heretics dwell, somehow sometimes managing to release a pamphlet containing the most shocking heresy to the world, despite the blockade.

As an Arbiter, you are free to pass - although the men from the Church eye you with suspicion.
}

{~You watch as the Inquisitors question a young boy carrying several steaming plates of food. Apparently, you overhear, the Scholars managed to place an order with a restaurant by means of carrier pigeon.|One of the Inquisitors, apparently in a jovial mood, offers you some tobacco. You politely decline.|There is a commotion from inside. A window is slammed, quickly.|You catch a glint of something in a window: a looking glass?|A cadre of Inquisitors are banging on the gate. When you arrive, they stop, and quickly leave, pulling their collars up.}

You enter, the young man who opened the door for you quickly shutting it behind you.

{atheneum<2:
For a supposed hive of hereticism and villainy, the Atheneum is uncommonly tidy. Rows upon rows of bookshelves, interspaced with reading desks. The smell of parchment and paper is heavy in the air, together with a certain stuffiness, probably from keeping the windows shuttered. You can hear the sound of someone speaking from further in - perhaps a lecture hall.
}
->options
=options
+ {timeSinceVisited>2}[(This option only available after time as passed!)]
How exciting. By zeroing timeSinceVisited, we can prevent this from happening until next time!
~timeSinceVisited = 0
->options
+ {rector && not nacre_done}[Deliver the nacre to the Rector. {displayCheck(nacre, 10)}]->nacre_check
+ {nacre_done && not tokens_done}[Hand over the  Royal Tokens to the Rector. {displayCheck(token, 10)}]->tokens_check
+ [Attend a lecture (Insight: {displayCheck(insight, 6)}{displayTime(1)}]
{passTime(1)}
{~The lecturer suggests nacre is proof that God was a beetle. The hall erupts in murmuring.|A debate about the true nature of the diet of bees devolves into name-calling.|A professor of Ossiology lectures at length about the number of teeth in the Mouth.|The lecture has an apocalyptic mood: someone suggests the breaking of the Seal might lead to things that should be dead no longer remaining dead.|”Is it not so,” The Annelidae Lecturer states. “That the Church hates the Boneless God specifically because It does not have bones, not because of anything in particular this so-called God has done?”}
{skillCheck(insight, 6):
You listen with interest.
~temp gainz = RANDOM(1,6)
{alter(snippet, gainz)}
-else:
The argument seems unnecessarily esoteric. You soon find your eyelids grow heavy.
}
->options
+ [Leave.]
You quietly leave the venerable halls of learning. ->leave
* [Speak with the Enthusiastic Rector]->rector
= rector
    You are admitted into the Rector’s office on the second floor. It would have a fine view, if the windows were not barred and barricaded with planks. The Rector makes no mention of it.

“The destruction of the Seal! Yes, very fascinating. Of course we have nothing to do with it, and, in fact, if it is not soon repaired, we will all suffer a horrendous fate.” The Rector pushes up his eyeglasses. “But you know that. Now - despite our situation here, I believe we can help one another. The first thing I will need is some nacre.”

<i>Nacre</i>. Only one place grows nacre. The Necropolis. You nod, and promise to return with some.
->options

=nacre_check
{resourceCheck(nacre, 10, -1):
->nacre_done
-else:
The Rector looks at you sadly. “I am afraid we are going to need more than this. And oh - careful! Look, this specimen is entirely spoiled already. You <i>must</i> keep it away from moisture, oh my…”
}
->options
= nacre_done
The Rector beams at you. “Perfect! Look how fresh this is - this isn’t some ossified remnant mined from the deep pits, this is <i>new</i>. A sign of the impending doom!” He nods, pleased. “Now, before we can continue with our research, we will need some royal dispensation.

<i>Of course.</i> It was never easy was it. That meant currying favour with the nobles in the Palace.
->options
=tokens_check
{resourceCheck(token, 10, -2):
->tokens_done
-else:
The Rector looks over your tokens with a critical eye. After a moment, he sighs, pushing his spectacles up his nose. “Some of these are counterfeit. Either way, this is not enough to safely begin our experiments.” He hands you back the, apparently, non-counterfeit ones.
}
->options
=tokens_done
The Rector counts the tokens, then recounts them. He looks giddy with excitement. “Ah, this is the first time in <i>yonks</i> that we are actually going to do <i>sanctioned</i> experiments! Oh happy day!”

He gets up and almost runs off, before noticing you still standing there. “Oh, right! Your, ah, seal fragment. It’s in my desk there.” He points, then runs out into the corridor, shouting for his staff.

You go to the desk and open the drawers. Amongst various bits of esoterica, much of it entirely illegal, you find a seal fragment. Why he had it, and why he kept it in his desk, you may never know. {alter(fragments, 1)}

->leave

=leave
{exitLocation(loc_atheneum)}
->DONE

==palace
{enterLocation(loc_palace)}
{palace<2:
You walk up the stairs of the lower mandible, and find yourself before the gates of the Buccal Palace. It rises before you, a necrotic husk, bone-white and imposing. The very walls seem to creak with the weight of years, and the great crest of the maxilla arches overhead, casting its long shadow over the city below.
}
As you pass through the desiccated hallways, you can’t help but wonder at how desolate it all seems. If you didn’t know better, you’d assume the place was abandoned.


->options
=options

+ [Leave.]
You gladly leave the stagnant, dusty halls of the sclerotic noblesse behind. 
->leave
+ {steward && not resource_done}[Deliver honey to the steward. {displayCheck(honey, 10)}]
->resource_check_honey
+ {steward && not resource_done}[Deliver obols to the steward. {displayCheck(obols, 10)}]
->resource_check_obols

+ [Ingratiate yourself with the atonic nobility. (Artifice: {displayCheck(artifice, 6)}{displayTime(1)}]
{passTime(1)}
{~You make smalltalk with some listless aristocratic youths.|You spend half an hour discussing the finer points of genealogy with an elderly patrician.}
{skillCheck(artifice, 6):
Against all odds you manage to gain some useful information from the encounter.
~temp gainz = RANDOM(1,6)
{alter(token, gainz)}
-else:
You find yourself unable to recall a single item of interest. What a waste of time.
}
->options
*[Wander the dessicated halls, and try not to get lost.] 
It’s easy to get turned around in the labyrinthine hallways, and after a while you find that one crumbling ballroom looks much like another. But it must have been beautiful once. ->options
*[Try to find someone who can help you, or well, anyone really.]
You wander aimlessly through the many rooms of the palace, until at last something catches your eye. You emerge into a massive room, the walls cloaked in shadow, the floor made of striated black marble. At first you think you’re alone but then you see them, sitting on their thrones at the shadowy edges of the room. 

The Sclerotic Princes; seven in number, and you observe that each one is more withered and decrepit than the last.

Are they even alive? Is that snoring you hear?

You move to investigate, but before you can do so someone taps you on the shoulder from behind. 

You nearly jump out of your skin, turning to find yourself face to face with the High Steward himself - a sepulchral gentlemen, stern of visage and temperament.

“<i>Shhhh</i>.” He hisses at you, somewhat violently. “You’ll wake them.”

He gestures impatiently for you to follow him outside, and then shuts the door firmly behind you both. ->steward

=steward
“Well, what do you want?” He asks. You get the distinct impression that he isn’t a man overly fond of small talk.

You explain your situation, trailing behind the Steward as he strides through the halls at a brisk pace, long coat tails flapping behind him.

“Yes, well I’m afraid I can’t help you with any of that.”He says, once you’ve finished talking. “At least not without recompense. The Princes have expensive dietary requirements, so if it’s tokens you’re looking for you’ll have to bring us something in return. Obols, or Honey. Either one will do.” He tells you.

Before you can respond he shuts the door in your face - rather rudely you might add, and you realise you’ve been lead back to the palace antechamber. Oh well, at least you don’t have to worry about finding your way out.->options

=resource_check_honey
{resourceCheck(honey, 10, -1):
->resource_done
-else:
->resource_fail
}
=resource_check_obols
{resourceCheck(obols, 10, -1):
->resource_done
-else:
->resource_fail
}
=resource_fail
“Oh, it’s <i>you</i>.” The Steward says, arching one razor-thin brow in your direction. 

He looks over the items you’ve brought, and waves them away dismissively. “No, none of this will do. Who do you think you’re dealing with, some grubby Guildmaster? Come back when you have something more substantial to offer.” He says, and waves you out of his office.

You suppose you should be grateful he didn’t just slam the door in your face like last time.
->options
=resource_done
The Steward accepts your offerings with great reluctance. “I suppose this will have to do.”he says begrudgingly, but you see the way he hands linger on the package.

You hold out a hand expectantly, and the Steward huffs.

“Fine, fine.”He mutters, and retrieves a sheaf of tokens from his desk. He counts them out for you with infinite slowness, then scowls.

“Happy?”

You wouldn’t go that far. But you’ll be glad to see the back of him. As you leave you notice there is something else hiding amidst the tokens. A seal fragment. Perhaps the Steward wasn’t so bad after all. {alter(fragments, 1)}
->options

=leave
{exitLocation(loc_palace)}
->DONE

==cenotaph
{enterLocation(loc_cenotaph)}
{alter(cenotaphTimer, -timeSinceVisited)}
{cenotaph<2:
You step into the dark hollow, eyes adjusting to the gloom. Before you lies the cenotaph of the Boneless God; a monument bound in heavy chains. Underneath the statue of the chained worm the Guilds have set up a lively marketplace, the stalls dimly lit with lanterns.
}

{~Hawkers cry out their wares in quick, staccato bursts, like carrion crows.|There is a ceremony of some kind occurring at the outskirts of the market. Hooded acolytes scatter when they spot you.|As you approach the market, all sound stops for an instant; like a silent wind came and gathered all the words and swept them away. Then the babble returns; subdued.|Lanterns burn. Grave candles flicker and spill wax on the carved rock. The sound of the marketplace ebbs and flows.|You spot a procession of nobles from the palace, surrounded by guards. They move very slowly, causing a jam. No-one dares say anything.|You are swamped almost immediately by over-eager hawkers, trying to sell you knucklebone rosaries and grave offerings.}

Everywhere you hear the soft clink of silver obols; funerary coins, stolen from the eyes of the dead. Illicit, but valuable.
->options
=options
+ [Leave.]
You turn around and leave the bustling market, for now. ->leave

+ {cenotaphTimer <= 0} [This only exists if the last time this was touched was 5 turns spent outside of Cenotaph ago!]
~cenotaphTimer = 5
Horaay!
->options
+ {mendicant_honey_done && not mendicant_token_done} [Return to the Mad Mendicant with proof of the Palace's favor. {displayCheck(token, 10)}]->mendicant_token_check
+ {mendicant && not mendicant_honey_done} [Return to the Mad Mendicant with a gift of Bone Honey. {displayCheck(honey, 10)}]->mendicant_honey_check
+ {mendicant} [Help the merchants mediate their squabbles.(Candour: {displayCheck(candour, 6)}{displayTime(1)})]
{passTime(1)}
{skillCheck(candour, 6):
{~Your help is much appreciated.{alter(obols, 3)}|You manage to solve a long-standing grudge. Both sides pay you well.{alter(obols, 6)}|Despite your best efforts, neither side walks away happy. The bystanders pay you for the entertainment.{alter(obols, 1)}|You listen for half an hour at an angry merchantwoman talk about her husband. Afterwards, she pays you handsomely.{alter(obols, 5)}}
-else:
{~Your lack of social graces leads to the squabbles becoming more, rather than less, intense.|Your attempts at mending a long-standing grudge only leads to it becoming worse.|Within minutes, you are entirely forgotten in a shouting match between two merchants.|You attempt to give some advice to an angry merchantwoman, but the moment you speak she goes beet red and leaves in a huff.|The parties in the argument end up in a street fight. {~Someone slips you an obol as a payment for providing entertainment.{alter(obols, 1)}|While you weren't looking, someone rifled through your purse!{alter(obols, -1)}}}
}

->options
*[Explore the market]
You take your time, walking between stalls. Most recognize you as the Arbiter, but that carries little weight here. Your kind rarely offers arbitration for their kind; yours is a Royal office, after all.
->options
*[Attempt to find a Guildmaster, or someone in charge. (Insight: {displayCheck(insight, 3)})]
{skillCheck(insight, 3):
Your inquiries do not lead you to a Guildmaster, but someone else entirely. At first you can barely believe it, but everything leads to the same man. You find the Mad Mendicant at the base of the monument to the Boneless God, up a set of stairs that takes you to the top of the dais. When he sees you he smiles a near-toothless smile.

“Welcome. We have been waiting for you. Here. Take these.” {alter(obols, 5)} He hands you a handful of cold silver coins. “For your trouble.” ->mendicant
-else:
You soon find the nominal leader of the marketplace, a master in the Guilds. When you speak to him, however, it quickly becomes apparent he holds no real power.

Frustrated, you make to leave, when someone grabs you by the sleeve. An urchin, face covered in dirt. “This way. Th’ Mendicant wants t’ speak t’ ya.” You follow a few steps behind him, frowning. The urchin leads you up a hidden set of stairs, up to the very base of the monument. There you find the Mad Mendicant. He smiles toothlessly when he sees you.

“Finally.”->mendicant
}
=mendicant
He sits on what you quickly realize is the ratty remains of a piece of tapestry from the royal palace, an opulent red. Around him candles melt into pools of wax, upon which more candles have been piled and lit. In front of him is another dirty cloth, upon which lies silvery obols; gifts, or bribes?

“We know why you are here.” He croons, settling down on his throne. He is dirty; wrinkled; gap-toothed. Even at a distance, he smells. “The servants of the Boneless God have broken through.”

*[Ask for his help.]
You open your mouth to ask for his assistance, but he pre-empts you with a raised hand, the nails dirty and claw-like.

“No, you listen to us.”
*[Listen.]
You watch. You listen.

- “We have what you need. But you will first give me what we need.” The Mad Mendicant licks his lips. “We desire <i>honey</i>.”

<i>Honey.</i> You know what kind of honey he desires. Bone Honey. There was only one place to get that, and it was not in the marketplace.

“Return with honey. Then we shall speak more.” He waves you aside.

As you return to the marketplace, you notice the sellers there are much more relaxed around you. As if the Mad Mendicant’s blessing had been bestowed on you. Soon, you are swamped with requests to mediate between the myriads of petty squabbles the merchants have amongst themselves. You are promised obols as payment.
->options

=mendicant_honey_check
{resourceCheck(honey, 10, -1):
The Mad Mendicant takes the little jars of honey, his eyes gleaming. You cannot believe he actually intends to eat it, though. It would melt the very bones in his body. ->mendicant_honey_done
-else:
The Mad Mendicant takes {honey>1: one of the jars|the jar} you offer, opens it, and sniffs. He shakes his head. "More. We need more. This is not enough." He sends you on your way - sans one jar. ->options
}
=mendicant_honey_done
Within moments, the honey is gone. He looks up at you and smiles, horrifically.

"Before we continue, we have one more thing to do for the people who are our charges. They desire...a communique, with the Palace. Bring us tokens of the Palace's favor, and we will give you the fragment you need."

There was only one place to gain the favor of the noblesse. The Palace.
->options

=mendicant_token_check
{resourceCheck(token, 10, -2):
You wordlessly give him the tokens. He counts them, nodding to himself.

"Yes. We can leave them now, without guilt. It is important to be without guilt." ->mendicant_token_done
-else:
The Mad Mendicant barely looks at you, or your offered tokens. "No. We need stronger assurances all will be well." He hands them back. Only after you leave do you realize some are missing. 
->options
}
=mendicant_token_done
From underneath the ratty tapestry, he produces what he promised. It glows faintly with magical light. He throws it to you, and you scramble to catch it. {alter(fragments, 1)}

"You are too late. What was broken was never meant to be mended. But we thank you." You notice the Mad Mendicant is shivering. Drooling. Next to him lies the empty vials of Bone Honey. Your eyes widen.

"Soon, we will be one with the Boneless One. We, too, shall slither into the Earth and join Him, and leave this calcified purgatory for good. One way we may meet again, Arbiter, but you should hope we do not.

* [Leave the madman to his doom.]
You turn and leave, quickly. Behind you, the Mad Mendicant melts. His screams follow you as you escape. ->leave
* [Ask him what he is doing.]
"We are descending. Soon we will be emptied of our useless bones. On this place, we will become one with the Boneless One." His smile is even more terrifying than it was before. His features seem to melt, his skin sag. "Ahhh, the pain...it is exquisite!"
** [Leave the madman to his doom.]
You turn and leave, quickly. Behind you, the Mad Mendicant melts. His screams follow you as you escape. ->leave

=leave
{exitLocation(loc_cenotaph)}
->DONE
==necropolis
{enterLocation(loc_necropolis)}
{necropolis<2:
The climb to the top of the hill left you feeling out of breath, and you take a moment to rest, and to admire the view. Below you can see the valley stretch out, each aspect taking its name from its anatomical neighbour. The Buccal Palace. The Apiary of the Intercostal Sisterhood. And the Marrow Church, even in its ruined state. The Necropolis is above and beyond all that; outside the embrace of the Body of God. An appropriate place to store the dead.
}

The Necropolis is not a place of rest for now, however, but a battlefield. As you stride through the gate, you can hear {~the hoarse coughing of ghouls.|the sounds of battle ahead.|a Warrior-Priest shouting for a medic.|the tell-tale sound of theurgy; like the air itself was thrilling.}
->options
=options
+ [Leave.]
You leave the Necropolis and its battlefield behind. ->leave
+ {commander && not obols_done}[Return with silver obols for the Steward. {displayCheck(obols, 10)}]->obols_check
+ {obols_done && not snippets_done}[Use what you have learned to find the Metatarsal Vault. {displayCheck(snippet, 10)}]->snippets_check
+ [Assist in the defence against the restless dead. (Theurgy: {displayCheck(theurgy, 6)}){displayTime(1)}]
{passTime(1)}
{skillCheck(theurgy, 6):
{~You stand side-by-side with the Warrior-Clerics, joining your magic to theirs. The shambling corpses crumble before you.|You summon a gust of wind with a prayer. It rips a skeletal warrior apart, the bones clattering to the marble floor.|A hail of arrows! At the last instance you summon up a shield; the arrows bounce harmlessly off it.|You go among the wounded, healing their wounds where you can.|You sense the spectre before you see it. With a whispered word, your magic ignites it. You watch as it flees, burning, into the ossuary.}
~temp gainz = RANDOM(1,6)
Grateful, the Warrior-Clerics let you scrape some of the nacre, spreading on the inside of the Transposed Scapula.
{alter(nacre, gainz)} 
- else:
You attempt to aid, but your aid is more harmful than helpful. Soon, you are forced to retreat, leaving the fighting to those with the skills for it.
}
->options
* [Find the Commander of the Wall]->commander

=commander
 You ask around for the Commander of the Wall, the woman in charge of the defence against the Unquiet Horde. But everywhere you turn you are met with ignorance. Until you come across the Commander’s steward, staring wide-eyed down a set of stairs leading to the catacombs below.

“She went there, with a company of her best men. Said there were fragments of the Seal there. She hasn’t come back.” The steward sighs. “And neither has anyone else.”

“And now...no-one can follow her either.” The Steward shakes his head. “That chamber down there is filled with Royal Mummies. Even if they stir, we are not permitted to destroy them.”

Royal Mummies. That meant they were wrapped up and, at least for the moment, approachable. If one were to return the obols to their eyes before they broke free, they would become paralyzed again. You suggest this to the steward, whose eyes light up. “Yes! Of course. We will need a number of them, of course…”
->options
=obols_check
{resourceCheck(obols, 10, -1):
->obols_done
-else:
The Steward looks at what you’ve brought and shakes his head sadly. “This won’t do. It’s simply not enough. We have to pacify <i>all</i> of them, or we run the risk of breaking the law by destroying a Royal Relative. He hands back the obols - or at least most of them.
}
->options
=obols_done
The Steward counts the silver coins, then nods. Together, you descend into the space below, lit only by a single lantern. It is, you quickly notice, indeed a Royal chamber. The walls are adorned with carvings depicting the history of the city and the generations of nobles who have lived within the protective walls of the remains of God.

On the shelves, the dead stir, struggling against their wrappings. One by one, you pacify them by placing the coins over their eyes. You know it is only a stopgap - soon, the forces that reach into the Nether to pull back these souls to the world of the living would overwhelm this simple charm, and the dead would once again move, the coins sliding off. But for now, they are calm.

“Right. So…” The steward looks down at the stairs leading further into the darkness. “The thing is...I am not entirely sure where she went from here. And the catacombs are vast.” You can faintly hear the wind howling through the sepulchral vaults below. “But she spoke of the Metatarsal Vault.”

Perhaps the scholars in the Atheneum would know something about that. The Steward looks glad he does not have to go down in the dark by himself. 
->options

=snippets_check
{resourceCheck(snippet, 10, -2):
->snippets_done
-else:
You venture into the dark below, attempting to navigate by what little snippets of knowledge you have acquired. But you soon realize parts of what you have is nonsense, and the rest does not correspond to what is actually in front of you. Luckily, you manage to make your way back to the surface safely. You discard the osseological snippets that turned out to be false.
}
->options
=snippets_done
Somehow, what you learned from the scholars actually translated into something practical. Deep below the necropolis lies the Metatarsal Vault, nestled between foot bones the size of a cathedral. In front of it, you find the Commander of the Wall, together with a bedraggled band of survivors. The vault doors are opened, surrounded on all sides by the corpses of its undead guardians.

“Ah. About time. And the Arbiter.” The Commander nods at you. “Glad you found your way here. Now I don’t have to trek all the way up to hand you this.” She holds out a Seal fragment. It is dusty - ancient - yet clearly still a part of the same design. You take it gratefully, feeling like you missed out on some grand adventure. {alter(fragments, 1)}. 

You make your way back up to the surface, fragment in hand.->leave

=leave
{exitLocation(loc_necropolis)}
->DONE

==apiary
{enterLocation(loc_apiary)}
{apiary<2:
The Apiary of the Intercostal Sisterhood lies nestled in the foothills beneath the Ribs of the city, white smoke drifts lazily into the air above the Apiary grounds. The nuns use the soporific fumes to lull the bees while they extract the sacred substance from the honeycombed furrows of bone that make up the hives.
}

{~If you squint you can just make out the shape of the Distillery across the garden. The sun glints off the tops of the great golden vats where the bone honey is tempered, and made safe for handling.|As you watch, nuns bustle to and fro along the orderly pathways, their faces obscured by the fine armoured veils that hangs from their rounded hats.|A nun passing nearby stumbles, nearly dropping the jar of honey she was carrying. There is a collective gasp from those who witness the event, but luckily nun recovers her footing without incident. A close call... One has to be careful with the nectar of bone-bees - untreated, even a single drop is corrosive enough to eat through fabric and flesh in a matter of seconds.|In the far fields you can see Nuns training in combat formations, the blades flashing in the sunlight as they slash through the air in synchronised sweeps. You remind yourself to never get on the bad side of a nun.|Scrimshaw covered wind-chimes hang from the trees, making a hollow sort of music as they clack together in the breeze.}

A nun meets you at the gate, and unlocks the heavy chains, granting you access to the garden beyond. The air around you buzzes with activity.
->options
=options
+[Leave]
Perhaps this isn’t the best time. You leave the bustle of the Apiary behind you, for now. ->leave

+ {nacre_done && not snippet_done} [Return to Abbess with the information she requested.{displayCheck(snippet, 10)}]->snippet_check

+ {abbess && not nacre_done} [Return to the Abbess with the nacre. {displayCheck(nacre, 10)}]->nacre_check

+{abbess} [Help the nuns contain a swarm of angry bone-bees.(Mettle: {displayCheck(mettle, 6)}){displayTime(1)}]
{passTime(1)}
{skillCheck(mettle, 6):
{~You don’t really know what you’re doing, but you muddle by, and hardly anyone gets stung. The nuns offer you honey in exchange for your labour.{alter(honey, 3)}|It’s like you were born for this. Together you and the nuns lull the swarm into quiescence. Their angry buzzing fades and grows still. The nuns invite you out for a drink of mead, and you spend the afternoon swapping war-stories. {alter(honey, 6)}|You wade into the fray, and the bees start stinging you with great enthusiasm. You just end up getting in the way, but you still receive a jar of honey for your efforts.{alter(honey, 1)}| The bees prove recalcitrant, but you triumph in the end. The nuns seem quietly impressed.{alter(honey, 5)}}->options
-else:
{~You’re clearly out of your depth. The nuns shoo you to the sidelines before you can cause any more damage. “Look, just stay out of the way while we handle things.” Perhaps it’s for the best.||You fail miserably, and just end up getting in the way. By the time you’re done several other hives have started swarming, and the Abbey is in chaos. You even manage to break a jar of honey, and narrowly avoid being burned. A nun escorts to the Abbey  entrance,, and wordlessly locks the iron gate behind you. {alter(honey, -1)}}
}
->options
*[Explore the gardens.]
You wander amidst the lush foliage, admiring the colourful flower beds. The constant soft drone of the hives, and the heady scent new blossoms  creates a soporific atmosphere. You’re almost inclined to pick a spot on the green embankment and take a quick nap. Then suddenly, the sound of terrible screams coming from the direction of the distillery. An industrial accident perhaps. 

You remind yourself that despite appearances, this is a dangerous place.->options
*[Seek out the Abbess. (Mettle: {displayCheck(mettle, 3)})]
{skillCheck(mettle, 3):
It takes some convincing, but you finally get one of the young Apiarists to show you the way.->abbess
-else:
You end up wandering the grounds aimlessly, travelling between walled gardens, and fragrant courtyards. You’ve all but given up hope when at last you spy a half-hidden pathway beyond the vines.

It’s worth a try.->abbess
}
=abbess
The Abbess’ Chambers lie within the ribs themselves, a hollowed out trunk of bone, the ceiling open to the sky. 

You find her sitting there on a pile of plush cushions, laughing as dozens of bone-bees crawl across her limbs. Unlike everyone else you’ve seen in the Abbey, her head is uncovered, her slender arms bare or armour or protective padding. Is she mad, or simply fearless? 

Perhaps a bit of both.

“Come.” She says to you, patting the cushions beside her. As if on command, the bees disperse in a swirling cloud.

Gingerly you take a seat beside her. She offers you some honey. “Treated of course.” She assures you, but something about her expression makes you doubt her veracity. Still, it wouldn’t do to be rude. {alter(honey, 3)}.

“Now let’s cut to the chase shall we. Our Distilliery requires nacre for the tempering process.  but the custodian of the Necropolis is refusing to hand it over.” She explains.

“He’s being quite unreasonable about the thing. Will you help us?” She pouts fetchingly, and you find yourself nodding in agreement.->options

=nacre_check
{resourceCheck(nacre, 10, -1):
->nacre_done
-else:
“Oh no, this won’t do.” The Abbess smiles and strokes your cheek, nullifying some of the sting of her dismissal. “But you know where to find me when you have more.”
}
->options
=nacre_done
“Yes.” The Abbess sighs. She flashes you a beatific smile, then hands the package over to one of her apiarists. They rush from the chamber with great haste. Above all else, the honey must flow.

You turn to leave, but she stops you. “Wait! Before you go there is one other tiny matter… There’s a rumour going around that the heretics holed up the Atheneum have managed to get their hands on some <i>interesting</i> material. It could be pertinent to a little problem we’ve been having with the hives of late. I don’t suppose you could go over there and see what they’ve unearthed?”

You’re happy to help.
->options
=snippet_check
{resourceCheck(snippet, 10, -2):
->snippet_done
-else:
The Abbess leafs through the papers you’ve brought, her expression pensive. 

“No. No, I’m afraid none of this will do.” She pushes the papers aside. “What I’m looking for is a little more <i>particular</i>, if you catch my drift.”

You aren’t sure that you do, but that hardly matters. You depart, resolving to do better the next time around.
}
=snippet_done
“Excellent. You never fail to impress.” The Abbess quickly places the osteological snippets within a drawer at her her dress, and locks it. You notice she didn’t even look at the contents. 

What exactly is she up to?

You get the feeling you’ve been played, but are surprised to find that you really don’t mind all that much.

The Abbess retrieves something from another drawer in the desk, and presses the object into your palm.

“Here, as a token of my thanks..”She says with a wink, and departs, leaving teh room colder in her abscence.

You examine the object in your hand, and are surprised to find that it’s a small seal fragment. You think you see a bee carved into the markings on one side. It makes you smile. {alter(fragments, 1)}
->leave

=leave
{exitLocation(loc_apiary)}
->DONE

==church_of_the_marrow
{enterLocation(loc_church)}
{fragments>0:
You return to the Church of the Marrow with your collected seal fragments. The most important task of all still lies before you.

The more fragments you possess, the more likely you are to succeed. 

But be warned, if you fail there can be no second attempt, and the consequences will be dire.

How do you wish to proceed?

*[Attempt to re-sanctify the church and restore the seal, whatever the cost.{displayCheck(fragments, 5)}]->sanctify

+[Maybe not just yet, you have more work to do, and more seal fragments to gather.]->leave
-else:
You return to the Church of the Marrow to gaze upon the shattered seal. To have any hope of repairing it, you will need to find Seal Fragments. Fragments that only the powerful and the influential have.
+[You turn and leave.]->leave  
}
=leave
{exitLocation(loc_church)}
->DONE

=sanctify
Night has fallen, and you avert your face from opening in the roof of the church, lest you glimpse the burning eyes of the stars far above.

There is no one else present.

You are the arbiter, and this task falls to you and you alone. It is a responsibility you do not take lightly.

Quietly you gather the broken fragments of the seal and lay them out upon the floor of the church. The magic that animated the lines of the old seal has at last sputtered out, and for that you are grateful.

At last everything is assembled.

You begin.

{resourceCheck(fragments, 5, -5):
The walls around you start to rattle as the magic takes hold, searing lines of light outlining the complicated geometry that makes up its core.

The light intensifies, and you are forced to shut your eyes. You stumble backward, one arm raised to protect yourself from the light, but it’s all around you now, withering, burning. The magic sears you to your very soul.

You hear screaming as though from a great distance. It takes you a moment to realise the sound is coming from your own mouth.

But it’s working, it’s really working. You can feel it in your phosphorescent bones, can hear the walls of the church reassembling around you, the roof yawning shut overhead.

The Arbiters find you sometime later, sitting in the centre of the reformed seal, laughing quietly to yourself; your brothers and sisters, come to take you home.

“You served the city well.” One tells you.

“They will write your name in the Halls of the Ancient.” Says another. For some reason this amuses you. What a ridiculous notion, you have no name, you are only the Arbiter.

Gently they gather you up and escort you from the church.

You have been driven quite mad. But perhaps with time you may recover.

It does not diminish what you have done. You saved the city, and the Boneless God shall continue his eternal slumber.

For now.
->theend
-else:
There is a glorious moment where it seems like everything is coming together. The church rumbles around you, the lines of the seal flaring with potent magicks as the incantation takes hold, but at the last moment the sanctification falters. 

There is a stutter, a <i>slippage</i>, and for a moment the world around you seems to come untethered. You realise the rumbling sound is not coming from the magic, but from the earth below your feet. Your throat becomes dry.

You have failed.

It’s getting difficult to breathe.

Consigned to your fate, you look up and the stars are waiting for you, their light sears your eyes. It is the last thing you see before the ground opens up beneath you and the Boneless God spews forth. At least your death is quick.

The same cannot be said for the rest of the people of the city.

The Boneless God writhes, formless, albescent, his skin a translucent carapace revealing the meat below. With each heave of his bulk, and every thrash of his unctuous tail, buildings topple. 

His Servants follow in his wake.

And as the pale moon rises above the shattered rooftops, they feast.
->theend
}
=theend
FIN #endgame
->END

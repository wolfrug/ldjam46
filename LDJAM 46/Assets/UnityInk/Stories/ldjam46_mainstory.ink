INCLUDE ldjam46_functions.ink
INCLUDE ldjam46_locationTemplate.ink

{debug:
->start_LOCATIONNAME
}

==debugTravel==
// Use this to debug travel!
+ [start_generic] ->start_generic

// This lets you simulate the trait picking - use the CONST form (with underline after, so Adaptable_)
==debugPickTrait(trait1,trait2,trait3)===

+ [Pick {trait1}]
{AddTrait(trait1)}
+ [Pick {trait2}]
{AddTrait(trait2)}
+ [Pick {trait3}]
{AddTrait(trait3)}
- Picked Trait.
->traitDoneDebug

// this is just the start of the entire game
==start
Vampire game story! How exciting.

* [Finish the story]->endStory

->endLocation

=endStory
#continueStory
Start fight mode now!
->END

// Any location needs to start with start_<locationName>
==start_generic
Hmm. Let's see.
{AddTraitChoice(Vengeful)}
{AddTraitChoice(Adaptable)}
{AddTraitChoice(Jumpy)}
~traitDoneKnot = "PickedTrait"
Make your choice!
// This debug lets you debug pick your choice - just use CONSTs
{debug:
// Put, with a ->, your 'done knot' here for debug.
~traitDoneDebug = ->PickedTrait
->debugPickTrait(Vengeful_, Adaptable_, Jumpy_)
}

->DONE

// This is just an example, don't use this
==PickedTrait
Vengeful: {Vengeful}
Adaptable: {Adaptable}
Jumpy: {Jumpy}
Random other (Ruthless): {Ruthless}
{Vengeful>0:
You are feeling very vengeful.
}
{Adaptable>0:
You feel very adaptable.
}
{Jumpy>0:
You feel jumpy.
}
->endLocation

// Always have something like this for ease of use, and always set canSelectNext to 1 to let you pick the next location on the map!
==endLocation
You return to the streets.
~canSelectNext = 1
{debug:
->debugTravel
}
->DONE

==town_0
Start town! Woop!
->start

==town_1
Town 1!
->endLocation

==town_2
Town 2!
->endLocation

==town_3
Town 3!
->endLocation
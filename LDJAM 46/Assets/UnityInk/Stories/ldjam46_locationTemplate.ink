// Template location
===start_LOCATIONNAME
This is where we go when we click the building.
~Vengeful = 1
* {Vengeful>0}[(<color=red>Vengeful</color>) This is an option that only appears if you are Vengeful.]
Ooo scary.
* [This option always appears.]
Stuff.
+ [You probably want to have a leave option] ->leave_LOCATIONNAME

* [Let's go pick traits.]->addTraits_LOCATIONNAME

- When you eventually leave, go to leave_locattionname.

+ [Leave] ->leave_LOCATIONNAME

===addTraits_LOCATIONNAME
Here we can add some trait options.
{AddTraitChoice(Vengeful)}
{AddTraitChoice(Adaptable)}
{AddTraitChoice(Jumpy)}
~traitDoneKnot = "addedTraits_LOCATIONNAME"
->DONE

===addedTraits_LOCATIONNAME
And this is where we'd end up after picking our trait.
->leave_LOCATIONNAME

===leave_LOCATIONNAME
This is the text that comes before leaving.
This canSelectNext means you can pick a new building.
~canSelectNext = 1
->DONE
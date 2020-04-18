// Keep this include while working/testing, I will comment out when integrating
//INCLUDE clay_functions.ink
// Change the 'locationName' of all of these to correspond to the knot name below
// Set in the 'leave' stitch
VAR locationName_last_visit = -1
// this is set to true when the location is spawned. Needs to be set to false manually
VAR locationName_new = true
// this is set when spawned.
VAR locationName_creationTime = 0
// this is set when the object is very close to the edge
VAR locationName_dangerClose = false
// set this to true if you want it to be hidden
VAR locationName_hidden = false


==locationName
{locationName == 1:
Text for the -very first- time you visit.
}
{locationName_new:
Fresh spawned description
- else:
Already visited description
}
// needs to be set manually - could also be used when e.g. disabling repeatable loot
~locationName_new = false
// comment out if not needed
{locationName_creationTime > currentTime+5:
Text that comes if the location was created over 5 days ago.
}
{locationName_dangerClose:
Text that shows if the location is very close to the edge. Use together with creation time to check if the reason is because it was newly created or because it is about to be destroyed.
}
+ [Start timer: {displayTime(5)}]
Start a timer with a start time of 0 and an end time of 5, that goes to the stitch timeEnded when finished.
{startTimer(0, 1, "locationName.timeEnded")}
->leave
// always use the leave stitch for leaving. Also be careful that it's a + not a *
+ [Leave]->leave

=timeEnded
This is the stitch we go to once the timer ends. Let's reward the player!
{alter(food, 1)} + 1 Food please!
->leave

=leave
// always use this when finishing
// this sets the last visit time to current time, if this is needed for checks
~locationName_last_visit = currentTime
->DONE
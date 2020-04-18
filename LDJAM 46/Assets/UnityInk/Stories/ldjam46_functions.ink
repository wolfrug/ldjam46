// InkWriter variable stuff
VAR debug = true
VAR lastSavedString  = ""
VAR lastSavedTags = ""

// Map variables
VAR canSelectNext = 1
VAR traitDoneKnot = ""
VAR traitDoneDebug = ->start


// Main "stat" variables
CONST Vengeful_ = "Vengeful"
VAR Vengeful = -1
CONST Cautious_ = "Cautious"
VAR Cautious = -1
CONST Adaptable_ = "Adaptable"
VAR Adaptable = -1
CONST Discreet_ = "Discreet"
VAR Discreet = -1

VAR Watchful = -1
CONST Watchful_ = "Watchful"
VAR Jumpy = -1
CONST Jumpy_ = "Jumpy"
VAR Ruthless = -1
CONST Ruthless_ = "Ruthless"
VAR Reckless = -1
CONST Reckless_ = "Reckless"

===function AddTraitChoice(ref trait)===
// needs to use the CONST version, so:
// {AddTrait(Ruthless_, true)}
~trait = 0


===function AddTrait(trait)===
{trait:
- "Vengeful":
~Vengeful = 1
- "Cautious":
~Cautious = 1
- "Adaptable":
~Adaptable = 1
- "Discreet":
~Discreet = 1
- "Watchful":
~Watchful = 1
- "Jumpy":
~Jumpy = 1
- "Ruthless":
~Ruthless = 1
- "Reckless":
~Reckless = 1
}
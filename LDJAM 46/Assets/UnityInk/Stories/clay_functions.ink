// FUNCTIONS FOR CLAY GAME DEMO 

// player info
// not relevant right now

// skills and resources are identical - just declare them here.
VAR testskill = 0
VAR _clayworker = "Clayworker"
VAR clayworker = 0
VAR _shaman = "Shaman"
VAR shaman = 0
VAR _trickster = "Trickster"
VAR trickster = 0
VAR _warchief = "Warchief"
VAR warchief = 0
VAR _pactmaker = "Pactmaker"
VAR pactmaker = 0

VAR food = 0
VAR _food = "Food"
VAR tribesmen = 0
VAR _tribesmen = "Tribesmen"
VAR warriors = 0
VAR _warriors = "Warriors"
VAR courage = 0
VAR _courage = "Courage"
VAR clay = 0
VAR _clay = "Clay"

VAR artifacts = 0

VAR hasOmbrascope = false

VAR easy = 2
VAR medium = 4
VAR hard = 6

LIST knownLocations = Bunker, WarGolem, EatingEngine, Disassembler, PowerPlant, CommandCenter

//specialists
LIST spec_clayworkers = Jaerd, Dhauf, Lael
LIST spec_shamans = Valfrig, Didild, Aerrirr
LIST spec_trickers = Chi, Dufrall, Ghateg
LIST spec_warchiefs = Barr, Khog, Storlan
LIST spec_pactmakers = Fayni, Gurste, Tras

//Time stuff
// these are set externally
VAR currentTime = 0
VAR dayTime = 0.0
VAR nightTime = false
// this is set to false when the day passes
VAR rested = true
// this is set to true if rested is false and the day passes
VAR exhausted = false
VAR timeSinceVisited = 0
// these have to do with 'tasks', i.e. timer-related things
VAR task_timeNeeded = 0
VAR task_timeGot = 0
VAR task_name = ""
VAR task_success = false

// counts down whenever moving; in seconds
VAR time_until_next_encounter = 10.0
// default time between encounters
VAR default_time_between_encounter = 10.0

//Saveables
VAR playerX = 0
VAR playerY = 0
VAR playerZ = 0

VAR daytime = 0 

VAR dissolverX = 0
VAR dissolverY = 0
VAR dissolverZ = 0

VAR dissolverTarget = 0

VAR lastSavedString = ""
VAR lastSavedTags = ""

===function enterLocation(ref location)===
// put this at the entrance point to the location; use "timeSinceVisited" to check what the value was before
~timeSinceVisited = turnsSinceVisited(location)
~location = 0

===function exitLocation(ref location)===
// Put this at all exit points to the location
~location = 1

===function passLocationTime(time, ref location)===
{location != 0 && location != -1:
    ~location++
}
~time--
{time > 0:
    {passLocationTime(time, location)}
}

===function turnsSinceVisited(ref location)===
// returns nr of turns (up to 5) since the last visit to the location
~return location
/*{location:
- not_visited:
~return -1
- last_visited:
~return 0
- current:
~return 0
- 1_turn_since:
~return 1
- 2_turn_since:
~return 2
- 3_turn_since:
~return 3
- 4_turn_since:
~return 4
- 5_turn_since:
~return 5
- more_than_5_turn_since:
~return 6
-:
~return -1
}*/

===function displayTime(time)===
// Change the size. Also remember to adjust it for the final sprite(s)
(<size=30><sprite name="tmp_time"></size>{time})

// does a skillcheck and returns either true or false. Does not change the skill's value
// use (name of skill, difficulty of challenge)
/*
{skillCheck(mettle, 6):
    good stuff
-else:
    bad stuff
}
*/
===function skillCheck(ref skillN, requiredskill)===
~temp skill = 0
{getSkill(skillN, skill)}
~temp success = false
~temp percentageNeeded = 0
{skill > 0:
~percentageNeeded = (skill*100) / requiredskill
{skill >= requiredskill:
    ~percentageNeeded = 100
    ~success = true
- else:
    ~temp randomValue = RANDOM(1,100)
        {randomValue <= percentageNeeded:
            ~success = true
        }
    }
}
{success:
    <color=green>Success! ({percentageNeeded} %)</color>
- else:
    <color=red>Failure! ({percentageNeeded} %)</color>
}
~return success

===function getSkill(ref skillN, ref outVar)===
~temp returnVar = 0
{skillN:
- "Shaman":
~returnVar = shaman
- "Trickster":
~returnVar = trickster
- "Warchief":
~returnVar = warchief
- "Pactmaker":
~returnVar = pactmaker
- "Clayworker":
~returnVar = clayworker
- "Tribesmen":
~returnVar = tribesmen
- "Warriors":
~returnVar = warriors
- "Clay":
~returnVar = clay
- "Food":
~returnVar = food
- "Courage":
~returnVar = courage
- else:
~returnVar = skillN
}
~outVar = returnVar

===function getResource(ref resourceN, ref outVal, ref outMax, ref outMin)
~outMin = 0
{resourceN:
- "Food":
~outVal = food
~outMax = 200
- "Clay":
~outVal = clay
~outMax = 100
- "Tribesmen":
~outVal = tribesmen
~outMax = 500
- "Warriors":
~outVal = warriors
~outMax = 500
- "Courage":
~outVal = courage
~outMax = 100
- else:
~outMax = 1000
~outVal = 0
}

// similar to skillcheck, except for two things:
// 1) if a success, the entirety of "requiredResources" is removed (down to 0)
// 2) if a failure, "resourcesLost" is removed from the total resources (or added)
// use:
/*
{resourceCheck(coins, 30, -5):
woop! Coins left: {coins}
- else:
noop! Coins left: {coins}
}
*/

===function resourceCheck(ref resource, requiredResources, resourcesLost)===
~temp success = skillCheck(resource, requiredResources)
{success: {alterValue(resource, -requiredResources, 0, 1000)} | {alterValue(resource, resourcesLost, 0, 1000)}}
~return success

// Displays the percentage likelihood that the check will pass
// use:
// {displayCheck(insight, 3)}

===function displayCheck(ref skillN, requiredskill)===
~temp skill = 0
~temp chanceOfSuccess = 0
{getSkill(skillN, skill)}
{skill>0:
~chanceOfSuccess = (skill*100) / requiredskill
}
Chance of Success ({skillN}): <>
{chanceOfSuccess > 60:
<color=green><>
- else:
{chanceOfSuccess < 30:
<color=red><>
- else:
<color=yellow><>
}
}
{chanceOfSuccess}</color> %

// convenience funtion that assumes min 0 and max 1000 on any value
===function alter(ref valueN, change)===
~temp value = 0
~temp max = 1000
~temp min = 0
{getResource(valueN, value, max, min)}
{alterValue(value, change, min, max)}

// if you need to alter values of things outside of checks, use this instead of directly changing them
// use (variable, change (can be negative), minimum (0) maximum (1000...or more).
// {alterValue(coins, 25, 0, 1000)
===function alterValue(ref value, newvalue, min, max) ===
~temp finalValue = value + newvalue
{finalValue > max:
    ~value = max
- else: 
    {finalValue < min: 
    ~value = min
- else:
    ~value = value + newvalue
    }
}

===function startTimer(startAmount, targetAmount, timerName)
~task_timeNeeded = targetAmount
~task_timeGot = startAmount
~task_name = timerName
#start.timer

===function skillCheckSpec(ref specialistList, ref skill, goal)
~temp totalSkill = LIST_COUNT(specialistList) + skill
{skillCheck(totalSkill, goal):
~return true
-else:
~return false
}
==function displayCheckSpec(ref specialistList, ref skill, goal)
~temp totalSkill = LIST_COUNT(specialistList) + skill
{displayCheck(totalSkill, goal)}


// prints a number as pretty text
=== function print_num(x) ===
// print_num(45) -> forty-five
{ 
    - x >= 1000:
        {print_num(x / 1000)} thousand { x mod 1000 > 0:{print_num(x mod 1000)}}
    - x >= 100:
        {print_num(x / 100)} hundred { x mod 100 > 0:and {print_num(x mod 100)}}
    - x == 0:
        zero
    - else:
        { x >= 20:
            { x / 10:
                - 2: twenty
                - 3: thirty
                - 4: forty
                - 5: fifty
                - 6: sixty
                - 7: seventy
                - 8: eighty
                - 9: ninety
            }
            { x mod 10 > 0:<>-<>}
        }
        { x < 10 || x > 20:
            { x mod 10:
                - 1: one
                - 2: two
                - 3: three
                - 4: four        
                - 5: five
                - 6: six
                - 7: seven
                - 8: eight
                - 9: nine
            }
        - else:     
            { x:
                - 10: ten
                - 11: eleven       
                - 12: twelve
                - 13: thirteen
                - 14: fourteen
                - 15: fifteen
                - 16: sixteen      
                - 17: seventeen
                - 18: eighteen
                - 19: nineteen
            }
        }
}
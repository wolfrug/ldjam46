// GLOBAL GAME JAM 2020 FUNCTIONS ET AL FILE

// skills and resources are identical - just declare them here.
VAR testskill = 0
VAR artifice = 0
VAR insight = 0
VAR mettle = 0
VAR candour = 0
VAR theurgy = 0

VAR obols = 0
VAR honey = 0
VAR nacre = 0
VAR token = 0
VAR snippet = 0

VAR fragments = 0

//Time stuff
VAR currentTime = 0
VAR timeSinceVisited = 0
VAR palaceTimer = 0
VAR cenotaphTimer = 0
VAR necropolisTimer = 0
VAR atheneumTimer = 0
VAR apiaryTimer = 0



LIST locationStatus = not_visited, current, last_visited, 1_turn_since, 2_turn_since, 3_turn_since, 4_turn_since, 5_turn_since, more_than_5_turn_since


VAR loc_church = -1
VAR loc_cenotaph = -1
VAR loc_palace = -1
VAR loc_atheneum = -1
VAR loc_necropolis = -1
VAR loc_apiary = -1

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
(<size=65><sprite name="tmp_time"></size>{time})

===function passTime(time)===
~currentTime +=time
// Pass time at locations
{time > 0:
{passLocationTime(time, loc_church)}
{passLocationTime(time, loc_cenotaph)}
{passLocationTime(time, loc_palace)}
{passLocationTime(time, loc_atheneum)}
{passLocationTime(time, loc_necropolis)}
{passLocationTime(time, loc_apiary)}
}
// does a skillcheck and returns either true or false. Does not change the skill's value
// use (name of skill, difficulty of challenge)
/*
{skillCheck(mettle, 6):
    good stuff
-else:
    bad stuff
}
*/
===function skillCheck(ref skill, requiredskill)===
~temp success = false
~temp percentageNeeded = (skill*100) / requiredskill
{skill >= requiredskill:
    ~percentageNeeded = 100
    ~success = true
- else:
    ~temp randomValue = RANDOM(1,100)
        {randomValue <= percentageNeeded:
            ~success = true
        }
    }
{success:
    <color=green>Success! ({percentageNeeded} %)</color>
- else:
    <color=red>Failure! ({percentageNeeded} %)</color>
}
~return success

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

===function displayCheck(ref skill, requiredskill)===
~temp chanceOfSuccess = (skill*100) / requiredskill
Chance of Success: <>
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
===function alter(ref value, change)===
{alterValue(value, change, 0, 1000)}

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
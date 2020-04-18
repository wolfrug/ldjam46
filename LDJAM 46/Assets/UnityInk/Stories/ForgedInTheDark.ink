// Insight
VAR hunt = 0
VAR study = 0
VAR survey = 0
VAR tinker = 0
// Prowess
VAR finesse = 0
VAR prowl = 0
VAR skirmish = 0
VAR wreck = 0
// Resolve
VAR attune = 0
VAR command = 0
VAR consort = 0
VAR sway = 0
/* In order to avoid unnecessary prints (which happens with 'return' every time, we use this variable 'rollResult' to gather the results of the latest roll. So the way to check a dice roll is:
~roll_dice(skillname, bonusdice)
{rollResult == 6: ....}
the potential rollResults are:
6+ -> critical success (7 = two sixes, 8 = 3 sixes etc)
6 -> success
4-5 -> success-but
1-3 -> fail
*/
VAR rollResult = 0
VAR clockResult = -1


=== function startClock(ref name) ===
/* Starting a new clock -> create a variable like so: VAR yourclock = ()
Use ~ startClock(yourclock)
Set value with e.g. ~alterClock4(yourClock, 1)
*/
LIST clock = (zero), (one), (two), (three), (four), (five), (six), (seven), (max)
~name = LIST_ALL(clock)
~name = clock.zero

=== function alterClock4(ref name, amount) ===
~ alterClock(name, amount, 4)
=== function alterClock6(ref name, amount) ===
~ alterClock(name, amount, 6)
=== function alterClock8(ref name, amount) ===
~ alterClock(name, amount, 8)

== function alterClock(ref name, amount, maximum) ===
{amount > 0 && name != max && LIST_VALUE(name)<=maximum:
    ~name++
    ~amount--
    {alterClock(name, amount, maximum)}
- else:
    {amount < 0 && name !=clock.zero:
        ~name--
        ~amount++
        {alterClock(name, amount, maximum)}
    }
}
=== function clockValue(ref name) ===
/*
Returns the value of the clock and places it into clockResult
Check value with e.g. a switch statement:
~clockValue(yourclock)
{clockResult:
- 0: zero
- 1: one
- else: something else
}
*/
    ~clockResult = LIST_VALUE(name)-1

=== function roll_dice(skill, bonus) ===
// Roll each die, and return the best roll
// Check if there's more than 0 dice
{skill+bonus > 0:
// if so, roll normally and return the highest
~ diceroller(1,skill+bonus)
- else:
// if not, manually roll two dice and return the -lowest-
    ~temp result1 = RANDOM(1,6)
    ~temp result2 = RANDOM(1,6)
    {result1 > result2: 
        ~ rollResult = result2 
    - else:
        ~ rollResult = result1
    }
}

=== function diceroller(best, diceleft) ===
{ diceleft > 0:
    ~ temp result = RANDOM(1,6)
    //{result}
        {result > best: 
            ~ best = result
        - else: 
            {result == 6 && best >= 6: 
            ~best++
            //~diceleft = 0
            }
        }
    ~ diceleft--
    {diceroller(best, diceleft)}
- else: 
    ~ rollResult = best
}

=== function alter(ref x, k) ===
// alter(gold, 5)
	~ x = x + k
	
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
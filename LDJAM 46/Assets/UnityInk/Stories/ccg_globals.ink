VAR mc_surName = "Wolf"
VAR mc_firstName = "Leo"
LIST companions = (Emilija), (Gregor), (Laura), MC, GC
LIST npcs = NPC1, NPC2, NPC3
LIST backgrounds = base, forest, city, countryside

// use _value for the value and _name for the name - auto-picked by the progress bars!
VAR emilija_like_value = 3
VAR emilija_like_name = "Determination"
VAR gregor_like_value = 3
VAR gregor_like_name = "Courage"
VAR laura_like_value = 3
VAR laura_like_name = "Affection"


VAR fontSizeLarge = 40
VAR clay = 5
VAR willpower_value = 6
VAR willpower_name = "Willpower"

//EXTERNAL say(character)

// Define here the tag names for each character
===function say(character) ===
#changeportrait
{character == Emilija:
#spawn.portrait.emilija
//<color=blue><size={fontSizeLarge}>{Emilija} -</color></size>
}
{character == Gregor: 
//<color=green><size={fontSizeLarge}>{Gregor} -</color></size>
#spawn.portrait.gregor
}
{character == Laura: 
//<color=blue><size={fontSizeLarge}>{Laura} -</color></size>
#spawn.portrait.laura
}
{character == MC:
//<color=red><size={fontSizeLarge}>{mc_firstName} -</color></size>
#spawn.portrait.player
}
{character == GC:<color=yellow>}
{character == NPC1:
//<color=black><size={fontSizeLarge}>{mc_firstName} -</color></size>
#spawn.portrait.npc1
}
{character == NPC2:
//<color=black><size={fontSizeLarge}>{mc_firstName} -</color></size>
#spawn.portrait.npc2
}
{character == NPC3:
//<color=black><size={fontSizeLarge}>{mc_firstName} -</color></size>
#spawn.portrait.npc3
}

===function say_gc(text)===
<color=yellow><i>{text}</color></i>

===function link(id, text)===
<link="{id}"><color=yellow>{text}</color></link>

===function say_npc(NPC) ===
#changeportrait
<color=black><size={fontSizeLarge}>{NPC} -</color></size>#spawn.portrait.npc
===function setBackground(background)===
#changebackground
{background:
- base: #spawn.background.base
- forest: #spawn.background.forest
- city: #spawn.background.city
- countryside: #spawn.background.countryside
}

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

===function alterLike(character, value) ===
{character == Emilija:
{alterValue(emilija_like_value, value, 0, 7)}
//<color=blue><size={fontSizeLarge}>{Emilija} attitude {value>0:+}{value}</color></size>
#alter.attitude.emilija
}
{character == Gregor:
{alterValue(gregor_like_value, value, 0, 7)}
//<color=green><size={fontSizeLarge}>{Gregor} attitude {value>0:+}{value}</color></size>
#alter.attitude.gregor
}
{character == Laura:
{alterValue(laura_like_value, value, 0, 7)}
//<color=blue><size={fontSizeLarge}>{Laura} attitude {value>0:+}{value}</color></size>
#alter.attitude.laura
}
}


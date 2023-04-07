# Spikecat
any readme info can go here

# TABLE OF CONTENTS:
- ToDo List
- Bugs & Issues
- How to use mod

# DONE:
- Reversed Breath
- Less breath = Less Speed
- Custom Waterboost cooldown
- Modified Slugbase stats
- Slugbase coloring
- Custom Diet (may need balancing)
- Can maul + Backspear
- Menu & Sleep art (Temp)
- Bubble weed on land


# IN PROGRESS:
- Adjust Water of SU
- Change Appearance of SU
- Lizard species reputation system
- Update ingame art
- Add Arena Icons and Cutscenes

# TO DO:
- Adjust Water in all regions
- Change appearance of all regions (near water)
- Add more creature dens near new water
- Modify Creature Spawns in all regions
- Modify Iterator text
- Make Spike start with Mark of Communication
- Change Spike's Sprites
- Make Spike's sprites able to be individually colorable (Main, Face, Spikes, Collar)
- Add Pearl slot in collar
- Change Spike's Tail lenght and width


# POTENTIAL FUTURE FEATURES:
- Config Menu
- Start in the gate from OE to SB, intro cutscene of unlocking the gate. Player regains control in SB
- Less breath = Slugcat color change
- Make world changes Spike exclusive
- Add spike shooting attack (down + e, takes some breath as penalty, spikes act like spears)
- Custom Region behind shoreline
- Add Custom Lizard Hybrids
- Have LTTM give Spike a pearl to unlock the OE gate permanently
- Custom Pastel Blue Purple Overseer
- Cycles start at night, end sometime during day


# ACTIVE BUGS & ISSUES:
3) Sometimes the menu art goes black. Cause unknown, needs further investigation. 

# FIXED BUGS:
1) Breath bar doesnt fill entirely (potential fix: change the value at wich it displays as "full" and fades out. Currently that would be 1 wich would cause fixed bug 2 )
|> fixed by setting airInLungs for issue 2 and the value at wich the bar disappears to 0.95 wich also still works

2) Sometimes bodyparts start to disappear and generally physics objects stop rendering correctly. Seems to be a rare occurance, if caused once will happen frequently until game restarts. Cause unknown, assumed to be either with lung issues (jumping in water) or slugbase itself. Needs further investigation.(Probably caused by airInLungs being 1 or more when underwater since value of airInLungs used for calculating some animations which require this value to be lower than 1). 
|> Fixed by changing the max value of airInLungs to be lower than 1 and more or equal to 0. (now 0.95)


# HOW TO USE FILES:
1) Download "mod" folder
2) Move "mod" folder into: C:\Program Files (x86)\Steam\steamapps\common\Rain World\RainWorld_Data\StreamingAssets\mods
3) Rename "mod" folder to "spikecat"
4) Enable the Spikecat mod in RainWorld

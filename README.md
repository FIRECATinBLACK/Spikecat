# Spikecat
Welcome to the Github repo of the Rainworld Spikecat mod (name may be subject to change)!
This is mostly a big work in progress and still in its early alpha stages but we are excited to hopefully bring this mod to the workshop soon!
If you are a Playtester or curious early bird please put your issues in a Expected Result, Actual Result, Steps to Replicate, Detailed description / Additional notes format and attach any Console Log, Exception log and Mod log files if possible. Additionally keep any questions, suggestions or other discussions to the appropiate discussion categories.

Also, this mod is created with Downpour content and **May Contain Rainworld Downpour Spoilers**

Spike is a Slugcat sent by the iterators beyond the extents of Five pebbles and Looks to the Moons facilities, tasked with assisting any collapsed iterators and acting as a aquatic messenger. However he is soon to make the devastating discovery that it might be already too late to help

# TABLE OF CONTENTS:
- ToDo List
- Bugs & Issues
- How to use mod
- Credits

## DONE:
- Reversed Breath
- Less breath = Less Speed
- Custom Waterboost cooldown
- Modified Slugbase stats
- Slugbase coloring
- Custom Diet (may need balancing)
- Can maul + Backspear
- Menu & Sleep art (Temp)
- Bubble weed on land
- Longer Cycles


## IN PROGRESS:
- Adjust Water of SU
- Change Appearance of SU
- Lizard species reputation system
- Update ingame art
- Add Arena Icons and Cutscenes
- Make this README actually look and function better

## TO DO:
- Add a Github issues form and move the issues there, then link them here in a more organized way
- Add Bubbleweed to all regions
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
- Replace all regular leeches with Jungle ones


## POTENTIAL FUTURE FEATURES:
- Scavengers can spawn with bubbleweed
- Config Menu
- Start in the gate from OE to SB, intro cutscene of unlocking the gate. Player regains control in SB
- Less breath = Slugcat color change (code could be borrowed from saint)
- Make world changes Spike exclusive
- Add spike shooting attack (down + e, takes some breath as penalty, spikes act like spears)
- Custom Region behind shoreline
- Add Custom Lizards & Hybrids (Yellow / White, Blue / Green, Spikezard, Red / Black)
- Have LTTM give Spike a pearl to unlock the OE gate permanently
- Custom (blueish) Black and Red Overseer
- Cycles start at night, end sometime during day
- Pearls if in pearl slot emit neuron glow
- Scav lanterns can be put into pearl slot
- Short glimpses of a scripted Rivulet running around SL
- Collapsed 5p during a Rivulet worldstate
- Dream Cutscenes showing Spike's story every 5 cycles and a select few at certain shelters


## ACTIVE BUGS & ISSUES:
3  ) Sometimes the menu art goes black. Cause unknown, needs further investigation. 

## FIXED BUGS:
1  ) Breath bar doesnt fill entirely (potential fix: change the value at wich it displays as "full" and fades out. Currently that would be 1 wich would cause fixed bug 

|> fixed by setting airInLungs for issue 2 and the value at wich the bar disappears to 0.95 wich also still works

|> Had to be same value as airInLungs but at the same time be one that doesnt send Spike's body flying all across the map

---

2  ) Sometimes bodyparts start to disappear and generally physics objects stop rendering correctly. Seems to be a rare occurance, if caused once will happen frequently until game restarts. Cause unknown, assumed to be either with lung issues (jumping in water) or slugbase itself. Needs further investigation.(Probably caused by airInLungs being 1 or more when underwater since value of airInLungs used for calculating some animations which require this value to be lower than 1). 

|> Fixed by changing the max value of airInLungs to be lower than 1 and more or equal to 0. (now 0.95)

|> What caused it was essentially airInLungs being 1 (at this value or higher it messes with the animations for whatever reason) for the sake of the breath bar working correctly, unknowing at the time that 0.95 would be fine too. So whenever Spike would enter a body of water with his breath bar already entirely full (like the head leaving and reentering water within a very short amount of time or hibernating and waking up submerged) he'd first lose his neck and then all other parts of his body except tail also causing all animation of other physics objects and creatures to freeze 

---

# HOW TO USE FILES:
1) Download "mod" folder
2) Move "mod" folder into: C:\Program Files (x86)\Steam\steamapps\common\Rain World\RainWorld_Data\StreamingAssets\mods
3) Rename "mod" folder to "spikecat"
4) Enable the Spikecat mod in RainWorld

# CREDITS:
- FIRECATinBLACK - Character and original concept, head of the project and main artist
- OneQuish - Helping with most of the bigger coding tasks
![C0945A04-F521-44EC-8C53-C73061CA139B](https://user-images.githubusercontent.com/71691122/230751349-f134c290-346b-4cc5-b71f-41c5689024da.png)

Generally a huge thanks to any Contributers! This project might not be possible without you


---
small additional noted for worldchanges:

bubbleweed / water needed: (putting this here for areas I know I'll forget bec I dont usually visit them lmao)
- Bubbleweed between Drainage and gutter

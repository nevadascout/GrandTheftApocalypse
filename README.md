# "Grand Theft Apocalypse" / "Eternal Dark"

## Concept
The year is 2021. Approximately 5 years ago an alien super weapon was detonated directly above Los Santos. The effect was immediate and crippling - all electronics were instantly disabled causing cars to stop working and planes to fall from the sky. This was but a first stage of the weapon. In the second stage all vehicles on the streets were lifted high into the air and dumped far out at sea. In the third stage a mysterious virus was released into the air causing all those who it infected to lose control of their body forcing them into a zombie-like state. In the final stage, the sky was tainted black - blocking out the sun and plunging the world into a state of perpetual darkness.

Some, it turned out, are naturally immune to the virus. These few people are all that remain of the world that once existed. They fight each other and the Zeds to survive.

The military response to this attack was slow and cumbersome. Thousands of soldiers died during the initial stages of the weapon and hundreds more were killed by the Zeds and mutated creatures that spawned from the virus. Transmitted by air, the only way the military can protect themselves is to wear breathing apparatus at all times.

While some survivors may be immune to the virus, there are some areas of the world that are too badly infected for even them to go. The subway and sewer system is an example of this. Breathing apparatus must be worn at all times else you will die a most painful death.

Some wildlife seem to be immune to the effects of the virus and can survive even the worst infected areas without problem. However, a lack of food had caused many animals to become aggressive and will attack anything they believe they can eat.

![Michael in an Alien nest](http://i.imgur.com/4ALUaZg.jpg)
![Taking down a pesky zed](http://i.imgur.com/s9G6mfl.jpg)
![Michael wandering with his companion](http://i.imgur.com/jtMLvvA.jpg)
![Michael wandering with his companion (2)](http://i.imgur.com/q5bUnnN.jpg)


***

## Roadmap

### Cinematic Teaser - Dec 2015 / Jan 2016
- ~~Zeds have proper animations + textures (no phone usage, no random voices)~~
- ~~Ability to enable/disable zed spawning (hash call ontick)~~
- ~~Zeds do not flee when hearing gunshots or shot at (set relationship with player)~~
- ~~One special zed area (alien nest) added - in subway station (aliens attack player when close by)~~
- ~~Military choppers spawn (+ fly low overhead) on command~~
- Wolf packs spawn on command
- Enable/disable wolf pack aggression towards player
- Zed hoards that chase the player
- ~~Enable/disable Aussie dog companion~~
- ~~Enable/disable zeds attacking player~~ *(no damage done to player yet)*

Using Enhanced Trainer:
- Enable/disable duffle bag
- Set player to michael, long hair, scruffy beard, wearing yankton outfit + toque
- Enable/disable rebreather (+ torch)
- Set player weapons to green carbine rifle w/ torch, pump shotgun w/ torch and combat pistol w/ suppressor
- Trigger EMP
- Set weather

### Gameplay Teaser - Feb 2016
- Zeds attack player only when detected instead of always
- Ability to enable/disable zeds
- Bandit supply convoys spawn on command
- Placing limited objects (firepit, etc)
- Gather loot from abandoned cars, etc
- Repair vehicles using toolkit
- Aussie dog can be recruited as a companion

***

# Todo List

### Zeds
- ~~Don't stop peds from spawning, instead let game engine do all the heavy lifting of spawn/destroy~~
- ~~Get all peds in world and make them drunk + apply blood decals~~
- Use NM commands to raise arms of Zeds like zombie arm *(might not be possible)*
- Zeds react to sound (gunshots, engines)
- Zeds react to player in line of sight
- Spawn special Zeds with extra health, speed, etc (bigfoot, alien)
- Predefined areas where special enemies spawn (alien nests, etc)
- Spawning of hoards (only ever one on map at a time) - map marker indicates rough hoard position

### World events engine
- Create random events at certain map areas at certain times (to keep occurrences infrequent)
- Bandit bases
- Bandit hunting parties
- Bandit supply convoys
- Bandits attack safe zone
- Military convoys
- Military choppers fly overhead
- Military vs bandits
- Bandits vs hoard
- Military vs hoard
- Aggressive packs of wolves/coyotes

### Base building / safe zones
- Place objects to block access
- Place special objects for perks (eg fireplace for health regen, [something] for zed shield)
- Bring supply trucks to the safe zone to upgrade it

### NPCs
- Find survivors and bring them to your safe zone to help defend it
- Recruit a companion (inc. dog)
- Control companions (attack, escort, etc)
- Once brought to safe zone, NPCs can give the player quests (find a loved one, rescue a belonging, kill some bandits, etc)

### Inventory / loot
- Spawn loot around the world?
- Find loot by searching areas?
- Ability to take items when carrying duffle bag
- Weapon attachments are rare
- Firearms are semi-rare
- Ammo is rare (get from bandits)
- Toolkit (bag you find/wear) used to repair vehicles + build safe zones
- Cannot carry toolkit and backpack at same time
- Can only carry one primary weapon and one secondary weapon

### World
- Spawn wrecked cars, trucks, etc around the player. Ideally persisted instead of random spawns
- Load as many destroyed IPLs as possible

### Vehicles
- Repair vehicles with a toolkit before using them
- Helicopters spawn but are rare and often guarded by bandits or military

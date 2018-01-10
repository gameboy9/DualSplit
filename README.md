# DualSplit
A time splitter that can be used for various two or four-player tournament races.

## What does it do?
Restreamers can either control this by themselves or have a "tracker" connect to the restreamer and have that person take care of it.  DualSplit will split each runner's performance by showing the time used from start for each split and the difference between the two runners.  If there is a four-runner race, it typically consists of two 1v1 races, so DualSplit will show differences for the top half and the bottom half.

## Setup

For restreamers, it is strongly recommended that you set up a Chroma Key of 0,0,128 to make the program transparent.

To set up DualSplit for your own game, you will need to edit the XML provided in the ZIPs.  Here are the various elements and their attributes, in order:

### game Element
- Name attribute - The game name
- Font attribute - The font that will be used.  It must be installed on the restreamer's machine in order for it to show correctly.
- Players attribute - 2 or 4 - The number of players this file supports.

### player[A/B/C/D] Element
- visible - True or false - probably should keep false for now
- Rest is unimplemented

### mic Element
- visible - True or false - probably should keep false for now
- Rest is unimplemented

### clock[A/B/C/D] Element
- fontSize - Size of font
- locX - X attribute inside the form.  It should be placed either close to the controls so the viewers won't see it, (used if you're showing the runner's clock instead), or somewhere away from the controls.  You can crop DualSplit in OBS so the controls aren't seen.
- locY - Y attribute inside the form.

### times Element (Time used after each split)
- fontSize - Size of font
- aLocX - X attribute for players 1 and 3.
- bLocX - X attribute for players 2 and 4.

### diffs Element (Time differentials)
- fontSize - Size of font
- aLocX - X attribute for players 1 and 3.
- bLocX - X attribute for players 2 and 4.

### title element (Titles for each split)
- fontSize - Size of font
- aLocX - X attribute for all players.  (It's typically placed in the middle between all of the splits)

### splits element
- fontSize - Size of font
- yStart - Y coordinate of the first split
- yGap - Distance between splits in the Y axis
- limit - Number of splits to show at one time

### split element (to be placed inside the splits element above)
- name - Name of the split

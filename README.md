# ClickerFramework

This is a framework of Unity scripts to support creating a Clicker/Idle/Iterative type game (Candy Box, Cookie Clicker, Dirt Inc, etc).

The Framework currently only supports one primary Resource. Extra currencies might be added later via a new system.

I've included a demo Unity Scene (TestScene) with most of these things hooked up and working in a very simple way.

--- CORE FILES ---

- GameManager.cs - A singleton. Tracks the resource and has helpful initialization and helper functions.


--- UI AND DISPLAY HELPERS ---

- DisplayResource.cs - Use on a UI Text object (or linked to one) to display the current amount of Resource
- DisplayMultiplier.cs - Use on a UI Text object (or linked to one) to display either the total Tick multiplier or Click multiplier


--- STUFF THAT GOES ON BUTTONS --- 
(these do most of the work)

- Buyable.cs - Put on a button (send "Buy" message) to make it a buyable. Disables button if you can't afford it. Works with following scripts:
- ClickForResource.cs - Use this on a button (send OnClick) if you want to click it to generate Resource. Works fine alongside Generators.
- MultiplyResource - Use this on a button (send OnClick) to multiply the total Resource count by n when clicked.
- RegisterMultiplier - Use this on a button (send OnClick) to add to the total global Tick or Click multiplier when clicked.
- Generator.cs - Generates Resource every n seconds (every tick) - basically a building. Can have many of these.  Put on a button with a Buyable script if you want the ability to to be purchaseable/upgradeable.

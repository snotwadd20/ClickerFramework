# ClickerFramework

This is a framework of Unity scripts to support creating a Clicker/Idle/Iterative type game (Candy Box, Cookie Clicker, Dirt Inc, etc).

The Framework currently only supports one primary Resource. Extra currencies might be added later via a new system.

I've included a demo Unity Scene (TestScene) with most of these things hooked up and working in a very simple way.

--- CORE FILES ---

- Currency.cs - Tracks a currency type and global bonuses associated with it. Game must have at least one currency.
- Generator.cs - Generates Resource every n seconds. This can occur on a timer (tick) or once every time something is clicked (OnClick). Put on a button with a Buyable script if you want the ability to to be purchaseable/upgradeable. Generators increase the amount generated every time one is bought. Generator must be at at least level 1 to generate resources.
- ClickForCurrency.cs - Use this on a button (send OnClick) if you want to click it to generate Resource. 

--- UI AND DISPLAY HELPERS ---
(Use these on UI Text objects, or linked to one, to display information about the game or objects in it)

- DisplayCurrency.cs - Display the amount of money currently stored in the linked Currency
- DisplayCurrencyRate.cs - Displays a tick or click rate of a currency
- DisplayGeneratorRate.cs - Displays the rate at which a generator generates currency
- DisplayMaximum.cs - Display the Maximum of any script that implements IMaximum
- DisplayMultiplier.cs - Display either the total Tick multiplier or Click multiplier
- DisplayPrice.cs - Display the Price of any script that implements IPrice
- DisplayQuantity - Display the Quantity of any script that implements IQuantity
- DisplayQuantAndMax.cs - Displays Quantity and Max together, one after the other.
- DisplayValue.cs - Display the current Value of a script that implements IValue

--- Progress Bars ---
(Any progress bar that implements the IValue interface will work.)

- ProgressBarText.cs - Attach this to a Text UI object and link it to a Generator. The text field will display, numerically, the progress depending on type: from 0 to 100% (Percentage) or 0.0 to 1.0 (Factor) 

--- STUFF THAT GOES ON BUTTONS --- 
(these do most of the work)

- AddToRate.cs - Put on a button. Send "OnClick" message to add to a currency's global click or tick rates
- Buyable.cs - Put on a button to make it a buyable (send "Buy" message to buy a thing). Disables button if you can't afford it. Works with following scripts:
- MultiplyCurrency - Use this on a button (send OnClick) to multiply the linkes currency total by n when clicked. Use with a buyable, and the price gets subtracted before the multiplication.

--- WRAPPERS ---
(Files to make this work with certain plugins. Delete these if you don't use those plugins)

- ProgressBarUI.cs & ProgressBarSprite.cs - These inherit from a script in the Unity Store Package "Power Progress Bars" and also implement the IValue interface. This allows me to use "Power Progress Bars" scripts with my IValue interface d
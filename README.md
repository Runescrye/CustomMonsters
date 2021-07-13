# Custom Monsters
A tool to import monsters from Masterplanner into other platforms

# Usage Instructions
This application uses .creature or .library files exported from Masterplanner and parses them into a format used by the roll20 API script or Foundry journal entries.

In the options menu direct the application to where the exported .creature/.library files are and it will list them in the main window. Choose either Roll20 or Foundry as the target to export to.

For parsing ease, it assumes that effects of a hit are preceded by "Hit: " and actual effects are "Effect: ". Otherwise it dumps the entire power description into the Roll20 hit clause.

In Roll20, add the "importmonster4e.js" script for your games Roll20 API. Use the generated !c command in chat with a selected token to assign the token all the relevant powers and stats.

It hasn't been tested on a whole lot of creatures, 

# Credits
Vorpal for writing the initial Roll20 JS import script.
Andy Aikan for the fantastic Masterplanner application, of course.
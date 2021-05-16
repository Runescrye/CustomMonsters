# MonsterPorter
A tool to import monsters from MonsterPlanner into other platforms

This application uses .creature files exported from MasterPlanner and parses them into a format used by the roll20 API script (or other formats in the future).
In the options menu simply direct the application to where the exported .creature files are and it will populate the list with them.

For parsing ease, it assumes that effects of a hit are preceded by "Hit: " and actual effects are "Effect: ". Otherwise it dumps the entire power description into the Roll20 hit clause.

In Roll20, add the "importmonster4e.js" script for your games Roll20 API. Use the generated !c command in chat with a selected token to assign the token all the relevant powers and stats.

It hasn't been tested on a whole lot of creatures, 
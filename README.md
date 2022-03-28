# act-total-duration
Plugin for the Advanced Combat Tracker application.

The normal duration field calculates the total time in combat. For merged
encounters (such as the All encounter), the duration field is only the sum
of the durations of all encounters. Any time between encounters is not
counted.

This plugin adds a TotalDuration field which tracks the time from start to
end of the encounter. For merged encounters, this is the total time of all
encounters in the merge. This can be used to track total time of a dungeon,
etc.

## Installation

You can install this plugin from the Plugins tab of ACT. Once installed, you
will need to enable the TotalDuration field from the options menus. There is
one field for the encounters view, and one for the combatants view.

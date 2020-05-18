# AerUtils-WWS
An WW_SYSTEM plugin for SCP:SL, that have very big count of functions

# Configs
| Option | Type | Default Value | Description |
| --- | --- | --- | --- |
| aerutils_enable | Bool | true | Enables/Disables the plugin |
| --- | --- | --- | --- |
| aerutils_jbc_enable | Bool | true | Enables/Disables join broadcasts |
| aerutils_jbc_message | String | Welcome to the server! | Join broadcast message |
| aerutils_jbc_time | UInt | 15 | Join broadcast time |
| --- | --- | --- | --- |
| aerutils_lo_enable| Bool | true | Enables/Disables LightsOff |
| aerutils_randomlo_delay_min | 30 | Int | Min time to wait before next lightsoff
| aerutils_randomlo_delay_max | 60 | Int | Max time to wait before next lightsoff
| aerutils_lo_cassie | Warning . Generator malfunction detected | string | LightsOff CASSIE message
| --- | --- | --- | --- |
| aerutils_breakdoors_enable | Bool | true | Enables/Disabled BreakDoors |
| --- | --- | --- | --- |

# Commands
| Command | Description |
| --- | --- |
| pbc | Shows usage of command |
| pbc help | Shows usage of command |
| pbc [RA Player id] [time in seconds] [text] | Sends Personal Broadcast for player |
| --- | --- |
| lights | Shows usage of command |
| lo | Shows usage of command |
| lights help | Shows usage of command |
| lo help | Shows usage of command |
| lights [time in seconds] | Disables lights in the facility for N time (in seconds) |
| lo [time in seconds] | Disables lights in the facility for N time (in seconds) |
| --- | --- |
| bd | Shows usage of command |
| bd help | Shows usage of command |
| bd [RA Player id] | Enables/Disabled BreakDoors mode for player |
| --- | --- |
| killall | Kills everyone on server |
| size [RA player id] [x] [y] [z] | Changes player's size |
| instakill [RA player id] | Instantly kills player
| cleanup [ragdolls / r, items / i, all / a] | Deletes ragdolls, items, or items & ragdolls | 

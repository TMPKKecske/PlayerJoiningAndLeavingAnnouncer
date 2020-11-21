# PlayerJoiningAndLeavingAnnouncer
This plugin can display a custom message if somebody joins or leaves the server. It can say any message you want, feel free to change it in the config of the plugin. The name of the player will be colored as same as their rank's color, but it can be turned off. If a player hasn't got a rank it can be colored too if you want it's your choice.
# Configs 
| Config option  | Value type | Default value | Description
| ------------- | ------------- | ------------- | ------------- |
| IsEnabled  | bool  | true  | Determines if the plugin is available ot not.  |
| IsRankColoringEnabled  | bool  | true  | Allow the plugin to display the player's name's in the color of their ranks. |
| NoRankColor  | string  | yellow | The color of the players with no ranks.  |
| JoinMessage  | string  | {playername} joined the server!  | The message which is displayed when a player joins the server. |
| JoinMessageDuration  | int  | 3  | The duration of the message which is displayed when a player joins the server. |
| LeftMessage  | string  | {playername} left the server!  | The message which is displayed when a player leaves the server.  |
| LeftMessageDuration  | int  | 3  | The duration of the message which is displayed when a player leaves the server.   |

# PlayerJoiningAndLeavingAnnouncer
This plugin can display a custom message if somebody joins or leaves the server. It can say any message you want, feel free to change it in the config of the plugin. The name of the player will be colored as same as their rank's color, but it can be turned off. If a player hasn't got a rank it can be colored too if you want it's your choice.You can have the option to make the plugin only display the players with a rank. You can even reference to specific players by giving a steam id and giving them a custom leave and join message. 
# Configs 
| Config option  | Value type | Default value | Description
| ------------- | ------------- | ------------- | ------------- |
| is_enabled  | bool  | true  | Determines if the plugin is available or not.  |
| is_rank_coloring_enabled  | bool  | true  | Allow the plugin to display the player's name's in the color of their ranks. |
| no_rank_color  | string  | yellow | The color of the players with no ranks.  (set it to blank to make the plugin not color the non-ranked player's name) |
| join_message  | string  | {playername} joined the server!  | The message which is displayed when a player joins the server. |
| join_message_duration  | int  | 3  | The duration of the message which is displayed when a player joins the server. |
| left_message  | string  | {playername} left the server!  | The message which is displayed when a player leaves the server. (set it to blank to disable it) |
| left_message_duration  | int  | 3  | The duration of the message which is displayed when a player leaves the server. |
| only_ranked_are_displayed | bool  | false  | This decides if the join message is only displayed when a ranked player joins or not.| 
| how_specific_players_are_displayed | int | 0 | It determines what will the plugin do with the players listed in ```specific_players``` variable.
specific_players | a list named as specific players steam id the with two strings | JoinMessage: {playername} joined the server! LeftMessage: {playername} left the server! | It determines who will be the specific player who will have a costume leaving and joining message

You can add more players to the ```specific_players``` list by adding this config variable again.
Like this:
```  
 specific_players:
    SomeSteamID@steam:
    - 'JoinMessage: {playername} joined the server!'
    - 'LeftMessage: {playername} left the server!'
    SomeOtherSteamID@steam:
    - 'JoinMessage: {playername} joined the server!'
    - 'LeftMessage: {playername} left the server!'
```

## All available rank colors in scp sl (at the bottom of the page):
https://en.scpslgame.com/index.php/Docs:Permissions
# Thank you! :D
For checking out my plugin, I hope you will use it sometime in your server. If you have any questions or suggestions contact me on discord: ```TMPKKecske#9536```.


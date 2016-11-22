# JunctionManager
A tool for pushing folders to another drive and linking them with junctions, and managing those junctions.

# DISCLAIMER:
Use this tool responsibly. This tool will copy a folder to a new location and delete the original folder.
I am not responsible for any data possibly lost with this tool.

The tool itself has never lost any data, but in the extreme case it does, simply create an issue for it here on GitHub describing what happened and I can work to fix the problem.

# WARNINGS:
- If you want to move a folder moved with Junction Manager, move it using the move button in the main window
	- If you move it yourself, the junction will still be pointing to where the folder was, and your link will be broken
- If you accidentally delete a junction, you will need to recreate it yourself using the "Create Junction" button in the main window

Instructions:
- Open the application shortcut in the start menu (search "Junction Manager") to open the main app window.
	- View the list of junctions made by Junction Manager, and move them back by clicking on them and clicking restore
	- Add existing junctions not made by Junction Manager to this list by clicking "Add" followed by "Add Existing Junctions"
	- Create new junction and add it to the list
- Right click on a folder and click "Switch drives" to move it to the folder specified in the config, with sub directories matching where its saved on the main drive.
- Right click on a junction (even one not made by Junction Manager, such as those made my SteamMover) and click "Switch drives" to move it back to the original folder location
- Right click on a folder moved and linked by a junction (created by Junction Manager) to move it back to where it was

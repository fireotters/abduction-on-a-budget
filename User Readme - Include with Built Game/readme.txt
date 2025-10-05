Abduction on a Budget v1.0.2

Developed by: benchi99, CrossfireCam, Darelt, Frank Busquets, TeslaSP2
Full credits and itch.io links are in the game's Help menu

------------------
How To Start Game
------------------
Launch the 'Abduction on a Budget' executable (File ending in .exe, .x86_64).
By default, the game starts in fullscreen. To change this, visit 'Options'. You'll also find volume options there, among other settings.

To uninstall this game, delete the entire 'Abduction on a Budget v1.0.2' folder.

------------------
OS Specific Instructions
------------------
Windows - Smartscreen may block the game from opening. Click 'More info' and 'Run anyway'.

Linux - The game may not run as an executable.
• For Ubuntu, go to the executable's 'Properties, Permissions' and check the box for 'Allow executing file as program'.
• For other distros, you could run "chmod +x" on the executable to mark it as an executable.
• If the game crashes immediately, check "~/.config/unity3d/FireOtters/Abduction on a Budget" for an error log

------------------
Changelog
------------------
v1.0.2 - Patch for CVE-2025-59489 (5 October 2025)
• Patched a Unity vulnerability (https://unity.com/security/sept-2025-01)

v1.0.1
• Alien's movement improved. Walking is faster, alien can swing while touching ceiling underwater, and rope controls are inverted while UFO is below alien (can be disabled in settings).
• Level layouts improved. Out-of-bounds areas patched up.
• UI: Fonts changed. UI buttons now show caption of what they do.
• Bug Fix: Gaps in map tiles and solid lines in water tiles removed.
• Bug Fix: Alien can now retract rope while touching ceiling underwater. Rope graphics no longer glitch out underwater.

v1.0 - GMTK Game Jam Submission (13 July 2021)
• Game released!

------------------
Attributions
------------------
Music & Sound:
- Soundtrack by Darelt
- SFX done by Frank Busquets

Sprites & UI by TeslaSP2

Script for tilemap opacity fix:
- IceHatGamedude on Reddit https://www.reddit.com/r/Unity3D/comments/645syq/need_help_preventing_additive_blending_of_sprites/dljlmkx/

Script for cutout mask (used for screen transition):
- Code Monkey on YouTube https://www.youtube.com/watch?v=XJJl19N2KFM

Script for invoking code when game is paused:
- Jade Skaggs of Funonium.com https://forum.unity.com/threads/unscaled-time-invoke.299526/
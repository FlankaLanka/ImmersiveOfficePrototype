Running the program:

Via editor:
You need to make sure you are building to Windows in Build Settings. I have a windows machine so I developed with the Vivox Windows SDK.
I tried using the MacOS SDK so I can build for Mac, but I don't think it works if you use a windows machine.
The Vivox SDK for Windows is uploaded on git and named "package".

Via game .exe file:
You need a Windows machine since I cannot build to MacOS because of the voice integration SDK situation explained above.
You can run the game by downloading the "KuraCyberpunk" folder. Open it up and run "Kura Cyberpunk.exe".

After running the game:
You can skip the cutscene by left-clicking through it or just pressing escape.
It's a quick story I wrote for fun to feel more immersive :D!
On the "Join or Host a Lobby Page", make sure to enter your name and have one person click Host, the other hit Join.
If you want to Host, you need to enter a name and share your IP with the person joining (you don't need to enter your IP).
If you want to Join, you need to enter a name and the IP of the Host, then click Join.
Preferably, have the first player Host first, then have the second player click Join.
How to check LAN IP -> https://www.whatismybrowser.com/detect/what-is-my-local-ip-address

Since I didn't set up any servers and left this as P2P connection, you need to port forward if you want to connect over WAN.
I'm happy to connect with you and test this out by port forwarding my own IP if you want.



Controls:
Here are some important controls you may need to know to properly check out everything I worked on.

W,S: Movement (Foward[W] or Backward[S])
A,D: Rotate character (Right[D] or Left[A])
E,C: Rotate Camera (Look Up[E] or Look Down[C])
Scroll Wheel: FPS/3rd Person (ToggleFPS[ScrollUp] or Toggle3rdPerson[ScrollDown])
Q: Topdown View Environment
TAB: Toggle Settings Menu
Left[Shift]: Toggle 2D/3D Video
ESC: Quit Game

Things To Note:
Webcam Feed -> direct encoding of webcam image to bytes and sent through Mirror protocols (I used TCP).
This I know will not support large number of users, but is effective for this task of connecting 2 people.
Voice Chat -> used Vivox


Resources/Tech Stack:

General Tech:
Networking Solution: https://mirror-networking.com/
Voice Chat Integration: https://unity.com/products/vivox
Visual Novel (Story) Engine: https://github.com/snozbot/fungus
Tweening: http://dotween.demigiant.com/
Glitch Shaders: https://github.com/keijiro/KinoGlitch

Assets (UI + Gameplay):
Character: https://assetstore.unity.com/packages/3d/characters/humanoids/sci-fi/stylized-astronaut-114298
Room/Environment: https://assetstore.unity.com/packages/3d/environments/sci-fi/stylized-sci-fi-interior-219970
UI: https://assetstore.unity.com/packages/2d/gui/sci-fi-ui-components-pack-106382
UI: https://assetstore.unity.com/packages/2d/gui/shift-complete-sci-fi-ui-157943


I had a lot of fun and am excited to share my submission with you guys. Hope to hear back soon!
#Ludo_A Readme

This project entails a straightforward implementation of Ludo gameplay logic to fulfill assignment requirements. The code and structure have been developed to address functional assignment specifications. The project comprises four main scripts:

1. RandomNumberGenerator
This C# class is responsible for accessing a public API that generates a random integer between 1 and 6. The generated number simulates the rolling of a dice in the Ludo game.

2. AddressablesController
This script handles the loading of the Ludo board image and player pawn images using Unity's Addressable system. The aim is to fulfill project requirements and improve overall performance.

3. LudoGameplayController
This script manages the rolling dice logic. It utilizes the RandomNumberGenerator script to obtain a random number, verifies if a player can move their pawn, and displays the win screen when a pawn reaches the home point.
Player pawns need to be added to the script in the Unity Editor. Additionally, dice faces (images) are included in the Editor for animating the rolling dice effect.
References to the dice faces , dice button and win panel are crucial for the script's proper functionality.

5. LudoPawnController
This script should be attached to each pawn in the game. The path movement speed value must be modified in the Unity Editor to define the speed at which a pawn moves to its destination.
Paths that the pawn can traverse should be added to the Editor as objects in the path array.
These objects are typically organized under path items and home path categories.
Note:- that these objects must be added for the path array to function correctly.

The functionality of these scripts has been developed with the assignment requirements in mind. It's essential to understand that not all functions are scalable for a complete game logic. Modifications may be necessary for a full game logic.

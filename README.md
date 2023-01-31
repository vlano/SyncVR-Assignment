# SyncVR-Assignment

- Unity Version Used: 2021.3.17f1
- Tested on: Oculus Quest 2 witch touch controllers

Project includes everything, models textures etc. so no additional setup should be required.

## Disclaimer:
The project is not polished. Some features are missing that could make th experience better. Like sound effects, visual effects, animations, map design.
These things require more research than development time so I decided not to include them.

## Overview
The purpose of the app is to incentivizes the user to bend and reach with their arms.
In this mini game you have to reach for acorns and throw them to the squirrel who is standing on the same colored box.

First you have to calibrate the game by following the instructions in the calibration menu. This ensures that the distance the acorns will spawn will be at an appropriate reach.

There is a countdown and you have to score points before the timer runs out. If the acorn collides with the correct colored box you will score 10 points.

The project consists of 2 Scenes:
- MenuScene which contains the main menu. This also includes setting up the game to the user(because of different arms reaches).
- MainScene which includes the gamelogic itself.
Both Scenes have a SceneSystems gameobject which includes all the manager scripts responsible for handling different parts of the game, like input, UI, etc.

Some classes that are used are not found on the scene hierarchy because it was not neccessery for them to be MonoBehaviours.

Most of the communication between the scripts are handled with Actions, to reduce dependencies.

On UI elements, no inspector-assigned events are used, instead we subcribed to the onClick events and call Actions. This is to make debugging and version control easier.

## Improvement options
- As mentioned proper map design, textures, sounds and animation can elevate the experience.
- Using hand-tracking can also increase accessibility so implementing that if running on a capable headset is good path to take.
- Locally stored high-scores and calibration data


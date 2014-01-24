Single Venue Sample App for Windows Phone 8
================

The single venue sample app for Windows Phone 8 shows you how to use the Locu SDK for .NET to display information for a single venue.

## Features 

Features of the app include:
- Displaying the venue on a map
- Obtaining driving directions to the venue
- Placing a phone call to the venue
- Sharing information about the venue
- Supporting voice commands

## Getting Started

To use the app you must make following updates to the __Keys.cs__ file in the __Resources__ directory of the project:
1.	Get a Locu API key at https://dev.locu.com/. Use your key for the value of the LocuApiKey property.
2.	Obtain the Locu Id of the venue you want to build the app for. Use the id for the value of the LocuVenuId property.
	__Note:__ You can either use the Locu developer web console (https://dev.locu.com/console/) or the Locu venue details console that is part of the Locu SDK for .NET (https://github.com/agrocholski/locu-sdk-for-net).

## Customizing

There are a number of ways you can easily customize the look and feel of your app using the __Styles.xaml__ file in the __Resources__ directory.
1.	To change the color of text  within the app, change the value of the __Color__ attribute of the __foreGroundBrush__ resources.
2.	To change the accent colors used in the map, change the values of __Color__ attribute of the __primaryAccentBrush__ and __secondaryAccentBrush__ resources.
3.	To change the background and foreground colors of the application bar, change the values of the __Color__ attribute of the __appBarBackgroundBrush__ and __appBarForeground__ resources respectively.

How to change the application icon, tiles, splash screen, and background image used by the app:
1.	To change the application icon, replace the __AppIcon.png__ file in the __Assets__ directory with your custom file. The file must be named __AppIcon.png__.
2.	To change the tiles, replace the __TileImage-Large.png__, __TileImage-Medium.png__, and __TileImage-Small.png__ images in the __Assets\Tiles__ directory with your own images. The files must be named __TileImage-Large.png__, __TileImage-Medium.png__, and __TileImage-Small.png__.
3.	To change the splash screen, replace the __SplashScreenImage.jpg__ in the project's root directory with your own image. The file must be named __SplashScreenImage.jpg__.
4.	To change the background image displayed in the app, replace the __venue.jpg__ file in the __Assets\Images__ directory with an image of your choice. The file must be named __venue.jpg__.

You can also customize the voice commands recognized by the applicaiton by editing the __VoiceCommands.xml__ file in the project's root directory.

## Submitting

To submit your app to the Windows Phone store, you will need to obtain an application id and authentication token for mapping functionality.
1.	Begin the app submission process to submit it to the Windows Phone store.
2.	On the __SUBMIT APP__ page, click __MAP SERVICES__
3.	On the page click, __GET TOKEN__. The new __ApplicationId__ and __AuthenticationToken__ are displayed on the same page
4.	Copy the ApplicationId and AuthenticationToken values and paste them into the __MapsApplicationId__ and __MapsAuthenticationToken__ properties in the __Keys.cs__ file in the __Resources__ directory of the project.
5.	Rebuild your app with the new code and upload to the Store.

![Powered by Locu](~/docs/images/Locu/poweredby-color.png)
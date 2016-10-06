# ANR Scoring And Visualisation
#### What is this?
Air Navigation Race (ANR) is an official competition discipline in General Aviation. 
For more details please refer to the FAI pages on http://www.fai.org/gac-our-sport/air-navigation-race  
The ANR scoring and visualisation software presented below is an application used in Air Navigation Race (ANR) competitions.
In brief,this software is used to create & manage ANR competitions and competition tasks, and -maps 
and to evaluate the results based on logger data collected during the competition flights. 

---
#### Tech
The actual application is a Windows forms (stand-alone) application based on c# (.NET framework 4.5, & Entity framework) using a local SQL database.

---
#### History
Based on the specifications of Heini Schawalder and Maurice Ducret, the development for the ANR scoring and visualisation 
software was initiated during 2009-2010 at the Commercial and Industrial Training college, Berne, Switzerland (GIBBS), 
under the technical lead of **Luc Baumann/SharpSoft**. Luc has been maintaining the application since its beginnings in 2009.
Old code can be found from the google code archive https://code.google.com/archive/p/anrl/downloads. 
At this time the application was basically a client-server application with a centralized database server.
There has been some development later (2015), developed probably from the 2013 version of the Anrl.zip (workaround-local-app) of above google code. 
Luc has stored the 2015 development code in a dedicated GitHub repository ( https://github.com/helios57/anrl )
My starting point for the code upgrade was a clone of Luc's Github repository.

--- 
#### What's new here (version 1.0.15)?


**Bug fixes and error handling** 
* Bug fix for Start list calculation

**Additional features** 
* User settings: the user can select a default batabase path (or being prompted when the application starts)
* User settings: enable/disable additional overlay text for Parcour PDF export
* Coordinate export (*.kml->*.txt) for Route Generator

**Other minor changes**
* Parcour PDF export, maps scale 1:250'000 implemented
* Parcour PDF export, text showing the map scale is added (if no overlay text, or overlay text is empty)
* Parcour PDF export, size and positioning of additional overlay text 

**Updated documentation** 
* Documentation about map conversion and map import
* Some documentation about the route generator
* General updates (some missing application parts included)

---
#### Download?
Binaries and documentation are available in the [Release section](../../releases) of this repository.
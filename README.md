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
#### What's new here (version 1.0.13.0)?
**Improvements in user interface**

* Less buttons, implemented editable datagridviews instead of lists in many places
* Separate input windows if reasonable

**Removed outdated and unused code/features**
* Outdated real-time tracking removed
* Google plugin deprecated & u/s, removed

**Bug fixes and error handling** 
* Mainly decimal separator related issues (dot/comma)
* Improved error handling

**Additional features** 
* Route generator implemented. This is probably the most important change. 
It is basically a replacement for the original coordinate upload with an AutoCAD (*.dxf) file. Routes can now be created with Google Earth Pro,and the resulting kml file uploaded. A dedicated AutoCAD software is not required anymore.
* Export for flight data in *.gpx format implemented

**Other minor changes**
* Results, Map Export + List Export: only teams that have flight data (=uploaded logger data) are shown 
* Result List Export, shared ranking implemented
* Map preview: transparency value can be saved; replaced Trackbar control with numericUpDown
* Start List calculation & re-calculation implemented
* Legacy calculator (WGS84-CH1903) moved to Tools

**Updated documentation** 
* Documentation about map conversion and map import
* Some documentation about the route generator

---
#### Download?
Binaries and documentation are available in the [Release section](../../releases) of this repository.
# ANR Scoring And Visualisation
The ANR scoring and visualisation software presented below is an application used in Air Navigation Race (ANR) competitions.
Air Navigation Race (ANR) is an official competition mode in the area of General Aviation. 
For more details please refer to http://www.fai.org/gac-our-sport/air-navigation-race

=================================================================================================================
Based on the specifications of Heini Schawalder and Maurice Ducret, the development for the ANR scoring and visualisation 
software was initiated during 2009-2010 at the Commercial and Industrial Training college, Berne, Switzerland (GIBBS), 
under the technical lead of **Luc Baumann/SharpSoft**. Luc has been maintaining the application since its beginnings in 2009.
The application is a Windows forms application based on c# (.NET framework 4.5, & Entity framework).
The code in this repository is based on Luc's original code( on https://github.com/helios57/anrl ).
 
### What is new/different in this version 1.0.13 (the initial version in this repository)?
**Improvements in user interface**
- Less buttons, implemented editable datagridviews instead of lists in many places
- Separate input windows if reasonable
**Removed outdated and unused code/features**
- Outdated real-time tracking removed
- Google plugin deprecated & u/s, removed
**Bug fixes and error handling** 
- Mainly decimal separator related issues (dot/comma)
- Improved error handling
- **Additional features** 
- Route generator implemented. This is probably the most important change. 
  It is basically a replacement for the original coordinate upload with an AutoCAD (*.dxf) file. Routes can now be created with Google Earth Pro,
  and the resulting kml file uploaded. A dedicated AutoCAD software is not required anymore.
**Export for flight data in *.gpx format** implemented
**Other minor changes**
- Results: Map Export + List Export: only teams that have flight data (=uploaded logger data) are shown 
- Result List Export, shared ranking implemented
- Map preview: transparency value can be saved. Replaced Trackbar control with numericUpDown
- StartList calculation & re-calculation
- Legacy calculator (WGS84-CH1903) moved to Tools
**Updated documentation** 
- Documentaion about map conversion and map import
- Some documentation about the route generator

Binaries and documentation are available in the Release section of this repository.
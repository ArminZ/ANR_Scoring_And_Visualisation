# ANR Scoring And Visualisation
ANR scoring and visualisation software is an application used in general Aviation Air Navigation Race (ANR) competitions.
For details refer to http://www.fai.org/gac-our-sport/air-navigation-race

The application is a Windows forms application based on c# (.NET framework 4.5, & Entity framework).

=================================================================================================================
Based on the specifications of Heini Schawalder and Maurice Ducret, the development for the ANR scoring and visualisation 
software was initiated during 2009-2010 at the Commercial and Industrial Training college, Berne, Switzerland (GIBBS), 
under the technical lead of Luc Baumann/SharpSoft. Luc has been maintaining the application since its beginnings in 2009.
The code in this repository is based on original code which can be found at https://github.com/helios57/anrl .
 
### What is new/different in this version 1.0.13 (the initial version in this repository)?
- Improvements in user interface (less buttons, implemented datagridviews in many places, separate input windows if reasonable)
- Removed outdated and unused code/features (real-time tracking, Google plugin deprecated & u/s)
- Bug corrections for decimal separator related issues in many place 
- Improved error handling
- Additional features: Route generator (route creation in Google Earth Pro) implemented
- Export for flight data in *.gpx format implemented
- Results: Map Export + List Export: only teams that have flight data (=uploaded logger data) are shown 
- Result List Export, shared ranking implemented
- Map preview: transparency value can be saved. Use numericUpDown instead of trackbar.
- StartList calculation & re-calculation
- Legacy calculator (WGS84-CH1903) moved to Tools
- Updated documentation on map conversion and map import
- Some documentation about the route generator

Binaries and documentation are available in the Release section of this repository.
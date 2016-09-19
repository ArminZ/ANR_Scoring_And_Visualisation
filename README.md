# ANR Scoring And Visualisation
ANR scoring and visualisation software is an application used in general Aviation Air Navigation Race (ANR) competitions.
For details refer to http://www.fai.org/gac-our-sport/air-navigation-race
The application is a Windows forms application based on c# (.NET framework 4.5, & Entity framework).
============================================================================
Based on the specifications of Heini Schawalder and Maurice Ducret, the development for the ANR scoring and visualisation 
software was initiated during 2009-2010 at the Commercial and Industrial Training college, Berne (GIBBS), 
under the technical lead of Luc Baumann/SharpSoft. Luc has been maintaining the application since its beginnings in 2009.
The code in this repository is based on original code which can be found at https://github.com/helios57/anrl
 
### Major changes as per 19.9.2016:
- removed all outdated and unused code (real-time tracking, Google plugin deprecated & u/s)
- streamlined existing code
- improved error handling
- improvements in user interface (datagridviews and pop-up input windows)
- Route generator integrated
- bug corections for decimal separator related issues
- Export for flights as .gpx
- Results: Map Export + List Export only for teams with logger data. List Export, shared ranking implemented
- Map preview: save of Alpha value implemented
- Improved error handling for KML parcour import
- Corrected bug for StartList calculation
- Calculator moved to Tools (bug corrrection for double format)
- Maps from Open Street map/google Earth: excluded from project 
 

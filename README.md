# ANR_Scoring_And_Visualisation
ANR scoring and visualisation software
Software used in general Aviation /Air Navigation Race (ANR)
For details refer to http://www.fai.org
============================================================================
This code is derived from the original code located https://github.com/helios57/anrl
Credits to Luc/sharpsoft who has been maintaining the application since 2009
The application is a Windows forms application based on c# (.NET framework 4.5, & Entity framework)
============================================================================
Major changes as per 19.9.2016: 
-removed all outdated and unused code (real-time tracking, Google plugin deprecated & u/s)
-streamlined existing code
-improved error handling
-improvements in user interface (datagridviews and pop-up input windows)
-Route generator integrated
-bug corections for decimal separator related issues
-Export for flights as .gpx
-Results: Map Export + List Export only for teams with logger data. List Export, shared ranking implemented
-Map preview: save of Alpha value implemented
-improved error handling for KML parcour import
-corrected bug for StartList calculation
-Calculator moved to Tools (bug corrrection for double format)
-Maps from Open Street map/google Earth: excluded from project

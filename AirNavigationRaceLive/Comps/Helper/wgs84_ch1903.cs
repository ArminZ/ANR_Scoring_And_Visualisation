
// Source: http://www.swisstopo.admin.ch/internet/swisstopo/de/home/topics/survey/sys/refsys.parsysrelated1.23611.downloadList.11186.DownloadFile.tmp/swissprojectionde.pdf

using System;

namespace swisstopo.geodesy.gpsref
{
	/// <summary>
	/// Summary description for ApproxSwissProj.
	/// </summary>
	public class ApproxSwissProj
	{
		public ApproxSwissProj()
		{
		}

		public static void LV03toWGS84(double east, double north, double height, ref double latitude, ref double longitude, ref double ellHeight)
		{
			latitude = CHtoWGSlat(east, north);
			longitude = CHtoWGSlng(east, north);
			ellHeight = CHtoWGSheight(east, north, height);
			return;
		}

		public static void WGS84toLV03(double latitude, double longitude, double ellHeight, ref double east, ref double north, ref double height)
		{
			east = WGStoCHy(latitude, longitude);
			north = WGStoCHx(latitude, longitude);
			height = WGStoCHh(latitude, longitude, ellHeight);
			return;
		}


		// Convert WGS lat/long (° dec) to CH y
		public static double WGStoCHy(double lat, double lng) 
		{
			// Converts degrees dec to sex
			lat = DecToSexAngle(lat);
			lng = DecToSexAngle(lng);

			// Converts degrees to seconds (sex)
			lat = SexAngleToSeconds(lat);
			lng = SexAngleToSeconds(lng);
  
			// Axiliary values (% Bern)
			double lat_aux = (lat - 169028.66)/10000;
			double lng_aux = (lng - 26782.5)/10000;
  
			// Process Y
			double y = 600072.37 
				+ 211455.93 * lng_aux 
				-  10938.51 * lng_aux * lat_aux
				-      0.36 * lng_aux * Math.Pow(lat_aux, 2)
				-     44.54 * Math.Pow(lng_aux, 3);
     
			return y;
		}

		// Convert WGS lat/long (° dec) to CH x
		public static double WGStoCHx(double lat, double lng) 
		{
			// Converts degrees dec to sex
			lat = DecToSexAngle(lat);
			lng = DecToSexAngle(lng);
  
			// Converts degrees to seconds (sex)
			lat = SexAngleToSeconds(lat);
			lng = SexAngleToSeconds(lng);
  
			// Axiliary values (% Bern)
			double lat_aux = (lat - 169028.66)/10000;
			double lng_aux = (lng - 26782.5)/10000;

			// Process X
			double x = 200147.07
				+ 308807.95 * lat_aux 
				+   3745.25 * Math.Pow(lng_aux, 2)
				+     76.63 * Math.Pow(lat_aux, 2)
				-    194.56 * Math.Pow(lng_aux, 2) * lat_aux
				+    119.79 * Math.Pow(lat_aux, 3);
 
			return x;
		}

		// Convert WGS lat/long (° dec) and height to CH h
		private static double WGStoCHh(double lat, double lng, double h) 
		{
			// Converts degrees dec to sex
			lat = DecToSexAngle(lat);
			lng = DecToSexAngle(lng);
  
			// Converts degrees to seconds (sex)
			lat = SexAngleToSeconds(lat);
			lng = SexAngleToSeconds(lng);
  
			// Axiliary values (% Bern)
			double lat_aux = (lat - 169028.66)/10000;
			double lng_aux = (lng - 26782.5)/10000;

			// Process h
			h = h - 49.55 
				  +  2.73 * lng_aux 
				  +  6.94 * lat_aux;
     
			return h;
		}



		// Convert CH y/x to WGS lat
		private static double CHtoWGSlat(double y, double x) 
		{
			// Converts militar to civil and  to unit = 1000km
			// Axiliary values (% Bern)
			double y_aux = (y - 600000)/1000000;
			double x_aux = (x - 200000)/1000000;
  
			// Process lat
			double lat = 16.9023892
				+  3.238272 * x_aux
				-  0.270978 * Math.Pow(y_aux, 2)
				-  0.002528 * Math.Pow(x_aux, 2)
				-  0.0447   * Math.Pow(y_aux, 2) * x_aux
				-  0.0140   * Math.Pow(x_aux, 3);
    
			// Unit 10000" to 1 " and converts seconds to degrees (dec)
			lat = lat * 100/36;
  
			return lat;
		}

		// Convert CH y/x to WGS long
		private static double CHtoWGSlng(double y, double x) 
		{
			// Converts militar to civil and  to unit = 1000km
			// Axiliary values (% Bern)
			double y_aux = (y - 600000)/1000000;
			double x_aux = (x - 200000)/1000000;
  
			// Process long
			double lng = 2.6779094
				+ 4.728982 * y_aux
				+ 0.791484 * y_aux * x_aux
				+ 0.1306   * y_aux * Math.Pow(x_aux, 2)
				- 0.0436   * Math.Pow(y_aux, 3);
     
			// Unit 10000" to 1 " and converts seconds to degrees (dec)
			lng = lng * 100/36;
     
			return lng;
		}

		// Convert CH y/x/h to WGS height
		private static double CHtoWGSheight(double y, double x, double h) 
		{
			// Converts militar to civil and  to unit = 1000km
			// Axiliary values (% Bern)
			double y_aux = (y - 600000)/1000000;
			double x_aux = (x - 200000)/1000000;
  
			// Process height
			h = h + 49.55
				  - 12.60 * y_aux
				  - 22.64 * x_aux;
     
			return h;
		}



		// Convert sexagesimal angle (degrees, minutes and seconds "dd.mmss") to decimal angle (degrees)
		public static double SexToDecAngle(double dms) 
		{
			// Extract DMS
			// Input: dd.mmss(,)ss
			double deg=0, min=0, sec=0;
			deg = Math.Floor(dms);
			min = Math.Floor((dms-deg)*100);
			sec = (((dms-deg)*100) - min) * 100;
			
			// Result in degrees dec (dd.dddd)
			return deg + min/60 + sec/3600;
		}

		// Convert decimal angle (degrees) to sexagesimal angle (degrees, minutes and seconds dd.mmss,ss)
		public static double DecToSexAngle(double dec) 
		{
			int deg = (int)Math.Floor(dec);
			int min = (int)Math.Floor((dec-deg)*60);
			double sec = (((dec-deg)*60)-min)*60;

			// Output: dd.mmss(,)ss
			return deg + (double)min/100 + (double)sec/10000;
		}

		// Convert sexagesimal angle (degrees, minutes and seconds dd.mmss,ss) to seconds
		public static double SexAngleToSeconds(double dms) 
		{
			double deg=0, min=0, sec=0;
			deg = Math.Floor(dms);
			min = Math.Floor((dms-deg)*100);
			sec = (((dms-deg)*100) - min) * 100;
  
			// Result in degrees sex (dd.mmss)
			return sec + (double)min*60 + (double)deg*3600;
		}

	}
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirNavigationRaceLive.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string directoryForDB {
            get {
                return ((string)(this["directoryForDB"]));
            }
            set {
                this["directoryForDB"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool promptForDB {
            get {
                return ((bool)(this["promptForDB"]));
            }
            set {
                this["promptForDB"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=localhost;Initial Catalog=anrl;Integrated Security=True")]
        public string anrlConnectionString {
            get {
                return ((string)(this["anrlConnectionString"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool HasCompMapAdditionalText {
            get {
                return ((bool)(this["HasCompMapAdditionalText"]));
            }
            set {
                this["HasCompMapAdditionalText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Red")]
        public global::System.Drawing.Color PROHColor {
            get {
                return ((global::System.Drawing.Color)(this["PROHColor"]));
            }
            set {
                this["PROHColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("22")]
        public decimal PROHTransp {
            get {
                return ((decimal)(this["PROHTransp"]));
            }
            set {
                this["PROHTransp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Red")]
        public global::System.Drawing.Color SPFPColor {
            get {
                return ((global::System.Drawing.Color)(this["SPFPColor"]));
            }
            set {
                this["SPFPColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public decimal SPFPenWidth {
            get {
                return ((decimal)(this["SPFPenWidth"]));
            }
            set {
                this["SPFPenWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SPFPCircle {
            get {
                return ((bool)(this["SPFPCircle"]));
            }
            set {
                this["SPFPCircle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color ChannelColor {
            get {
                return ((global::System.Drawing.Color)(this["ChannelColor"]));
            }
            set {
                this["ChannelColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public decimal ChannelPenWidth {
            get {
                return ((decimal)(this["ChannelPenWidth"]));
            }
            set {
                this["ChannelPenWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public decimal IntersectionPenWidth {
            get {
                return ((decimal)(this["IntersectionPenWidth"]));
            }
            set {
                this["IntersectionPenWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Blue")]
        public global::System.Drawing.Color IntersectionColor {
            get {
                return ((global::System.Drawing.Color)(this["IntersectionColor"]));
            }
            set {
                this["IntersectionColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool HasChannelFill {
            get {
                return ((bool)(this["HasChannelFill"]));
            }
            set {
                this["HasChannelFill"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color ChannelFillColor {
            get {
                return ((global::System.Drawing.Color)(this["ChannelFillColor"]));
            }
            set {
                this["ChannelFillColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public decimal ChannelTransp {
            get {
                return ((decimal)(this["ChannelTransp"]));
            }
            set {
                this["ChannelTransp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public decimal IntersectionCircleRadius {
            get {
                return ((decimal)(this["IntersectionCircleRadius"]));
            }
            set {
                this["IntersectionCircleRadius"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ShowPROHBorder {
            get {
                return ((bool)(this["ShowPROHBorder"]));
            }
            set {
                this["ShowPROHBorder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public decimal PROHBorderPenWidth {
            get {
                return ((decimal)(this["PROHBorderPenWidth"]));
            }
            set {
                this["PROHBorderPenWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Red")]
        public global::System.Drawing.Color PROHBorderColor {
            get {
                return ((global::System.Drawing.Color)(this["PROHBorderColor"]));
            }
            set {
                this["PROHBorderColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ShowIntersectionCircles {
            get {
                return ((bool)(this["ShowIntersectionCircles"]));
            }
            set {
                this["ShowIntersectionCircles"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int MaxPenaltyPerEvent {
            get {
                return ((int)(this["MaxPenaltyPerEvent"]));
            }
            set {
                this["MaxPenaltyPerEvent"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public decimal FlightPenWidth {
            get {
                return ((decimal)(this["FlightPenWidth"]));
            }
            set {
                this["FlightPenWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color FlightPenColor {
            get {
                return ((global::System.Drawing.Color)(this["FlightPenColor"]));
            }
            set {
                this["FlightPenColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int ParcourType {
            get {
                return ((int)(this["ParcourType"]));
            }
            set {
                this["ParcourType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int TimeToleranceSPFP {
            get {
                return ((int)(this["TimeToleranceSPFP"]));
            }
            set {
                this["TimeToleranceSPFP"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int TimeToleranceEnroute {
            get {
                return ((int)(this["TimeToleranceEnroute"]));
            }
            set {
                this["TimeToleranceEnroute"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int PenaltyPointsPerSecond {
            get {
                return ((int)(this["PenaltyPointsPerSecond"]));
            }
            set {
                this["PenaltyPointsPerSecond"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int LoggerDefaultFileType {
            get {
                return ((int)(this["LoggerDefaultFileType"]));
            }
            set {
                this["LoggerDefaultFileType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("200")]
        public int MaxPenaltySPFP {
            get {
                return ((int)(this["MaxPenaltySPFP"]));
            }
            set {
                this["MaxPenaltySPFP"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int TimeToleranceLowerTKOF {
            get {
                return ((int)(this["TimeToleranceLowerTKOF"]));
            }
            set {
                this["TimeToleranceLowerTKOF"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public int TimeToleranceUpperTKOF {
            get {
                return ((int)(this["TimeToleranceUpperTKOF"]));
            }
            set {
                this["TimeToleranceUpperTKOF"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("200")]
        public int MaxPenaltyTKOF {
            get {
                return ((int)(this["MaxPenaltyTKOF"]));
            }
            set {
                this["MaxPenaltyTKOF"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public long GACFileWarningThresholdDate {
            get {
                return ((long)(this["GACFileWarningThresholdDate"]));
            }
            set {
                this["GACFileWarningThresholdDate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string CompMapAdditionalText {
            get {
                return ((string)(this["CompMapAdditionalText"]));
            }
            set {
                this["CompMapAdditionalText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://airsports.no")]
        public string AirsportsHost {
            get {
                return ((string)(this["AirsportsHost"]));
            }
            set {
                this["AirsportsHost"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("14b3f9e9d6352010623e4afe61a727c775e58852")]
        public string AirsportsToken {
            get {
                return ((string)(this["AirsportsToken"]));
            }
            set {
                this["AirsportsToken"] = value;
            }
        }
    }
}

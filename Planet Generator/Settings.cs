using System.Collections.Generic;
using System.Drawing;

namespace Planet_Generator
{
    public class Settings
    {
        public int Resolution { get; set; } = 400;
        public int AtmosphereThickness { get; set; } = 4;
        public bool GenerateClouds { get; set; } = true;
        public List<Color> CloudColors { get; set; }
        public List<Color> PlanetColors { get; set; }
        public int CloudContinentCount { get; set; }
        public int ContinentCount { get; set; }
        public int ContinentRadius { get; set; } // resolution / 3
        public int CloudContinentRadius { get; set; }
        public int ContinentBoost { get; set; }
        public int CloudSmoothHeightMap { get; set; }
        public int SmoothHeightMapAmount { get; set; } // 12

        public int ExpandColors { get; set; } = 4;

        public int SmoothTextureAmount { get; set; } // 5

        public int CloudSmoothAmount { get; set; }

        public int CloudBoost { get; set; }

        #region Earth Settings

        public static Settings GetEarthSettings(int resolution)
        {
            var settings = new Settings();
            settings.Resolution = resolution;
            settings.AtmosphereThickness = 4;
            settings.GenerateClouds = true;
            settings.PlanetColors = Settings.GetEarthPlanetColors();

            settings.CloudColors = Settings.GetEarthCloudColors();
            settings.CloudContinentCount = 400;
            settings.CloudContinentRadius = 20;
            settings.CloudBoost = 800;
            settings.CloudSmoothHeightMap = 6;
            settings.CloudSmoothAmount = 1;

            settings.ContinentCount = 12;
            settings.ContinentRadius = settings.Resolution / 3;
            settings.ContinentBoost = 150;
            settings.SmoothHeightMapAmount = 12;
            settings.SmoothTextureAmount = 0;
            settings.ExpandColors = 1;

            return settings;
        }

        public static List<Color> GetEarthPlanetColors()
        {
            return new List<Color>()
            {
                Color.DarkBlue,
                Color.Blue,
                Color.CornflowerBlue,
                Color.Tan,
                Color.DarkKhaki,
                Color.Green,
                Color.Green,
                Color.Green,
                Color.DarkOliveGreen,
                Color.DarkOliveGreen,
                Color.DarkOliveGreen,
                Color.DarkOliveGreen,
                Color.DarkGray,
                Color.LightGray,
                Color.White
            };
        }

        public static List<Color> GetEarthCloudColors()
        {
            return new List<Color>()
            {
                Color.Transparent,
                Color.White,
                Color.LightGray,
            };
        }

        #endregion Earth Settings

        #region Third Impact Settings

        public static Settings GetThirdImpactSettings(int resolution)
        {
            var settings = new Settings();
            settings.Resolution = resolution;
            settings.AtmosphereThickness = 4;
            settings.GenerateClouds = true;
            settings.CloudColors = Settings.GetEarthCloudColors();
            settings.PlanetColors = Settings.GetThirdImpactColors();
            settings.CloudContinentCount = 24;
            settings.ContinentCount = 12;
            settings.ContinentRadius = settings.Resolution / 3;
            settings.CloudContinentRadius = settings.Resolution / 3;
            settings.ContinentBoost = 200;
            settings.CloudSmoothHeightMap = 12;
            settings.SmoothHeightMapAmount = 12;
            settings.SmoothTextureAmount = 5;
            settings.CloudSmoothAmount = 5;

            return settings;
        }

        public static List<Color> GetThirdImpactColors()
        {
            return new List<Color>()
            {
                Color.Red,
                Color.DarkRed,
                Color.MediumVioletRed,
                Color.Tan,
                Color.DarkKhaki,
                Color.YellowGreen,
                Color.Green,
                Color.DarkOliveGreen,
                Color.DarkOliveGreen,
                Color.DarkGray,
                Color.White
            };
        }

        #endregion Third Impact Settings

        #region Moon Settings

        public static Settings GetMoonSettings(int resolution)
        {
            var settings = new Settings();
            settings.Resolution = resolution;
            settings.AtmosphereThickness = 0;
            settings.GenerateClouds = false;
            settings.CloudColors = Settings.GetEarthCloudColors();
            settings.PlanetColors = Settings.GetMoonColors();
            settings.CloudContinentCount = 12;
            settings.ContinentCount = 50;
            settings.ContinentRadius = settings.Resolution / 3;
            settings.CloudContinentRadius = settings.Resolution / 3;
            settings.ContinentBoost = 300;
            settings.CloudSmoothHeightMap = 12;
            settings.SmoothHeightMapAmount = 12;
            settings.SmoothTextureAmount = 5;
            settings.CloudSmoothAmount = 5;

            return settings;
        }

        public static List<Color> GetMoonColors()
        {
            return new List<Color>()
            {
                Color.Gray,
                Color.DarkGray,
                Color.LightGray,
            };
        }

        #endregion Moon Settings
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int SmoothTextureAmount { get; set; } // 5

        public int CloudSmoothAmount { get; set; }

        public static Settings GetEarthSettings(int resolution)
        {
            var settings = new Settings();
            settings.Resolution = resolution;
            settings.AtmosphereThickness = 4;
            settings.GenerateClouds = true;
            settings.CloudColors = Settings.GetEarthCloudColors();
            settings.PlanetColors = Settings.GetEarthPlanetColors();
            settings.CloudContinentCount = 12;
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

        public static List<Color> GetEarthPlanetColors()
        {
            return new List<Color>()
            {
                Color.Blue,
                Color.LightBlue,
                Color.SkyBlue,
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

        public static List<Color> GetEarthCloudColors()
        {
            return new List<Color>()
            {
                Color.Transparent,
                Color.White,
                Color.LightGray,
            };
        }

    }
}

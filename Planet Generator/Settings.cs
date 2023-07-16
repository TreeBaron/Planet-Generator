using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Planet_Generator
{
    public class Settings
    {
        public bool Upscale { get; set; }
        public float CloudTransparency { get; set; } = 0.75f;
        public Color AtmosphereColor { get; set; }
        public float AtmosphereTransparency { get; set; } = 0.75f;
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
        public int ContinentHinder { get; set; }
        public int CloudSmoothHeightMap { get; set; }
        public int SmoothHeightMapAmount { get; set; } // 12

        public int ExpandColors { get; set; } = 4;

        public int SmoothTextureAmount { get; set; } // 5

        public int CloudSmoothAmount { get; set; }

        public int CloudBoost { get; set; }

        public int RaiseAllLandAmount { get; set; } = 0;

        public bool AddCraters { get; set; } = false;

        public int CraterDepth { get; set; } = 150;

        public int CraterCount { get; set; } = 25;

        #region TOS Settings
        public static Settings GetTOSSettings(int resolution)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            var settings = new Settings();
            settings.AtmosphereColor = Color.Blue;
            settings.Resolution = resolution;
            settings.AtmosphereThickness = 4;
            settings.AtmosphereTransparency = 0.5f;
            settings.GenerateClouds = true;
            settings.PlanetColors = Settings.GetTOSPlanetColors();

            settings.CloudColors = Settings.GetTOSCloudColors();
            settings.CloudContinentCount = 400;
            settings.CloudContinentRadius = 20;
            settings.CloudBoost = 800;
            settings.CloudSmoothHeightMap = 6;
            settings.CloudSmoothAmount = 1;

            settings.ContinentCount = 12;
            settings.ContinentRadius = 150;
            settings.ContinentBoost = 350;
            settings.ContinentHinder = -350;
            settings.SmoothHeightMapAmount = 12;
            settings.SmoothTextureAmount = 0;
            settings.ExpandColors = r.Next(1, 3);

            settings.RaiseAllLandAmount = r.Next(-100, 100);

            return settings;
        }

        public static List<Color> GetTOSPlanetColors()
        {
            Random random = new Random();
            return new List<Color>()
            {
                random.NextDouble() > 0.5 ? Color.DarkSeaGreen : Color.Blue,
                Color.Tan,
                Color.DarkKhaki,
                random.Next() > 0.5 ? Color.Green : Color.Red,
                random.Next() > 0.5 ? Color.DarkOliveGreen : Color.DarkRed,
                Color.Gray,
                Color.SlateGray,
                Color.White
            };
        }

        public static List<Color> GetTOSCloudColors()
        {
            Random random = new Random(DateTime.Now.Second);
            return new List<Color>()
            {
                Color.Transparent,
                random.NextDouble() > 0.5 ? random.NextDouble() > 0.5 ? Color.Purple : Color.White : Color.Yellow
            };
        }
        #endregion

        #region Earth Settings

        public static Settings GetEarthSettings(int resolution)
        {
            var settings = new Settings();
            settings.AtmosphereColor = Color.Blue;
            settings.Resolution = resolution;
            settings.AtmosphereThickness = 4;
            settings.AtmosphereTransparency = 0.5f;
            settings.GenerateClouds = true;
            settings.PlanetColors = Settings.GetEarthPlanetColors();

            settings.CloudColors = Settings.GetEarthCloudColors();
            settings.CloudContinentCount = 400;
            settings.CloudContinentRadius = 20;
            settings.CloudBoost = 800;
            settings.CloudSmoothHeightMap = 6;
            settings.CloudSmoothAmount = 1;

            settings.ContinentCount = 12;
            settings.ContinentRadius = 150;
            settings.ContinentBoost = 350;
            settings.ContinentHinder = -350;
            settings.SmoothHeightMapAmount = 12;
            settings.SmoothTextureAmount = 0;
            settings.ExpandColors = 0;

            return settings;
        }

        public static List<Color> GetEarthPlanetColors()
        {
            Random random = new Random();
            return new List<Color>()
            {
                random.NextDouble() > 0.5 ? Color.DarkBlue : Color.Blue,
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
            settings.AtmosphereColor = Color.Red;
            settings.AtmosphereTransparency = 0.5f;
            settings.Resolution = resolution;
            settings.AtmosphereThickness = 4;
            settings.GenerateClouds = true;
            settings.PlanetColors = Settings.GetThirdImpactColors();
            settings.CloudTransparency = 0.9f;

            settings.CloudColors = Settings.GetEarthCloudColors();
            settings.CloudContinentCount = 400;
            settings.CloudContinentRadius = 20;
            settings.CloudBoost = 800;
            settings.CloudSmoothHeightMap = 6;
            settings.CloudSmoothAmount = 1;

            settings.ContinentCount = 12;
            settings.ContinentRadius = 145;
            settings.ContinentBoost = 150;
            settings.ContinentHinder = -150;
            settings.SmoothHeightMapAmount = 12;
            settings.SmoothTextureAmount = 0;
            settings.ExpandColors = 1;

            return settings;
        }

        public static List<Color> GetThirdImpactColors()
        {
            return new List<Color>()
            {
                Color.Red,
                Color.DarkRed,
                Color.Maroon,
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
            settings.AtmosphereColor = Color.Gray;
            settings.Resolution = resolution;
            settings.AtmosphereThickness = 0;
            settings.AtmosphereTransparency = 0.0f;
            settings.GenerateClouds = false;
            settings.CloudColors = Settings.GetEarthCloudColors();
            settings.PlanetColors = Settings.GetMoonColors();
            settings.CloudContinentCount = 12;
            settings.ContinentCount = 150;
            settings.ContinentRadius = 50;
            settings.CloudContinentRadius = 150;
            settings.ContinentBoost = 600;
            settings.ContinentHinder = -600;
            settings.CloudSmoothHeightMap = 12;
            settings.SmoothHeightMapAmount = 2;
            settings.SmoothTextureAmount = 1;
            settings.CloudSmoothAmount = 5;
            settings.AddCraters = true;
            settings.CraterCount = 6;
            settings.CraterDepth = 1200;

            return settings;
        }

        public static List<Color> GetMoonColors()
        {
            return new List<Color>()
            {
                Color.Gray,
                Color.DarkGray,
                Color.LightGray,
                Color.Gray,
                Color.DarkGray,
                Color.LightGray
            };
        }

        public static Dictionary<string, Settings> GetSettingsDictionary(int resolution)
        {
            var map = new Dictionary<string, Settings>();
            map.Add("Earth-Like", GetEarthSettings(resolution));
            map.Add("Moon", GetMoonSettings(resolution));
            map.Add("Third Impact", GetThirdImpactSettings(resolution));
            map.Add("TOS Planet", GetTOSSettings(resolution));
            return map;
        }

        #endregion Moon Settings

        #region From Image Settings
        public static Settings GetSettingsFromImage(int resolution, Bitmap image, Settings selectedSetting)
        {
            var scaledImage = new Bitmap(image, new Size(5, 5));

            Random r = new Random(DateTime.Now.Millisecond);

            var settings = selectedSetting;

            settings.AtmosphereColor = SampleImage(image, 1).First();
            settings.AddCraters = r.NextDouble() > 0.5 ? true : false;
            settings.GenerateClouds = r.NextDouble() > 0.5 ? true : false;
            settings.AtmosphereThickness = r.Next(1, 4);
            settings.Resolution = resolution;
            settings.CloudColors = SampleImage(image, 2);

            var planetColors = GetColors(scaledImage);
            planetColors.AddRange(SampleImage(image, 24));

            settings.PlanetColors = planetColors;
            settings.ContinentBoost = 150;
            settings.ContinentHinder = r.Next(-50, -5);

            return settings;
        }

        private static List<Color> SampleImage(Bitmap bitmap, int amount)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            var list = new List<Color>();
            for(int i = 0; i < amount; i++)
            {
                var pixel = bitmap.GetPixel(r.Next(0, bitmap.Width-1), r.Next(0, bitmap.Height-1));
                list.Add(pixel);
            }

            return list;
        }

        private static List<Color> GetColors(Bitmap image)
        {
            Random r = new Random();
            var colors = new List<Color>();
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    colors.Add(pixel);
                }
            }

            colors = colors.OrderBy(x => r.NextDouble()).ToList();

            return colors;
        }

        #endregion
    }
}
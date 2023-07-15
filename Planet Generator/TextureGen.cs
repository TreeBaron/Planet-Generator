using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Planet_Generator
{
    internal class TextureGen
    {
        public static Bitmap GenerateClouds(Settings settings)
        {
            var image = GetTemplateBitmapTransparent(settings.Resolution, settings.Resolution);
            var heightMap = GenerateHeightMapDiamondSquareAlgo(settings.Resolution);

            AddContinents(heightMap, settings.CloudContinentCount, settings.CloudContinentRadius, settings.CloudBoost);

            AddContinents(heightMap, settings.CloudContinentCount, settings.CloudContinentRadius, settings.CloudBoost*-1);

            for (int i = 0; i < settings.CloudSmoothHeightMap; i++)
            {
                heightMap = SmoothHeightMap(heightMap);
            }

            List<Color> colors = Settings.GetEarthCloudColors();

            GenerateColorMap(heightMap, image, colors, settings.ExpandColors);

            for (int i = 0; i < settings.CloudSmoothAmount; i++)
            {
                SmoothColors(image);
            }

            return image;
        }

        private static void AddContinents(int[,] heightMap, int cloudContinentCount, int cloudContinentRadius, object cloudBoost)
        {
            throw new NotImplementedException();
        }

        public static Bitmap GenerateAtmosphere(int resolution, Color color)
        {
            Bitmap bmp = new Bitmap(resolution, resolution);
            SolidBrush colorBrush = new SolidBrush(color);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle rectangle = new Rectangle(0, 0, resolution, resolution);
                graph.FillEllipse(colorBrush, rectangle);
            }
            return bmp;
        }

        public static Bitmap GeneratePlanet(Settings settings)
        {
            var image = GetTemplateBitmapTransparent(settings.Resolution, settings.Resolution);
            var heightMap = GenerateHeightMapDiamondSquareAlgo(settings.Resolution);

            AddContinents(heightMap, settings.ContinentCount, settings.ContinentRadius, settings.ContinentBoost);

            AddContinents(heightMap, settings.ContinentCount, settings.ContinentRadius, -1 * settings.ContinentBoost);

            for (int i = 0; i < settings.SmoothHeightMapAmount; i++)
            {
                heightMap = SmoothHeightMap(heightMap);
            }

            List<Color> colors = settings.PlanetColors;

            GenerateColorMap(heightMap, image, colors, settings.ExpandColors);

            for (int i = 0; i < settings.SmoothTextureAmount; i++)
            {
                SmoothColors(image);
            }

            Bitmap planet;

            if (settings.GenerateClouds)
            {
                planet = OverlayImage(image, GenerateClouds(settings), 0, 0, 0.75f);
            }

            planet = CutOutHole(image, settings.Resolution);

            // make final canvas that is larger by the thickness of the atmosphere
            var canvas = GetTemplateBitmapTransparent(settings.Resolution + (settings.AtmosphereThickness * 2), settings.Resolution + (settings.AtmosphereThickness * 2));
            var atmosphere = GenerateAtmosphere(settings.Resolution + settings.AtmosphereThickness, Color.CornflowerBlue);

            // place planet on canvas
            var canvasAndPlanet = OverlayImage(canvas, planet, settings.AtmosphereThickness / 2, settings.AtmosphereThickness / 2, 1.0f);

            // place atmosphere on canvas, over the planet
            return OverlayImage(atmosphere, canvasAndPlanet, 0, 0, 1.0f);
        }

        private static Bitmap CutOutHole(Bitmap image, int resolution)
        {
            Bitmap finalImage = GetTemplateBitmapTransparent(resolution, resolution);

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    if (GetDistance(x, y, resolution / 2, resolution / 2) < resolution / 2)
                    {
                        finalImage.SetPixel(x, y, image.GetPixel(x, y));
                    }
                }
            }

            return finalImage;
        }

        private static int[,] MergeHeightMaps(int[,] noiseHeightMap, int[,] heightMap)
        {
            var newMap = new int[noiseHeightMap.GetLength(0), noiseHeightMap.GetLength(1)];
            for (int x = 0; x < noiseHeightMap.GetLength(0); x++)
            {
                for (int y = 0; y < noiseHeightMap.GetLength(1); y++)
                {
                    newMap[x, y] = (noiseHeightMap[x, y] + heightMap[x, y]) / 2;
                }
            }
            return newMap;
        }

        /// <summary>
        /// Special thinks to: https://stackoverflow.com/questions/33284841/place-image-over-image-in-c-sharp-using-specific-alpha-transparency-level
        /// </summary>
        private static Bitmap OverlayImage(Bitmap background, Bitmap overlay, int x, int y, float alpha)
        {
            using (Graphics graphics = Graphics.FromImage(background))
            {
                var cm = new ColorMatrix();
                cm.Matrix33 = alpha;

                var ia = new ImageAttributes();
                ia.SetColorMatrix(cm);

                graphics.DrawImage(
                    overlay,
                    // target
                    new Rectangle(x, y, overlay.Width, overlay.Height),
                    // source
                    0, 0, overlay.Width, overlay.Height,
                    GraphicsUnit.Pixel,
                    ia);
            }

            return background;
        }

        private static Bitmap GetTemplateBitmapTransparent(int width, int height)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle rectangle = new Rectangle(0, 0, width, height);
                graph.FillRectangle(Brushes.Transparent, rectangle);
            }
            return bmp;
        }

        private static void AddContinents(int[,] heightMap, int amount, int radius, double heightAdjust)
        {
            var circles = new List<(int, int)>();
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < amount; i++)
            {
                var x = r.Next(0, heightMap.GetLength(0) - 1);
                var y = r.Next(0, heightMap.GetLength(1) - 1);
                circles.Add((x, y));
            }

            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    foreach (var point in circles)
                    {
                        if (GetDistance(x, y, point.Item1, point.Item2) < radius)
                        {
                            var multiplier = 1.0 / radius;
                            var distance = GetDistance(x, y, point.Item1, point.Item2);
                            var finalMultiplier = 1.0 - multiplier;
                            heightMap[x, y] += (int)(heightAdjust * finalMultiplier);
                        }
                    }
                }
            }
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        private static void AddShadeNoise(Bitmap scaledUp, int amount)
        {
            Random r = new Random();
            for (int x = 0; x < scaledUp.Width; x++)
            {
                for (int y = 0; y < scaledUp.Height; y++)
                {
                    try
                    {
                        var pixel = scaledUp.GetPixel(x, y);
                        var ran1 = r.Next(-1 * amount, amount);
                        var ran2 = r.Next(-1 * amount, amount);
                        var ran3 = r.Next(-1 * amount, amount);
                        scaledUp.SetPixel(x, y, Color.FromArgb(pixel.R + ran1, pixel.G + ran2, pixel.B + ran3));
                    }
                    catch { }
                }
            }
        }

        private static Bitmap SmartScale(Bitmap original, int scale, int fill = 50)
        {
            var template = GetTemplateBitmapTransparent(original.Width * scale, original.Height * scale);
            var scaleAdjust = scale + 3;

            Random r = new Random(DateTime.Now.Millisecond);
            using (Graphics graph = Graphics.FromImage(template))
            {
                for (int x = 0; x < original.Width; x++)
                {
                    for (int y = 0; y < original.Height; y++)
                    {
                        var pixel = original.GetPixel(x, y);
                        using (SolidBrush brush = new SolidBrush(pixel))
                        {
                            // Rectangle rectangle = new Rectangle(x* scaleAdjust, y* scaleAdjust, scaleAdjust, scaleAdjust);
                            // graph.FillRectangle(brush, rectangle);
                            for (int i = 0; i < fill; i++)
                            {
                                Rectangle rectangle = new Rectangle((x * scaleAdjust) + r.Next(-scaleAdjust, scaleAdjust), (y * scaleAdjust) + r.Next(-scaleAdjust, scaleAdjust), r.Next(1, scaleAdjust), r.Next(1, scaleAdjust));
                                graph.FillEllipse(brush, rectangle);
                            }
                        }
                    }
                }
            }

            return template;
        }

        private static int[,] RaiseHeightMap(int[,] heightMap, int amount)
        {
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    heightMap[x, y] += amount;
                    heightMap[x, y] = Math.Min(255 * 3, heightMap[x, y]);
                }
            }

            return heightMap;
        }

        private static void SmoothColors(Bitmap map)
        {
            for (int x = 1; x < map.Width - 1; x++)
            {
                for (int y = 1; y < map.Height - 1; y++)
                {
                    var value = map.GetPixel(x, y);
                    var up = map.GetPixel(x, y + 1);
                    var down = map.GetPixel(x, y - 1);
                    var left = map.GetPixel(x - 1, y);
                    var right = map.GetPixel(x + 1, y);
                    var smoothed = MergePixels(right, MergePixels(up, MergePixels(MergePixels(down, value), left)));
                    map.SetPixel(x, y, smoothed);
                }
            }
        }

        private static List<List<Color>> ExpandColors(List<Color> colors)
        {
            var newColors = new List<List<Color>>();

            for (int i = 0; i < colors.Count; i++)
            {
                var list = new List<Color>();
                newColors.Add(list);
                list.Add(colors[i]);

                for (int x = 0; x < 50; x++)
                {
                    list.Add(RandomColorAdjust(colors[i]));
                }    
            }

            return newColors;
        }

        private static Color RandomColorAdjust(Color color)
        {
            try
            {
                Random r = new Random(DateTime.Now.Millisecond);
                int min = -15;
                int max = (min*-1)+1;
                return Color.FromArgb(color.A, color.R + r.Next(min,max), color.G + r.Next(min, max), color.B + r.Next(min, max));
            }
            catch(Exception) 
            {
                return color;
            }
        }

        private static void GenerateColorMap(int[,] heightMap, Bitmap origin, List<Color> colors, int colorExpansionAmount)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            var rangedColors = new List<List<Color>>();
            for (int i = 0; i < colorExpansionAmount; i++)
            {
                rangedColors = ExpandColors(colors);
            }

            for (int x = 0; x < origin.Width; x++)
            {
                for (int y = 0; y < origin.Height; y++)
                {
                    double divisor = (255.0 * 3.0) / (colors.Count-1);
                    var total = heightMap[x, y];
                    total = Math.Max(1, total);
                    var selectPosition = (int)(total / divisor) - 1;
                    selectPosition = Math.Max(0, selectPosition);
                    selectPosition = Math.Min(rangedColors.Count - 1, selectPosition);
                    var color = rangedColors[selectPosition][r.Next(0, rangedColors[selectPosition].Count-1)];
                    origin.SetPixel(x, y, color);
                }
            }
        }

        private static int[,] SmoothHeightMap(int[,] map)
        {
            var newMap = new int[map.GetLength(0), map.GetLength(1)];
            for (int x = 1; x < map.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < map.GetLength(1) - 1; y++)
                {
                    var value = map[x, y];
                    var up = map[x, y + 1];
                    var down = map[x, y - 1];
                    var left = map[x - 1, y];
                    var right = map[x + 1, y];
                    var smoothed = (up + down + left + right + value) / 5;
                    newMap[x, y] = smoothed;
                }
            }

            return newMap;
        }

        //https://www.youtube.com/watch?v=4GuAV1PnurU
        private static int[,] GenerateHeightMapDiamondSquareAlgo(int width)
        {
            var heightMap = new int[width, width];

            // set random values 4 corners between 0 and 255*3
            Random r = new Random(DateTime.Now.Millisecond);
            heightMap[0, 0] = r.Next(0, 255 * 3);
            heightMap[0, width - 1] = r.Next(0, 255 * 3);
            heightMap[width - 1, 0] = r.Next(0, 255 * 3);
            heightMap[width - 1, width - 1] = r.Next(0, 255 * 3);

            var chunkSize = width - 1;
            var roughness = 2.0;

            while (chunkSize > 1)
            {
                SquareStep(heightMap, width, chunkSize);
                DiamondStep(heightMap, width, chunkSize);
                chunkSize /= 2;
                roughness /= 2.0;
            }

            return heightMap;
        }

        private static void SquareStep(int[,] heightMap, int width, int chunkSize)
        {
            var half = chunkSize / 2;
            var random = new Random(DateTime.Now.Millisecond);
            for (int y = 0; y < width - 1; y += chunkSize)
            {
                for (int x = 0; x < width - 1; x += chunkSize)
                {
                    // check if we're done...
                    if (y + half > width - 1 || x + half > width - 1)
                    {
                        return;
                    }

                    var values = GetSquareValues(heightMap, chunkSize, y, x);
                    var value = values.Sum() / values.Count;
                    heightMap[y + half, x + half] = value + random.Next(-255 * 3, 255 * 3);
                }
            }
        }

        private static void DiamondStep(int[,] heightMap, int width, int chunkSize)
        {
            var half = chunkSize / 2;
            var random = new Random(DateTime.Now.Millisecond);
            for (int y = 0; y < width - 1; y += half)
            {
                for (int x = (y + half) % chunkSize; x < width - 1; x += chunkSize)
                {
                    var values = GetDiamondValues(heightMap, half, y, x);
                    var value = values.Sum() / values.Count;
                    heightMap[y, x] = value + random.Next(-255 * 3, 255 * 3);
                }
            }
        }

        private static List<int> GetSquareValues(int[,] heightMap, int chunkSize, int y, int x)
        {
            var list = new List<int>();
            try
            {
                list.Add(heightMap[y, x]);
            }
            catch { /* Just eat it... */ }

            try
            {
                list.Add(heightMap[y, x + chunkSize]);
            }
            catch { /* Just eat it... */ }

            try
            {
                list.Add(heightMap[y + chunkSize, x]);
            }
            catch { /* Just eat it... */ }

            try
            {
                list.Add(heightMap[y + chunkSize, x + chunkSize]);
            }
            catch { /* Just eat it... */ }

            return list;
        }

        private static List<int> GetDiamondValues(int[,] heightMap, int half, int y, int x)
        {
            var list = new List<int>();

            try
            {
                list.Add(heightMap[y - half, x]);
            }
            catch { /* Just eat it... */ }

            try
            {
                list.Add(heightMap[y, x - half]);
            }
            catch { /* Just eat it... */ }

            try
            {
                list.Add(heightMap[y, x + half]);
            }
            catch { /* Just eat it... */ }

            try
            {
                list.Add(heightMap[y + half, x]);
            }
            catch { /* Just eat it... */ }

            return list;
        }

        private static Bitmap MergeMaps(Bitmap a, Bitmap b)
        {
            Random r = new Random();
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    var pixelA = a.GetPixel(x, y);
                    var pixelB = b.GetPixel(x, y);

                    if (r.NextDouble() < 0.1)
                    {
                        a.SetPixel(x, y, MergePixels(pixelA, pixelB));
                    }
                }
            }

            return a;
        }

        private static Color MergePixels(Color first, Color second)
        {
            var r = (first.R + second.R) / 2;
            var g = (first.G + second.G) / 2; ;
            var b = (first.B + second.B) / 2; ;
            var a = (first.A + second.A) / 2; ;

            return Color.FromArgb(a, r, g, b);
        }
    }
}
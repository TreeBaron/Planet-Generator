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

            List<Color> colors = settings.CloudColors;

            GenerateColorMap(heightMap, image, settings, settings.CloudColors);

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

        public static Bitmap GenerateAtmosphere(int resolution, Color color, Settings settings)
        {
            Bitmap bmp = new Bitmap(resolution, resolution);
            SolidBrush colorBrush = new SolidBrush(color);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle rectangle = new Rectangle(0, 0, resolution, resolution);
                graph.FillEllipse(colorBrush, rectangle);
            }
            bmp = MakeSemiTransparent(settings, bmp);
            return CutOutHole(bmp, resolution);
        }

        public static Bitmap MakeSemiTransparent(Settings settings, Bitmap origin)
        {
            for(int y = 0; y < origin.Height; y++)
            {
                for(int x = 0; x < origin.Width; x++)
                {
                    var pixel = origin.GetPixel(x, y);
                    var newPixel = Color.FromArgb((int)(settings.AtmosphereTransparency * 255f), pixel.R, pixel.G, pixel.B);
                    origin.SetPixel(x, y, newPixel);
                }
            }

            return origin;
        }

        public static Bitmap GeneratePlanet(Settings settings)
        {
            var image = GetTemplateBitmapTransparent(settings.Resolution, settings.Resolution);
            var heightMap = GenerateHeightMapDiamondSquareAlgo(settings.Resolution);

            AddContinents(heightMap, settings.ContinentCount, settings.ContinentRadius, settings.ContinentBoost);

            AddContinents(heightMap, settings.ContinentCount, settings.ContinentRadius, settings.ContinentHinder);

            heightMap = RaiseHeightMap(heightMap, settings.RaiseAllLandAmount);


            for (int i = 0; i < settings.SmoothHeightMapAmount; i++)
            {
                heightMap = SmoothHeightMap(heightMap);
            }

            if (settings.AddCraters)
            {
                AddCraters(settings, heightMap);
            }

            GenerateColorMap(heightMap, image, settings, settings.PlanetColors);

            for (int i = 0; i < settings.SmoothTextureAmount; i++)
            {
                SmoothColors(image);
            }

            Bitmap planet;

            if (settings.GenerateClouds)
            {
                planet = OverlayImage(image, GenerateClouds(settings), 0, 0, settings.CloudTransparency);
            }

            if (settings.Upscale)
            {
                var upscaled = SmartScale(image, 4);
                settings.Resolution *= 4;

                for (int i = 0; i < 2; i++)
                {
                    SmoothColors(upscaled);
                }

                image = upscaled;
            }

            planet = CutOutHole(image, settings.Resolution);

            // make final canvas that is larger by the thickness of the atmosphere
            var canvas = GetTemplateBitmapTransparent(settings.Resolution + (settings.AtmosphereThickness * 2), settings.Resolution + (settings.AtmosphereThickness * 2));
            var atmosphere = GenerateAtmosphere(settings.Resolution + settings.AtmosphereThickness, settings.AtmosphereColor, settings);

            // place planet on canvas
            var canvasAndPlanet = OverlayImage(canvas, planet, settings.AtmosphereThickness / 2, settings.AtmosphereThickness / 2, 1.0f);

            // place atmosphere on canvas, over the planet
            var final =  OverlayImage(atmosphere, canvasAndPlanet, 0, 0, 1.0f);

            return final;
        }

        private static void AddCraters(Settings settings, int[,] heightMap)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            for(int i = 0; i < settings.CraterCount; i++)
            {
                var outterRadius = r.Next(10, 30);
                var innerRadius = outterRadius - (outterRadius / 10);
                var posX = r.Next(0, heightMap.GetLength(0)-1);
                var posY = r.Next(0, heightMap.GetLength(1)-1);

                AddDepression(heightMap, outterRadius, settings.CraterDepth, posX, posY);
                AddDepression(heightMap, innerRadius, settings.CraterDepth*-2, posX, posY);
            }

            for (int i = 0; i < settings.CraterCount; i++)
            {
                var outterRadius = r.Next(2, 9);
                var innerRadius = outterRadius - (outterRadius / 5);
                var posX = r.Next(0, heightMap.GetLength(0) - 1);
                var posY = r.Next(0, heightMap.GetLength(1) - 1);

                AddDepression(heightMap, outterRadius, settings.CraterDepth, posX, posY);
                AddDepression(heightMap, innerRadius, settings.CraterDepth * -2, posX, posY);
            }
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

        private static void AddDepression(int[,] heightMap, int radius, int heightAdjust, int xPos, int yPos)
        {
            Random r = new Random(DateTime.Now.Millisecond);

            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    if (GetDistance(x, y, xPos, yPos) < radius)
                    {
                        if (r.NextDouble() < 0.9)
                        {
                            var multiplier = 1.0 / radius;
                            var distance = GetDistance(x, y, xPos, yPos);
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
            var template = new Bitmap(original, new Size(original.Width*scale, original.Height*scale));

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

        private static List<Color> ExpandColors(List<Color> colors)
        {
            var newColors = new List<Color>();
            for (int i = 1; i < colors.Count; i++)
            {
                newColors.Add(colors[i-1]);
                newColors.Add(MergePixels(colors[i - 1], colors[i]));
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

        private static void GenerateColorMap(int[,] heightMap, Bitmap origin, Settings settings, List<Color> colors)
        {
            for (int i = 0; i < settings.ExpandColors; i++)
            {
                colors = ExpandColors(colors);
            }

            for (int x = 0; x < origin.Width; x++)
            {
                for (int y = 0; y < origin.Height; y++)
                {
                    var divisor = (255 * 3) / colors.Count;
                    var total = heightMap[x, y];
                    total = Math.Max(1, total);
                    var selectPosition = (total / divisor) - 1;
                    selectPosition = Math.Max(0, selectPosition);
                    selectPosition = Math.Min(colors.Count - 1, selectPosition);
                    var color = colors[selectPosition];
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
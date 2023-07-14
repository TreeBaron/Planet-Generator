using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Planet_Generator
{
    class TextureGen
    {
        private Random random = new Random(DateTime.Now.Millisecond);

        private static int PatternWidth = 3;

        public static int Passes = 12;

        public static double BlackPercentage = 0.5;

        public static int LandRaiseAmount = 0;

        public static int ContinentHeightAdjust = 500;

        public static int ContinentRadius = 20;

        public static int ContinentCount = 1;

        public static int Scale = 4;

        public static Bitmap GenerateClouds(int resolution)
        {
            var image = GetTemplateBitmapTransparent(resolution, resolution);
            var heightMap = GenerateHeightMapDiamondSquareAlgo(resolution);


            AddContinents(heightMap, 12, resolution / 3, 200);

            AddContinents(heightMap, 12, resolution / 3, -200);

            for (int i = 0; i < 12; i++)
            {
                heightMap = SmoothHeightMap(heightMap);
            }

            List<Color> colors = new List<Color>()
            {
                Color.Transparent,
                Color.White,
                Color.LightGray,
            };

            GenerateColorMap(heightMap, image, colors);

            for (int i = 0; i < 5; i++)
            {
                SmoothColors(image);
            }

            return image;
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

        public static Bitmap GeneratePlanet(int resolution, int atmosphereThickness)
        {
            var image = GetTemplateBitmapTransparent(resolution, resolution);
            var heightMap = GenerateHeightMapDiamondSquareAlgo(resolution);


            AddContinents(heightMap, 12, resolution / 3, 200);

            AddContinents(heightMap, 12, resolution / 3, -200);

            for (int i = 0; i < 12; i++)
            {
               heightMap = SmoothHeightMap(heightMap);
            }

            List<Color> colors = new List<Color>()
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

            GenerateColorMap(heightMap, image, colors, 4);

            for (int i = 0; i < 5; i++)
            {
                SmoothColors(image);
            }

            var planet = OverlayImage(image, GenerateClouds(resolution), 0, 0, 0.75f);

            planet = CutOutHole(image, resolution);

            // make final canvas that is larger by the thickness of the atmosphere
            var canvas = GetTemplateBitmapTransparent(resolution + (atmosphereThickness*2), resolution + (atmosphereThickness*2));
            var atmosphere = GenerateAtmosphere(resolution + atmosphereThickness, Color.CornflowerBlue);

            // place planet on canvas
            var canvasAndPlanet = OverlayImage(canvas, planet, atmosphereThickness / 2, atmosphereThickness / 2, 1.0f);

            // place atmosphere on canvas, over the planet
            return OverlayImage(atmosphere, canvasAndPlanet, 0, 0, 1.0f);

        }

        private static Bitmap CutOutHole(Bitmap image, int resolution)
        {
            Bitmap finalImage = GetTemplateBitmapTransparent(resolution, resolution);

            for(int y = 0; y < resolution; y++)
            {
                for(int x = 0; x < resolution; x++)
                {
                    if(GetDistance(x, y, resolution / 2, resolution / 2) < resolution / 2)
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
            for(int x = 0; x < noiseHeightMap.GetLength(0); x++)
            {
                for(int y = 0; y < noiseHeightMap.GetLength(1); y++)
                {
                    newMap[x,y] = (noiseHeightMap[x, y] + heightMap[x, y]) / 2;
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

        public static void MakeTransparent(int alpha, Bitmap bitmap)
        {
            for(int x = 0; x < bitmap.Width; x++)
            {
                for(int y = 0; y < bitmap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    var newPixel = Color.FromArgb(alpha, pixel.R, pixel.G, pixel.B);
                    bitmap.SetPixel(x, y, newPixel);
                }
            }
        }

        private static Bitmap GetTemplateBitmap(int width, int height)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle rectangle = new Rectangle(0, 0, width, height);
                graph.FillRectangle(Brushes.Black, rectangle);
            }
            return bmp;
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
                var x = r.Next(0, heightMap.GetLength(0)-1);
                var y = r.Next(0, heightMap.GetLength(1)-1);
                circles.Add((x, y));
            }

            for(int x = 0; x < heightMap.GetLength(0); x++)
            {
                for(int y = 0; y < heightMap.GetLength(1); y++)
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

        private static Bitmap GenerateNoise(Bitmap origin, Color a, Color b, double aColorPercentage)
        {
            var result = new Bitmap(origin);
            Random r = new Random(DateTime.Now.Millisecond);
            for(int y = 0; y < result.Height; y++)
            {
                for(int x = 0; x < result.Width; x++)
                {
                    if (r.NextDouble() < aColorPercentage)
                    {
                        result.SetPixel(x, y, a);
                    }
                    else
                    {
                        result.SetPixel(x, y, b);
                    }
                }
            }

            return result;
        }

        private static void AddShadeNoise(Bitmap scaledUp, int amount)
        {
            Random r = new Random();
            for(int x = 0; x < scaledUp.Width; x++)
            {
                for(int y = 0; y < scaledUp.Height; y++)
                {
                    try
                    {
                        var pixel = scaledUp.GetPixel(x, y);
                        var ran1 = r.Next(-1 * amount, amount);
                        var ran2 = r.Next(-1 * amount, amount);
                        var ran3 = r.Next(-1 * amount, amount);
                        scaledUp.SetPixel(x, y, Color.FromArgb(pixel.R + ran1, pixel.G + ran2, pixel.B + ran3));
                    } catch {}
                }
            }
        }

        private static Bitmap SmartScaleClouds(Bitmap original, int scale, int fill = 50)
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
                                Rectangle rectangle = new Rectangle((x * scaleAdjust) + r.Next(-scaleAdjust, scaleAdjust), (y * scaleAdjust) + r.Next(-scaleAdjust*5, scaleAdjust*5), r.Next(1, scaleAdjust*5), r.Next(1, scaleAdjust*5));
                                graph.FillEllipse(brush, rectangle);
                            }
                        }
                    }
                }
            }

            return template;
        }


        private static Bitmap SmartScale(Bitmap original, int scale, int fill = 50)
        {
            var template = GetTemplateBitmapTransparent(original.Width * scale, original.Height* scale);
            var scaleAdjust = scale + 3;

            Random r = new Random(DateTime.Now.Millisecond);
            using (Graphics graph = Graphics.FromImage(template))
            {
                for(int x = 0; x < original.Width; x++)
                {
                    for(int y = 0; y < original.Height; y++)
                    {
                        var pixel = original.GetPixel(x, y);
                        using (SolidBrush brush = new SolidBrush(pixel))
                        {
                            // Rectangle rectangle = new Rectangle(x* scaleAdjust, y* scaleAdjust, scaleAdjust, scaleAdjust);
                            // graph.FillRectangle(brush, rectangle);
                            for (int i = 0; i < fill; i++)
                            {
                                Rectangle rectangle = new Rectangle((x*scaleAdjust) + r.Next(-scaleAdjust, scaleAdjust), (y*scaleAdjust) + r.Next(-scaleAdjust, scaleAdjust), r.Next(1, scaleAdjust), r.Next(1, scaleAdjust));
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
            for(int x = 0; x < heightMap.GetLength(0); x++)
            {
                for(int y = 0; y < heightMap.GetLength(1); y++)
                {
                    heightMap[x, y] += amount;
                    heightMap[x, y] = Math.Min(255*3, heightMap[x, y]);
                }
            }

            return heightMap;
        }

        private static void SmoothColors(Bitmap map)
        {
            for (int x = 1; x < map.Width-1; x++)
            {
                for (int y = 1; y < map.Height-1; y++)
                {
                    var value = map.GetPixel(x, y);
                    var up = map.GetPixel(x, y +1);
                    var down = map.GetPixel(x, y -1);
                    var left = map.GetPixel(x - 1, y);
                    var right = map.GetPixel(x + 1, y);
                    var smoothed = MergePixels(right,MergePixels(up, MergePixels(MergePixels(down, value), left)));
                    map.SetPixel(x, y, smoothed);
                }
            }
        }

        private static List<Color> ExpandColors(List<Color> colors)
        {
            var newColors = new List<Color>();

            for(int i = 1; i < colors.Count; i++)
            {
                newColors.Add(colors[i-1]);
                newColors.Add(MergePixels(colors[i - 1], colors[i]));
            }

            return newColors;
        }

        private static void GenerateColorMap(int[,] heightMap, Bitmap origin, List<Color> colors, int colorExpansionAmount = 1)
        {
            
            for(int i = 0; i < colorExpansionAmount; i++)
            {
                colors = ExpandColors(colors);
            }

            for (int x = 0; x < origin.Width; x++)
            {
                for (int y = 0; y < origin.Height; y++)
                {
                    var divisor = (255*3) / colors.Count;
                    var total = heightMap[x,y];
                    total = Math.Max(1, total);
                    var selectPosition = (total / divisor) - 1;
                    selectPosition = Math.Max(0, selectPosition);
                    selectPosition = Math.Min(colors.Count-1, selectPosition);
                    var color = colors[selectPosition];
                    origin.SetPixel(x, y, color);
                }
            }
        }

        private static int[,] ToHeightMap(Bitmap bitmap)
        {
            var array = new int[bitmap.Width, bitmap.Height];
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    var height = pixel.R + pixel.G + pixel.B;
                    array[x, y] = height;
                }
            }
            return array;
        }

        private static int[,] SmoothHeightMap(int[,] map)
        {
            var newMap = new int[map.GetLength(0),map.GetLength(1)];
            for(int x = 1; x < map.GetLength(0) - 1; x++)
            {
                for(int y = 1; y < map.GetLength(1) - 1; y++)
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
            heightMap[width-1, 0] = r.Next(0, 255 * 3);
            heightMap[width-1, width - 1] = r.Next(0, 255 * 3);

            var chunkSize = width - 1;
            var roughness = 2.0;

            while(chunkSize > 1)
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
                    if(y + half > width -1 || x + half > width - 1)
                    {
                        return;
                    }

                    var values = GetSquareValues(heightMap, chunkSize, y, x);
                    var value = values.Sum() / values.Count;
                    heightMap[y + half, x + half] = value + random.Next(-255*3, 255*3);
                }
            }
        }

        private static void DiamondStep(int[,] heightMap, int width, int chunkSize)
        {
            var half = chunkSize / 2;
            var random = new Random(DateTime.Now.Millisecond);
            for (int y = 0; y < width - 1; y += half)
            {
                for (int x = (y+half) % chunkSize; x < width - 1; x += chunkSize)
                {
                    var values = GetDiamondValues(heightMap, half, y, x);
                    var value = values.Sum() / values.Count;
                    heightMap[y, x] = value + random.Next(-255*3, 255*3);
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
            } catch { /* Just eat it... */ }

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

        private static void DiamondStep(int[,] heightMap)
        {

        }

        private static int[,] SmoothHeightMapDiamond(int[,] map)
        {
            var newMap = new int[map.GetLength(0), map.GetLength(1)];
            for (int x = 2; x < map.GetLength(0) - 2; x++)
            {
                for (int y = 2; y < map.GetLength(1) - 2; y++)
                {
                    var value = map[x, y];
                    var upleft1 = map[x -1, y+1];
                    var upleft2 = map[x -1, y+1];
                    var upright1 = map[x + 1, y+1];
                    var upright2 = map[x + 1, y+1];
                    var downright1 = map[x + 1, y -1];
                    var downright2 = map[x + 1, y -1];
                    var downleft1 = map[x -1, y -1];
                    var downleft2 = map[x -1, y -1];
                    var smoothed = (value + upleft1 + upleft2 + upright1 + upright2 + downleft1 + downleft2 + downright1 + downright2) / 9;
                    newMap[x, y] = smoothed;
                }
            }

            return newMap;
        }

        private static Bitmap MergeMaps(Bitmap a, Bitmap b)
        {
            Random r = new Random();
            for(int x = 0; x < a.Width; x++)
            {
                for(int y = 0 ; y < a.Height; y++)
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

        private static void ApplyMajorityRulePass(Bitmap origin, Bitmap newMap)
        {
            for (int x = 1; x < origin.Width - 1; x++)
            {
                for (int y = 1; y < origin.Height - 1; y++)
                {
                    // get current section...
                    var section = GetSection(new Rectangle(x-1, y-1, PatternWidth, PatternWidth), origin);

                    MajorityRule(section, newMap, x, y);
                }
            }
        }

        private static void MajorityRule(Bitmap section, Bitmap origin, int xOrigin, int yOrigin)
        {
            int middleX = section.Width / 2;
            int middleY = section.Height / 2;
            int whiteCount = 0;
            int blackCount = 0;
            for (int x = 0; x < section.Width; x++)
            {
                for (int y = 0; y < section.Height; y++)
                {
                    var pixel = section.GetPixel(x, y);
                    if(!(x == middleX && y == middleY))
                    {
                        if(SameColor(Color.White, pixel))
                        {
                            whiteCount++;
                        }

                        if(SameColor(Color.Black, pixel))
                        {
                            blackCount++;
                        }
                    }
                }
            }

            if(blackCount > 4)
            {
                origin.SetPixel(xOrigin, yOrigin, Color.White);
            }
            else
            {
                origin.SetPixel(xOrigin, yOrigin, Color.Black);
            }

        }

        private static void ApplyPatterns(Bitmap origin, Dictionary<Bitmap, Bitmap> patterns)
        {
            for(int x = 0; x < origin.Width - PatternWidth; x++)
            {
                for(int y = 0; y < origin.Height - PatternWidth; y++)
                {
                    // get current section...
                    var section = GetSection(new Rectangle(x, y, PatternWidth, PatternWidth), origin);
                    var match = GetDictionaryMatch(patterns, section);

                    if(match != null)
                    {
                        ApplyPatch(origin, match, x, y);
                    }
                }
            }
        }

        private static void ApplyPatch(Bitmap origin, Bitmap pattern, int x, int y)
        {
            Random r = new Random();
            var xCoords = new List<int>();
            var yCoords = new List<int>();
            for (int i = 0; i < PatternWidth; i++)
            {
                xCoords.Add(x + i);
                yCoords.Add(y + i);
            }

            for(int xPos = 0; xPos < origin.Width; xPos++)
            {
                for(int yPos = 0; yPos < origin.Height; yPos++)
                {
                    if(xCoords.Contains(xPos) && yCoords.Contains(yPos))
                    {
                        var xSelect = xPos - x;
                        var ySelect = yPos - y;
                        var patternPixel = pattern.GetPixel(xSelect, ySelect);

                        if(SameColor(patternPixel, Color.Gray))
                        {
                            if(r.NextDouble() < 0.5)
                            {
                                origin.SetPixel(xPos, yPos, Color.Black);
                            }
                            else
                            {
                                origin.SetPixel(xPos, yPos, Color.White);
                            }
                        }
                        if (!SameColor(patternPixel, Color.Transparent))
                        {
                            origin.SetPixel(xPos, yPos, patternPixel);
                        }
                    }
                }
            }
        }

        private static bool SameColor(Color a, Color b)
        {
            if(a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A)
            {
                return true;
            }
            return false;
        }

        private static Bitmap GetDictionaryMatch(Dictionary<Bitmap, Bitmap> dictionary, Bitmap select)
        {
            foreach(var key in dictionary.Keys)
            {
                if(SameBitmap(key, select))
                {
                    return dictionary[key];
                }
            }

            return null;
        }

        private static bool SameBitmap(Bitmap a, Bitmap b)
        {
            if (a.Width != b.Width || a.Height != b.Height) return false;

            for(int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    var pixelOne = a.GetPixel(x, y);
                    var pixelTwo = b.GetPixel(x, y);

                    if (!SameColor(pixelOne, Color.Transparent))
                    { 
                        if (pixelOne.R != pixelTwo.R || pixelOne.G != pixelTwo.G || pixelOne.B != pixelTwo.B)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static Bitmap GetSection(Rectangle section, Bitmap source)
        {
            return source.Clone(section, source.PixelFormat);
        }

        private static Dictionary<Bitmap, Bitmap> GeneratePatternDictionary()
        {
            var dictionary = new Dictionary<Bitmap, Bitmap>();

            for (int i = 0; i < 3; i++)
            {
               (var item1, var item2) = LoadPatternFromDisk(@"Patterns\Pattern"+(i+1)+".png");
               dictionary.Add(item1, item2);
            }

            return dictionary;
        }

        private static (Bitmap, Bitmap) LoadPatternFromDisk(string filepath)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filepath);
            var bitmap = new Bitmap(path);

            var bitmapLeft = bitmap.Clone(new Rectangle(0,0,PatternWidth, PatternWidth), bitmap.PixelFormat);
            var bitmapRight = bitmap.Clone(new Rectangle(PatternWidth, 0, PatternWidth, PatternWidth), bitmap.PixelFormat);

            return (bitmapLeft, bitmapRight);
        }
    }
}

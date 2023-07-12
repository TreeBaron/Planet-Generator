using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Planet_Generator
{
    class TextureGen
    {
        private Random random = new Random(DateTime.Now.Millisecond);

        private static int PatternWidth = 3;

        public static int Passes = 12;

        public static double BlackPercentage = 0.5;

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

        public static Bitmap GeneratePlanet(int resolution)
        {
            var template = GetTemplateBitmap(resolution, resolution);
            var noise = GenerateNoise(template, Color.Black, Color.White, BlackPercentage);
            var newMap = GetTemplateBitmap(resolution, resolution);

            for (int i = 0; i < Passes; i++)
            {
                Console.WriteLine("Pass " + (i + 1) + " of " + Passes);
                ApplyMajorityRulePass(noise, newMap);
                var swap = noise;
                noise = newMap;
                newMap = swap;
            }

            var islandMap = GetTemplateBitmap(resolution, resolution);
            var finalIslandMap = GenerateNoise(islandMap, Color.Black, Color.White, BlackPercentage);
            var patterns = GeneratePatternDictionary();
            ApplyPatterns(finalIslandMap, patterns);

            var mergedMap = MergeMaps(finalIslandMap, newMap);
            var heightMap = ToHeightMap(mergedMap);

            for(int i = 0; i < 5; i++)
            {
                heightMap = SmoothHeightMap(heightMap);
            }

            GenerateColorMap(heightMap, mergedMap);

            return mergedMap;

        }

        private static void GenerateColorMap(int[,] heightMap, Bitmap origin)
        {
            List<Color> colors = new List<Color>()
            {
                Color.Blue,
                Color.LightBlue,
                Color.SkyBlue,
                Color.Tan,
                Color.DarkKhaki,
                Color.Green,
                Color.YellowGreen,
                Color.LightGray,
                Color.White
            };

            for (int x = 0; x < origin.Width; x++)
            {
                for (int y = 0; y < origin.Height; y++)
                {
                    var divisor = (255*3) / colors.Count;
                    var total = heightMap[x,y];
                    total = Math.Max(1, total);
                    var selectPosition = (total / divisor) - 1;
                    selectPosition = Math.Max(0, selectPosition);
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
            for(int x = 1; x < map.GetLength(0); x++)
            {
                for(int y = 1; y < map.GetLength(1); y++)
                {
                    var value = map[x, y];
                    var lastVal1 = map[x - 1, y -1];
                    var lastVal2 = map[x, y - 1];
                    var lastVal3 = map[x - 1, y];
                    var smoothed = (lastVal1 + lastVal2 + lastVal3 + value) / 4;
                    newMap[x, y] = smoothed;
                }
            }

            return newMap;
        }

        private static Bitmap MergeMaps(Bitmap a, Bitmap b)
        {
            for(int x = 0; x < a.Width; x++)
            {
                for(int y = 0 ; y < a.Height; y++)
                {
                   var pixelA = a.GetPixel(x, y);
                   var pixelB = b.GetPixel(x, y);
                   a.SetPixel(x, y, MergePixels(pixelA, pixelB));
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

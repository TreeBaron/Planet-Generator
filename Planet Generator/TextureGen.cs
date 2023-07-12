using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Planet_Generator
{
    class TextureGen
    {
        private Random random = new Random(DateTime.Now.Millisecond);

        private static int PatternWidth = 3;

        public static int RandomPatternCount = 0;

        public static int Passes = 1;

        public static double BlackPercentage = 0.75;

        private static Bitmap GetTemplateBitmap(int width, int height)
        {
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
            var patternDictionary = GeneratePatternDictionary();

            for(int i = 0; i < Passes; i++)
            {
                foreach (var pattern in patternDictionary)
                {
                    Console.WriteLine("Pass " + (i + 1) + " of " + Passes);
                    ApplyPatterns(noise, new Dictionary<Bitmap, Bitmap>() { { pattern.Key, pattern.Value } });
                }
            }

            return noise;
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

            (var item1, var item2) = GeneratePattern1();
            dictionary.Add(item1, item2);

            var patterns1 = GeneratePatterns();
            var patterns2 = GeneratePatterns();
            foreach(var pattern in patterns1)
            {
                dictionary.Add(pattern, patterns2.First());
                patterns2.RemoveAt(0);
            }

            return dictionary;
        }

        private static (Bitmap, Bitmap) GeneratePattern1()
        {
            var pattern = GetBlankPattern(Color.Black);
            var result = GetBlankPattern(Color.White);

            // pattern match
            pattern.SetPixel(1, 1, Color.White);

            // result
            result.SetPixel(0, 0, Color.Black);
            result.SetPixel(1, 0, Color.White);
            result.SetPixel(2, 0, Color.Black);

            result.SetPixel(0, 1, Color.White);
            result.SetPixel(1, 1, Color.White);
            result.SetPixel(2, 1, Color.White);

            result.SetPixel(0, 2, Color.Black);
            result.SetPixel(1, 2, Color.White);
            result.SetPixel(2, 2, Color.Black);

            return (pattern, result);
        }

        private static Bitmap GetBlankPattern(Color color)
        {
            Bitmap pattern = new Bitmap(PatternWidth, PatternWidth);
            for (int y = 0; y < PatternWidth; y++)
            {
                for (int x = 0; x < PatternWidth; x++)
                {
                    pattern.SetPixel(x, y, color);
                }
            }
            return pattern;
        }

        private static List<Bitmap> GeneratePatterns()
        {
            var patterns = new List<Bitmap>();
            Random r = new Random();
            for (int i = 0; i < RandomPatternCount; i++)
            {
                Bitmap pattern = new Bitmap(PatternWidth, PatternWidth);
                for (int y = 0; y < PatternWidth; y++)
                {
                    for (int x = 0; x < PatternWidth; x++)
                    {
                        if (r.NextDouble() < 0.5)
                        {
                            pattern.SetPixel(x, y, Color.Transparent);
                        }
                        else
                        {
                            if (r.NextDouble() < 0.5)
                            {
                                pattern.SetPixel(x, y, Color.Black);
                            }
                            else
                            {
                                pattern.SetPixel(x, y, Color.White);
                            }
                        }
                    }
                }
                patterns.Add(pattern);
            }

            return patterns;
        }
    }
}

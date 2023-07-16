using System;

namespace Planet_Generator
{
    internal class PlanetNames
    {
        private static string[] A =
        {
            "Ar",
            "Qu",
            "Quo",
            "Xan",
            "Tar",
            "Mel",
            "Melb",
            "Par",
            "Lon",
            "Klin",
            "Clin",
            "Qua",
            "Tarr",
            "Maggle",
            "Zin",
            "Yin",
            "Yang",
            "Post",
            "Po",
            "P",
            "RBX-",
            "Dangery",
            "Doodle",
            "Doo",
            "Foo",
            "Loor",
             "Lor",
             "Data",
             "Plex"
        };

        private static string[] B =
        {
            "ag",
            "tta",
            "all",
            "up",
            "mi",
            "a",
            "e",
            "i",
            "o",
            "u"
        };

        private static string[] C =
        {
            "un",
            "on",
            "ar",
            "ep",
            "en",
            "in",
            "ith",
            "eth",
            "ude",
            "tte",
            "it",
            "an",
            "ex",
            "lip",
            "tar",
            "ol",
            "er",
            "ing",
            "eck",
            "ck",
            "k"
        };

        public static string GetRandomPlanetName()
        {
            Random random = new Random(DateTime.Now.Millisecond);

            var postfix = "";
            int Rando = random.Next(-5, 11);

            //A SWITCH STATEMENT IN THE WILD! :O
            switch (Rando)
            {
                case 1:
                    postfix = " I";
                    break;

                case 2:
                    postfix = " II";
                    break;

                case 3:
                    postfix = " III";
                    break;

                case 4:
                    postfix = " IV";
                    break;

                case 5:
                    postfix = " V";
                    break;

                case 6:
                    postfix = " VI";
                    break;

                case 7:
                    postfix = " VII";
                    break;

                case 8:
                    postfix = " VIII";
                    break;

                case 9:
                    postfix = " IX";
                    break;

                case 10:
                    postfix = " X";
                    break;

                default:
                    postfix = "";
                    break;
            }

            return A[random.Next(0, A.Length)] + B[random.Next(0, B.Length)] + C[random.Next(0, C.Length)] + postfix;
        }
    }
}
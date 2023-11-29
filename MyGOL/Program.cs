using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Globalization;
using System.Resources;

namespace MyGOL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Nyelvi beállítás bekérése
            Console.WriteLine("Nyelv beállítása / Select language: (hu/en)");

            string language = Console.ReadLine();
            switch (language)
            {
                case "hu": language = "hu-HU"; break;
                case "en": language = "en-GB"; break;
            }

            var resourceManager = new ResourceManager("MyGOL.Resources.Messages", typeof(Program).Assembly);

            var cultureInfo = new CultureInfo(language);

            // A GetString metódus segítségével kérjük le az üzenetet a kulcs alapján
            // pl: Console.WriteLine(resourceManager.GetString("MinSzel", cultureInfo));

            //Kezdeti értékek bekérése a felhasználótól
            Console.WriteLine(resourceManager.GetString("Size", cultureInfo)); //Size

            Console.Write(resourceManager.GetString("Width", cultureInfo)); //Width
            string besz = Console.ReadLine();
            int szel = 0;
            if (besz == "")
            {
                szel = 0;
            }
            else
            {
                szel = int.Parse(besz);
            }

            if (szel < 5)
            {
                Console.WriteLine(resourceManager.GetString("MinWidth", cultureInfo)); //MinWidth
                szel = 5;
                Thread.Sleep(4000);
            }
            if (szel > 116)
            {
                Console.WriteLine(resourceManager.GetString("MaxWidth", cultureInfo)); //MaxWidth
                szel = 116;
                Thread.Sleep(4000);
            }

            Console.Write(resourceManager.GetString("Height", cultureInfo)); //Height
            string bem = Console.ReadLine();
            int mag = 0;
            if (bem == "")
            {
                mag = 0;
            }
            else
            {
                mag = int.Parse(bem);
            }

            if (mag < 5)
            {
                Console.WriteLine(resourceManager.GetString("MinHeight", cultureInfo)); //MinHeight
                mag = 5;
                Thread.Sleep(4000);
            }
            if (mag > 35)
            {
                Console.WriteLine(resourceManager.GetString("MaxHeight", cultureInfo)); //MaxHeight
                mag = 35;
                Thread.Sleep(4000);
            }

            Console.Write(resourceManager.GetString("densityCoefficient", cultureInfo)); //densityCoefficient
            string bese = Console.ReadLine();
            int se = 0;
            if (bese == "")
            {
                se = 0;
            }
            else
            {
                se = int.Parse(bese);
            }
            if (se < 2)
            {
                Console.WriteLine(resourceManager.GetString("MinDensityCoefficient", cultureInfo)); //MinDensityCoefficient
                se = 5;
                Thread.Sleep(4000);
            }
            if (se > 20)
            {
                Console.WriteLine(resourceManager.GetString("MaxDensityCoefficient", cultureInfo)); //MaxDensityCoefficient
                se = 5;
                Thread.Sleep(4000);
            }

            Console.Write(resourceManager.GetString("lifeCycle", cultureInfo)); //lifeCycle
            string beEletCiklus = Console.ReadLine();
            int eletCiklus = 0;
            if (beEletCiklus == "")
            {
                eletCiklus = 0;
            }
            else
            {
                eletCiklus = int.Parse(beEletCiklus);
            }
            if (eletCiklus < 2)
            {
                Console.WriteLine(resourceManager.GetString("MinLifeCycle", cultureInfo)); //MinLifeCycle
                eletCiklus = 42;
                Thread.Sleep(4000);
            }
            if (eletCiklus > 100)
            {
                Console.WriteLine(resourceManager.GetString("MaxLifeCycle", cultureInfo));  //MaxLifeCycle
                eletCiklus = 42;
                Thread.Sleep(4000);
            }

            Console.Write(resourceManager.GetString("speed", cultureInfo)); //speed
            string beSebesseg = Console.ReadLine();
            int sebesseg = 0;
            if (beSebesseg == "")
            {
                sebesseg = 0;
            }
            else
            {
                sebesseg = int.Parse(beSebesseg);
            }
            if (sebesseg < 1)
            {
                Console.WriteLine(resourceManager.GetString("MinSpeed", cultureInfo)); //MinSpeed
                sebesseg = 1;
                Thread.Sleep(4000);
            }
            if (sebesseg > 10)
            {
                Console.WriteLine(resourceManager.GetString("MaxSpeed", cultureInfo)); //MaxSpeed
                sebesseg = 10;
                Thread.Sleep(4000);
            }


            //sejtszám +6ározása
            double s = szel * mag;
            //sűrüségi együtt6ó: játéktér területének 1/se-ed mérete
            int sejtSzam = Convert.ToInt32(Math.Floor(s / se));
            //Kezdeti értékek bekérése a felhasználótól - VÉGE

            /*Kezdeti értékek megadása tesztelésre
            int szel = 64;
            int mag = 32;
            int eletCiklus = 420;
            int alvas = 250;
            //Kezdeti értékek megadása tesztelésre - VÉGE
            */

            int alvas = 10 / sebesseg * 100;

            // képernyő méretének és színeinek beállítása
            SetSizeAndColors(szel, mag);

            // keret megrajzolása
            SetFrame(szel, mag);

            int sejtHelyX = (Console.WindowWidth / 2) - (szel / 2);

            //sejtek elhejezése a játéktér tömbjében véletlenszerűen
            int[,] frame = new int[szel + 2, mag + 2];
            for (int i = 0; i < sejtSzam; i++)
            {
                Random rszel = new Random();
                int oszlop = rszel.Next(1, szel + 1);
                Random rmag = new Random();
                int sor = rmag.Next(1, mag + 1);
                frame[oszlop, sor] = 1;
            }

            SaveFrame(frame);

            /*sejtek elhejezése a játéktér tömbjében tervezetten teszteléshez
            //Blinker
            frame[3, 2] = 1;
            frame[3, 3] = 1;
            frame[3, 4] = 1;
            //Toad
            frame[8, 2] = 1;
            frame[8, 3] = 1;
            frame[8, 4] = 1;
            frame[9, 3] = 1;
            frame[9, 4] = 1;
            frame[9, 5] = 1;
            //Beacon
            frame[13, 2] = 1;
            frame[14, 2] = 1;
            frame[13, 3] = 1;
            frame[14, 3] = 1;
            frame[15, 4] = 1;
            frame[15, 5] = 1;
            frame[16, 4] = 1;
            frame[16, 5] = 1;
            //Glider
            frame[4, 7] = 1;
            frame[5, 8] = 1;
            frame[3, 9] = 1;
            frame[4, 9] = 1;
            frame[5, 9] = 1;
            */


            //sejtek kirajzolása
            SejtRajzolas(frame, sejtHelyX);

            //következő ciklus +6ározása
            //SZABÁLYOK:
            //Bármely élő sejt, amelynek két vagy három élő szomszédja van, túléli.
            //Minden elhalt sejt három élő szomszéddal élő sejtté válik.
            //Az összes többi élő sejt elhal a következő generációban. Hasonlóképpen az összes többi elhalt sejt halott marad.

            bool lakott = true;
            bool fagyott = false; //nincs megoldva
            int ec = 0;

            while (lakott && ec < eletCiklus && !fagyott)
            {
                ec++;

                int[,] tempGameBoard = new int[szel + 2, mag + 2];

                for (int j = 1; j < mag + 1; j++)
                {
                    for (int i = 1; i < szel + 1; i++)
                    {
                        int szom = 0;
                        szom = frame[i - 1, j - 1] +
                                frame[i, j - 1] +
                                frame[i + 1, j - 1] +
                                frame[i - 1, j] +
                                frame[i + 1, j] +
                                frame[i - 1, j + 1] +
                                frame[i, j + 1] +
                                frame[i + 1, j + 1];

                        if (frame[i, j] == 1)
                        {
                            if (szom == 2 || szom == 3)
                            {
                                tempGameBoard[i, j] = 1;
                            }
                            else
                            {
                                tempGameBoard[i, j] = 0;
                            }
                        }
                        else
                        {
                            if (szom == 3)
                            {
                                tempGameBoard[i, j] = 1;
                            }
                            else
                            {
                                tempGameBoard[i, j] = 0;
                            }
                        }
                    }
                }

                if (frame == tempGameBoard)
                {
                    fagyott = true;
                }

                frame = tempGameBoard;

                Thread.Sleep(alvas);
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Clear();
                SetSizeAndColors(szel, mag);
                SetFrame(szel, mag);
                SejtRajzolas(frame, sejtHelyX);

                lakott = !kihalt(szel, mag, tempGameBoard);

            }
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(resourceManager.GetString("PressAKey", cultureInfo)); //PressAKey
            Console.ReadKey();
            Console.SetWindowSize(80, 25);
            Console.ResetColor();
            Console.Clear();

            //Statisztika
            if (!lakott)
            {
                Console.WriteLine(resourceManager.GetString("allDeath_1", cultureInfo) + eletCiklus + resourceManager.GetString("allDeath_2", cultureInfo) + sejtSzam + resourceManager.GetString("allDeath_3", cultureInfo)); //allDeath_1, allDeath_2, allDeath_3
                Console.WriteLine(resourceManager.GetString("LifeTime_1", cultureInfo) + ec); //LifeTime_1
            }
            else
            {
                if (fagyott)
                {
                    Console.WriteLine(resourceManager.GetString("LifeTime_2", cultureInfo) + ec); //LifeTime_2
                }

                Console.WriteLine(resourceManager.GetString("startNumber", cultureInfo) + sejtSzam); //startNumber

                int sejtszamVege = 0;
                for (int j = 0; j < frame.GetLength(1); j++)
                {
                    for (int i = 0; i < frame.GetLength(0); i++)
                    {
                        sejtszamVege += frame[i, j];
                    }
                }
                Console.WriteLine(resourceManager.GetString("closeNumber", cultureInfo) + sejtszamVege); //closeNumber
                int db = sejtszamVege - sejtSzam;
                string kulonbseg = resourceManager.GetString("reproduction", cultureInfo) + sejtSzam; //reproduction
                if (db < 0)
                {
                    kulonbseg = resourceManager.GetString("decrease", cultureInfo); //decrease
                }
                Console.WriteLine("A " + kulonbseg + ": " + Math.Abs(db));
            }
        }

        private static bool kihalt(int szel, int mag, int[,] tempGameBoard)
        {
            int sejtszam = 0;

            for (int i = 0; i < szel + 2; i++)
            {
                for (int j = 0; j < mag + 2; j++)
                {
                    sejtszam += tempGameBoard[i, j];
                }
            }

            if (sejtszam == 0)
                return true;

            return false;
        }

        public static void SaveFrame(int[,] frame)
        {
            string path = "frames.txt";

            // a frames.txt fájl létének ellenőrzése
            if (!File.Exists(path))
            {
                // ha nem létezik, létrehozzuk
                string createText = "A már lefuttatott tömbjeink listája";
                File.WriteAllText(path, createText);
            }

            // az egész tömb átalakítása 1db string-é
            string kimenet = "";
            for (int j = 0; j < frame.GetLength(1); j++)
            {
                for (int i = 0; i < frame.GetLength(0); i++)
                {
                    kimenet += frame[i, j].ToString();
                }
            }

            // a dátum és az idő szóközök nélküli hozzáadása
            string appendText = Environment.NewLine + DateTime.Now + "#" + kimenet;
            appendText = appendText.Replace(" ", "");
            File.AppendAllText(path, appendText);

        }

        public static void SejtRajzolas(int[,] frame, int sejtHelyX)
        {
            Console.BackgroundColor = ConsoleColor.Blue;

            for (int i = 0; i < frame.GetLength(0); i++)
            {
                for (int j = 0; j < frame.GetLength(1); j++)
                {
                    if (frame[i, j] == 1)
                    {
                        Console.SetCursorPosition(i + sejtHelyX - 1, j + 1);
                        Console.Write(" ");
                    }
                }
            }

            //Console.SetCursorPosition(frame.GetLength(0),frame.GetLength(1));
            //Console.BackgroundColor = ConsoleColor.Yellow;
            //Console.Write(" ");
        }

        public static void SetFrame(int szel, int mag)
        {
            int keretBal = (Console.WindowWidth / 2) - ((szel + 2) / 2);
            int keretJobb = keretBal + szel + 2;
            int keretFent = 1;
            int keretLent = mag + 2;

            DrawFrame(keretBal, keretJobb, keretFent, keretLent);
        }

        public static void DrawFrame(int keretBal, int keretJobb, int keretFent, int keretLent)
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            for (int j = 0; j < keretJobb - keretBal; j++)
            {
                Console.SetCursorPosition(j + keretBal, keretFent);
                Console.Write(" ");
                Console.SetCursorPosition(j + keretBal, keretLent);
                Console.Write(" ");
            }

            for (int j = 0; j < keretLent - keretFent; j++)
            {
                Console.SetCursorPosition(keretBal, j + 1);
                Console.Write(" ");
                Console.SetCursorPosition(keretJobb - 1, j + 1);
                Console.Write(" ");
            }
        }

        public static void SetSizeAndColors(int szel, int mag)
        {
            Console.SetWindowSize(szel + 4, mag + 4);
            Console.BackgroundColor = ConsoleColor.Gray;

            Console.Clear();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_mintazh
{
    class Program
    {
        static char[,] PalyaGeneralas()
        {
            char[,] palya = new char[15, 30];
            Random r = new Random();

            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == palya.GetLength(0) - 1 || j == palya.GetLength(1) - 1)
                    {
                        palya[i, j] = 'O';
                    }
                    else
                    {
                        int esely = r.Next(1, 101);

                        if (esely <= 80)
                        {
                            // 80%
                            palya[i, j] = '-';
                        }
                        else
                        {
                            // 20%
                            if (r.Next(2) == 1)
                            {
                                palya[i, j] = 'O';
                            }
                            else
                            {
                                palya[i, j] = 'C';
                            }
                        }
                    }
                }
            }

            // pacman elhelyezése
            int x = r.Next(1, palya.GetLength(0) - 2);
            int y = r.Next(1, palya.GetLength(1) - 2);

            palya[x, y] = 'P';

            // ellenség(ek) elhelyezése
            EllensegGeneral(palya);

            return palya;
        }

        static void EllensegGeneral(char[,] palya)
        {
            Random r = new Random();
            int ellensegLeteve = 0;

            while (ellensegLeteve != 30)
            {
                int x = r.Next(1, palya.GetLength(0) - 2);
                int y = r.Next(1, palya.GetLength(1) - 2);

                if (palya[x, y] != 'P' && palya[x, y] != 'E')
                {
                    palya[x, y] = 'E';
                    ellensegLeteve++;
                }
            }
        }

        static void Megjelenit(char[,] palya)
        {
            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    if (palya[i, j] == 'P')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(palya[i, j] + " ");
                        Console.ResetColor();
                    }
                    else if (palya[i, j] == 'C')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(palya[i, j] + " ");
                        Console.ResetColor();
                    }
                    else if (palya[i, j] == 'E')
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(palya[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(palya[i, j] + " ");
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        static char MezoLekerdezes(char[,] palya)
        {
            Console.Write("Add meg <X@Y> formában:\t");
            string bemenet = Console.ReadLine();

            int x = int.Parse(bemenet.Split('@')[0].Split('<')[1]);
            int y = int.Parse(bemenet.Split('@')[1].Split('>')[0]);

            //Console.WriteLine(x);
            //Console.WriteLine(y);

            return palya[x, y];
        }

        static double[] CoinAranySzamSoronkent(char[,] palya)
        {
            double[] eredmeny = new double[palya.GetLength(0)];

            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    if (palya[i, j] == 'C')
                    {
                        eredmeny[i]++;
                    }
                }
                eredmeny[i] = eredmeny[i] / palya.GetLength(1);
            }

            return eredmeny;
        }

        static double[] MaxCoinDarabszam(double[] coinAranySzam)
        {
            // maxkiv
            int max = 0;
            for (int i = 1; i < coinAranySzam.Length; i++)
            {
                if (coinAranySzam[i] > coinAranySzam[max])
                {
                    max = i;
                }
            }

            double[] vissza = new double[2];
            vissza[0] = max; // index
            vissza[1] = coinAranySzam[max]; // érték

            return vissza;
        }

        static int[] EllensegOszloponkent(char[,] palya)
        {
            int[] eredmeny = new int[palya.GetLength(1)];

            for (int i = 0; i < palya.GetLength(1); i++)
            {
                for (int j = 0; j < palya.GetLength(0); j++)
                {
                    if (palya[j, i] == 'E')
                    {
                        eredmeny[i]++;
                    }
                }
            }

            return eredmeny;
        }

        static int[] MaxEllensegSzam(int[] ellensegekOszloponkent, int[] elemekIndexei)
        {
            int maxErtek = ellensegekOszloponkent[0];
            int db = 0;
            elemekIndexei[db] = 0;

            for (int i = 1; i < ellensegekOszloponkent.Length; i++)
            {
                if (maxErtek < ellensegekOszloponkent[i])
                {
                    // van új max érték / elem
                    db = 0;
                    elemekIndexei[db] = i;
                    maxErtek = ellensegekOszloponkent[i];
                }
                else
                {
                    if (maxErtek == ellensegekOszloponkent[i])
                    {
                        // max értékből van egy új
                        elemekIndexei[++db] = i;
                    }
                }
            }

            return new int[] { db, maxErtek };
        }

        static void RendezesCsokkenobe(double[] tomb)
        {
            // buborék rendezés
            for (int i = tomb.Length; i >= 1; i--)
            {
                for (int j = 0; j < i-1; j++)
                {
                    if (tomb[j] < tomb[j+1])
                    {
                        // csere
                        double temp = tomb[j];
                        tomb[j] = tomb[j + 1];
                        tomb[j + 1] = temp;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            // -----------------------------------------------------------------------------
            // 1. FELADAT
            char[,] palya = PalyaGeneralas();


            // -----------------------------------------------------------------------------
            // 2. FELADAT
            Megjelenit(palya);

            // -----------------------------------------------------------------------------
            // 3. FELADAT
            // palyageneralas metódusban van meghívva!

            // -----------------------------------------------------------------------------
            // 4. FELADAT
            //char karakter = MezoLekerdezes(palya);
            //Console.WriteLine("A lekérdezett karakter: " + karakter);

            // -----------------------------------------------------------------------------
            // 5. FELADAT
            double[] coinArany = CoinAranySzamSoronkent(palya);

            for (int i = 0; i < coinArany.Length; i++)
            {
                Console.WriteLine("--" + coinArany[i]);
            }

            // -----------------------------------------------------------------------------
            // 6. FELADAT
            double[] maxCoin = MaxCoinDarabszam(coinArany);

            Console.WriteLine("A max coin-arány indexe: " + maxCoin[0]);
            Console.WriteLine("A max coin-arány értéke: " + maxCoin[1]);

            // -----------------------------------------------------------------------------
            // 7. FELADAT
            int[] ellensegOszloponkent = EllensegOszloponkent(palya);

            // -----------------------------------------------------------------------------
            // 8. FELADAT
            int[] elemekIndexei = new int[ellensegOszloponkent.Length];
            int[] maxEllErtekek = MaxEllensegSzam(ellensegOszloponkent, elemekIndexei);

            Console.WriteLine("Legtöbb ellenség egy oszlopban: " + maxEllErtekek[1]);
            Console.WriteLine("Helye(k): ({0})" , maxEllErtekek[0]+1 );
            for (int i = 0; i <= maxEllErtekek[0]; i++)
            {
                Console.WriteLine("\toszlop: " + elemekIndexei[i]);
            }

            // -----------------------------------------------------------------------------
            // 9. FELADAT
            Console.WriteLine("\n\nELŐTTE:");
            for (int i = 0; i < coinArany.Length; i++)
            {
                Console.WriteLine("\t" + coinArany[i]);
            }

            RendezesCsokkenobe(coinArany);

            Console.WriteLine("\n\nUTÁNA:");
            for (int i = 0; i < coinArany.Length; i++)
            {
                Console.WriteLine("\t" + coinArany[i]);
            }
        }
    }
}

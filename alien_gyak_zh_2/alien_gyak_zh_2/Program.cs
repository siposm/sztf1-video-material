using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alien_gyak_zh_2
{
    class Program
    {
        static void AlienMegjelenites(Alien[] aliens)
        {
            foreach (Alien a in aliens)
            {
                Console.SetCursorPosition(a.XPoz , a.YPoz);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(a.Jelolo());
                Console.ResetColor();
            }
            Console.SetCursorPosition(0, 55);
        }

        static void Main(string[] args)
        {
            // ----------------------------------------------------------------
            // 1. FELADAT
            Eszkoztar eszkoztar = new Eszkoztar();
            // létrehozáskor lefut az import() és a chestbursterKivalogat()

            // ----------------------------------------------------------------
            // 7. FELADAT
            AlienMegjelenites(eszkoztar.Aliens);

            // ----------------------------------------------------------------
            // 2. FELADAT
            eszkoztar.EmberVisszaallitas();

            // ----------------------------------------------------------------
            // 3. FELADAT
            foreach (Alien x in eszkoztar.KikeltChestbursterKivalogatas())
            {
                if (x != null)
                    Console.WriteLine(x.Tipus + " - " + x.ID);
            }

            // ----------------------------------------------------------------
            // 4. FELADAT
            string kod = eszkoztar.MegoltEmberekKodolas();

            Console.WriteLine("\n"+ kod +"\n");

            // ----------------------------------------------------------------
            // 5. FELADAT
            eszkoztar.Logolas();

            // ----------------------------------------------------------------
            // 6. FELADAT
            int db = eszkoztar.DeaconMegszamolas();
            Console.WriteLine("{0} db deacon van." , db);

            

        }
    }
}

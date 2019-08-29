using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kavezo_rendszer
{
    class Program
    {
        static void KiirSzines(string szoveg, bool ujsorba)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (ujsorba)
            {
                Console.WriteLine(szoveg);
            }
            else
            {
                Console.Write(szoveg);
            }
            Console.ResetColor();
        }

        // (1) Árlista bekérése
        // Egy adatbázis létrehozása ezáltal, amiből később termékeket lehet keresni.
        static string[,] BekerArlista()
        {
            KiirSzines("Árlista feltöltése. Formátum: termék,ár [enter]", true);

            string kimenet = "";
            bool bekeresVege = false;

            while (! bekeresVege)
            {
                string beolv = Console.ReadLine();
                if (beolv == "")
                {
                    bekeresVege = true;
                }
                else
                {
                    kimenet += beolv + "&"; // utolsó elemnél is jelen van !
                }
            }
            
            // kimenet >>> bor,130&almalé,320& ...
            string[] termekek = kimenet.Split('&');
            string[,] termekEsAr = new string[ termekek.Length - 1 , 2 ];
            
            for (int i = 0; i < termekek.Length - 1; i++)
            {
                // termekek[i] >>> bor,130
                termekEsAr[i, 0] = termekek[i].Split(',')[0];
                termekEsAr[i, 1] = termekek[i].Split(',')[1];
            }
            
            return termekEsAr;
        }


        // (2) Napok számának bekérése
        // Felhasználó megadhatja, hogy hány nap adatait szeretné rögzíteni.
        static int BekerNapokSzama()
        {
            KiirSzines("Add meg a napok számát:\t", false);
            return int.Parse(Console.ReadLine());
        }

        // (3) Eladott italok bekérése
        // Adott napi italok felvitele a rendszerbe.
        static string[,] BekerEladottItalok(int napokSzama)
        {
            string[,] tablazat = new string[ napokSzama , 10 ];

            for (int i = 0; i < napokSzama; i++)
            {
                KiirSzines((i+1).ToString() + ". napi ital(ok):" , true);

                int szamlalo = 0;
                bool italokVege = false;

                while ( ! (italokVege || szamlalo == 10) )
                {
                    string bekertItal = Console.ReadLine();
                    if (bekertItal == "")
                    {
                        italokVege = true;
                    }
                    else
                    {
                        tablazat[i, szamlalo] = bekertItal;
                        szamlalo++;
                    }
                }
            }

            //TablazatKiir(tablazat);

            return tablazat;
        }
        static void TablazatKiir(string[,] tablazat)
        {
            for (int i = 0; i < tablazat.GetLength(0); i++)
            {
                for (int j = 0; j < tablazat.GetLength(1); j++)
                {
                    Console.Write(tablazat[i,j] + " - ");
                }
                Console.WriteLine();
            }
        }

        // (4) Lekérdezendő nap bekérése
        // Felhasználó megadhatja, hogy mely nap adatait szeretné lekérdezni
        static int MelyikNap()
        {
            KiirSzines("** NAPI ÖSSZESÍTŐ LEKÉRDEZÉSE **\nMelyik napra vagy kíváncsi?\t",false);
            return int.Parse(Console.ReadLine()) - 1;
        }

        // (5) Eladott italok darabszámának lekérdezése egy napra
        // Felhasználó lekérdezheti, hogy x.napon mennyi italt adott el összesen
        static int ItalokDarabszamPerNap(string[,] eladottItalok, int melyikNap)
        {
            int szamlalo = 0;
            for (int i = 0; i < eladottItalok.GetLength(1); i++)
            {
                if (eladottItalok[melyikNap,i] != null)
                {
                    szamlalo++;
                }
            }
            return szamlalo;
        }

        // (6) Egy nap teljes eladott italjainak listájának lekérdezése
        // Egy kiválasztott nap italjainak kigyűjtése
        static string[] ItalokListajaPerNap(string[,] eladottItalok, int melyikNap)
        {
            string[] tomb = new string[ eladottItalok.GetLength(1) ];
            for (int i = 0; i < eladottItalok.GetLength(1); i++)
            {
                tomb[i] = eladottItalok[melyikNap, i]; // minden belekerül, null-ok is!
            }

            string[] nullmentesTomb = NullokElhagy(tomb);

            return nullmentesTomb;
        }
        static string[] NullokElhagy(string[] tomb)
        {
            
            int db = 0;
            for (int i = 0; i < tomb.Length; i++)
            {
                if (tomb[i] != null)
                {
                    db++;
                }
            }

            string[] tombNullokNelkul = new string[db];
            db = 0;

            for (int i = 0; i < tomb.Length; i++)
            {
                if (tomb[i] != null)
                {
                    tombNullokNelkul[db++] = tomb[i];
                }
            }
            
            return tombNullokNelkul;
        }


        // (7) Italok rendezése
        // Tömbben található italok rendezése név szerint
        static void Rendez(string[] tomb)
        {
            for (int i = 0; i < tomb.Length - 1; i++)
            {
                int min = i;
                for (int j = (i+1); j < tomb.Length; j++)
                {
                    if ( StringKisebbE(tomb[j], tomb[min]) )
                    {
                        min = j;
                    }
                }
                string seged = tomb[i];
                tomb[i] = tomb[min];
                tomb[min] = seged;
            }
        }
        static bool StringKisebbE(string a, string b)
        {
            if (a[0] < b[0])
                return true;
            else
                return false;
        }

        // (8) Ismétlődő italok eltávolítása
        // Ismétlődés mentes tömb létrehozása, amelyben minden elem egyedi
        static string[] IsmetlodoItalokKiszurese(string[] italokAdottNapra , ref int db)
        {
            string[] kivalogatottak = new string[ italokAdottNapra.Length ];
            db = 0;
            kivalogatottak[db] = italokAdottNapra[0];


            for (int i = 0; i < italokAdottNapra.Length; i++)
            {
                if (italokAdottNapra[i] != kivalogatottak[db])
                {
                    // új elem van
                    db++;
                    kivalogatottak[db] = italokAdottNapra[i];
                }
            }
            
            return kivalogatottak;
        }

        // (9) Eladott italok darabszámának lekérdezése, italonként
        // Megszámolni, hogy hány eladás történt az egyes italokból
        static int[] EgyesItalokDarabSzamaPerNap(string[] italokIsmNelkul, string[] italokIsm)
        {
            int[] italokDbSzama = new int[ italokIsmNelkul.Length ];

            for (int i = 0; i < italokIsmNelkul.Length; i++)
            {
                for (int j = 0; j < italokIsm.Length; j++)
                {
                    if ( italokIsmNelkul[i] == italokIsm[j] )
                    {
                        italokDbSzama[i]++;
                    }
                }
            }
            
            return italokDbSzama;
        }

        // (10) Eredmények összesített kiírása
        // Lekérdezett nap összesítő adatainak kiírása(vásárolt italok és azok ára)
        static void KiirEredmeny(string[,] arlista, int limitDb, string[] napiItalokIsmNelkul, int[] dbSzamok)
        {
            int vegosszeg = 0;

            for (int i = 0; i <= limitDb; i++)
            {
                int ar = 0;
                for (int j = 0; j < arlista.GetLength(0); j++)
                {
                    if ( napiItalokIsmNelkul[i] == arlista[j,0] )
                    {
                        ar = int.Parse(arlista[j, 1]);
                    }
                }
                vegosszeg += ar * dbSzamok[i];
                Console.WriteLine("\n\t{0} : {1} db ({2}.-)" , 
                    napiItalokIsmNelkul[i], dbSzamok[i], ar * dbSzamok[i]);
            }
            Console.WriteLine("\n_____________________");
            Console.WriteLine("\t\t\t" + vegosszeg + ".-");
        }

        // (11) Két adathalmaz összefuttatása
        // A már leválogatott italok és egy másik adatforrás összefuttatása
        static string[] Osszefuttatas(string[] tombA)
        {
            string[] tombB = Import();

            Rendez(tombA);
            Rendez(tombB);

            int i = 0;
            int j = 0;
            int db = -1;

            string[] tombY = new string[ tombB.Length + tombA.Length ];

            while ( i < tombA.Length && j < tombB.Length )
            {
                db++;

                if (tombA[i][0] < tombB[j][0])
                {
                    tombY[db] = tombA[i];
                    i++;
                }
                else
                {
                    if ( tombA[i][0] > tombB[j][0] )
                    {
                        tombY[db] = tombB[j];
                        j++;
                    }
                    else
                    {
                        tombY[db] = tombA[i];
                        i++;
                        j++;
                    }
                }
            }

            while (i < tombA.Length)
            {
                tombY[++db] = tombA[i++];
            }

            while (j < tombB.Length)
            {
                tombY[++db] = tombB[j++];
            }

            return NullokElhagy(tombY);
        }

        static string[] Import()
        {
            return new string[]
            {
                "pepsi-zero", "szóda" , "x-sprite" , "almalé"
            };
        }



        static void Main(string[] args)
        {
            // 1. feladat
            string[,] arlista = BekerArlista();

            // 2-3. feladat
            string[,] eladottItalok = BekerEladottItalok(BekerNapokSzama());

            // 4-5. feladat
            int melyikNap = MelyikNap();
            int adottNapiDbSzam = ItalokDarabszamPerNap(eladottItalok, melyikNap);
            Console.WriteLine("A(z) {0}. napon {1} db italt adtak el." , melyikNap + 1, adottNapiDbSzam);

            // 6. feladat
            string[] napiItalok = ItalokListajaPerNap(eladottItalok, melyikNap);

            // 7. feladat
            Rendez(napiItalok);

            // 8. feladat
            int limitDb = 0; // = 8912734;
            string[] napiItalokIsmNelkul = IsmetlodoItalokKiszurese(napiItalok, ref limitDb);

            // 9. feladat
            int[] dbSzamok = EgyesItalokDarabSzamaPerNap(napiItalokIsmNelkul, napiItalok);

            // 10. feladat
            KiirEredmeny(arlista, limitDb, napiItalokIsmNelkul, dbSzamok);

            // 11. feladat
            Console.WriteLine("\n\n\n");
            foreach (string elem in Osszefuttatas(NullokElhagy(napiItalokIsmNelkul)))
            {
                Console.WriteLine(elem);
            }
        }
    }
}

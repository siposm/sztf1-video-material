using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace utazos
{
    class Utazo
    {
        private string nev;
        public string Nev {
            get { return nev; }
            set { nev = value; }
        }

        private string meglatogatottVarosok;
        public string MeglatogatottVarosok
        {
            get { return meglatogatottVarosok; }
            set { meglatogatottVarosok = value; }
        }

        private int eddigiHelyekSzama;
        public int EddigiHelyekSzama
        {
            get { return MeglatogatottVarosok.Split(';').Length - 1; }
            set { eddigiHelyekSzama = value; }
        }

        public Utazo(string nev)
        {
            MeglatogatottVarosok = "";
            EddigiHelyekSzama = 0;
            Nev = nev;
        }

        // ================================================================================================

        // x helyre elutazik
        public void Utazik(string hova)
        {
            MeglatogatottVarosok += hova + ";";
            EddigiHelyekSzama++;
        }

        // ================================================================================================

        // jart-e x helyen
        public bool JartE(string hol)
        {
            bool jarte = false;

            string[] varosok = MeglatogatottVarosok.Split(';');
            int i = 0;
            
            while (i <= varosok.Length-2 && varosok[i] != hol)
                i++;

            if (i <= varosok.Length - 2 )
                jarte = true;

            return jarte;
        }

        // ================================================================================================

        // összesen hány helyen járt eddig
        // + on-the-fly tulajdonság
        public int HanyHelyenJart()
        {
            return MeglatogatottVarosok.Split(';').Length - 1;
        }

        // ================================================================================================

        // két város közül melyikben járt előbb
        public string HolVoltElobb(string egyikVaros, string masikVaros)
        {
            string[] varosok = MeglatogatottVarosok.Split(';');

            int egyikHely = -1;
            int masikHely = -1;

            for (int i = 0; i < varosok.Length - 1; i++)
            {
                if (varosok[i] == egyikVaros)
                    egyikHely = i;

                if (varosok[i] == masikVaros)
                    masikHely = i;
            }

            if (egyikHely < masikHely)
                return egyikVaros;
            else if (egyikHely > masikHely)
                return masikVaros;
            else
                return "egyforma";
        }

        // ================================================================================================

        // helyek, ahol az utazó járt már ismétlődés nélkül
        public string[] HelyekIsmetlesNelkul(ref int utolsoHely)
        {
            // kiinduló tömb létrehozása
            string x = AtmasolKarakterenkent(MeglatogatottVarosok);
            string[] varosok = x.Split(';');
            
            // rendezés
            Rendez(varosok);

            // halmaz létrehozása (ism. elemek eltávolítása)
            string[] kivalogatottVarosok = new string[varosok.Length];
            int db = 0;
            kivalogatottVarosok[db] = varosok[0];

            for (int i = 1; i < varosok.Length; i++)
            {
                if (varosok[i] != kivalogatottVarosok[db])
                {
                    kivalogatottVarosok[++db] = varosok[i];
                }
            }

            utolsoHely = db;
            
            return kivalogatottVarosok;
        }
        
        // karakterenként átmásol, lehagyva az utolsó 1 db karaktert
        private string AtmasolKarakterenkent(string bemenet)
        {
            string kimenet = "";

            for (int i = 0; i < bemenet.Length - 1; i++)
                kimenet += bemenet[i];

            return kimenet;
        }
        
        // rendezi a bemenetet (minkiv.rend.)
        private void Rendez(string[] tomb)
        {
            for (int i = 0; i < tomb.Length-1; i++)
            {
                int min = i;
                for (int j = (i+1); j < tomb.Length; j++)
                {
                    if (tomb[min].CompareTo(tomb[j]) > 0) // tomb[min][0] > tomb[j][0]
                    {
                        min = j;
                    }
                }
                string temp = tomb[i];
                tomb[i] = tomb[min];
                tomb[min] = temp;
            }
        }

        // ================================================================================================

        // helyek ahol többször is járt
        public string[] HelyekAholTobbszorJart()
        {
            string[] varosokIsmNelk = HelyekIsmetlesNelkul2();

            int[] varosokDbSzam = new int[varosokIsmNelk.Length];

            string[] varosokFull = AtmasolKarakterenkent(MeglatogatottVarosok).Split(';');

            for (int i = 0; i < varosokFull.Length; i++)
            {
                for (int j = 0; j < varosokIsmNelk.Length; j++)
                {
                    if (varosokFull[i] == varosokIsmNelk[j])
                    {
                        varosokDbSzam[j]++;
                    }
                }
            }

            string vissza = "";
            for (int i = 0; i < varosokDbSzam.Length; i++)
            {
                if (varosokDbSzam[i] > 1)
                {
                    vissza += varosokIsmNelk[i] + ";";
                }
            }

            return AtmasolKarakterenkent(vissza).Split(';');
        }

        public string[] HelyekIsmetlesNelkul2()
        {
            // kiinduló tömb létrehozása
            string x = AtmasolKarakterenkent(MeglatogatottVarosok);
            string[] varosok = x.Split(';');

            // rendezés
            Rendez(varosok);

            // válogatás
            string kimenet = varosok[0] + ";";
            for (int i = 1; i < varosok.Length; i++)
            {
                if (!kimenet.Contains(varosok[i]))
                    kimenet += varosok[i] + ";";
            }

            return AtmasolKarakterenkent(kimenet).Split(';');
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Utazo utazo = new Utazo("teszt jakab");

            utazo.Utazik("budapest");
            utazo.Utazik("tihany");
            utazo.Utazik("mogyoród");

            // ================================================================================================

            Console.WriteLine(utazo.MeglatogatottVarosok);

            // ================================================================================================

            Console.WriteLine(utazo.JartE("tihany"));
            Console.WriteLine(utazo.JartE("mogyoród"));
            Console.WriteLine(utazo.JartE("x-pest"));
            Console.WriteLine(utazo.JartE("mogyi"));

            // ================================================================================================

            Console.WriteLine(utazo.HanyHelyenJart());

            // ================================================================================================

            Console.WriteLine(utazo.HolVoltElobb("budapest","mogyoród"));
            utazo.Utazik("óbuda");
            Console.WriteLine(utazo.HolVoltElobb("óbuda", "mogyoród"));
            Console.WriteLine(utazo.HolVoltElobb("óbuda", "óbuda"));

            // ================================================================================================

            utazo.Utazik("óbuda");
            utazo.Utazik("óbuda");

            Console.WriteLine("\nHelyek ism. nélkül:\n");

            int utolsoHely = 0;
            string[] helyek = utazo.HelyekIsmetlesNelkul(ref utolsoHely);
            for (int i = 0; i <= utolsoHely; i++)
                Console.WriteLine("-" + helyek[i]);

            // ================================================================================================

            Console.WriteLine("\nHelyek többször:\n");

            string[] helyekTobbszor = utazo.HelyekAholTobbszorJart();
            for (int i = 0; i < helyekTobbszor.Length; i++)
                Console.WriteLine("-" + helyekTobbszor[i]);

            Console.WriteLine("\nHelyek többször v2:\n");

            utazo.Utazik("budapest");
            helyekTobbszor = utazo.HelyekAholTobbszorJart();

            for (int i = 0; i < helyekTobbszor.Length; i++)
                Console.WriteLine("-" + helyekTobbszor[i]);
        }
    }
}

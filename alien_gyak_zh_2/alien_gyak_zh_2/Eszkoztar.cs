using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alien_gyak_zh_2
{
    class Eszkoztar
    {
        public Alien[] Aliens { get; set; }
        public static Alien[] Chestbursters;

        public Eszkoztar()
        {
            ImportalasFajlbol();
            ChestbursterKivalogatas();
        }

        private void ImportalasFajlbol()
        {
            StreamReader sr = new StreamReader("ALIENS.txt");
            int db = 0;
            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                db++;
            }
            sr.Close();

            Aliens = new Alien[db];

            sr = new StreamReader("ALIENS.txt");
            db = 0;

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] typeAndDate = line.Split('{')[0].Split(':');
                string[] deads = line.Split('{')[1].Split(';');

                // sample: Facehugger:2030-9-7:6:8{9936;3398;3613;3506;3613;6227;

                Aliens[db] = new Alien(
                    typeAndDate[0],
                    int.Parse(typeAndDate[2]),
                    int.Parse(typeAndDate[3]),
                    DateTime.Parse(typeAndDate[1])
                    );

                Aliens[db].EmberAzonositok = new string[deads.Length - 1];

                for (int i = 0; i < deads.Length - 1; i++)
                    Aliens[db].EmberAzonositok[i] = deads[i];

                db++;
            }
            sr.Close();
        }

        private void ChestbursterKivalogatas()
        {
            int db = 0;
            foreach (Alien alien in Aliens)
                if (alien.Tipus == "Chestburster")
                    db++;

            Chestbursters = new Alien[db];
            db = 0;

            foreach (Alien alien in Aliens)
                if (alien.Tipus == "Chestburster")
                    Chestbursters[db++] = alien;
        }

        public void EmberVisszaallitas()
        {
            // olvasás PERSON.txt
            StreamReader sr = new StreamReader("PEOPLE.txt");
            int db = 0;
            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                db++;
            }

            Ember[] person = new Ember[db];
            db = 0;

            sr = new StreamReader("PEOPLE.txt");
            while (!sr.EndOfStream)
            {
                string x = sr.ReadLine();
                person[db++] = new Ember(x);
            }
            sr.Close();

            // PERSON hozzákapcsolása ALIEN-hez (alien.person ID alapján)
            
            for (int i = 0; i < Aliens.Length; i++)
            {
                db = 0;
                for (int j = 0; j < person.Length; j++)
                {
                    if (Aliens[i].EmberAzonositok.Contains(person[j].ID))
                        //if (db < Aliens[i].EmberAzonositok.Length)
                        Aliens[i].MegoltEmberek[db++] = person[j];
                }
            }
        }

        public Alien[] KikeltChestbursterKivalogatas()
        {
            Alien[] kikeltek = new Alien[Chestbursters.Length];
            int db = 0;

            for (int i = 0; i < Chestbursters.Length; i++)
            {
                if (Chestbursters[i].KikelesIdeje.Year <= 2020)
                    kikeltek[db++] = Chestbursters[i];
            }

            return kikeltek;
        }

        public string MegoltEmberekKodolas()
        {
            string vissza = "";
            for (int i = 0; i < Aliens.Length; i++)
            {
                vissza += "ALIEN: " + Aliens[i].Tipus + "\n";

                for (int j = 0; j < Aliens[i].MegoltEmberek.Length; j++)
                {
                    if (Aliens[i].MegoltEmberek[j] != null)
                        vissza += "\t>> " + Aliens[i].MegoltEmberek[j].ID + "\n";
                }
            }

            return vissza;
        }

        public void Logolas()
        {
            StreamWriter sw = new StreamWriter("ALIENS-LOG.txt");
            for (int i = 0; i < Aliens.Length; i++)
            {
                string output = "";

                output += Aliens[i].ID + ":";

                for (int j = 0; j < Aliens[i].MegoltEmberek.Length; j++)
                {
                    if (Aliens[i].MegoltEmberek[j] != null)
                        output += Aliens[i].MegoltEmberek[j].Nev + "-";
                }

                sw.WriteLine(output);
            }
            sw.Close();
        }

        public int DeaconMegszamolas()
        {
            int db = 0;

            for (int i = 0; i < Aliens.Length; i++)
            {
                if (Aliens[i].Tipus == "Deacon" && Aliens[i].YPoz <= 9)
                    db++;
            }
            return db;
        }
    }
}

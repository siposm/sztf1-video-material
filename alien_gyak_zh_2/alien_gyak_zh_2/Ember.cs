using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alien_gyak_zh_2
{
    enum Nem
    {
        Ferfi = 0, No = 1
    }

    class Ember
    {
        private string Id;
        public string ID
        {
            get { return Id.Split('#')[1]; }
            set { Id = "#" + value; }
        }
        public string Nev { get; set; }
        public DateTime FelszallasIdeje { get; set; }
        public Nem Nem { get; set; }
        public bool EletbenVan { get; set; }

        public Ember(string nev, string id, DateTime felszallas, Nem nem, bool eletben)
        {
            this.EletbenVan = eletben;
            this.Nem = nem;
            this.FelszallasIdeje = felszallas;
            this.Nev = nev;
            this.ID = id;
        }

        public Ember(string fullBemenet)
        {
            // 5929:PETER:2030-9-7:1:1

            string[] adatok = fullBemenet.Split(':');

            this.ID = adatok[0];
            this.Nev = adatok[1];
            this.FelszallasIdeje = DateTime.Parse(adatok[2]);

            if (adatok[3] == "1")
                this.Nem = Nem.Ferfi;
            else
                this.Nem = Nem.No;

            if (adatok[4] == "1")
                this.EletbenVan = true;
            else
                this.EletbenVan = false;
        }
    }
}

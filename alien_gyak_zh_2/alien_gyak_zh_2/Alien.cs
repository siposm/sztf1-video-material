using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alien_gyak_zh_2
{
    class Alien
    {
        public string ID { get; private set; }
        public string Tipus { get; set; }
        public DateTime KikelesIdeje { get; set; }
        public int XPoz { get; set; }
        public int YPoz { get; set; }
        public string[] EmberAzonositok { get; set; }
        public Ember[] MegoltEmberek { get; set; }

        public Alien(string tipus, int x, int y, DateTime kikelesIdeje)
        {
            this.Tipus = tipus;
            this.XPoz = x;
            this.YPoz = y;
            this.KikelesIdeje = kikelesIdeje;
            MegoltEmberek = new Ember[7];
            this.ID = IDGeneral();
        }

        private string IDGeneral()
        {
            string ev = KikelesIdeje.Year.ToString();
            string honap = KikelesIdeje.Month.ToString();
            string nap = KikelesIdeje.Day.ToString();
            int sum = 0;

            for (int i = 0; i < ev.Length; i++)
                sum += int.Parse(ev[i].ToString());

            for (int i = 0; i < honap.Length; i++)
                sum += int.Parse(honap[i].ToString());

            for (int i = 0; i < nap.Length; i++)
                sum += int.Parse(nap[i].ToString());

            return
                sum.ToString() + "-" +
                Tipus[0].ToString().ToUpper() + "-" +
                Tipus[1].ToString().ToUpper();
        }

        public char Jelolo()
        {
            return char.ToUpper(this.Tipus[0]);
        }

    }
}

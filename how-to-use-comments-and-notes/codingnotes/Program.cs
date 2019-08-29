using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codingnotes
{
    /// <summary>
    /// Személy osztály amely a rendszerbe belépett felhasználót reprezentálja.
    /// Használata kötelező a [Neptun], [Ügyfélkapu] és [Tanulmányi Osztály] osztályokban.
    /// </summary>
    class Szemely
    {
        public Szemely()
        {
            ID = "#123ASD";
        }

        
        public string ID { get; set; }
        public string Nev { get; set; }
        public bool Nem { get; set; }

        /// <summary>
        /// Személy osztály metódusa, amely a nevet adja vissza beágyazva egy bemutatkozó mondatba.
        /// </summary>
        /// <returns></returns>
        public string Bemutatkozas()
        {
            return "Szia, én " + Nev + " vagyok.";
        }

        #region Matematikai műveleteket végző metódusok
        /// <summary>
        /// Két szám eredményét (összeadás vagy kivonás) a harmadik változóban adja vissza.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="eredmeny"></param>
        /// <param name="kivonas"></param>
        public void Metodus1(int a, int b, ref int eredmeny, bool kivonas)
        {
            /*
             * itt
             * 
             * mondjuk
             * 
             * sok
             * 
             * dolog
             * 
             * történik
             * 
             * sok-sok
             * 
             * soron
             * 
             * keresztül.
             * 
             * */
        }

        /// <summary>
        /// Személy osztály metódusa, a bemenet alapján kiszámolja a csillagok állását.
        /// </summary>
        /// <param name="bemenet"></param>
        public void Metodus2(string bemenet)
        {
            /*
             * itt
             * 
             * is
             * 
             * sok-sok
             * 
             * kód van,
             * 
             * amelyet
             * 
             * nem akarunk
             * 
             * mindig
             * 
             * fel-le görgetésnél
             * 
             * áttekerni
             * 
             * feleslegesen.
             * 
             * ezért ezt a két metódust, region-be tesszük, így azok külön zárhatók és nyithatók.
             * 
             * */
        }

        #endregion 
    }

    class Program
    {
        static void Main(string[] args)
        {
            Szemely szemely = new Szemely();

            szemely.Bemutatkozas();

            
        }
    }
}

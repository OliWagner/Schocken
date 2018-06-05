using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Schocken
{
    public static class Wuerfeln
    {

        public static string Wuerfle(int _anzahlWuerfel){
            int[] wurf = new int[3];
            int anzahlEinser = 3 - _anzahlWuerfel;

            for (int i = 0; i < anzahlEinser; i++)
            {
                wurf[i] = 1;
            }

            for (int i = anzahlEinser; i < 3; i++)
            {
                wurf[i] = EinWuerfel();
            }

            Array.Sort(wurf);
            Array.Reverse(wurf);
            string returner ="" + wurf[0] + wurf[1] + wurf[2] ;

            if (returner.Equals("321") && _anzahlWuerfel == 3) {
                returner = "123";
            }
            if (returner.Equals("432") && _anzahlWuerfel == 3)
            {
                returner = "234";
            }
            if (returner.Equals("543") && _anzahlWuerfel == 3)
            {
                returner = "345";
            }
            if (returner.Equals("654") && _anzahlWuerfel == 3)
            {
                returner = "456";
            }

            return returner;
        }








        public static int EinWuerfel() {
            return GenerateRandomNumber(1,6);
        }

        static int GenerateRandomNumber(int min, int max)
        {
            RNGCryptoServiceProvider c = new RNGCryptoServiceProvider();
            // Ein integer benötigt 4 Byte
            byte[] randomNumber = new byte[4];
            // dann füllen wir den Array mit zufälligen Bytes
            c.GetBytes(randomNumber);
            // schließlich wandeln wir den Byte-Array in einen Integer um
            int result = Math.Abs(BitConverter.ToInt32(randomNumber, 0));
            // da bis jetzt noch keine Begrenzung der Zahlen vorgenommen wurde,
            // wird diese Begrenzung mit einer einfachen Modulo-Rechnung hinzu-
            // gefügt
            return result % max + min;
        }
    }

    public static class Wuerfe {
        public static string[] Wurfbezeichnungen = { "221", "321", "322", "331", "332", "421", "422", "431", "433", "441", "442", "443", "521", "522", "531", "532", "533", "541", "542", "544", "551", "552", "553", "554", "621", "622", "631", "632", "633", "641", "642", "643", "644", "651", "652", "653", "655", "661", "662", "663", "664", "665", "Straße 123", "Straße 234", "Straße 345", "Straße 456", "General 2", "General 3", "General 4", "General 5", "General 6", "Schock 2", "Schock 3", "Schock 4", "Schock 5", "Schock 6", "SCHOCK AUS!!!" };
        public static string[] WuerfeListe = { "221", "321", "322", "331", "332", "421", "422", "431", "433", "441", "442", "443", "521", "522", "531", "532", "533", "541", "542", "544", "551", "552", "553", "554", "621", "622", "631", "632", "633", "641", "642", "643", "644", "651", "652", "653", "655", "661", "662", "663", "664", "665", "123", "234", "345", "456", "222", "333", "444", "555", "666", "211", "311", "411", "511", "611", "111" };
        public static int[] Wurfwerte = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 2, 3, 4, 5, 6, 13 };
    }
}

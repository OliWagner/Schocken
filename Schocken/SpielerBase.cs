using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schocken
{
    public class SpielerBase
    {
        public string NameSpieler { get; set; }
        public int AnzahlDeckel { get; set; }
        public bool ErsteHaelfte { get; set; }
        public bool ZweiteHaelfte { get; set; }
        public bool Dabei { get; set; }
        public string Wurf { get; set; }
        public int Wuerfel1Wert { get; set; }
        public int Wuerfel2Wert { get; set; }
        public int Wuerfel3Wert { get; set; }

        public SpielerBase(string _name, int _anzahlDeckel, bool _ersteGaelfte, bool _zweiteHaelfte, bool _dabei, string _wurf) {
            NameSpieler = _name;
            AnzahlDeckel = _anzahlDeckel;
            ErsteHaelfte = _ersteGaelfte;
            ZweiteHaelfte = _zweiteHaelfte;
            Dabei = _dabei;
            Wurf = _wurf;
            Wuerfel1Wert = 0;
            Wuerfel2Wert = 0;
            Wuerfel3Wert = 0;
        }

        public SpielerBase()
        {
        }
    }
}

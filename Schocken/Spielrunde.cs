using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Schocken
{
    public class WurfRunde {
        public Tuple<int, SpielerBase> HoechsterWurfWert { get; set; }
        public Tuple<int, SpielerBase> NiedrigsterWurfWert { get; set; }
        public int AnzahlWuerfe { get; set; }
    }

    public class Spielrunde
    {
        public List<SpielerBase> AlleMitspieler { get; set; }
        public bool ErsteHaelfte = false;
        public bool ZweiteHaelfte = false;
        public bool Finale = false;
        public int AnzahlDeckelStapel { get; set; }
        public WurfRunde Wurfrunde { get; set; }
        MainWindow _MainWindow { get; set; }

        public Spielrunde(MainWindow _mainWindow) {
            AlleMitspieler = new List<SpielerBase>();
            ErsteHaelfte = false;
            ZweiteHaelfte = false;
            Finale = false;
            AnzahlDeckelStapel = 13;

            _MainWindow = _mainWindow;
        }

        private void SortiereWurfInWurfrundeEin(string wurf, SpielerBase spieler) {
            int wurfwert = Array.IndexOf(Wuerfe.WuerfeListe, wurf);
            if (Wurfrunde.HoechsterWurfWert == null)
            {
                Wurfrunde.HoechsterWurfWert = Tuple.Create(wurfwert, spieler);
                Wurfrunde.NiedrigsterWurfWert = Tuple.Create(wurfwert, spieler);
            }
            else
            {
                if (wurfwert > Wurfrunde.HoechsterWurfWert.Item1) {
                    Wurfrunde.HoechsterWurfWert = Tuple.Create(wurfwert, spieler);
                }
                if (wurfwert <= Wurfrunde.NiedrigsterWurfWert.Item1) {
                    Wurfrunde.NiedrigsterWurfWert = Tuple.Create(wurfwert, spieler);
                }
            }
        }

        public void ReStart() {
            List<SpielerBase> AlleMitspielerNeueRunde = new List<SpielerBase>();
            int index = AlleMitspieler.IndexOf(Wurfrunde.NiedrigsterWurfWert.Item2);
            for (int i = index; i < 5; i++)
            {
                AlleMitspielerNeueRunde.Add(AlleMitspieler.ElementAt(i));
            }
            for (int i = 0; i < index; i++)
            {
                AlleMitspielerNeueRunde.Add(AlleMitspieler.ElementAt(i));
            }
            AlleMitspieler = AlleMitspielerNeueRunde;
            Start();
        }
        
        public void Start() {
            //Der Spieler beginnt
            Wurfrunde = new WurfRunde();
            Wurfrunde.AnzahlWuerfe = 3;

            int CounterWegenAnzahlWuerfe = 0;
            foreach (SpielerBase spieler in AlleMitspieler)
            {
                if (spieler.NameSpieler.Equals("Spieler"))
                {
                    if (spieler.Dabei) { 
                        //Der User würfelt selber
                        WuerfelDialog dialog = new WuerfelDialog(Wurfrunde.AnzahlWuerfe);
                        if (dialog.ShowDialog() == true) {
                            spieler.Wurf = dialog.Wurf;
                            _MainWindow.Spieler.SetCubeImages(dialog.Wurf);
                            _MainWindow.Spieler.lblSpielername.Content = Wuerfe.Wurfbezeichnungen[Array.IndexOf(Wuerfe.WuerfeListe, spieler.Wurf)];
                            SortiereWurfInWurfrundeEin(spieler.Wurf, spieler);
                        }
                    }
                }
                else
                {
                    if (spieler.Dabei)
                    {
                        //Der COmputer würfelt
                        //Welcher SPieler?
                        ucMitspieler aktSpieler = (from ucMitspieler spl in _MainWindow.Computerspieler where spl.Spieler.NameSpieler.Equals(spieler.NameSpieler) select spl).FirstOrDefault();

                        //TODO, das hier ist vorläufig. An diese STelle kommt noch eine Methode mit der Würfellogik für den Computerspieler
                        spieler.Wurf = Wuerfeln.Wuerfle(3);
                        aktSpieler.SetCubeImages(spieler.Wurf);
                        aktSpieler.lblWurfbezeichnung.Content = Wuerfe.Wurfbezeichnungen[Array.IndexOf(Wuerfe.WuerfeListe, spieler.Wurf)];
                        SortiereWurfInWurfrundeEin(spieler.Wurf, spieler);
                    }
                }
                CounterWegenAnzahlWuerfe++;
            }
            //Alle haben gewürfelt --> Bewerten
            if (Bewerte()) {
                //Die aktuelle Wuerfelrunde ist vorbei!

                WeiterDialog d = new WeiterDialog();
                if (d.ShowDialog() == true)
                {
                    _MainWindow.ResetSpielerFuerWurfrunde();
                    _MainWindow.UpdateSpieler(true);
                    ResetRunde();
                    ReStart();
                }
            } else {
                //Dialog, um weiter zu würfeln
                WeiterDialog d = new WeiterDialog();
                if (d.ShowDialog() == true)
                {
                    _MainWindow.ResetSpielerFuerWurfrunde();
                    ReStart();
                }
            }
            
        }

        private void ResetRunde() {
            
            AnzahlDeckelStapel = 13;
            if (ZweiteHaelfte) {
                foreach (SpielerBase sb in AlleMitspieler)
                {
                    if (sb.ErsteHaelfte || sb.ZweiteHaelfte) {
                        sb.Dabei = true;
                    } else {
                        sb.Dabei = false;
                    }
                    sb.AnzahlDeckel = 0;
                    sb.Wurf = "000";
                    sb.Wuerfel1Wert = 0;
                    sb.Wuerfel2Wert = 0;
                    sb.Wuerfel3Wert = 0;
                }
                ZweiteHaelfte = false; Finale = true;
            }
            if (ErsteHaelfte) {
                foreach (SpielerBase sb in AlleMitspieler)
                {
                    sb.Dabei = true;
                    sb.AnzahlDeckel = 0;
                    sb.Wurf = "000";
                    sb.Wuerfel1Wert = 0;
                    sb.Wuerfel2Wert = 0;
                    sb.Wuerfel3Wert = 0;
                }
                ErsteHaelfte = false; ZweiteHaelfte = true;
            }
        }

        //Die Wurfrunde auswerten und die Deckel verteilen
        public bool Bewerte()
        {
            int anzahlDeckelWurf = Wuerfe.Wurfwerte[Wurfrunde.HoechsterWurfWert.Item1];
            SpielerBase looser = Wurfrunde.NiedrigsterWurfWert.Item2;
            bool schockAus = false;

            //Schock aus abfangen
            if (anzahlDeckelWurf == 13) {
                schockAus = true;
                _MainWindow.Mitspieler1.SetStapelImage(0);
                _MainWindow.Mitspieler2.SetStapelImage(0);
                _MainWindow.Mitspieler3.SetStapelImage(0);
                _MainWindow.Mitspieler4.SetStapelImage(0);
                _MainWindow.Spieler.SetStapelImage(0);
                foreach (var item in AlleMitspieler)
                {
                    item.Dabei = false;
                }
            }


            if (AnzahlDeckelStapel > 0)
            {//Es werden Deckel vom Stapel verteilt
             //es gibt weniger Deckel auf dem Stapel als für den Wurf
                if (AnzahlDeckelStapel < anzahlDeckelWurf) { anzahlDeckelWurf = AnzahlDeckelStapel; }

                //Deckel vom Stapel abziehen und Bild setzen
                AnzahlDeckelStapel = AnzahlDeckelStapel - anzahlDeckelWurf;
                _MainWindow.Anzeige.ImgStapel.Source = SetStapelImage(AnzahlDeckelStapel);

                //Zwischen Mitspieler und Spieler unterscheiden
                if (looser.NameSpieler.Equals("Spieler"))
                {
                    SpielerBase sb = (from SpielerBase player in AlleMitspieler where player.NameSpieler.Equals(looser.NameSpieler) select player).First();
                    sb.AnzahlDeckel += anzahlDeckelWurf;
                    if (schockAus)
                    {
                        sb.Dabei = true;
                        _MainWindow.Spieler.SetStapelImage(13);
                    }
                    else
                    {
                        _MainWindow.Spieler.SetStapelImage(sb.AnzahlDeckel);
                    }


                }
                else
                {
                    SpielerBase sb = (from SpielerBase player in AlleMitspieler where player == looser select player).First();
                    sb.AnzahlDeckel += anzahlDeckelWurf;

                    ucMitspieler aktSpieler = (from ucMitspieler spl in _MainWindow.Computerspieler where spl.Spieler.NameSpieler.Equals(looser.NameSpieler) select spl).FirstOrDefault();
                    if (schockAus)
                    {
                        aktSpieler.ImgDeckelUser.Source = SetStapelImage(13);
                        aktSpieler.Spieler.Dabei = true;
                    }
                    else
                    {
                        aktSpieler.ImgDeckelUser.Source = SetStapelImage(sb.AnzahlDeckel);
                    }
                }
                //Checken, ob Stapel nicht gerade leer wurde...
                if (AnzahlDeckelStapel == 0) {
                    CheckMitSpielerDabei();
                }
            }
            else {//Die SPieler verteilen untereinander
                //TODO AB HIER WEITER --> SPieler verteilen Deckel
                VerteileUntereinander();

                CheckMitSpielerDabei();
            }
            
            //Die Anzeige im WIndow aktualisieren
            _MainWindow.UpdateSpieler();

            //TODO SetTextANzeige noch um Parameter ergänzen, welcher Text usw...
            if (!CheckRundeVorbei())
            {
                SetTextAnzeige(looser.NameSpieler, anzahlDeckelWurf);
                return false;
            }
            else
            {
                return true;
            }
                 
        }

        private bool CheckRundeVorbei() {
            SpielerBase Verlierer = new SpielerBase(); ;
            int counter = 0;
            foreach (SpielerBase spieler in AlleMitspieler)
            {
                if (spieler.Dabei) {
                    counter++;
                    Verlierer = spieler;
                }
            }
            //Wenn nur einer übrig bleibt...
            if (counter == 1) {
                if (ErsteHaelfte) {
                    Verlierer.ErsteHaelfte = true;
                    _MainWindow.Spieler.BtnRoll.Content = "Weiter";
                    _MainWindow.InitializeAnzeige(Wurfrunde.NiedrigsterWurfWert.Item2.NameSpieler + " verliert die erste Runde und fängt neu an!!");
                    return true;
                }
                if (ZweiteHaelfte)
                {
                    Verlierer.ZweiteHaelfte = true;
                    if (Verlierer.ErsteHaelfte) {//Blattschuss
                        _MainWindow.Spieler.BtnRoll.Content = "";
                        _MainWindow.InitializeAnzeige(Wurfrunde.NiedrigsterWurfWert.Item2.NameSpieler + " hat einen Blattschuss gekriegt!!");
                    } else {
                        _MainWindow.Spieler.BtnRoll.Content = "Finale";
                        _MainWindow.InitializeAnzeige(Wurfrunde.NiedrigsterWurfWert.Item2.NameSpieler + " verliert die zweite Runde und beginnt das Finale!!");
                    }                   
                    return true;
                }
                if (Finale)
                {
                    //Finale = false;
                    _MainWindow.Spieler.BtnRoll.Content = "";
                    _MainWindow.InitializeAnzeige(Wurfrunde.NiedrigsterWurfWert.Item2.NameSpieler + " verliert die Runde BIER!!");
                    return true;
                }
            }
            return false;
        }

        private void VerteileUntereinander() {
            int anzahlDeckelWurf = Wuerfe.Wurfwerte[Wurfrunde.HoechsterWurfWert.Item1];
            SpielerBase looser = (from SpielerBase player in AlleMitspieler where player == Wurfrunde.NiedrigsterWurfWert.Item2 select player).First();
            SpielerBase winner = (from SpielerBase player in AlleMitspieler where player == Wurfrunde.HoechsterWurfWert.Item2 select player).First();
            //SCHOCK AUS ABFANGEN
            if (anzahlDeckelWurf == 13)
            {
                looser.AnzahlDeckel = 13;
            }
            else {
                //Deckel ermitteln und verteilen
                if (anzahlDeckelWurf > winner.AnzahlDeckel) {
                    anzahlDeckelWurf = winner.AnzahlDeckel;
                    winner.AnzahlDeckel = 0;
                    winner.Dabei = false;
                }
                looser.AnzahlDeckel += anzahlDeckelWurf;
                winner.AnzahlDeckel -= anzahlDeckelWurf;
            }
        }

        private void CheckMitSpielerDabei() {
            foreach (SpielerBase sb in AlleMitspieler)
            {
                //Erst die Daten für die SPielrunde --> Scheissarchitektur ;)
                if (sb.AnzahlDeckel == 0) { sb.Dabei = false; }
                //Zwischen UcMitspieler und Spieler unterscheiden
                ucMitspieler ucm = (from ucMitspieler item in _MainWindow.Computerspieler where item.Spieler.NameSpieler.Equals("") select item).FirstOrDefault();
                if (ucm != null) { // Es handelt sich um einen ucMitspieler
                    if (ucm.Spieler.AnzahlDeckel == 0) { ucm.Spieler.Dabei = false; }
                } else { //Es handelt sich um den Spieler
                    if (_MainWindow.Spieler.Spieler.AnzahlDeckel == 0) { _MainWindow.Spieler.Spieler.Dabei = false; }
                }
            }
        }

        private void SetTextAnzeige(string spielerNiedrig, int anzahlDeckel) {
            string anzeigeText = spielerNiedrig + " ist klein, erhält " + anzahlDeckel + " Deckel und beginnt neu!";
            _MainWindow.Anzeige.SetTxtAnzeige(anzeigeText);
        }

        public BitmapImage SetStapelImage(int anzahl)
        {

            string strBmp = "/images/Deckel" + anzahl + ".png";

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(@strBmp, UriKind.RelativeOrAbsolute);
            bmp.EndInit();
            return bmp;
        }




    }
}

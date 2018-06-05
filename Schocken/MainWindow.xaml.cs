using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schocken
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Spielrunde Spielrunde;
        public List<ucMitspieler> Computerspieler = new List<ucMitspieler>();

        public MainWindow()
        {
            InitializeComponent();
            Spielrunde = new Spielrunde(this);
            InitializeSpieler();
            InitializeAnzeige("Klicken Sie auf Start, um das Spiel zu beginnen!!");
            Spieler.BtnRoll.Click += BtnRoll_Click;
            
        }


        #region Events Spieler
        private void BtnRoll_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.Equals("Start")) {
                ((Button)sender).Content = "";
                Spielrunde.ErsteHaelfte = true;
                Anzeige.SetSpielImage(true, false, false);
                SetMitspielerDabei();
                InitializeAnzeige("Der Spieler beginnt!!");
                //Spieler.BtnRoll.Content = "";
                Spielrunde.Start();
            }
        }

        private void SetMitspielerDabei() {
            Mitspieler1.Spieler.Dabei = true;
            Mitspieler1.InitializeAnzeige();
            Computerspieler.Add(Mitspieler1);
            Mitspieler2.Spieler.Dabei = true;
            Mitspieler2.InitializeAnzeige();
            Computerspieler.Add(Mitspieler2);
            Mitspieler3.Spieler.Dabei = true;
            Mitspieler3.InitializeAnzeige();
            Computerspieler.Add(Mitspieler3);
            Mitspieler4.Spieler.Dabei = true;
            Mitspieler4.InitializeAnzeige();
            Computerspieler.Add(Mitspieler4);
            Spieler.Spieler.Dabei = true;
            Spieler.InitializeAnzeige();
        }

        #endregion

        public void ResetSpielerFuerWurfrunde() {
            Mitspieler1.SetCubeImages("000");
            Mitspieler2.SetCubeImages("000");
            Mitspieler3.SetCubeImages("000");
            Mitspieler4.SetCubeImages("000");
            Spieler.SetCubeImages("000");
            Mitspieler1.lblWurfbezeichnung.Content = "Noch nix";
            Mitspieler2.lblWurfbezeichnung.Content = "Noch nix";
            Mitspieler3.lblWurfbezeichnung.Content = "Noch nix";
            Mitspieler4.lblWurfbezeichnung.Content = "Noch nix";
            Spieler.lblSpielername.Content = "Noch nix";
        }

        public void UpdateSpieler(bool trigger = false) {
            Mitspieler1.InitializeAnzeige();
            Mitspieler2.InitializeAnzeige();
            Mitspieler3.InitializeAnzeige();
            Mitspieler4.InitializeAnzeige();
            Spieler.InitializeAnzeige();
            if (trigger) {
                Mitspieler1.SetStapelImage(0);
                Mitspieler2.SetStapelImage(0);
                Mitspieler3.SetStapelImage(0);
                Mitspieler4.SetStapelImage(0);
                Spieler.SetStapelImage(0);
            }
        }

        private void InitializeSpieler()
        {
            Mitspieler1.Spieler = new SpielerBase("Ralf", 0, false, false, true, "000");
            Mitspieler2.Spieler = new SpielerBase("Arthur", 0, false, false, true, "000");
            Mitspieler3.Spieler = new SpielerBase("Cetin", 0, false, false, true, "000");
            Mitspieler4.Spieler = new SpielerBase("Wolfgang", 0, false, false, true, "000");
            Spieler.Spieler = new SpielerBase("Spieler", 0, false, false, true, "000");

            Spielrunde.AlleMitspieler.Add(Spieler.Spieler);
            Spielrunde.AlleMitspieler.Add(Mitspieler1.Spieler);
            Spielrunde.AlleMitspieler.Add(Mitspieler2.Spieler);
            Spielrunde.AlleMitspieler.Add(Mitspieler3.Spieler);
            Spielrunde.AlleMitspieler.Add(Mitspieler4.Spieler);


            Mitspieler1.InitializeAnzeige("Noch nix");
            Mitspieler2.InitializeAnzeige("Noch nix");
            Mitspieler3.InitializeAnzeige("Noch nix");
            Mitspieler4.InitializeAnzeige("Noch nix");
            Spieler.InitializeAnzeige("Noch nix");
            Spieler.BtnRoll.Content = "Start";
        }

        public void InitializeAnzeige(string anzeigeText)
        {
            Anzeige.SetStapelImage(Spielrunde.AnzahlDeckelStapel);
            Anzeige.SetTxtAnzeige(anzeigeText);
            Anzeige.SetSpielImage(Spielrunde.ErsteHaelfte, Spielrunde.ZweiteHaelfte, Spielrunde.Finale);
        }

    }
}

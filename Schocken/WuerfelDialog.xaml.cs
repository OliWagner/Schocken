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
using System.Windows.Shapes;

namespace Schocken
{
    /// <summary>
    /// Interaktionslogik für WuerfelDialog.xaml
    /// </summary>
    public partial class WuerfelDialog : Window
    {
        #region fields and constructor
        public string Wurf { get; set; }
        public int AnzahlErlaubteWuerfe { get; set; }
        public int AnzahlWuerfe { get; set; }

        private BitmapImage Null { get; set; }
        private BitmapImage Eins { get; set; }
        private BitmapImage Zwei { get; set; }
        private BitmapImage Drei { get; set; }
        private BitmapImage Vier { get; set; }
        private BitmapImage Fuenf { get; set; }
        private BitmapImage Sechs { get; set; }

        private bool ButtonReadyAktiv { get; set; }

        private int ValCube1 { get; set; }
        private int ValCube2 { get; set; }
        private int ValCube3 { get; set; }

        private bool Cube1Raus { get; set; }
        private bool Cube2Raus { get; set; }
        private bool Cube3Raus { get; set; }

        public WuerfelDialog(int _anzahl)
        {
            InitializeComponent();
            ButtonReadyAktiv = false;
            Null = new BitmapImage();
            Null.BeginInit();
            Null.UriSource = new Uri(@"/images/0.png", UriKind.RelativeOrAbsolute);
            Null.EndInit();

            Eins = new BitmapImage();
            Eins.BeginInit();
            Eins.UriSource = new Uri(@"/images/1.png", UriKind.RelativeOrAbsolute);
            Eins.EndInit();

            Zwei = new BitmapImage();
            Zwei.BeginInit();
            Zwei.UriSource = new Uri(@"/images/2.png", UriKind.RelativeOrAbsolute);
            Zwei.EndInit();

            Drei = new BitmapImage();
            Drei.BeginInit();
            Drei.UriSource = new Uri(@"/images/3.png", UriKind.RelativeOrAbsolute);
            Drei.EndInit();

            Vier = new BitmapImage();
            Vier.BeginInit();
            Vier.UriSource = new Uri(@"/images/4.png", UriKind.RelativeOrAbsolute);
            Vier.EndInit();

            Fuenf = new BitmapImage();
            Fuenf.BeginInit();
            Fuenf.UriSource = new Uri(@"/images/5.png", UriKind.RelativeOrAbsolute);
            Fuenf.EndInit();

            Sechs = new BitmapImage();
            Sechs.BeginInit();
            Sechs.UriSource = new Uri(@"/images/6.png", UriKind.RelativeOrAbsolute);
            Sechs.EndInit();
            AnzahlErlaubteWuerfe = _anzahl;
            LblAnzahl.Content = "noch " + _anzahl + " Würfe";
        }
        #endregion

        public BitmapImage SetWuerfelBild(string which)
        {
            switch (which)
            {
                case "1": return Eins;
                case "2": return Zwei;
                case "3": return Drei;
                case "4": return Vier;
                case "5": return Fuenf;
                case "6": return Sechs;
            }
            return Null;
        }

        private void SetCubeImages(string wurf)
        {
            int sechser = 0;

            if (Cube1Raus)
            {
                ImgCube1.Source = SetWuerfelBild("1");
            }
            else
            {
                ImgCube1.Source = SetWuerfelBild(wurf[0].ToString());
                ValCube1 = Int32.Parse(wurf[0].ToString());
            }

            if (Cube2Raus)
            {
                ImgCube2.Source = SetWuerfelBild("1");
            }
            else
            {
                ImgCube2.Source = SetWuerfelBild(wurf[1].ToString());
                ValCube2 = Int32.Parse(wurf[1].ToString());
            }

            if (Cube3Raus)
            {
                ImgCube3.Source = SetWuerfelBild("1");
            }
            else
            {
                ImgCube3.Source = SetWuerfelBild(wurf[2].ToString());
                ValCube3 = Int32.Parse(wurf[2].ToString());
            }


            //Set Button's text's
            if (AnzahlErlaubteWuerfe - AnzahlWuerfe > 0)
            {
                if (ValCube1 == 1) { Btn1.Content = "Halten"; } else { Btn1.Content = ""; }
                if (ValCube2 == 1) { Btn2.Content = "Halten"; } else { Btn2.Content = ""; }
                if (ValCube3 == 1) { Btn3.Content = "Halten"; } else { Btn3.Content = ""; }

                if (ValCube1 == 6) { sechser++; }
                if (ValCube2 == 6)
                {
                    sechser++;
                    if (sechser == 2) { Btn2.Content = "Drehen"; sechser = 0; }
                }
                if (ValCube3 == 6)
                {
                    sechser++;
                    if (sechser == 2) { Btn3.Content = "Drehen"; }
                }
            }
        }

        #region commands

        private void BtnRollCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (AnzahlErlaubteWuerfe > AnzahlWuerfe)
            {
                e.CanExecute = true;
            }
            else {
                e.CanExecute = false;
            }                    
        }

        private void BtnReadyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ButtonReadyAktiv) {
                e.CanExecute = true;
            } else {
                e.CanExecute = false;
            }
                 
        }

        private void Btn1Command_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Btn1.Content != null && Btn1.Content.Equals(""))
            {
                e.CanExecute = false;
            }
            else
            {
                if (!Cube1Raus) {
                    e.CanExecute = true;
                }               
            }           
        }
        
        private void Btn2Command_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Btn2.Content != null && Btn2.Content.Equals(""))
            {
                e.CanExecute = false;
            }
            else
            {
                if (!Cube2Raus)
                {
                    e.CanExecute = true;
                }
            }
        }
        
        private void Btn3Command_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Btn3.Content != null && Btn3.Content.Equals(""))
            {
                e.CanExecute = false;
            }
            else
            {
                if (!Cube3Raus)
                {
                    e.CanExecute = true;
                }
            }
        }

        private void BtnRollCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Do nothing
        }

        private void BtnReadyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Do nothing
        }

        private void Btn1Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Do nothing
        }

        private void Btn2Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Do nothing
        }

        private void Btn3Command_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Do nothing
        }
        #endregion

        #region eventhandler
        private void BtnReady_Click(object sender, RoutedEventArgs e)
        {
            int[] arr = new int[] { ValCube1, ValCube2, ValCube3 };
            Array.Sort(arr);
            Array.Reverse(arr);
            Wurf = arr[0].ToString() + "" + arr[1].ToString() + "" + arr[2].ToString();
            if(!Cube1Raus && !Cube2Raus && !Cube3Raus) { 
                if (Wurf.Equals("321") || Wurf.Equals("432") || Wurf.Equals("543") || Wurf.Equals("654")) {
                    Wurf = arr[2].ToString() + "" + arr[1].ToString() + "" + arr[0].ToString();
                }
            }
            DialogResult = true;
        }
        private void BtnRoll_Click(object sender, RoutedEventArgs e)
        {
            ButtonReadyAktiv = true;
            AnzahlWuerfe++;
            Btn1.Content = "";
            Btn2.Content = "";
            Btn3.Content = "";

            int wurfZahl = 3;
            if (Cube1Raus) { wurfZahl--; }
            if (Cube2Raus) { wurfZahl--; }
            if (Cube3Raus) { wurfZahl--; }

            string _wurf = Wuerfeln.Wuerfle(wurfZahl);
            if (wurfZahl == 3)
            {
                ValCube1 = Int32.Parse(_wurf[0].ToString());
                ValCube2 = Int32.Parse(_wurf[1].ToString());
                ValCube3 = Int32.Parse(_wurf[2].ToString());
                _wurf = ValCube1 + "" + ValCube2 + "" + ValCube3; 
            }
            //Schauen ob und welche Würfel rausgelegt sind
            if (wurfZahl == 2) {
                int wert1 = Int32.Parse(_wurf[0].ToString());
                int wert2 = Int32.Parse(_wurf[1].ToString());
                if (Cube1Raus) { _wurf = "1" + wert1 + "" + wert2; ValCube2 = wert1; ValCube3 = wert2; }
                if (Cube2Raus) { _wurf = "" + wert1 + "1" + wert2; ValCube1 = wert1; ValCube3 = wert2; }
                if (Cube3Raus) { _wurf = wert1 + "" + wert2 + "1"; ValCube1 = wert1; ValCube2 = wert2; }
            }
            if (wurfZahl == 1)
            {
                int wert1 = Int32.Parse(_wurf[0].ToString());
                if (Cube1Raus && Cube2Raus) { _wurf = "11" + wert1; ValCube3 = wert1; }
                if (Cube2Raus && Cube3Raus) { _wurf = wert1 + "11"; ValCube1 = wert1; }
                if (Cube1Raus && Cube3Raus) { _wurf = "1" + wert1 + "1"; ValCube2 = wert1; }
            }

            SetCubeImages(_wurf);
            LblAnzahl.Content = (AnzahlErlaubteWuerfe - AnzahlWuerfe) > 1 ? "Noch " + (AnzahlErlaubteWuerfe - AnzahlWuerfe) + " Würfe" : "Noch " + (AnzahlErlaubteWuerfe - AnzahlWuerfe) + " Wurf";
        }
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.Equals("Halten")) {
                Cube1Raus = true;
            }
            if (button.Content.Equals("Drehen"))
            {
                ButtonReadyAktiv = false;
                Cube1Raus = true;
                ImgCube1.Source = SetWuerfelBild("1");
                ValCube1 = 1;
            }
        }
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.Equals("Halten")) {
                Cube2Raus = true;
            }
            if (button.Content.Equals("Drehen"))
            {
                Cube2Raus = true;
                ImgCube2.Source = SetWuerfelBild("1");
                ButtonReadyAktiv = false;
                ValCube2 = 1;
            }
        }
        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.Equals("Halten")) {
                Cube3Raus = true;
            }
            if (button.Content.Equals("Drehen"))
            {
                Cube3Raus = true;
                ImgCube3.Source = SetWuerfelBild("1");
                ButtonReadyAktiv = false;
                ValCube3 = 1;
            }
        }
        #endregion

    }
}

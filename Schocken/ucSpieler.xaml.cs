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
    /// Interaktionslogik für ucSpieler.xaml
    /// </summary>
    public partial class ucSpieler : UserControl
    {
        int lastSize_Width;
        int fontsize;

        public SpielerBase Spieler { get; set; }
        private BitmapImage GruenAktiv { get; set; }
        private BitmapImage GruenPassiv { get; set; }
        private BitmapImage RotAktiv { get; set; }
        private BitmapImage RotPassiv { get; set; }

        private BitmapImage Null { get; set; }
        private BitmapImage Eins { get; set; }
        private BitmapImage Zwei { get; set; }
        private BitmapImage Drei { get; set; }
        private BitmapImage Vier { get; set; }
        private BitmapImage Fuenf { get; set; }
        private BitmapImage Sechs { get; set; }
        public ucSpieler()
        {
            InitializeComponent();

            #region Bilder registrieren
            GruenAktiv = new BitmapImage();
            GruenAktiv.BeginInit();
            GruenAktiv.UriSource = new Uri(@"/images/gruenaktiv.png", UriKind.RelativeOrAbsolute);
            GruenAktiv.EndInit();

            GruenPassiv = new BitmapImage();
            GruenPassiv.BeginInit();
            GruenPassiv.UriSource = new Uri(@"/images/gruenpassiv.png", UriKind.RelativeOrAbsolute);
            GruenPassiv.EndInit();

            RotAktiv = new BitmapImage();
            RotAktiv.BeginInit();
            RotAktiv.UriSource = new Uri(@"/images/rotaktiv.png", UriKind.RelativeOrAbsolute);
            RotAktiv.EndInit();

            RotPassiv = new BitmapImage();
            RotPassiv.BeginInit();
            RotPassiv.UriSource = new Uri(@"/images/rotpassiv.png", UriKind.RelativeOrAbsolute);
            RotPassiv.EndInit();

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
            #endregion
        }

        public BitmapImage SetWuerfelBild(string which) {
            switch (which) {
                case "1": return Eins;
                case "2": return Zwei;
                case "3": return Drei;
                case "4": return Vier;
                case "5": return Fuenf;
                case "6": return Sechs;
            }
            return Null;
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

        public void SetCubeImages(string _wurf)
        {
            ImgCube1.Source = SetWuerfelBild(_wurf[0].ToString());
            ImgCube2.Source = SetWuerfelBild(_wurf[1].ToString());
            ImgCube3.Source = SetWuerfelBild(_wurf[2].ToString());

        }

        public void InitializeAnzeige(string text = "")
        {
            ImgAktiv.Source = Spieler.Dabei ? GruenAktiv : GruenPassiv;
            Img1st.Source = Spieler.ErsteHaelfte ? RotAktiv : RotPassiv;
            Img2nd.Source = Spieler.ZweiteHaelfte ? RotAktiv : RotPassiv;
            ImgDeckelUser.Source = SetStapelImage(Spieler.AnzahlDeckel);
            if (!text.Equals(""))
            {
                lblSpielername.Content = text;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (lastSize_Width == 0)
            {
                lastSize_Width = Convert.ToInt32(e.NewSize.Width);
                fontsize = 14;
            }

            double faktor = e.NewSize.Width / lastSize_Width;
            lastSize_Width = Convert.ToInt32(e.NewSize.Width);
            double newFontSize = fontsize * faktor;
            Int32.TryParse(Math.Round(newFontSize).ToString(), out fontsize);

            lblSpielername.FontSize = fontsize + 3;
        }

        #region Commands

        private void BtnRollCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (((ucSpieler)sender).BtnRoll.Content != null && ((ucSpieler)sender).BtnRoll.Content.Equals("Start"))
            {
                e.CanExecute = true;
            }
            else if (((ucSpieler)sender).BtnRoll.Content != null && ((ucSpieler)sender).BtnRoll.Content.Equals("Weiter"))
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void BtnRollCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Do nothing
        }
        #endregion

       

        #region Events - alle leer, werden in MainWindow behandelt
        private void BtnRoll_Click(object sender, RoutedEventArgs e)
        {
            SetCubeImages("000");
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion


    }
}

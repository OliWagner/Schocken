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
    /// Interaktionslogik für ucAnzeige.xaml
    /// </summary>
    public partial class ucAnzeige : UserControl
    {
        int lastSize_Width;
        int fontsize;
        private BitmapImage GruenAktiv { get; set; }
        private BitmapImage GruenPassiv { get; set; }

        public ucAnzeige()
        {
            InitializeComponent();

            GruenAktiv = new BitmapImage();
            GruenAktiv.BeginInit();
            GruenAktiv.UriSource = new Uri(@"/images/gruenaktiv.png", UriKind.RelativeOrAbsolute);
            GruenAktiv.EndInit();

            GruenPassiv = new BitmapImage();
            GruenPassiv.BeginInit();
            GruenPassiv.UriSource = new Uri(@"/images/gruenpassiv.png", UriKind.RelativeOrAbsolute);
            GruenPassiv.EndInit();
        }

        public void SetTxtAnzeige(string txt) {
            lblText.Text = txt;
        }

        public void SetSpielImage(bool first, bool second, bool third) {
            Img1st.Source = first ? GruenAktiv : GruenPassiv;
            Img2nd.Source = second ? GruenAktiv : GruenPassiv;
            Img3rd.Source = third ? GruenAktiv : GruenPassiv;
        }

        public BitmapImage SetStapelImage(int anzahl)
        {

            string strBmp = "/images/Stapel" + anzahl + ".png";

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(@strBmp, UriKind.RelativeOrAbsolute);
            bmp.EndInit();
            return bmp;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (lastSize_Width == 0)
            {
                lastSize_Width = Convert.ToInt32(e.NewSize.Width);
                fontsize = 20;
            }

            double faktor = e.NewSize.Width / lastSize_Width;
            lastSize_Width = Convert.ToInt32(e.NewSize.Width);
            double newFontSize = fontsize * faktor;
            Int32.TryParse(Math.Round(newFontSize).ToString(), out fontsize);

            lblText.FontSize = fontsize;
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Araba_yarisi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            basla();
           
        }

        /*
         Program içinde kullanacağımız genel değişkenleri tanımlıyoruz...
        */
        int kazanilan_puan = 0;
        int yol_hizi2 = 5;
        int araba_hiz2 = 5;
        bool sol_yon = false;
        bool sag_yon = false;
        int diger_araba_hizi2 = 5;
        Random rnd2 = new Random();
        //
        
        int yol_hizi = 5;
        int araba_hiz = 5;
        int diger_araba_hizi = 5;
        Random rnd = new Random();

        private void basla()
        {
            //başlat butonu ve çaprma efektini pasif yapıyoruz
            button1.Enabled = false;
            carpma2.Visible = false;
            carpma.Visible = false;
            //Arabanın ve yolun aşağıya doğru kayma hızı   
            diger_araba_hizi2 = 5;
            yol_hizi2 = 5;
            //
            diger_araba_hizi = 10;
            yol_hizi = 40;

            kazanilan_puan = 0;

            //Arabamızın koordinatı
            bizim_araba2.Left = 166;
            bizim_araba2.Top = 293;
            //
            bizim_araba.Left = 166;
            bizim_araba.Top = 293;
            //Kontrol tuşlarını yasif yapıyoruz
            sol_yon = false;
            sag_yon = false;

            //Diğer arabaların koordinatları
            araba3.Left = 66;
            araba3.Top = -120;
            araba4.Left = 294;
            araba4.Top = -185;
            //
            araba1.Left = 66;
            araba1.Top = -120;
            araba2.Left = 294;
            araba2.Top = -185;

            //Zeminde hareket edecek olan yolun koordinatı
            yol3.Left = -3;
            yol3.Top = -222;
            yol4.Left = -2;
            yol4.Top = -638;

            yol1.Left = -3;
            yol1.Top = -222;
            yol2.Left = -2;
            yol2.Top = -638;

            timer1.Start();

        }
        int araba1_konum;
        private void timer1_Tick(object sender, EventArgs e)
        {
            araba1.Image = Properties.Resources.araba5;
            araba2.Image = Properties.Resources.araba5;
            //Süre başladığında puan artışı ve ekrana yazdırma
            kazanilan_puan++;
            puan.Text = kazanilan_puan.ToString();

            //Yolun yukardan aşağı hareketi ve tekrar başa dönme kontrolü    
            yol3.Top += yol_hizi2;
            yol4.Top += yol_hizi2;
            if (yol3.Top > 630) yol3.Top = -630;
            if (yol4.Top > 630) yol4.Top = -630;
            //
            yol1.Top += yol_hizi;
            yol2.Top += yol_hizi;
            if (yol1.Top > 630) yol1.Top = -630;
            if (yol2.Top > 630) yol2.Top = -630;
            //Yön tuşları ile arabanın hareketi 
            if (sol_yon) bizim_araba2.Left -= araba_hiz2;
            if (sag_yon) bizim_araba2.Left += araba_hiz2;
            if (bizim_araba2.Left < 1) { sol_yon = false; }
            else if (bizim_araba2.Left + bizim_araba2.Width > 380) { sag_yon = false; }
            
            //Diğer arabaların aşağı doğru hareketi ve rastgele bir değer üretilip tekrardan ekrana gelmesi..
            araba3.Top += diger_araba_hizi2;
            araba4.Top += diger_araba_hizi2;
            if (araba3.Top > panel1.Height)
            {
                araba3_degistir();
                araba3.Left = rnd2.Next(2, 160);
                araba3.Top = rnd2.Next(100, 200) * -1;
            }

            if (araba4.Top > panel1.Height)
            {
                araba4_degistir();
                araba4.Left = rnd2.Next(185, 327);
                araba4.Top = rnd2.Next(100, 200) * -1;
            }

            //
            araba1.Top += diger_araba_hizi;
            araba2.Top += diger_araba_hizi;
            if (araba1.Top > panel1.Height)
            {
                araba3_degistir();

                araba1_konum = rnd.Next(10, 125); // Araba1'in leftinin rastgele geldiği yeri araba1_konum değişkeninde tutuyoruz.

                araba1.Left = araba1_konum;
                araba1.Top = rnd.Next(100, 200) * -1;
            }

            if (araba2.Top > panel1.Height)
            {
                araba4_degistir();

                araba2.Left = rnd.Next(228, 327);
                araba2.Top = rnd.Next(100, 200) * -1;
            }
            int bizimSol = bizim_araba.Left + bizim_araba.Width;
            int arabaSol = araba1_konum + araba1.Width;

            if (bizim_araba.Left < 1)
            {
                bizim_araba.Left += 0;
            }
            else if (araba2.Left < 3)
            {
                araba2.Left += araba_hiz;
            }
            else if (bizimSol > 380)
            {
                bizim_araba.Left -= araba_hiz;
            }

            else
            {

                if (bizimSol < araba1_konum)
                {
                    bizim_araba.Left += 0;
                }
                else if (bizim_araba.Left > arabaSol)
                {
                    bizim_araba.Left += 0;
                }
                else if (bizim_araba.Left < arabaSol)
                {
                    if (araba1.Left < bizim_araba.Width + 10)
                    {
                        bizim_araba.Left += araba_hiz;
                    }
                    else
                    {
                        bizim_araba.Left -= araba_hiz;
                    }
                }
                else if (bizimSol < araba1.Left)
                {
                    bizim_araba.Left += araba_hiz;
                }
                else
                {
                    bizim_araba.Left += 0;
                }
            }

            //Arabanın diğer arabaları çarpma kontrolü ve oyun biti fonk. çalıştırılması
            if (bizim_araba2.Bounds.IntersectsWith(araba3.Bounds) || bizim_araba2.Bounds.IntersectsWith(araba4.Bounds))
            {
                oyunBitti();
            }



        }

        private void oyunBitti()
        {
            //Eğer çarpma olduysa ekranda puan gösterilip tekrar başlayabilmek için buton aktif hale geliyor....  
            timer1.Stop();
            button1.Enabled = true;
            carpma2.Visible = true;
            carpma.Visible = true;
            bizim_araba2.Controls.Add(carpma2);
            carpma2.Location = new Point(-8, 5);
            carpma2.BringToFront();
            carpma2.BackColor = Color.Transparent;
            MessageBox.Show("Tebrikler " + puan.Text + " kazandınız", "Oyun Sonu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void araba3_degistir()
        {
            //Diğer arabalar ekranda kaybolunca 1-7 arasında rastgele bir değer üretilip 
            //eklenen resimlerin ekrana getirilmesini sağlıyoruz....
            //
            
            int sira = 5;
            switch (sira)
            {
                case 1:
                    araba3.Image = Properties.Resources.araba5;
                    break;
                case 2:
                    araba3.Image = Properties.Resources.araba5;
                    break;
                case 3:
                    araba3.Image = Properties.Resources.araba5;
                    break;
                case 4:
                    araba3.Image = Properties.Resources.araba5;
                    break;
                case 5:
                    araba3.Image = Properties.Resources.araba5;
                    break;
                case 6:
                    araba3.Image = Properties.Resources.araba5;
                    break;
                case 7:
                    araba3.Image = Properties.Resources.araba5;
                    break;
                default:
                    break;

            }
        }

        private void araba4_degistir()
        {
            //Diğer arabalar ekranda kaybolunca 1-7 arasında rastgele bir değer üretilip 
            //eklenen resimlerin ekrana getirilmesini sağlıyoruz....
            int sira = rnd2.Next(1, 7);
            sira = 5;
            switch (sira)
            {
                case 1:
                    araba4.Image = Properties.Resources.araba2;
                    break;
                case 2:
                    araba4.Image = Properties.Resources.araba3;
                    break;
                case 3:
                    araba4.Image = Properties.Resources.araba4;
                    break;
                case 4:
                    araba4.Image = Properties.Resources.araba5;
                    break;
                case 5:
                    araba4.Image = Properties.Resources.araba5;
                    break;
                case 6:
                    araba4.Image = Properties.Resources.araba7;
                    break;
                case 7:
                    araba4.Image = Properties.Resources.araba8;
                    break;
                default:
                    break;

            }
        }
       
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Tuş basma olayında sağa veya sola hareketin kontrolü
            if (e.KeyCode == Keys.Left && bizim_araba2.Left > 0) sol_yon = true;

            if (e.KeyCode == Keys.Right && bizim_araba2.Left + bizim_araba2.Width < panel1.Width) sag_yon = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //Tuş bırakılınca arabanın hareketinin bitmesi
            if (e.KeyCode == Keys.Left) sol_yon = false;
            if (e.KeyCode == Keys.Right) sag_yon = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {   //Butona basınca tekrar oyunu başlatıyoruz....
            basla();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}

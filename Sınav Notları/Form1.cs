using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sınav_Notları
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ismailak_EntityEntities db = new ismailak_EntityEntities();
        private void btnderslist_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Tbl_dersler.ToList();
        }

        private void btnogrencilist_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.Tbl_ogrenci.ToList();
        }

        private void btnnotlist_Click(object sender, EventArgs e)
        {
            var sorgu = from item in db.Tbl_notlar
                        select new { item.notid, item.ogr, item.ders, item.sınav1 };
            dataGridView1.DataSource = sorgu.ToList();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            Tbl_ogrenci OG = new Tbl_ogrenci();
            OG.ad = txtad.Text;
            OG.soyad = txtsoyad.Text;
            OG.sehir = txtsehir.Text;
            db.Tbl_ogrenci.Add(OG);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Ekleme İşlemi Başarılı");
            txtad.Clear();
            txtsoyad.Clear();


            Tbl_dersler de = new Tbl_dersler();
            de.dersad = txtdersad.Text;
            db.Tbl_dersler.Add(de);
            db.SaveChanges();
            MessageBox.Show("Ders Ekleme İşlemi Başarılı");
            txtdersad.Clear();

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtid.Text);
            var x = db.Tbl_ogrenci.Find(id);
            db.Tbl_ogrenci.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Silme İşlemi Başarılı");
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtid.Text);
            var y = db.Tbl_ogrenci.Find(id);
            y.ad = txtad.Text;
            y.soyad = txtsoyad.Text;
            db.SaveChanges();
            MessageBox.Show("Değişiklikler Kaydedildi");
        }

        private void btnpro_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.notlistesi(); 
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Tbl_ogrenci.Where(x => x.ad == txtad.Text | x.soyad == txtsoyad.Text).ToList();
        }

        private void txtad_TextChanged(object sender, EventArgs e)
        {
            string girilendeger = txtad.Text;
            var degerler = from item in db.Tbl_ogrenci
                           where item.ad.Contains(girilendeger)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void btnsirala_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                List<Tbl_ogrenci> liste1 = db.Tbl_ogrenci.OrderBy(p => p.ad).ToList();
                dataGridView1.DataSource = liste1;
                  // list<1,2,3,4,5> Sayılar = 1
            }
            if (radioButton2.Checked==true)
            {
                List<Tbl_ogrenci> liste2 = db.Tbl_ogrenci.OrderByDescending(p => p.ad).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked==true)
            {
                List<Tbl_ogrenci> liste3 = db.Tbl_ogrenci.OrderBy(p => p.ad).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            if (radioButton4.Checked==true)
            {
                List<Tbl_ogrenci> liste4 = db.Tbl_ogrenci.Where(p => p.ad.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked==true)
            {
                List<Tbl_ogrenci> liste5 = db.Tbl_ogrenci.Where(p => p.ad.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }
            if (radioButton6.Checked==true)
            {
                bool varmı = db.Tbl_ogrenci.Any();
                MessageBox.Show(varmı.ToString(), "değer var mı yoksa yok mu", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
            }
            if (radioButton7.Checked==true)
            {
                int toplam = db.Tbl_ogrenci.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton8.Checked==true)
            {
                var toplam = db.Tbl_notlar.Sum(p => p.sınav1);
                MessageBox.Show(toplam.ToString(),"sınav notları toplamı");
            }
            if (radioButton9.Checked==true)
            {
                var ortalama = db.Tbl_notlar.Average(p => p.sınav1);
                MessageBox.Show(ortalama.ToString());
            }
            if (radioButton10.Checked==true)
            {
                var ortalama = db.Tbl_notlar.Average(p => p.sınav1);
                List<Tbl_notlar> liste6 = db.Tbl_notlar.Where(p => p.sınav1 > ortalama).ToList();
                dataGridView1.DataSource = liste6;
            }
            //Sınav notu 50den küçük olanlar
            if (radioButton11.Checked == true)
            {
                var not = db.Tbl_notlar.Where(x => x.sınav1 < 50);
                dataGridView1.DataSource = not.ToList();
            }
            //Adı emre olanlar
            if (radioButton18.Checked == true)
            {
                var isim = db.Tbl_ogrenci.Where(x => x.ad == "Emre");
                dataGridView1.DataSource = isim.ToList();
            }
            //adı ve soyadı textboxtan alanlar
            if (radioButton12.Checked == true)
            {
                var isimler = db.Tbl_ogrenci.Where(x => x.ad == textBox1.Text || x.soyad == textBox1.Text);
                dataGridView1.DataSource = isimler.ToList();
            }
            //Soyadlar
            if (radioButton13.Checked == true)
            {
                var soyadı = db.Tbl_ogrenci.Select(x => new { soyad = x.soyad });
                dataGridView1.DataSource = soyadı.ToList();
            }
            //ad büyük soyad küçük
            //emre hariç olanlar
            if (radioButton14.Checked == true)
            {
                var adsoyad = db.Tbl_ogrenci.Select(x => new
                {
                    adi = x.ad.ToUpper(),
                    soyadi = x.soyad.ToLower()
                }).Where(x => x.adi != "emre");
                dataGridView1.DataSource = adsoyad.ToList();
            }
            //ortalama
            if (radioButton15.Checked == true)
            {
                var ort = db.Tbl_notlar.Select(x => new
                {
                    Öğrenci_Adı = x.ogr,
                    Ortalaması = x.ortalama,
                    Durumu = x.durum == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = ort.ToList();
            }
            if (radioButton16.Checked == true)
            {
                var birlestir = db.Tbl_notlar.SelectMany(x => db.Tbl_ogrenci.Where(y => y.id == x.ogr), (x, y) => new
                {
                    adı = y.ad,
                    ortalaması = x.ortalama
                });
                dataGridView1.DataSource = birlestir.ToList();
            }
            if (radioButton17.Checked == true)
            {
                var atla = db.Tbl_ogrenci.OrderBy(x => x.ad).Skip(3);
                dataGridView1.DataSource = atla.ToList();
            }






        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void emeğGeçenlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("             İsmail Aktaş \n Tarafından Geliştirilmiştir.\n\n                  2019", "Geliştirciler");
        }
    }
}

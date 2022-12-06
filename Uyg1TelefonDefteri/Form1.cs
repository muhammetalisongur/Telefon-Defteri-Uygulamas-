using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uyg1TelefonDefteri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet ulkeler = DBIslemleri.UlkeleriCek();
            comboBox1.DisplayMember = "UlkeAdi"; // bize gosterecegi kisim
            comboBox1.ValueMember = "UlkeID";// degerini alacagi kisim
            comboBox1.DataSource = ulkeler.Tables[0];//ulkeler ismi tables [0] tabloyu attik
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ulkeID = (int)comboBox1.SelectedValue;
            DataSet sehirlerDS=DBIslemleri.SehirleriCek(ulkeID);
            comboBox2.DisplayMember = "Sehir";
            comboBox2.ValueMember = "SehirID";
            comboBox2.DataSource = sehirlerDS.Tables[0];


            
                /*DataSet sehirler = DBIslemleri.SehirleriCek();
            comboBox1.DisplayMember = "SehirAdi";
            comboBox1.ValueMember = "SehirID";
            comboBox1.DataSource = sehirler.Tables[0];*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adi = textBox1.Text;
            string soyadi=textBox2.Text;
            string telefon = textBox3.Text;
            int sid = (int)comboBox2.SelectedValue;
            string adres = textBox4.Text;

            DBIslemleri.kisiEkle(adi, soyadi, telefon, sid, adres);
            MessageBox.Show("Kisi Eklendi");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            DataSet sonuclar = DBIslemleri.Arama(textBox5.Text);
            dataGridView1.DataSource = sonuclar.Tables[0];
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)//secili satir var mi?
            {
                int kid = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // cell 0 hucre
                textBox8.Text= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)//secili satir var mi?
            {
                int kid = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                string yeniAd = textBox8.Text;
                string yeniSoyad = textBox7.Text;
                string yeniTelefon = textBox9.Text;
                string yeniAdres = textBox6.Text;

                DBIslemleri.Guncelle(yeniAd, yeniSoyad, yeniTelefon, yeniAdres,kid);
                MessageBox.Show("Guncellendi");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)//secili satir var mi?
            {
                int kid = (int)dataGridView1.SelectedRows[0].Cells[0].Value;

                DBIslemleri.Sil(kid);
                MessageBox.Show("Silindi");

            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Uyg1TelefonDefteri
{
    internal class DBIslemleri
    {
        static string baglantiYolu = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\source\repos\Programlama II\hafta13\Uyg1TelefonDefteri\Uyg1TelefonDefteri\database\TelefonDefteri.mdf;Integrated Security=True;Connect Timeout=30";
        //kullanacagimiz veritabanina sag tikladik properties dedik oradan Connection string deki yaziyi tum ekledik
        static SqlConnection baglanti = new SqlConnection(baglantiYolu);
        //sql connection veritabanina baglanti acmasi icin
        public static DataSet UlkeleriCek()
        {
            string sql = "Select * from Ulkeler";
            SqlCommand komutNesnesi = new SqlCommand();//sql tanimlamasi icin komutnesnsi tanimladik
            komutNesnesi.CommandText = sql;
            komutNesnesi.Connection = baglanti;


            DataSet ulkelerDS = new DataSet();//donen sql komutlari tutmak icin dataset tanimladik
            SqlDataAdapter adaptor = new SqlDataAdapter();//dataset dogrudan baglanti yapamiyoruz bunu adaptor ile sagladik
            adaptor.SelectCommand = komutNesnesi; //adaptorun selectCommandina eslestirdik

            baglanti.Open();
            adaptor.Fill(ulkelerDS); //kayitlari datasete yuklemesine sagladik
            baglanti.Close();

            return ulkelerDS;

        }

        public static DataSet SehirleriCek(int ulkeID)
        {
            string sql = "select *from Sehirler where UlkeID=" + ulkeID;
            SqlCommand komutNesnesi = new SqlCommand(sql, baglanti);

            DataSet sonuc = new DataSet();
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);

            baglanti.Open();
            adaptor.Fill(sonuc);
            baglanti.Close();

            return sonuc;


        }


        public static void kisiEkle(string a, string s, string t, int sid, string adr)
        {
            //insert update delete yapar iken adaptor ve dataset ihtiyac yok
            // string sql = "insert into Kisiler values ('" + a + "','" + s + "','" + t + "'," + sid + ",'" + adr + "')";
            string sql = "insert into Kisiler values (@pa,@ps,@pt,@psid,@padr)";
            SqlCommand komutNesnesi = new SqlCommand(sql, baglanti);
            komutNesnesi.Parameters.AddWithValue("@pa", a);
            komutNesnesi.Parameters.AddWithValue("@ps", s);
            komutNesnesi.Parameters.AddWithValue("@pt", t);
            komutNesnesi.Parameters.AddWithValue("@psid", sid);
            komutNesnesi.Parameters.AddWithValue("@padr", adr);

            baglanti.Open();
            komutNesnesi.ExecuteNonQuery();
            baglanti.Close();
        }

        public static DataSet Arama(string ad)
        {
            string sql = "select * from Kisiler where Adi like @pa+'%'";// sonu ne olursa olsun % ifadesi
            SqlCommand komutNesnesi = new SqlCommand(sql, baglanti);
            komutNesnesi.Parameters.AddWithValue("@pa", ad);
            SqlDataAdapter adaptor = new SqlDataAdapter(komutNesnesi);
            DataSet sonuc = new DataSet();
            baglanti.Open();
            adaptor.Fill(sonuc);
            baglanti.Close();
            return sonuc;
        }

        public static void Guncelle(string a, string s, string t, string adr, int kid)
        {
            string sql = "Update Kisiler set Adi=@pa,Asoyadi=@ps,Adres=@padr, Telefon=@pt where KisiID=@pkid";
            SqlCommand komutNesnesi = new SqlCommand(sql, baglanti);
            komutNesnesi.Parameters.AddWithValue("@pa", a);
            komutNesnesi.Parameters.AddWithValue("@ps", s);
            komutNesnesi.Parameters.AddWithValue("@padr", adr);
            komutNesnesi.Parameters.AddWithValue("@pt", t);
            komutNesnesi.Parameters.AddWithValue("@pkid", kid);

            baglanti.Open();
            komutNesnesi.ExecuteNonQuery();
            baglanti.Close();
        }

        public static void Sil(int kid)
        {
            string sql = "delete from Kisiler where KisiID=" + kid;
            SqlCommand komutNesnesi = new SqlCommand(sql, baglanti);

            baglanti.Open();
            komutNesnesi.ExecuteNonQuery();
            baglanti.Close();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OtoPark_Otomasyon_Sistemi
{
    public partial class gecmis : Form
    {
        public gecmis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-KH982DGC;Initial Catalog=otopakk;Integrated Security=True");

        DataTable tablo = new DataTable();

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        //arama
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            tablo.Clear();
            SqlCommand adap = new SqlCommand("select * from gecmis where plaka like '%" + textBox1.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(adap);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }


        //verileri tabloda gösterme
        private void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("Select * from gecmis");

        }

      

        //listede üstüne basınca textboxda yerine gelmesi 

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            string plaka = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string isim = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
            string soyisim = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            string marka = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
            string model = dataGridView1.Rows[secilialan].Cells[4].Value.ToString();


            textBox2.Text = plaka;
            textBox3.Text = isim;
            textBox4.Text = soyisim;
            textBox5.Text = marka;
            textBox6.Text = model;

        }


        //update
        private void button3_Click(object sender, EventArgs e)
        {
            //müşteri tablosunu güncelle
            Kullanıcı_Girişi.baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Insert Into musteri (marka,model,plaka, adi,soyadi) Values ('" + textBox2.Text + "','" + textBox3.Text +"','"+textBox1+ "','" + textBox4.Text + "','" + textBox5.Text + "')", Kullanıcı_Girişi.baglanti);
            komut2.ExecuteNonQuery();
            Kullanıcı_Girişi.baglanti.Close();
            


            //geçmiş tablsonu güncelleme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update gecmis set adi='" + textBox3.Text + "',soyadi='" + textBox4.Text + "',marka='" + textBox5.Text + "',model='"+textBox6.Text+ "' where plaka='" + textBox2.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            verilerigoster("select * from gecmis");
            baglanti.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodPC
{
    public partial class FrmMenuDuzenle : Form
    {
        public FrmMenuDuzenle()
        {
            InitializeComponent();
        }
        int id = 0;

        static string baglanti = "Data Source=DESKTOP-U8C1Q8J\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True";
        //Uzaktan Bağlantı yapılacağı zaman baglanti değişkenini alttaki ile değiştirin
        //Data Source=192.168.62.135;Initial Catalog=Otomasyon;Persist Security Info=True;User ID=meric;Password=123456789
        SqlConnection conn = new SqlConnection(baglanti);
        AdminPanel admpanel = (AdminPanel)Application.OpenForms[(("AdminPanel"))];
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            admpanel.timer1.Start();
            admpanel.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Uygulamayı kapatmak İstediğinden emin misin?", "Fast Food", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }
        private void Yenile()
        {
            listView1.Items.Clear();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand komut = new SqlCommand("select * from Employee_Menu", conn);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = oku["id"].ToString();
                    ekle.SubItems.Add(oku["MenuName"].ToString());
                    ekle.SubItems.Add(oku["Price"].ToString());
                    listView1.Items.Add(ekle);
                }
                conn.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }

        }
        private void FrmMenuDuzenle_Load(object sender, EventArgs e)
        {
            Yenile();
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string kayit = "insert into Employee_Menu (MenuName,Price) values(@menuname,@price)";
                SqlCommand komut = new SqlCommand(kayit, conn);

                komut.Parameters.AddWithValue("@menuname", txtOlusturMenuAdı.Text);

                komut.Parameters.AddWithValue("@price", txtOlusturMenuFiyat.Text);

                komut.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Başarıyla Kayıt Yapıldı.");
                Yenile();
                txtOlusturMenuAdı.Clear();
                txtOlusturMenuFiyat.Clear();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand komut = new SqlCommand("select * from Employee_Menu where MenuName like '%" + txtAra.Text + "%'", conn);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = oku["id"].ToString();
                    ekle.SubItems.Add(oku["MenuName"].ToString());
                    ekle.SubItems.Add(oku["Price"].ToString());
                    listView1.Items.Add(ekle);
                }
                conn.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand komut = new SqlCommand("update Employee_Menu set MenuName='" + txtMenuAdi.Text + "',Price='" + txtFiyat.Text + "'where id=" + id + "", conn);

                komut.ExecuteNonQuery();
                conn.Close();
                Yenile();
                MessageBox.Show("Veriler Başarıyla Güncellendi");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            txtMenuAdi.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtFiyat.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sil = "Delete from Employee_Menu where id=@id";
                SqlCommand komut = new SqlCommand(sil, conn);
                komut.Parameters.AddWithValue("@id", listView1.SelectedItems[0].SubItems[0].Text);

                komut.ExecuteNonQuery();
                conn.Close();
                Yenile();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace FastFoodPC
{
    public partial class FrmCalisanYonet : Form
    {
        public FrmCalisanYonet()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);
        static string baglanti = "Data Source=DESKTOP-U8C1Q8J\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True";
        SqlConnection conn = new SqlConnection(baglanti);

        AdminPanel admpanel = (AdminPanel)Application.OpenForms[(("AdminPanel"))];

        int id = 0;
        private void Yenile()
        {
            listView1.Items.Clear();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand komut = new SqlCommand("select * from Employee_Table", conn);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = oku["id"].ToString();
                    ekle.SubItems.Add(oku["kullanici_adi"].ToString());
                    ekle.SubItems.Add(oku["sifre"].ToString());
                    listView1.Items.Add(ekle);
                }
                conn.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }

        }

        private void Ara()
        {
            listView1.Items.Clear();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand komut = new SqlCommand("select * from Employee_Table where Kullanici_adi like '%" + txtAra.Text + "%'", conn);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = oku["id"].ToString();
                    ekle.SubItems.Add(oku["Kullanici_adi"].ToString());
                    ekle.SubItems.Add(oku["Sifre"].ToString());
                    listView1.Items.Add(ekle);
                }
                conn.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }

        private void Kaydet()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand komut = new SqlCommand("update Employee_Table set Kullanici_adi='" + txtKullanici.Text + "',Sifre='" + txtSifre.Text + "'where id=" + id + "", conn);

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

        private void Sil()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sil = "Delete from Employee_Table where id=@id";
                SqlCommand komut = new SqlCommand(sil, conn);
                komut.Parameters.AddWithValue("@id", listView1.SelectedItems[0].SubItems[0].Text);

                komut.Parameters.AddWithValue("@kullanici_adi", listView1.SelectedItems[0].SubItems[1].Text);

                komut.Parameters.AddWithValue("@sifre", listView1.SelectedItems[0].SubItems[2].Text);

                komut.ExecuteNonQuery();
                conn.Close();
                Yenile();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }
        private void FrmCalisanYonet_Load(object sender, EventArgs e)
        {
            Yenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sil();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txt_kayit_kullanici.Text == "")
            {
                MessageBox.Show("Kullanıcı Adı Boş Bırakılamaz!");
                return;
            }
            if (txt_kayit_sifre.Text == "")
            {
                MessageBox.Show("Şifre Boş Bırakılamaz!");
                return;
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string kayit = "insert into Employee_Table (kullanici_adi,sifre) values(@kullanici_adi,@sifre)";
                SqlCommand komut = new SqlCommand(kayit, conn);

                komut.Parameters.AddWithValue("@kullanici_adi", txt_kayit_kullanici.Text);

                komut.Parameters.AddWithValue("@sifre", txt_kayit_sifre.Text);

                komut.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Başarıyla Kayıt Yapıldı.");
                Yenile();
                txtKullanici.Text = "";
                txtSifre.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Yenile();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            admpanel.timer1.Start();
            admpanel.Show();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            txtKullanici.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtSifre.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnAra_Click_1(object sender, EventArgs e)
        {
            Ara();
        }

        private void btnKayitGizle_Click(object sender, EventArgs e)
        {
            if (txtSifre.PasswordChar == '*')
            {
                button6.BringToFront();
                txtSifre.PasswordChar = '\0';
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtSifre.PasswordChar == '\0')
            {
                btnKayitGizle.BringToFront();
                txtSifre.PasswordChar = '*';
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txt_kayit_sifre.PasswordChar == '*')
            {
                button8.BringToFront();
                txt_kayit_sifre.PasswordChar = '\0';
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txt_kayit_sifre.PasswordChar == '\0')
            {
                button7.BringToFront();
                txt_kayit_sifre.PasswordChar = '*';
            }
        }
    }
}

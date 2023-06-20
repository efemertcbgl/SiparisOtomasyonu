using FastFoodPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodEmployee
{
    public partial class Form1 : Form
    {
        string defaultisim = "Kullanıcı Adı Girin";

        string defaultsifre = "Şifre Girin";
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);
        static string baglanti = "Data Source=DESKTOP-U8C1Q8J\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True";
        //Uzaktan Bağlantı yapılacağı zaman baglanti değişkenini alttaki ile değiştirin
        //Data Source=192.168.62.135;Initial Catalog=Otomasyon;Persist Security Info=True;User ID=sa;Password=6Ze_o{Y54q+JN7g
        SqlConnection conn = new SqlConnection(baglanti);

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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtKullanici.ForeColor = System.Drawing.Color.Gray;
            txtSifre.ForeColor = System.Drawing.Color.Gray;
            txtKullanici.Text = defaultisim;
            txtSifre.Text = defaultsifre;
        }

        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtKullanici.Text == defaultisim)
            {
                MessageBox.Show("Kullanıcı Adı Boş Bırakılamaz!");
                return;
            }
            if (txtSifre.Text == defaultsifre)
            {
                MessageBox.Show("Şifre Boş Bırakılamaz!");
                return;
            }
            try
            {
                string sorgu = "SELECT * FROM Employee_Table where kullanici_adi=@user AND sifre=@pass";
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.Parameters.AddWithValue("@user", txtKullanici.Text);

                komut.Parameters.AddWithValue("@pass", txtSifre.Text);

                conn.Open();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    KullaniciClass.Name = txtKullanici.Text;
                    EmployeePanel panel = new EmployeePanel();
                    panel.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
                }
                conn.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }

        private void btnKayitGizle_Click(object sender, EventArgs e)
        {
            if (txtSifre.PasswordChar == '*')
            {
                btnSakla.BringToFront();
                txtSifre.PasswordChar = '\0';
            }
        }

        private void btnSakla_Click(object sender, EventArgs e)
        {
            if (txtSifre.PasswordChar == '\0')
            {
                btnKayitGizle.BringToFront();
                txtSifre.PasswordChar = '*';
            }
        }

        private void txtKullanici_Enter_1(object sender, EventArgs e)
        {
            txtKullanici.ForeColor = System.Drawing.Color.Black;
            if (txtKullanici.Text == defaultisim)
            {
                txtKullanici.Text = "";
            }
        }

        private void txtKullanici_Leave_1(object sender, EventArgs e)
        {
            if (txtKullanici.Text == "")
            {
                txtKullanici.Text = defaultisim;
                txtKullanici.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void txtSifre_Enter_1(object sender, EventArgs e)
        {
            txtSifre.ForeColor = System.Drawing.Color.Black;
            if (txtSifre.Text == defaultsifre)
            {
                txtSifre.Text = "";

            }
        }

        private void txtSifre_Leave(object sender, EventArgs e)
        {
            if (txtSifre.Text == "")
            {
                txtSifre.Text = defaultsifre;
                txtSifre.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label8_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
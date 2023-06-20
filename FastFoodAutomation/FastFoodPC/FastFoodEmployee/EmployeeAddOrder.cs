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
    public partial class EmployeeAddOrder : Form
    {
        public EmployeeAddOrder()
        {
            InitializeComponent();
        }
        int id = 0;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);
        static string baglanti = "Data Source=DESKTOP-U8C1Q8J\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True";
        static string baglantiMobil = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodAppDb";
        //Uzaktan Bağlantı yapılacağı zaman baglanti değişkenini alttaki ile değiştirin
        //Data Source=192.168.62.135;Initial Catalog=Otomasyon;Persist Security Info=True;User ID=sa;Password=6Ze_o{Y54q+JN7g
        SqlConnection conn = new SqlConnection(baglanti);
        SqlConnection connn = new SqlConnection(baglantiMobil);

        EmployeePanel employeepanel = (EmployeePanel)Application.OpenForms[(("EmployeePanel"))];
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
        private void EmployeeAddOrder_Load(object sender, EventArgs e)
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
            employeepanel.Show();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = id.ToString();
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand komut = new SqlCommand("insert into Cooking_Status (MenuAdi,Durum) values(@menuadi,@durum)", conn);
                komut.Parameters.AddWithValue("@menuadi", textBox2.Text);
                komut.Parameters.AddWithValue("@durum", "Sipariş Alındı");
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Sipariş Başarıyla Oluşturuldu.");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                id = 0;
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

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

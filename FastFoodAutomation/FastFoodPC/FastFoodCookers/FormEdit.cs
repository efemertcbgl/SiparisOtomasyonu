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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FastFoodCookers
{
    public partial class FormEdit : Form
    {
        public FormEdit()
        {
            InitializeComponent();
        }
        static string baglanti = "Data Source=DESKTOP-U8C1Q8J\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True;MultipleActiveResultSets=True";
        //Uzaktan Bağlantı yapılacağı zaman baglanti değişkenini alttaki ile değiştirin
        //Data Source=192.168.62.135;Initial Catalog=Otomasyon;Persist Security Info=True;User ID=sa;Password=6Ze_o{Y54q+JN7g
        SqlConnection conn = new SqlConnection(baglanti);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand komut = new SqlCommand("UPDATE Cooking_Status SET MenuAdi = @menuadi, Durum = @durum WHERE id = @id", conn);
                komut.Parameters.AddWithValue("@menuadi", textBox2.Text);
                komut.Parameters.AddWithValue("@durum", comboBox1.Text);
                komut.Parameters.AddWithValue("@id", textBox1.Text);
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Sipariş Başarıyla Düzenlendi.");
                this.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand komut = new SqlCommand("UPDATE Cooking_Status SET MenuAdi = @menuadi, Durum = @durum WHERE id = @id", conn);
                komut.Parameters.AddWithValue("@menuadi", textBox2.Text);
                komut.Parameters.AddWithValue("@durum", comboBox1.Text);
                komut.Parameters.AddWithValue("@id", textBox1.Text);
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Sipariş Başarıyla Düzenlendi.");
                this.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message);
            }
        }
    }
}

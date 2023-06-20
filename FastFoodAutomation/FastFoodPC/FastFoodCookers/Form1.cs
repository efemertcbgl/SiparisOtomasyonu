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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int gec = 0;
        static string baglanti = "Data Source=DESKTOP-U8C1Q8J\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True;MultipleActiveResultSets=True";
        static string Mobilbaglanti = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FoodAppDb";
        //Uzaktan Bağlantı yapılacağı zaman baglanti değişkenini alttaki ile değiştirin
        //Data Source=192.168.62.135;Initial Catalog=Otomasyon;Persist Security Info=True;User ID=sa;Password=6Ze_o{Y54q+JN7g
        SqlConnection conn = new SqlConnection(baglanti);
        SqlConnection connn = new SqlConnection(Mobilbaglanti);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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
        private void Yenile()
        {
            listView1.Items.Clear(); // listView1'in tüm öğelerini siler

            if (conn.State == ConnectionState.Closed) // eğer bağlantı kapalıysa
            {
                conn.Open(); // bağlantıyı aç
            }

            try
            {
                SqlCommand komut = new SqlCommand("select * from Cooking_Status", conn); // SQL sorgusu oluştur
                SqlDataReader oku = komut.ExecuteReader(); // SQL sorgusunu çalıştır ve sonuçları oku
                while (oku.Read()) // sonuçlarda her kayıt için döngü başlat
                {
                    if (oku["Durum"].ToString() == "Hazır") // eğer kaydın Durum sütunu "Hazır" ise
                    {
                        ListViewItem ekle = new ListViewItem(); // yeni bir ListViewItem öğesi oluştur
                        ekle.Text = oku["id"].ToString(); // ListViewItem'in ilk sütunu "id" değerine eşitle
                        ekle.SubItems.Add(oku["MenuAdi"].ToString()); // ListViewItem'in ikinci sütunu "MenuAdi" değerine eşitle
                        ekle.SubItems.Add(oku["Durum"].ToString()); // ListViewItem'in üçüncü sütunu "Durum" değerine eşitle
                        listView1.Items.Add(ekle); // ListView'e yeni ListViewItem öğesini ekle
                        timerYenile.Stop(); // timerYenile'ı durdur
                        timer1.Start(); // timer1'ı başlat
                    }
                    else if (oku["Durum"].ToString() != "Hazır") // eğer kaydın Durum sütunu "Hazır" değilse
                    {
                        ListViewItem ekle = new ListViewItem(); // yeni bir ListViewItem öğesi oluştur
                        ekle.Text = oku["id"].ToString(); // ListViewItem'in ilk sütunu "id" değerine eşitle
                        ekle.SubItems.Add(oku["MenuAdi"].ToString()); // ListViewItem'in ikinci sütunu "MenuAdi" değerine eşitle
                        ekle.SubItems.Add(oku["Durum"].ToString()); // ListViewItem'in üçüncü sütunu "Durum" değerine eşitle
                        listView1.Items.Add(ekle); // ListView'e yeni ListViewItem öğesini ekle
                    }
                }
                oku.Close(); // SqlDataReader'ı kapat
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir sorun oluştu " + hata.Message); // hata mesajı göster
            }
            finally // try bloğundaki kod çalıştıktan sonra her durumda çalışacak kod bloğu
            {
                if (conn.State == ConnectionState.Open) // eğer bağlantı açıksa
                {
                    conn.Close(); // bağlantıyı kapat
                }
            }
        }

        private void timerYenile_Tick(object sender, EventArgs e)
        {
            Yenile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timerYenile.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)//Önce mevcut formun "listView1" öğesinde seçili öğe var mı diye kontrol ediyoruz.
            {
                FormEdit form = new FormEdit();//Seçili öğe varsa, "FormEdit" formunu açıyoruz ve seçili öğenin alt öğelerini "FormEdit" formundaki ilgili öğelere yükleyebiliyoruz.
                form.textBox1.Text = this.listView1.SelectedItems[0].SubItems[0].Text;
                form.textBox2.Text = this.listView1.SelectedItems[0].SubItems[1].Text;
                form.comboBox1.Text = this.listView1.SelectedItems[0].SubItems[2].Text;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem selected = this.listView1.SelectedItems[0];
                    selected.SubItems[0].Text = form.textBox1.Text;
                    selected.SubItems[1].Text = form.textBox2.Text;
                    selected.SubItems[2].Text = form.comboBox1.Text;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gec == 0)
            {
                gec ++;
            }
            else if (gec == 1) 
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                // Durumu "Hazır" olan kayıtları silme
                SqlCommand silKomutu = new SqlCommand("delete from Cooking_Status where Durum='Hazır'", conn);
                silKomutu.ExecuteNonQuery();
                conn.Close();
                gec = 0;
                timer1.Stop();
                timerYenile.Start();
            }
            else 
            {
                gec = 0;
            }
        }
    }
}

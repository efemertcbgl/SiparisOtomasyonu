using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodPC
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            label3.Text = KullaniciClass.Name;
            timer1.Start();
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSaat.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Stop();
            FrmCalisanYonet cy = new FrmCalisanYonet();
            cy.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Stop();
            FrmMenuDuzenle cy = new FrmMenuDuzenle();
            cy.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }
    }
}

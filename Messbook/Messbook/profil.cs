using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Messbook
{
    public partial class profil : Form
    {
        public profil()
        {
            InitializeComponent();
        }
        SqlConnection connectt = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        string imagess;
        private void Profill_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Profil Edit";
            openFileDialog1.Filter = "Jpeg Dosyaları (*.jpeg)|*.jpeg| Jpg Dosyaları (*.jpg)|*.jpg| Png Dosyaları(*.png)|*.png| Gif Dosyaları(*.gif)|*gif| Tif Dosyaları(*.tif)|*.tif ";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                imagess = openFileDialog1.FileName;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            connectt.Open();
            if (imagess != null)
            {
                FileStream fileStream = new FileStream(imagess, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                byte[] resim = binaryReader.ReadBytes((int)fileStream.Length);
                binaryReader.Close();
                fileStream.Close();

                SqlCommand komut = new SqlCommand("Select *from Messenbok_regihstirasyon  where username=@p1", connectt);
                komut.Parameters.AddWithValue("@p1", Cryptolojy.Encryption(Logen.loginname, 3));
                SqlDataReader reader = komut.ExecuteReader();
                if (reader.Read())
                {
                    if (Logen.loginname == Cryptolojy.Decryption(reader["username"].ToString().TrimEnd(), 3))
                    {
                        reader.Close();
                        SqlCommand cod = new SqlCommand("Update Messenbok_regihstirasyon set poto=@images,imaj=@foto where username=@ad", connectt);
                        cod.Parameters.AddWithValue("@ad", Cryptolojy.Encryption(Logen.loginname, 3));
                        cod.Parameters.AddWithValue("@foto", "true");
                        cod.Parameters.Add("@images", SqlDbType.Image, resim.Length).Value = resim;
                        cod.ExecuteNonQuery();
                        connectt.Close();
                        MessageBox.Show("katyıt yapıldı");
                        connectt.Close();
                    }

                }

            }

            connectt.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu mu = new Menu();
            mu.Show();
            this.Close();
        }
 bool move;
        int mouse_x, mouse_y;
        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);

            }
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }
       
        private void liveModToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

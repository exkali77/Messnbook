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
    public partial class forgotpassword : Form
    {
        public forgotpassword()
        {
            InitializeComponent();
        }
        public static SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");

        private void forgotpassword_Load(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            textBox3.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            button3.Visible = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        bool move;
        int mouse_x, mouse_y;
        private void button1_Click(object sender, EventArgs e)
        {
            connet.Open();
            SqlCommand cl = new SqlCommand("Select *from  Messenbok_regihstirasyon where Gmail=@p1 ", connet);
            cl.Parameters.AddWithValue("@p1", Cryptolojy.Encryption(textBox1.Text, 3));
            SqlDataReader reader = cl.ExecuteReader();
               if (reader.Read())
               {
                    button1.Visible = false;
                    button3.Visible = true;
                textBox1.Visible = false;
                textBox2.Visible = true;
                textBox3.Visible = true;
                label1.Visible = false;
                label2.Visible = true;
                label3.Visible = true;
                label4.ResetText();
                   
               }
               else
               {
                label4.Text = "Böylebir Gmail yok";
               }
                
            
          connet.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login_Up login_ = new Login_Up();
            this.Hide();
            login_.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            connet.Open();
            SqlCommand cod = new SqlCommand("Update Messenbok_regihstirasyon set pass=@pss1,s_pass=@pss2 where Gmail=@gm", connet);
            cod.Parameters.AddWithValue("@gm", Cryptolojy.Encryption(textBox1.Text,3));
            cod.Parameters.AddWithValue("@pss1", Cryptolojy.Encryption(textBox2.Text, 3));
            cod.Parameters.AddWithValue("@pss2", Cryptolojy.Encryption(textBox3.Text, 3));
            cod.ExecuteNonQuery();
            connet.Close();
            label4.Text = "Şifre Değiştirildi";
            textBox2.ResetText();
            textBox3.ResetText();



        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
             move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);

            }
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {

            move = false;
           
        }
    }
}

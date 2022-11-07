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
using System.IO;

namespace Messbook
{
    public partial class createaccount : Form
    {
        public createaccount()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool move;
        int mouse_x, mouse_y;
            private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            Login_Up lu = new Login_Up();
            lu.Show();
            this.Hide();
            }
        bool istrh;
        void CretcOpen()
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(textbox4.Text))
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    textBox1.BackColor = Color.Red;
                }
                if (String.IsNullOrEmpty(textBox2.Text))
                {
                    textBox2.BackColor = Color.Red;
                }
                if (String.IsNullOrEmpty(textBox3.Text))
                {
                    textBox3.BackColor = Color.Red;
                }
                if (String.IsNullOrEmpty(textbox4.Text))
                {
                    textbox4.BackColor = Color.Red;
                }
              
                label9.Text = "Kırmızı alanlar boş bırakılamaz";
            }
            else
            {
                connection.Open();
                SqlCommand cl = new SqlCommand("Select *from Messenbok_regihstirasyon where username=@p1 ", connection);
                cl.Parameters.AddWithValue("@p1", Cryptolojy.Encryption(textBox1.Text, 3));

                SqlDataReader reader = cl.ExecuteReader();
                if (reader.Read())
                {
                    
                    label6.Text = "Bu Kulanıcı Adı Zaten Kayıtlı";
                    istrh = false;
                }
                else
                {
                  istrh = true;
                }
            }
           if (textBox3.Text != textbox4.Text)
           {
            
                label8.Text = "Şifre Tekrerını Doğru giriniz";
               
                istrh = false;
           }
           connection.Close();
        }
        bool istren = false;
        void Gmail()
        {
            if (textBox2.Text!="")
            {
               connection.Open();
               SqlCommand cl = new SqlCommand("Select *from  Messenbok_regihstirasyon where Gmail=@p1 ", connection);
               cl.Parameters.AddWithValue("@p1", Cryptolojy.Encryption(textBox2.Text, 3));
              SqlDataReader reader = cl.ExecuteReader();
           
                if (reader.Read())
                {
                istren = false;
                }
                else
                {
                istren = true;
                }   
                 connection.Close();
            }
            else
            {
                label7.Text = "Gmail boş bırakılmaz";
            }
            
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void createaccount_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.Lavender;

        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.Lavender;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.Lavender;
        }

        private void textbox4_Click(object sender, EventArgs e)
        {
            textbox4.BackColor = Color.Lavender;
        }

        private void createaccount_Load(object sender, EventArgs e)
        {

        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            Gmail();
            CretcOpen();
            if (istren==true)
            {
 
            if (istrh==true)
            {
            connection.Open();
                SqlCommand komutt = new SqlCommand("Insert into Messenbok_regihstirasyon (username,Gmail,pass,s_pass,phone,imaj) values('" + Cryptolojy.Encryption(textBox1.Text, 3) + "','" + Cryptolojy.Encryption(textBox2.Text, 3) + "','" + Cryptolojy.Encryption(textBox3.Text, 3) + "','" + Cryptolojy.Encryption(textbox4.Text, 3) + "','" + Cryptolojy.Encryption(textBox5.Text, 3) + "','"+"false"+"')", connection) ;
            komutt.ExecuteNonQuery();
            connection.Close();
                    label9.Text = "Kayıt yapıldı";
                    Texta();
            }

            }
            else
            {
               
                label7.Text = "Gmail zaten kayıtlı";
            }
           
        }
        void Texta()
        {
                     label6.ResetText();
                    label7.ResetText();
                    label8.ResetText();
                    textBox1.ResetText();
                    textBox2.ResetText();
                    textBox3.ResetText();
                    textbox4.ResetText();
                    textBox5.ResetText();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                button3.BringToFront();
                textBox3.PasswordChar = '\0';
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '\0')
            {
                button2.BringToFront();
                textBox3.PasswordChar = '*';
            }
        }
        private void button4_Click(object sender, EventArgs e)
        { 
            if (textbox4.PasswordChar == '*')
            {
                button5.BringToFront();
                textbox4.PasswordChar = '\0';
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (textbox4.PasswordChar == '\0')
            {
                button4.BringToFront();
                textbox4.PasswordChar = '*';
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }
        

    }
}


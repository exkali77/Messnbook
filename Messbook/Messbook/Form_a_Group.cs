using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Messbook
{
    public partial class Form_a_Group : Form
    {
        public Form_a_Group()
        {
            InitializeComponent();
        }
        private static SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool istri;
        private void Grup()
        {
            connet.Open();
            SqlCommand command = new SqlCommand("select *from Messenbok_Form_a_Group", connet);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
               if (textBox1.Text != Cryptolojy.Decryption(reader[0].ToString(),3))
               {
                    istri = true;
                    reader.Close();
                    connet.Close();
                    break;
               }
                else
                {
                    istri = false;
                    reader.Close();
                    connet.Close();
                    break;
                }
            }
           
            connet.Close();


        }
        private void button2_Click(object sender, EventArgs e)
        {
            Grup();
            if (istri==true)
            {
                 if (textBox1.Text!="" &&textBox2.Text!="")
                 {
                  connet.Open();
                  SqlCommand command = new SqlCommand("Insert into Messenbok_Form_a_Group(name,executive,tim,password) values('" +Cryptolojy.Encryption(textBox1.Text,3)+"','"+Cryptolojy.Encryption(Logen.loginname,3)+"','"+DateTime.Now+"','"+Cryptolojy.Encryption(textBox2.Text,3)+"')",connet);
                  command.ExecuteNonQuery();
                  connet.Close();
                 }
                 else
                 {
                   MessageBox.Show("BOŞBIRAKMA");
                 }

            }
            else
            {
                MessageBox.Show("naber");
            }
           
        }
        bool move;
        int mouse_x, mouse_y;
        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
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

   
    }
}

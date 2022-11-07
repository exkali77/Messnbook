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
using System.IO;

namespace Messbook
{
    public partial class Change_Gmail : Form
    {
        public Change_Gmail()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        private void Change_Gmail_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Mess_regihst()
        {
           connection.Open();
            SqlCommand command = new SqlCommand("select *from  Messenbok_regihstirasyon", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {  
                reader.Close();
                SqlCommand command1 = new SqlCommand("Update Messenbok_regihstirasyon set Gmail=@gm where username=@ad", connection);
                command1.Parameters.AddWithValue("@ad", Cryptolojy.Encryption(Logen.loginname, 3));
                command1.Parameters.AddWithValue("@gm", Cryptolojy.Encryption(textBox1.Text, 3));
                command1.ExecuteNonQuery();
                connection.Close();
                label2.Text = "Gmail Changed";
             
            }
        }
      

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select *from  Messenbok_regihstirasyon", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (textBox1.Text!="")
                {
                    if (Cryptolojy.Encryption(textBox1.Text,3) != reader["Gmail"].ToString())
                    {
                      
                        reader.Close();
                    connection.Close();
                        Mess_regihst();

                   
                    }
                    else
                    {
                        reader.Close();
                        connection.Close();
                        label2.Text = "An account already exists with this Gmail";
                    }
             
                }
                else
                {
                    label2.Text = "field cannot be left blank";
                    reader.Close();
                    connection.Close();
                }
               
               
            }
            reader.Close();
            connection.Close();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Change_Profile change_Profile = new Change_Profile();
            this.Hide();
            change_Profile.Show();
        }
        bool move;
        int mouse_x, mouse_y;
      
        

     
        private void button3_Click(object sender, EventArgs e)
        {
            Change_Profile mainMenu = new Change_Profile();
            this.Hide();
            mainMenu.Show();
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

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }
    }
}

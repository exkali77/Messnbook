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
    public partial class Member_Password_Field : Form
    {
        public Member_Password_Field()
        {
            InitializeComponent();
        }
        private SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        
        private void join()
        {
           
            connet.Open();
            SqlCommand command = new SqlCommand("Insert into Messenbok_member(name,tim,grupname) values('" + Cryptolojy.Encryption(Logen.loginname, 3) + "','" + DateTime.Now + "')", connet);
            command.ExecuteNonQuery();
            connet.Close();
            this.Close();

        } 
       
         Browse_Groups browse = new Browse_Groups();
        private void button2_Click(object sender, EventArgs e)
        {                            
         
                connet.Open();
                SqlCommand command = new SqlCommand("Select *from Messenbok_Form_a_Group where  password='" + Cryptolojy.Encryption(textBox1.Text, 3) + "'", connet);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
              
                join();
                }
                else
                {
                    MessageBox.Show("so");
                }
                connet.Close();
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

        private void button5_Click(object sender, EventArgs e)
        {
             if (textBox1.PasswordChar=='*')
            {
                button6.BringToFront();
                textBox1.PasswordChar = '\0';
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
             if (textBox1.PasswordChar=='\0')
            {
                button5.BringToFront();
                textBox1.PasswordChar='*';
            }
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }
    }
}

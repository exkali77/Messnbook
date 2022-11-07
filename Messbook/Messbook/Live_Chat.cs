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
    public partial class Live_Chat : Form
    {
        public Live_Chat()
        {
           
           
            InitializeComponent();
        }
        public static SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool move;
        int mouse_x, mouse_y;
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
        private void Live_Chat_Load(object sender, EventArgs e)
        {
            Chat();
            timer1.Start();
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
          move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text!="")
            {
                if(e.KeyCode==Keys.Enter)
                {
                  connet.Open();
                  SqlCommand command = new SqlCommand("Insert into Messenbok_Chat (username,messenbok_caht,tim,chat_name)  values('" + Cryptolojy.Encryption(Logen.loginname,3)+"','"+Cryptolojy.Encryption(textBox1.Text,3)+"','"+DateTime.Now+"','"+Logen.chat+"')",connet);
                    command.ExecuteNonQuery();
                  connet.Close();
                    textBox1.ResetText();
                    Chat();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Live_Chat_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void registeredChatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {    
        }
        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            if (time>=5)
            {
                listBox1.Items.Clear();
                Chat();
            }
        }

        private void Chat()
        {   
            listBox1.Items.Clear();
            connet.Open(); 
      
            SqlCommand command = new SqlCommand("Select *from Messenbok_Chat where chat_name='"+Logen.chat+"'", connet);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                listBox1.Items.Add(Cryptolojy.Decryption(reader[0].ToString(), 3) + ":" + Cryptolojy.Decryption(reader[1].ToString(), 3));
            }
            connet.Close();
          
        }

            
        
    }
}

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
    public partial class Browse_Groups : Form
    {
        public Browse_Groups()
        {
            InitializeComponent();
        }
        private SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        private void Member_Exit()
        {
            string item = listBox1.GetItemText(listBox1.SelectedItem);
            connet.Open();
            SqlCommand command = new SqlCommand("Select *from Messenbok_member where name='"+Cryptolojy.Encryption(Logen.loginname,3)+"'and grupname='" + Cryptolojy.Encryption(item,3)+"'", connet);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
               
                
                button2.Visible = false;
                button4.Visible = true;
            }
            else
            {
                 button2.Visible = true;
                 button4.Visible = false;
            }
            
            connet.Close();

        }
        private void Browse()
        {
            connet.Open();
            SqlCommand command = new SqlCommand("select *from Messenbok_Form_a_Group", connet);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader[0].ToString()!=null)
                {
                    listBox1.Items.Add(Cryptolojy.Decryption( reader[0].ToString(),3));
            
                }
                else
                {
                    listBox1.Items.Add("There are no registered Groups");
             
                }
            }
            connet.Close();
        }
        private void Browse_Groups_Load(object sender, EventArgs e)
        {
            Browse();
            button2.Visible = false;
            button4.Visible = false;
            textBox2.Visible = false;
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
        private void join()
        {
            string item = listBox1.GetItemText(listBox1.SelectedItem);
            connet.Open();
            SqlCommand command = new SqlCommand("Insert into Messenbok_member(name,tim,grupname) values('" + Cryptolojy.Encryption(Logen.loginname, 3) + "','" + DateTime.Now + "','" + Cryptolojy.Encryption(item, 3) + "')", connet);
            command.ExecuteNonQuery();
            connet.Close();
            button2.Visible = false;
            button4.Visible = true;
            textBox2.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        { 
            string item = listBox1.GetItemText(listBox1.SelectedItem);

            connet.Open();
            SqlCommand command = new SqlCommand("Select *from Messenbok_Form_a_Group where name='" + Cryptolojy.Encryption(item, 3) + "'and password='" + Cryptolojy.Encryption(textBox2.Text, 3) + "'", connet);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                connet.Close();
                join();
            }
            else
            {
                MessageBox.Show("so");
            }
            connet.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
          
          
        }
        private void button3_Click(object sender, EventArgs e)
        {
             connet.Open();
             SqlCommand command = new SqlCommand("select *from Messenbok_Form_a_Group where name='"+Cryptolojy.Encryption(textBox1.Text,3)+"'", connet);
             SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(Cryptolojy.Decryption(reader[0].ToString(),3));
            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("There are no registered Groups");
            }
             connet.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
          
            string item = listBox1.GetItemText(listBox1.SelectedItem);
            connet.Open();
            SqlCommand command = new SqlCommand("select *from Messenbok_Form_a_Group where name='"+Cryptolojy.Encryption(item,3)+"'", connet);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                textBox2.Visible=true;
                label5.Text =Cryptolojy.Decryption(reader[0].ToString(),3);
                label2.Text =Cryptolojy.Decryption(reader[1].ToString(),3);
                connet.Close();
               Member_Exit();
            }
            connet.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connet.Open();
            string item = listBox1.GetItemText(listBox1.SelectedItem);
            string searc = "Delete from Messenbok_member where name=@log and grupname=@nog";
            SqlCommand komut = new SqlCommand(searc, connet);
            komut.Parameters.AddWithValue("@log", Cryptolojy.Encryption(item, 3));
            komut.Parameters.AddWithValue("@nog", Cryptolojy.Encryption(Logen.loginname, 3));
            komut.ExecuteNonQuery();
            connet.Close();
            button2.Visible = true;
            button4.Visible = false;
            MessageBox.Show("silindi" + MessageBoxButtons.OK);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }
    }
}

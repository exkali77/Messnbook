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
    public partial class Change_Profile : Form
    {
        public Change_Profile()
        {
            InitializeComponent();
        }
        SqlConnection connectt = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");

        bool move;
        int mouse_x, mouse_y;
  
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        MemoryStream af = new MemoryStream();
        void defualt()
        {
            connectt.Open();
            SqlCommand command = new SqlCommand("Select  *from dfualt_ımaj", connectt);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                byte[] MisDatos = new byte[0];
                MisDatos = (byte[])reader["imaj"];
                MemoryStream aaf = new MemoryStream(MisDatos);
                Logen.memorysrtm = aaf;
                connectt.Close();
                reader.Close();
                af = aaf;
            }
        }
        private void Change_Profile_Load(object sender, EventArgs e)
        {
            connectt.Open();
            SqlCommand command = new SqlCommand("select *from  Messenbok_regihstirasyon where username='"+Cryptolojy.Encryption(Logen.loginname,3)+"'", connectt);
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                label3.Text = Cryptolojy.Decryption(reader[0].ToString(), 3);
                label5.Text = Cryptolojy.Decryption(reader[1].ToString(), 3);
                label6.Text = Cryptolojy.Decryption(reader[4].ToString(), 3);
                if (reader["imaj"].ToString() == "true")
                {
                    byte[] MisDatos = new byte[0];
                    MisDatos = (byte[])reader["poto"];
                    MemoryStream cf = new MemoryStream(MisDatos);
                    connectt.Close();
                    reader.Close();
                    af = cf;
                }
                else
                {
                    connectt.Close();
                    reader.Close();
                    defualt();
                }
            }            
            connectt.Close();
            pictureBox1.Image = Image.FromStream(af);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Change_Gmail change_Username = new Change_Gmail();
            this.Hide();
            change_Username.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Change_Password change_Password = new Change_Password();
            this.Hide();
            change_Password.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Change_Phone_Number change_Phone_Number = new Change_Phone_Number();
            this.Hide();
            change_Phone_Number.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Menu mainMenu = new Menu();
            this.Hide();
            mainMenu.Show();
        }
    }
}

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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        SqlConnection connectt = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        private void messengerModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Messenger_Mod ms = new Messenger_Mod();
            ms.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {   
        }       
        MemoryStream af = new MemoryStream();  
        void defualt()
        {
            connectt.Open();
            SqlCommand command = new SqlCommand("Select  *from dfualt_ımaj",connectt);
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
  
        private void Menu_Load(object sender, EventArgs e)
        {
            connectt.Open();
            SqlCommand command = new SqlCommand("Select  *from Messenbok_regihstirasyon   where username='" + Cryptolojy.Encryption(Logen.loginname, 3) + "'", connectt);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader["imaj"].ToString()=="true")
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

            Logen.memorysrtm = af;
                label2.Text = Logen.loginname;
                label5.Text = Logen.loginmail;
                label3.Text = Logen.loginphone;
                pictureBox1.Image = Image.FromStream(af);
            reader.Close();
            connectt.Close();

        }
    
        private void button1_Click_1(object sender, EventArgs e)
        {
            profil pf = new profil();
            pf.Show();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void pictureBox1_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_Profile change_Profile = new Change_Profile();
            this.Hide();
            change_Profile.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        bool move;
        int mouse_x, mouse_y;
        private void liveModToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
            connectt.Open();
            SqlCommand cod = new SqlCommand("Update Messenbok_regihstirasyon set live=@liva where username=@ad", connectt);
            cod.Parameters.AddWithValue("@ad", Cryptolojy.Encryption(Logen.loginname, 3));
            cod.Parameters.AddWithValue("@liva", "false");
            cod.ExecuteNonQuery();
            connectt.Close();
        }
        private void tiwicModToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            
            Chat_Selection live_Chat = new Chat_Selection ();
            this.Hide();
            live_Chat.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }
    }
}

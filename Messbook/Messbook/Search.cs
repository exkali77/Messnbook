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
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        SqlConnection sqlConnection = Login_Up.connet;
        private  void Buton_viselber()
        {
            sqlConnection.Close();
           
            connet.Open();
            SqlCommand cod = new SqlCommand("Select *from Messagestab_frends where username='" + Cryptolojy.Encryption(Logen.loginname, 3) + "'and frends='"+ Cryptolojy.Encryption(Logen.nolinname, 3) + "'", connet);
            SqlDataReader sqlDataReader = cod.ExecuteReader();
            if (sqlDataReader.Read())
            {
                button1.Visible = false;
                button2.Visible = true;
            }
            else
            {
                button1.Visible = true;
                button2.Visible = false;
            }
            sqlDataReader.Close();
            connet.Close();

        }
        private void Search_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("Select *from Messagestab_frends where username='" + Cryptolojy.Encryption(Logen.nolinname, 3) + "'", sqlConnection);
            SqlDataReader sqlDataReade = command.ExecuteReader();
            if (sqlDataReade.Read())
            {

                listBox1.Items.Add(Cryptolojy.Decryption(sqlDataReade[1].ToString(), 3));
                sqlDataReade.Close();

            }


            Buton_viselber();
            label2.Text = Logen.nolinname;
            label3.Text = Logen.nolingmail;
            //label1.Text = Logen.nopohno;
            pictureBox1.Image = Image.FromStream(Logen.memorysrtmf);
            if (Logen.loginname == Logen.nolinname)
            {
                button1.Visible = false;
                button2.Visible = false;
            }
            sqlConnection.Close();
        }
      
   
        private void button1_Click(object sender, EventArgs e)
        {
            connet.Open();
            SqlCommand komut = new SqlCommand("Insert into Messagestab_frends (username,frends) values('" + Cryptolojy.Encryption(Logen.loginname, 3) + "','" + Cryptolojy.Encryption(Logen.nolinname, 3) + "')", connet);
            komut.ExecuteNonQuery();
            connet.Close(); 
            Butto_ıeabl();
            MessageBox.Show("Eklendi"+MessageBoxButtons.OK);
         ara();
           

        }     
        private   void ara()
        {
            connet.Open();
            SqlCommand command = new SqlCommand("Select *from Messagestab_frends where username='" + Cryptolojy.Encryption(Logen.nolinname, 3) + "'and frends='" + Cryptolojy.Encryption(Logen.loginname, 3) + "'", connet);
            SqlDataReader sqlDataReade = command.ExecuteReader();
            if (sqlDataReade.Read())
            {
               sqlDataReade.Close();
                connet.Close();
            
            }
            else
            {
                sqlDataReade.Close();
                connet.Close();
                Serac_add();
              
            }  
            
        }  
        private void Serac_add()
        {
            connet.Open();
              SqlCommand komut = new SqlCommand("Insert into Messagestab_frends (username,frends) values('" + Cryptolojy.Encryption(Logen.nolinname, 3) + "','" + Cryptolojy.Encryption(Logen.loginname, 3) + "')", connet);
              komut.ExecuteNonQuery();
             connet.Close();
       
     

        }
        private void button2_Click(object sender, EventArgs e)
        {
            connet.Open();
            string searc = "Delete from Messagestab_frends where frends=@log and username=@nog";
            SqlCommand komut = new SqlCommand(searc, connet);
            komut.Parameters.AddWithValue("@log",Cryptolojy.Encryption(Logen.nolinname,3));
            komut.Parameters.AddWithValue("@nog",Cryptolojy.Encryption(Logen.loginname,3));
            komut.ExecuteNonQuery();
            connet.Close();
            Butto_ıeabl();
            MessageBox.Show("silindi" + MessageBoxButtons.OK);
        }
        private void Butto_ıeabl()
        {

            connet.Open();
            SqlCommand cod = new SqlCommand("Select *from Messagestab_frends where username='" + Cryptolojy.Encryption(Logen.loginname, 3) + "'and frends='" + Cryptolojy.Encryption(Logen.nolinname, 3) + "'", connet);
            SqlDataReader sqlDataReader = cod.ExecuteReader();
            if (sqlDataReader.Read())
            {
                button1.Visible = false;
                button2.Visible = true;
            }
            else
            {
                button1.Visible = true;
                button2.Visible = false;
            }
            sqlDataReader.Close();
            connet.Close();

        }
       
        private void Profil_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Messenger_Mod messenger_Mod = new Messenger_Mod();
            this.Hide();
            messenger_Mod.Show();

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
       
        }
}

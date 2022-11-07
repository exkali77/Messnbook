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
using System.Data.Common;
using System.IO;

namespace Messbook
{
    public partial class Messenger_Mod : Form
    {
        public Messenger_Mod()
        {
            InitializeComponent();
        }
        SqlConnection conneti = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        bool isteher;
        private void Messenger_Mod_Load(object sender, EventArgs e)
        { 
            conneti.Open();
            SqlCommand cod = new SqlCommand("Select *from Messagestab_frends", conneti);
            SqlDataReader sqlDataReaderr = cod.ExecuteReader();
            while (sqlDataReaderr.Read())
            {
                if (Cryptolojy.Encryption(Logen.loginname,3)==sqlDataReaderr["username"].ToString())
                {
                  listBox1.Items.Add(Cryptolojy.Decryption(sqlDataReaderr[1].ToString(),3));
                 
                }
               
            }
               sqlDataReaderr.Close();
                conneti.Close(); 
            MemoryStream af = new MemoryStream();
            af = Logen.memorysrtm;
            label4.Text = Logen.loginname;
            label5.Text = Logen.loginmail;
            pictureBox2.Image = Image.FromStream(af);
            sqlDataReaderr.Close();
            conneti.Close();
           
        }
         void defualt()
         {
             conneti.Open();
            SqlCommand command = new SqlCommand("Select  *from dfualt_ımaj", conneti);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            { 
                   byte[] MisDatos = new byte[0];
                   MisDatos = (byte[])reader["imaj"];
                   MemoryStream af = new MemoryStream(MisDatos);
                 
                   Logen.memorysrtmf = af;
            }  
            reader.Close();
            conneti.Close();
         }
        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            string username = search.Text;

            if (e.KeyCode == Keys.Enter)
            {
                conneti.Open();
                SqlCommand komut = new SqlCommand("Select *from Messenbok_regihstirasyon ", conneti);
                SqlDataReader reader = komut.ExecuteReader();
                while(reader.Read())
                {

                    if (Cryptolojy.Encryption(username,3) == reader["username"].ToString())
                    {
                        isteher = true;
                        break; 
                        
                       
                     
                    }
                    else
                    {
                        isteher = false;
                       
                    }
                   
                }
              
                if (isteher)
                {
                    Logen.nolinname = Cryptolojy.Decryption(reader["username"].ToString().TrimEnd(), 3);
                    Logen.nolingmail = Cryptolojy.Decryption(reader["Gmail"].ToString().TrimEnd(), 3);
                    SqlCommand com = new SqlCommand("Select * from Messenbok_regihstirasyon ", conneti);
                    if (reader["imaj"].ToString()== "true")
                    {
                        byte[] MisDatos = new byte[0];
                        MisDatos = (byte[])reader["poto"];
                        MemoryStream af = new MemoryStream(MisDatos);
                        Logen.memorysrtmf = af;
                    }
                    else
                    {
                      conneti.Close();
                      reader.Close();
                        defualt();
                    }
                    Search sh = new Search();
                    sh.Show();
                    conneti.Close();
                }
                else
                {
                    MessageBox.Show("Böylebir username yok", "bilgilendierme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conneti.Close();
                }
            }
        }
        
        private void message_KeyDown(object sender, KeyEventArgs e)
        {
            if (message1.Text == "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    conneti.Open();
                    SqlCommand komut = new SqlCommand("Select *from Messagestab ", conneti);
                    SqlDataReader reader = komut.ExecuteReader();
                    while (reader.Read())
                    {

                        if (isteher == true)
                        {
                            conneti.Open();
                            SqlCommand kod = new SqlCommand("Insert into Messenbok_regihstirasyon (username,time,Friends) values('" + Cryptolojy.Encryption(message1.Text, 3));
                            kod.ExecuteNonQuery();
                            conneti.Close();
                            MessageBox.Show("Kayıt yapıldı", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }


                    }

                }


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu mu = new Menu();
            mu.Show();
            this.Close();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            
            string kulanici = listBox1.GetItemText(listBox1.SelectedItem);
            conneti.Open();
            SqlCommand cod = new SqlCommand("Select *from Messagestab_frends where username='"+Cryptolojy.Encryption(Logen.loginname,3)+"'and  frends='"+Cryptolojy.Encryption(kulanici,3)+"'", conneti);
            SqlDataReader sqlDataReader = cod.ExecuteReader();
            if (sqlDataReader.Read())
            {
                label8.Text = Cryptolojy.Decryption(sqlDataReader["frends"].ToString(), 3);
                 sqlDataReader.Close();
                conneti.Close();
                listBox3.Items.Clear();
                imaj();
                messong();
            }
        }
        void imaj()
        {
            string kulanici = listBox1.GetItemText(listBox1.SelectedItem);
            conneti.Open();
            SqlCommand command = new SqlCommand("Select  *from Messenbok_regihstirasyon  where username='" + Cryptolojy.Encryption(kulanici, 3) + "'", conneti);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader["imaj"].ToString()=="true")
                {
                  byte[] MisDatos = new byte[0];
                  MisDatos = (byte[])reader["poto"];
                  MemoryStream af = new MemoryStream(MisDatos);
                  pictureBox1.Image=Image.FromStream(af);
                }
                else
                {
                    reader.Close();
                    conneti.Close();
                    defualt();
                }
         
            }
            conneti.Close();

        }
        void messongs()
        {
           
            string kulanici = listBox1.GetItemText(listBox1.SelectedItem);
            conneti.Open();
            SqlCommand command = new SqlCommand("Select  *from Messagestab  where friends='" + Cryptolojy.Encryption(Logen.loginname, 3) + "'and username='" + Cryptolojy.Encryption(kulanici, 3) + "'", conneti);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                listBox3.Items.Add(reader[3].ToString()+Cryptolojy.Decryption(reader[1].ToString(), 3)+":"+reader[2].ToString());
            }
            conneti.Close();

        }
 
        void messong()
        {
            string kulanici = listBox1.GetItemText(listBox1.SelectedItem);
            conneti.Open();
            SqlCommand command = new SqlCommand("Select  *from Messagestab  where username='" +Cryptolojy.Encryption(Logen.loginname,3) + "'and friends='" + Cryptolojy.Encryption(kulanici,3)+"'", conneti);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listBox3.Items.Add(reader[3].ToString()+Cryptolojy.Decryption(reader[1].ToString(),3)+":"+reader[2].ToString());
           
            }
            conneti.Close(); 
            messongs();
          timer1.Start();

        }
    
        private void button3_Click(object sender, EventArgs e)
        {  
            if (message1.Text!="")
            {
              string kulanici = listBox1.GetItemText(listBox1.SelectedItem);
            string mes = message1.Text;
            conneti.Open();
            SqlCommand command = new SqlCommand("Insert into Messagestab (username,message_person,messages,time,friends,fren_messagebool) values('" + Cryptolojy.Encryption(Logen.loginname,3)+"','"+Cryptolojy.Encryption(Logen.loginname,3)+"','"+mes+"','"+ DateTime.Now+ "','"+Cryptolojy.Encryption(kulanici,3)+"','"+ " false" + "')",conneti);
            command.ExecuteNonQuery();
            conneti.Close();
            message1.ResetText();
            listBox3.Items.Clear();   
            messong();
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void message1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            conneti.Open();
            SqlCommand cod = new SqlCommand("Update Messenbok_regihstirasyon set live=@liva where username=@ad", conneti);
            cod.Parameters.AddWithValue("@ad", Cryptolojy.Encryption(Logen.loginname, 3));
            cod.Parameters.AddWithValue("@liva", "false");
            cod.ExecuteNonQuery();
            conneti.Close();
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

        private void label7_Click(object sender, EventArgs e)
        {

        }
        int tim = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            tim ++;
            if(tim>=5)
            {
                listBox3.Items.Clear();
                messong();
                
            }
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
         {
            move = true;
            mouse_x = e.X;
            mouse_y =e.Y;
         }

        
    }
}


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
    public partial class Login_Up : Form
    {
       public static SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
       bool isteher;
        public Login_Up()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
      
        private void I_forgot_my_password_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forgotpassword fp = new forgotpassword();
            fp.Show();
            this.Hide();
        }
        private void create_a_new_account_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            createaccount ca = new createaccount();
            ca.Show();
            this.Hide();
        }
        MemoryStream fa = new MemoryStream();
        string username,password;
           
        private void Login_Click(object sender, EventArgs e)
        {  username = textBox1.Text;
            password = textBox2.Text;
          
            connet.Open();
            SqlCommand command = new SqlCommand("Select username,Gmail,pass,phone,poto from Messenbok_regihstirasyon", connet);
            SqlDataReader reader = command.ExecuteReader();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
          
            
            while(reader.Read())                
            {
                if (username==Cryptolojy.Decryption(reader["username"].ToString().TrimEnd(),3) && password==Cryptolojy.Decryption(reader["pass"].ToString().TrimEnd(),3))
                {
                    Logen.loginname=Cryptolojy.Decryption(reader["username"].ToString(),3);
                    Logen.loginmail=Cryptolojy.Decryption(reader["Gmail"].ToString(),3);
                    Logen.loginphone=Cryptolojy.Decryption(reader["phone"].ToString(),3);
               
                    reader.Close();
                    connet.Close();
                    isteher = true;     
                    Live_chat();
                    break;
                }
                else 
                {   
                    isteher = false;
                } 
            }
            if (isteher)
            {
                Menu me = new Menu();
                me.Show();
                this.Hide();
            }
            else 
            {
       
                label3.Text = "Kulanıcı Adı Veya Şifre Yanlış";
                connet.Close();
            }
          

        }
      
        bool move;
        int mouse_x, mouse_y;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar=='\0')
            {
                button2.BringToFront();
                textBox2.PasswordChar = '*';
            }
        }
        private void Live_chat()
        {
            connet.Open();
            SqlCommand cod = new SqlCommand("Update Messenbok_regihstirasyon set live=@liva where username=@ad", connet);
            cod.Parameters.AddWithValue("@ad", Cryptolojy.Encryption(Logen.loginname, 3));
            cod.Parameters.AddWithValue("@liva", "true");
            cod.ExecuteNonQuery();
            connet.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                button1.BringToFront();
                textBox2.PasswordChar = '\0';
            }
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
             this.SetDesktopLocation(MousePosition.X-mouse_x,MousePosition.Y-mouse_y);

            }
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
     move = false;
        }

        private void Login_Up_Load(object sender, EventArgs e)
        {

        }
    }
}

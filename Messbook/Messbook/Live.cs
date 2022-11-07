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
    public partial class Live : Form
    {
        public Live()
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
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Member()
        {
            listBox1.Items.Clear();
            connet.Open();
            SqlCommand command = new SqlCommand("Select  *from Messenbok_member where grupname='" +Cryptolojy.Encryption(Logen.chat,3)+"'", connet);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
               listBox1.Items.Add(Cryptolojy.Decryption(reader[0].ToString(), 3));
            }
            connet.Close();
        }
        private void Live_Load(object sender, EventArgs e)
        {
        
            Member();
        }
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }
    }
}

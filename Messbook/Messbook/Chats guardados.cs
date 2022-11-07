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
    public partial class Chats_guardados : Form
    {
        public Chats_guardados()
        {
            InitializeComponent();
        }
        SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        private void Member()
        {
            listBox1.Items.Clear();

            connet.Open();
            SqlCommand command = new SqlCommand("Select  *from Messenbok_member where name='" + Cryptolojy.Encryption(Logen.loginname, 3) + "'", connet);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                listBox1.Items.Add(Cryptolojy.Decryption(reader[2].ToString(), 3));
            }
            connet.Close();
        }
        private void Chats_guardados_Load(object sender, EventArgs e)
        {
            Member();
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
           
            string itm = listBox1.GetItemText(listBox1.SelectedItem);
            connet.Open();
            listBox1.Items.Clear();
            SqlCommand command = new SqlCommand("Select *from Messenbok_member where grupname='" + Cryptolojy.Encryption(itm, 3) + "'", connet);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Live_Chat live1 = new Live_Chat();
                Logen.chat = itm;
                Live live = new Live();
                this.Close();
                live.Show();
                live1.Show();
            }
            connet.Close();
        }
        bool move;
        int mouse_x, mouse_y;
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
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }
    }
}

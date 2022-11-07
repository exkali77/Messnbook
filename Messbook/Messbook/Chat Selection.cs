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
    public partial class Chat_Selection : Form
    {
        public Chat_Selection()
        {
            InitializeComponent();
        }
        SqlConnection connet = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        private void Chat_Selection_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {

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

        private void formAGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_a_Group form = new Form_a_Group();
            form.Show();
        }

        private void browseGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browse_Groups browse = new Browse_Groups();
            browse.Show();
        }

        private void chatsGuardadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chats_guardados chats = new Chats_guardados();
            chats.Show();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Application.Exit();
            connet.Open();
            SqlCommand cod = new SqlCommand("Update Messenbok_regihstirasyon set live=@liva where username=@ad", connet);
            cod.Parameters.AddWithValue("@ad", Cryptolojy.Encryption(Logen.loginname, 3));
            cod.Parameters.AddWithValue("@liva", "false");
            cod.ExecuteNonQuery();
            connet.Close();
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }
    }
}

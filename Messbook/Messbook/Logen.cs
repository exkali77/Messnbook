using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;

namespace Messbook
{
    class Logen
    {
      public static  SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EASQMRG\\SQLEXPRESS;Initial Catalog=Messnbook_Server;Integrated Security=True");
        public static string loginname = null;
        public static string loginmail = null;
        public static string loginphone = null;
        public static MemoryStream memorysrtm = new MemoryStream();
        public static MemoryStream memorysrtmf = new MemoryStream();

        public static string nolinname = null;
        public static string nolingmail = null;
        public static string nopohno = null;

        public static string chat= null;


        

         
    }
}

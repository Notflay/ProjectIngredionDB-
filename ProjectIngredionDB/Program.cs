using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ProjectIngredionDB
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 

        static public SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=IngredionDB;Integrated Security=True");

        static public void AbrirConexion()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        static public void CerrarConexion()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InicioSesion());
        }
    }
}

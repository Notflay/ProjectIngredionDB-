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

namespace ProjectIngredionDB
{
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void InicioSesion_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Hola");
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            
            string Mensaje = "";
            Program.AbrirConexion();
            SqlCommand cmd = new SqlCommand("Registrar_Usuario", Program.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
            cmd.Parameters.AddWithValue("@Contraseña", txtContraseña.Text);
            cmd.Parameters.Add("@msje", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            Console.WriteLine(cmd);
            cmd.ExecuteNonQuery();
            Mensaje = cmd.Parameters["@msje"].Value.ToString();
            Program.CerrarConexion();
            MessageBox.Show(Mensaje, "Sistema Ingredion", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCorreo.Text != "")
                {
                    Program.AbrirConexion();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("validationUsuario", Program.con);
                    da.SelectCommand.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    if (txtContraseña.Text == dt.Rows[0][0].ToString())
                    {
                        MessageBox.Show("Exitoso", "Sistema Ingredion",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        Inicio Inicio = new Inicio();
                        Inicio.ShowDialog();
                        
                    }
                    else
                    {
                        MessageBox.Show("Datos errado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Program.CerrarConexion();
                }
                else
                {
                    MessageBox.Show("Falta completar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("Datos errado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

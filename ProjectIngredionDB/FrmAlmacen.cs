using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectIngredionDB
{
    public partial class FrmAlmacen : Form
    {
        public String imagen = "";

        public FrmAlmacen()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmAlmacen_Load(object sender, EventArgs e)
        {
            CargarAlmacen();
        }

        private void CargarAlmacen()
        {
            Program.AbrirConexion();
            DataTable dt = new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter("listaStock", Program.con);
            dp.SelectCommand.CommandType = CommandType.StoredProcedure;
            dp.Fill(dt);
            DgvAlmacen.DataSource = dt;
            Program.CerrarConexion();
        }

        private void CargarCodigo()
        {
            Program.AbrirConexion();
            DataTable dt = new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter("listIdStock", Program.con);
            dp.SelectCommand.Parameters.AddWithValue("@Id_Producto", txtIdProduct.Text);
            dp.SelectCommand.CommandType = CommandType.StoredProcedure;
            dp.Fill(dt);
            DgvAlmacen.DataSource = dt;
            Program.CerrarConexion();
        }


        private void dgvAlmacen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.AbrirConexion();
            SqlCommand cmd = new SqlCommand("updateProducto", Program.con); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_Producto", 
                Convert.ToString(DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[0].Value.ToString()));
            cmd.Parameters.AddWithValue("@Stock",
                Convert.ToDouble(DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[2].Value.ToString()) - 1);
            cmd.ExecuteNonQuery();
            Program.CerrarConexion();
            MessageBox.Show("Se retiro 1 de " + 
                DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[1].Value.ToString(),
                "Stock Ingredion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarAlmacen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.AbrirConexion();
            SqlCommand cmd = new SqlCommand("updateProducto", Program.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_Producto",
                Convert.ToString(DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[0].Value.ToString()));
            cmd.Parameters.AddWithValue("@Stock",
                Convert.ToDouble(DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[2].Value.ToString()) + 1);
            cmd.ExecuteNonQuery();
            Program.CerrarConexion();
            MessageBox.Show("Se agrego 1 de " +
                DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[1].Value.ToString(),
                "Stock Ingredion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarAlmacen();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CargarCodigo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[0].Value.ToString());
            Console.WriteLine(DgvAlmacen.RowCount.ToString());

            grpDatos.Enabled = true;
            grpDatos.Visible = true;
            btnRegistrar.Visible = false;
            btnActualizar.Visible = true;
            lblIdProducto.Text = DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtNombre.Text = DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[1].Value.ToString();
            txtStock.Text = DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[2].Value.ToString();
            txtPrecio.Text = DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[3].Value.ToString();
            if (txtNombre.Text == "Maiz")
            {
                imagen = "D:\\SQL server\\Pictures\\maiz.jpg";
            }
            else if (txtNombre.Text == "Galletas")
            {
                imagen = "D:\\SQL server\\Pictures\\galletas_con_chispas_de_chocolate_caseras_35926_orig.jpg";
            }
            else if (txtNombre.Text == "Pan")
            {
                imagen = "D:\\SQL server\\Pictures\\pan-largo-del-pan-22826883.jpg";
            }
            else if (txtNombre.Text == "Pastel vegano")
            {
                imagen = "D:\\SQL server\\Pictures\\840_560.jpg";
            }
            else if (txtNombre.Text == "Noni")
            {
                imagen = "D:\\SQL server\\Pictures\\noni.jpg";
            }
            else if (txtNombre.Text == "Piña")
            {
                imagen = "D:\\SQL server\\Pictures\\piña_g.jpg";
            }
            else if (txtNombre.Text == "Pai de limon")
            {
                imagen = "D:\\SQL server\\Pictures\\ZIJQWMN3UVGCFAM3SHBWAJIHIY.png";
            }
            else if (txtNombre.Text == "Aceite de maiz")
            {
                imagen = "D:\\SQL server\\Pictures\\aceitademaiz.jpg";
            }
            else if (txtNombre.Text == "Harina de maiz")
            {
                imagen = "D:\\SQL server\\Pictures\\índice.jpg";
            }
            else if (txtNombre.Text == "Palomitas de maiz")
            {
                imagen = "D:\\SQL server\\Pictures\\palomitas.jpg";
            }
            else if (txtNombre.Text == "Snacks salados")
            {
                imagen = "D:\\SQL server\\Pictures\\snacks.jpg";
            }
            else if (txtNombre.Text == "Cereal")
            {
                imagen = "D:\\SQL server\\Pictures\\Cereales-Zuck-ANGEL-bolsa-de-20g.jpg";
            }
            else if (txtNombre.Text == "Gelatina")
            {
                imagen = "D:\\SQL server\\Pictures\\Gelatina.jpg";
            }
            else { 
                imagen = DgvAlmacen.Rows[DgvAlmacen.CurrentCell.RowIndex].Cells[4].Value.ToString();
            }
            pcProducto.Image = Image.FromFile(imagen);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Program.AbrirConexion();
            SqlCommand cmd = new SqlCommand("updateAllProduct", Program.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_Producto", lblIdProducto.Text);
            cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);
            cmd.ExecuteNonQuery();
            Program.CerrarConexion();
            MessageBox.Show("Se realizaron los  cambios", "Sistema Ingredion",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarAlmacen();
        }

        private void btnVentanaRegistrar_Click(object sender, EventArgs e)
        {
            Limpiar();
            grpDatos.Enabled = true;
            grpDatos.Visible = true;
            btnActualizar.Visible = false;
            btnRegistrar.Visible = true;
            lblIdProducto.Text = "P00" + Convert.ToDouble(DgvAlmacen.RowCount + 1) ;
        }

        private void Limpiar()
        {
            lblIdProducto.Text = "";
            txtNombre.Clear();
            txtStock.Clear();
            txtPrecio.Clear();
            pcProducto.Image = Image.FromFile("D:\\SQL server\\Pictures\\no-imagen.jpg");
        }

        private void pcProducto_Click(object sender, EventArgs e)
        {
            if (openFoto.ShowDialog() == DialogResult.OK)
            {
                imagen = openFoto.FileName;
                pcProducto.Image = Image.FromFile(imagen);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string Mensaje = "";
            Program.AbrirConexion();
            SqlCommand cmd = new SqlCommand("Registrar_Productos", Program.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_Producto", lblIdProducto.Text);
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
            cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);
            cmd.Parameters.AddWithValue("@Imagen", imagen);
            cmd.Parameters.Add("@msje", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            Mensaje = cmd.Parameters["@msje"].Value.ToString();
            MessageBox.Show(Mensaje, "Sistema Ingredion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Program.CerrarConexion();
            CargarAlmacen();
        }

        private void DgvAlmacen_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

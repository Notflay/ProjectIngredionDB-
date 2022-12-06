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
    public partial class FrmRegistrar : Form
    {
        public String imagen = "";
        public FrmRegistrar()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.AbrirConexion();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("validationIdProd", Program.con);
                da.SelectCommand.Parameters.AddWithValue("@Nombre", cbxProducto.Text);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                lblCodigo.Text = dt.Rows[0][0].ToString();
                lblCosto.Text =  dt.Rows[0][1].ToString();
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void lblCosto_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lblTotal.Text = Convert.ToString(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(lblCosto.Text));
            }
            catch {
                MessageBox.Show("No has digitado cantidad", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    string Mensaje = "";
                    Program.AbrirConexion();
                    SqlCommand cmd = new SqlCommand("Registrar_Pedidos", Program.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                    if (rbnRuc.Checked == true)
                        cmd.Parameters.AddWithValue("@Tipo_Documento", "Ruc");
                    if (rbnDni.Checked == true)
                        cmd.Parameters.AddWithValue("@Tipo_Documento", "Dni");
                    cmd.Parameters.AddWithValue("@Nr_Documento", txtNrdoc.Text);
                    if (rbnEfectivo.Checked == true)
                        cmd.Parameters.AddWithValue("@Tipo_Pago", "Efectivo");
                    if (rbnTarjeta.Checked == true)
                        cmd.Parameters.AddWithValue("@Tipo_Pago", "Tarjeta");
                    cmd.Parameters.AddWithValue("@Producto", cbxProducto.Text);
                    cmd.Parameters.AddWithValue("@Costo", Convert.ToDouble(lblCosto.Text));
                    cmd.Parameters.AddWithValue("@Cantidad", Convert.ToDouble(txtCantidad.Text));
                    cmd.Parameters.AddWithValue("@Total", Convert.ToDouble(lblTotal.Text));
                    cmd.Parameters.AddWithValue("@Fecha", Dtp_Fechanac.Value.Date);
                    cmd.Parameters.AddWithValue("@Id_producto", lblCodigo.Text);
                    cmd.Parameters.Add("@msje", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    Console.WriteLine(cmd.Container);
                    cmd.ExecuteNonQuery();
                    Mensaje = cmd.Parameters["@msje"].Value.ToString();
                    MessageBox.Show(Mensaje, "Sistema Ingredion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.CerrarConexion();
                    CargarPedidos();
                }
                catch(FormatException ex) {
                    MessageBox.Show("No has ingresado datos", "Sistema Ingredion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch 
            {
                MessageBox.Show("Stock no disponible intenta de nuevo", "Sistema Ingredion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void FrmRegistrar_Load(object sender, EventArgs e)
        {
            CargarPedidos();
        }

        private void CargarPedidos()
        {
            Program.AbrirConexion();
            DataTable dt = new DataTable();
            DataTable dtp = new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter("listarProductos", Program.con);
            SqlDataAdapter da = new SqlDataAdapter("listarPedidos", Program.con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            dp.SelectCommand.CommandType = CommandType.StoredProcedure;
            dp.Fill(dtp);
            da.Fill(dt);
            cbxProducto.DataSource = dtp;
            DgvVentas.DataSource = dt;
            Program.CerrarConexion();
            cbxProducto.ValueMember = "Id_Producto";
            cbxProducto.DisplayMember = "Nombre";
        }

        private void DgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int pos;
            pos = DgvVentas.CurrentCell.RowIndex;
 
            DialogResult rpta;
            rpta = MessageBox.Show("¿Desea Eliminar?", "Sistema Ingredion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rpta == DialogResult.OK)
                Program.AbrirConexion();
                SqlCommand cmd = new SqlCommand("Eliminar_Pedido", Program.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Pedidos",Convert.ToDouble(DgvVentas.Rows[pos].Cells[0].Value.ToString()));
                cmd.ExecuteNonQuery();
                Program.CerrarConexion();
            CargarPedidos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtNrdoc.Clear();
            txtCantidad.Clear();
            Dtp_Fechanac.DataBindings.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult rpta;
            rpta = MessageBox.Show("¿Desea Salir?", "Sistema Ingredion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rpta == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            Console.Write(txtCantidad.Text);
        }
    }
}

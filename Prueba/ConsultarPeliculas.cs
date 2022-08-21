using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PracticaTecnica
{
    public partial class ConsultarPeliculas : Form
    {

        private void Refresh()
        {
            ConexionPeliculas PeliculaDB = new ConexionPeliculas();

            dataGridView1.DataSource = PeliculaDB.Get();
        }


        public ConsultarPeliculas()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ConsultarPeliculas().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ConsultarAutores().Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximisar.Visible = true;
        }

        private void btnMaximisar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximisar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AgregarModificarPeliculas().Show();
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ConsultarPeliculas_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        // Hace un Foreach de lo guardado en el textbox y hace un recorrido del data gridview
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtGenero.Text != "")
            {
                dataGridView1.CurrentCell = null;

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    r.Visible = false;
                }

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtGenero.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                Refresh();
            }
        }

        // Hace un Foreach de lo guardado en el textbox y hace un recorrido del data gridview

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text != "")
            {
                dataGridView1.CurrentCell = null;

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    r.Visible = false;
                }

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtFiltro.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                Refresh();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Conexion Agregar = new Conexion();

            int? PeliculaID = GetId();
            if (PeliculaID != null)
            {
                DetallesPelicula detallesPelicula = new DetallesPelicula(PeliculaID);
                detallesPelicula.ShowDialog();
                Refresh();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private int? GetId()
        {
            try
            {
                return int.Parse(
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString()
                    );
            }
            catch
            {
                return null;
            }
        }
    }
}

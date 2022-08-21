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
    public partial class ConsultarAutores : Form
    {
        public ConsultarAutores()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        // Hace un Foreach de lo guardado en el textbox y hace un recorrido del data gridview
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFiltroSexo.Text != "")
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
                        if ((c.Value.ToString().ToUpper()).IndexOf(txtFiltroSexo.Text.ToUpper()) == 0)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AgregarModificarAutores().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ConsultarAutores().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ConsultarPeliculas().Show();
        }

        private void CrearModificarAutor_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        // Obtiene los datos para mostrarlos es decir refresca
        private void Refresh()
        {
            Conexion ActorDB = new Conexion();

            dataGridView1.DataSource = ActorDB.Get();
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

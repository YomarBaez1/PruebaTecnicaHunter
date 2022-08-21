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
    public partial class AgregarModificarPeliculas : Form
    {

        // Refresca los datos y los muestra mediante la funcion GET
        private void Refresh()
        {
            ConexionPeliculas PeliculaDB = new ConexionPeliculas();

            dataGridView1.DataSource = PeliculaDB.Get();
        }

        // Cosume la Conexion de ConexionPeliculas
        ConexionPeliculas dbConexion = new ConexionPeliculas();


        private int? PeliculaID;
        public AgregarModificarPeliculas(int? PeliculaID = null)
        {
            InitializeComponent();
            this.PeliculaID = PeliculaID;
            if (PeliculaID != null)
                LoadData();
        }

        // Cargar los datos de la Pelicula
        private void LoadData()
        {
            ConexionPeliculas PeliculaIDA = new ConexionPeliculas();
            PeliculaT oPeople = PeliculaIDA.Get((int)PeliculaID);

            txttitulo.Text = oPeople.Titulo;
            txtgenero.Text = oPeople.Genero;
            txtfecha.Text = oPeople.FechaEstreno.ToShortDateString();
        }



        public AgregarModificarPeliculas()
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximisar.Visible = true;
        }

        private void btnMaximisar_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximisar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog Imagen = new OpenFileDialog();

            Imagen.Title = "Agregar Imagenes";
            Imagen.Filter = "Archivos de imagen (*.jpg, *.png, *.bmp) | *.jpg; *.png; *.bmp";

            if (Imagen.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(Imagen.FileName);

            }
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
        // usa ConexionPeliculas la funcion Agregar, luego obtiene los datos mediante el ID y si es diferente a nulo, muestra otro form para modificar
        private void button3_Click(object sender, EventArgs e)
        {
            ConexionPeliculas Agregar = new ConexionPeliculas();

            txttitulo.Text = "";
            txtgenero.Text = "";
            pictureBox1.ImageLocation = "";

            int? PeliculaID = GetId();
            if (PeliculaID != null)
            {
                ModificarPeliculas frmEditar = new ModificarPeliculas(PeliculaID);
                frmEditar.ShowDialog();
                Refresh();
            }
        }
        // Usa ConexionPeliculas la funcion Agregar para guardar los datos de los txt a los get y set y guardar los datos a la base de datos
        private void button2_Click(object sender, EventArgs e)
        {
            ConexionPeliculas Add = new ConexionPeliculas();
            try
            {
                Add.Agregar(txttitulo.Text, txtgenero.Text, DateTime.Parse(txtfecha.Text));
                Refresh();
                txttitulo.Text = "";
                txtgenero.Text = "";
                pictureBox1.ImageLocation = "";

                MessageBox.Show("Los datos se han ingresado correctamente");

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido ingresar las peliculas" + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AgregarModificarPeliculas_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        // Helper para obtener los datos en la posicion 0

        #region HELPER
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


        #endregion

    }
}

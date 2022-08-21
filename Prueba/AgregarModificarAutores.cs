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
    public partial class AgregarModificarAutores : Form
    {
        

        Conexion dbConexion = new Conexion();


        private int? ActorID;
        // Si acotr ID es igual a Nulo cargara los datos.

        public AgregarModificarAutores(int? ActorID=null)
        {
            InitializeComponent();
            LoadComboBox();
            this.ActorID = ActorID;
            if (ActorID != null)
                LoadData();
        }

        // Carga datos para mostrarlos
        private void LoadData()
        {
            Conexion ActorIDA = new Conexion();
            Actor oPeople = ActorIDA.Get((int)ActorID);
            txtnombre.Text = oPeople.NombreCompleto;
            txtfecha.Text = oPeople.FechaNacimiento.ToShortDateString();
            txtsexo.Text = oPeople.Sexo;
        }

        // Carga el combobox con las peliculas Existentes
        private void LoadComboBox()
        {
            Conexion ActorIDA = new Conexion();
            List<Pelicula> peliculas = ActorIDA.GetPelicula();
            comboBox1.DataSource = peliculas;
            comboBox1.DisplayMember = "Titulo";
            comboBox1.ValueMember = "PeliculaID";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ConsultarAutores().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ConsultarAutores().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ConsultarPeliculas().Show();
        }

        // Boton de agregar aqui haremos una conexion con la clase Conexion y asi poder usar las funciones de las mismas.
        
        private void button2_Click(object sender, EventArgs e)
        {
            Conexion Add = new Conexion();
            var Imagen = new ImageConverter().ConvertTo(pictureBox4.Image, typeof(Byte[]));
            
            try
            {
                Add.Agregar(txtnombre.Text, DateTime.Parse(txtfecha.Text), txtsexo.Text, int.Parse(comboBox1.SelectedValue.ToString() ));
                Refresh();

                MessageBox.Show("Los autores se han ingresado correctamente");
                txtnombre.Text = "";
                txtsexo.Text = "";
                comboBox1.Text = "";
                pictureBox4.ImageLocation = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show("No se ha podido ingresar los Autores" + ex.Message);
            }
        }

        private void AgregarYMODIFICARVERDAD_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        // Refrescar los datos, esto nos permitira obtener los datos de la base de datos
        private void Refresh()
        {
            Conexion ActorDB = new Conexion();
            dataGridView1.DataSource = ActorDB.Get();
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
        // Boton para obtener la imagen y guardarla en un pictureBox
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog Imagen = new OpenFileDialog();
            
            Imagen.Title = "Agregar Imagenes";
            Imagen.Filter = "Archivos de imagen (*.jpg, *.png, *.bmp) | *.jpg; *.png; *.bmp";

            if(Imagen.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.Load(Imagen.FileName);
                
            }
        }


        // Utiliza la funcion Agregar y obtiene los datos para posteriormente Editar y refrescar los datos
        private void button3_Click(object sender, EventArgs e)
        {
            txtnombre.Text = "";
            txtsexo.Text = "";
            comboBox1.Text = "";
            pictureBox4.ImageLocation = "";

            Conexion Agregar = new Conexion();

            int? ActorID = GetId();
            if (ActorID != null)
            {
                ModificarAutores frmEditar = new ModificarAutores(ActorID);
                frmEditar.ShowDialog();
                Refresh();
            }
        }

        // Helper obtiene los datos del datagridview en la posicion 0 o sea AutorID, lo que hace que busque ese campo
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

        private void txtpelicula_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

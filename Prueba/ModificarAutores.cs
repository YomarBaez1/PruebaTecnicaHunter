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
    public partial class ModificarAutores : Form
    {

        Conexion dbConexion = new Conexion();


        private int? ActorID;
        public ModificarAutores(int? ActorID = null)
        {
            InitializeComponent();
            this.ActorID = ActorID;
            if (ActorID != null)
                LoadData();
        }

        public ModificarAutores()
        {
            InitializeComponent();
        }

        private void ModificarAutores_Load(object sender, EventArgs e)
        {

        }

        // Carga los datos y los obtiene de la clase Conexion
        private void LoadData()
        {
            Conexion ActorIDA = new Conexion();
            Actor oPeople = ActorIDA.Get((int)ActorID);
            txtnombre.Text = oPeople.NombreCompleto;
            txtfecha.Text = oPeople.FechaNacimiento.ToShortDateString();
            txtsexo.Text = oPeople.Sexo;
            txtpelicula.Text = oPeople.PeliculaID.ToString();
        }

        // Agrega los datos de los Texbox a los Get y Set
        private void button3_Click(object sender, EventArgs e)
        {
            Conexion Agregar = new Conexion();

            try
            {

                if (ActorID == null)
                    Agregar.Agregar(txtnombre.Text, DateTime.Parse(txtfecha.Text), txtsexo.Text, int.Parse(txtpelicula.Text));
                else
                    Agregar.Update(txtnombre.Text, DateTime.Parse(txtfecha.Text), txtsexo.Text, int.Parse(txtpelicula.Text), (int)ActorID);
                this.Close();

                MessageBox.Show("Los datos se han modificado correctamente");

            }
            catch  (Exception ex){ 
            
            }


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
    }
}

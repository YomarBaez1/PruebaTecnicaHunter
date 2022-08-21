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
    public partial class ModificarPeliculas : Form
    {

        ConexionPeliculas dbConexion = new ConexionPeliculas();


        private int? PeliculaID;
        public ModificarPeliculas(int? PeliculaID = null)
        {
            InitializeComponent();
            this.PeliculaID = PeliculaID;
            if (PeliculaID != null)
                LoadData();
        }

        private void LoadData()
        {
            ConexionPeliculas PeliculaIDA = new ConexionPeliculas();
            PeliculaT oPeople = PeliculaIDA.Get((int)PeliculaID);
            txttitulo.Text = oPeople.Titulo;
            txtgenero.Text = oPeople.Genero;
            txtfecha.Text = oPeople.FechaEstreno.ToShortDateString();
           
        }


        public ModificarPeliculas()
        {
            InitializeComponent();
        }

        private void ModificarPeliculas_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConexionPeliculas Agregar = new ConexionPeliculas();

            try
            {

                if (PeliculaID == null)
                    Agregar.Agregar(txttitulo.Text, txtgenero.Text, DateTime.Parse(txtfecha.Text));
                else
                    Agregar.Update(txttitulo.Text, txtgenero.Text, DateTime.Parse(txtfecha.Text), (int)PeliculaID);
                this.Close();

                MessageBox.Show("Los datos se han modificado correctamente");

            }
            catch (Exception ex)
            {

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

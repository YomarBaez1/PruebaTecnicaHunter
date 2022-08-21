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
    public partial class DetallesPelicula : Form
    {
        private int? PeliculaID;
        public DetallesPelicula(int? PeliculaID = null)
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
            List<Actor> actores = PeliculaIDA.GetPeliculaActor((int)PeliculaID);
            txttitulo.Text = oPeople.Titulo;
            txtgenero.Text = oPeople.Genero;
            txtfecha.Text = oPeople.FechaEstreno.ToShortDateString();
            dataGridView1.DataSource = actores;

        }

        private void DetallesPelicula_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PracticaTecnica
{
    public class ConexionPeliculas
    {
        // Las funciones aqui son las mismas que en AUTOR lo unico que lo haremos en Peliculas

        // Conexion de base de datos a un string
        private string connectionString = "Data Source=DESKTOP-GVMOAH7\\SQLEXPRESS; Initial Catalog=PruebaTecnica;Integrated Security=SSPI";


        // Lo pasamos a una funcion para asi solo llamarla, esto nos ahorra codigo y aumenta la velocidad del programa
        public bool Ok()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch
            {
                return false;
            }
            return true;

        }

        // Obtenemos lista de las Peliculas mediante un GET
        public List<PeliculaT> Get()
        {
            List<PeliculaT> PeliculaSi = new List<PeliculaT>();

            string query = "select PeliculaID,Titulo,Genero,FechaEstreno from Pelicula";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PeliculaT oPeople = new PeliculaT();
                        oPeople.PeliculaID = reader.GetInt32(0);
                        oPeople.Titulo = reader.GetString(1);
                        oPeople.Genero = reader.GetString(2);
                        oPeople.FechaEstreno = reader.GetDateTime(3);
                        PeliculaSi.Add(oPeople);
                    }
                    reader.Close();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error" + ex.Message);
                }

            }

            return PeliculaSi;

        }

        public List<Actor> GetPeliculaActor(int PeliculaID)
        {
            List<Actor> Actores = new List<Actor>();

            string query = "  SELECT Actor.* FROM PeliculaActor " +
                "INNER JOIN Actor " +
                "ON PeliculaActor.ActorID = Actor.ActorID " +
                "INNER JOIN Pelicula " +
                "ON PeliculaActor.PeliculaID = Pelicula.PeliculaID " +
                "WHERE Pelicula.PeliculaID = @PeliculaID;";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PeliculaID", PeliculaID);


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Actor oPeople = new Actor();
                        oPeople.ActorID = reader.GetInt32(0);
                        oPeople.NombreCompleto = reader.GetString(1);
                        oPeople.FechaNacimiento = reader.GetDateTime(2);
                        oPeople.Sexo = reader.GetString(3);
                        oPeople.PeliculaID = reader.GetInt32(4);


                        Actores.Add(oPeople);
                    }
                    reader.Close();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error" + ex.Message);
                }

            }

            return Actores;

        }

        // Funcion de Peliculas que nos permitira Obtener los Datos mediante PELICULA ID
        public PeliculaT Get(int PeliculaID)
        {


            string query = "select PeliculaID,Titulo,Genero,FechaEstreno from Pelicula" +
                " where PeliculaID=@PeliculaID";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PeliculaID", PeliculaID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    PeliculaT oPeople = new PeliculaT();
                    oPeople.PeliculaID = reader.GetInt32(0);
                    oPeople.Titulo = reader.GetString(1);
                    oPeople.Genero = reader.GetString(2);
                    oPeople.FechaEstreno = reader.GetDateTime(3);

                    reader.Close();
                    connection.Close();

                    return oPeople;



                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error" + ex.Message);
                }

            }





        }

        // Funcion Agregar para poder agregar los datos necesarios
        public void Agregar(string Titulo, string Genero, DateTime FechaEstreno)
        {
            string query = "insert into Pelicula(Titulo,Genero,FechaEstreno) values " +
                "(@Titulo,@Genero,@FechaEstreno)";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Titulo", Titulo);
                command.Parameters.AddWithValue("@Genero", Genero);
                command.Parameters.AddWithValue("@FechaEstreno", FechaEstreno);
                
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    connection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error" + ex.Message);
                }

            }

        }

        // Funcion actualizar que nos permitira Actualizar los Datos
        public void Update(string Titulo, string Genero, DateTime FechaEstreno, int PeliculaID)
        {
            string query = "update Pelicula set Titulo=@Titulo, Genero=@Genero, FechaEstreno=@FechaEstreno"
                + " where PeliculaID=@PeliculaID";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Titulo", Titulo);
                command.Parameters.AddWithValue("@Genero", Genero);
                command.Parameters.AddWithValue("@FechaEstreno", FechaEstreno);
                command.Parameters.AddWithValue("@PeliculaID", PeliculaID);
             

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    connection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error" + ex.Message);
                }

            }

        }



    }
    // Creamos una clase para obtener los datos de los TextBox y asi mandarlos a la base de datos
    public class PeliculaT
    {
        public int PeliculaID { get; set; }
        public string Titulo { get; set; }

        public string Genero { get; set; }

        public DateTime FechaEstreno { get; set; }

    }

  
}

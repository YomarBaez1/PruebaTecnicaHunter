using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace PracticaTecnica
{
    public class Conexion
    {
        // Creamos la conexion con la base de datos
        private string connectionString = "Data Source=DESKTOP-GVMOAH7\\SQLEXPRESS; Initial Catalog=PruebaTecnica;Integrated Security=SSPI";


        // Mediante una funcion creamos el OK que sera la conexion de la base de datos, esto nos ayuda a solo usar Ok en vez de usar el codigo de Arriba.
        // Esto es para mas eficiencia en el codigo
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

        // Creamos una lista de las peliculas usando SELECT para asi mostrarlas
        public List<Pelicula> GetPelicula()
        {
            List<Pelicula> Peliculas = new List<Pelicula>();

            string query = "select * from Pelicula";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Pelicula oPeople = new Pelicula();
                        oPeople.PeliculaID = reader.GetInt32(0);
                        oPeople.Titulo = reader.GetString(1);
                        oPeople.Genero = reader.GetString(2);
                        oPeople.FechaEstreno = reader.GetDateTime(3);
                        

                        Peliculas.Add(oPeople);
                    }
                    reader.Close();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error" + ex.Message);
                }

            }

            return Peliculas;

        }

        // Creamos una lista de los actores usando SELECT para asi mostrarlas
        public List<Actor> Get()
        {
            List<Actor> ActoresSi = new List<Actor>();

            string query = "select actorID,NombreCompleto,FechaNacimiento,Sexo,PeliculaID from Actor";

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Actor  oPeople = new Actor();
                        oPeople.ActorID = reader.GetInt32(0);
                        oPeople.NombreCompleto = reader.GetString(1);
                        oPeople.FechaNacimiento = reader.GetDateTime(2);
                        oPeople.Sexo = reader.GetString(3);
                        oPeople.PeliculaID = reader.GetInt32(4);

                        ActoresSi.Add(oPeople);
                    }
                    reader.Close();
                    connection.Close();

                }
                catch(Exception ex)
                {
                    throw new Exception("Hay un error" + ex.Message);
                }

            }



            return ActoresSi;

        }

        // Usamos Get para obtener el Actor ID y asi podremos hacer la busqueda esto nos sirve para el modificar y si asi lo queremos tambien para un futuro Eliminar
        public Actor Get(int ActorId)
        {


            string query = "select actorID,NombreCompleto,FechaNacimiento,Sexo,PeliculaID from Actor" + 
                " where ActorID=@ActorID";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ActorID", ActorId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    
                        Actor oPeople = new Actor();
                        oPeople.ActorID = reader.GetInt32(0);
                        oPeople.NombreCompleto = reader.GetString(1);
                        oPeople.FechaNacimiento = reader.GetDateTime(2);
                        oPeople.Sexo = reader.GetString(3);
                        oPeople.PeliculaID = reader.GetInt32(4);
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

        // Usamos un Get para obtener el Nombre de los Actores, se usa para leer los datos y mostrarlos
        public Actor GetByName(string name)
        {


            string query = "select actorID,NombreCompleto,FechaNacimiento,Sexo,PeliculaID from Actor" +
                " where NombreCompleto=@name";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    Actor oPeople = new Actor();
                    oPeople.ActorID = reader.GetInt32(0);
                    oPeople.NombreCompleto = reader.GetString(1);
                    oPeople.FechaNacimiento = reader.GetDateTime(2);
                    oPeople.Sexo = reader.GetString(3);
                    oPeople.PeliculaID = reader.GetInt32(4);
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

        // Esta funcion lo utilizamos para Agregar los datos del Autor
        public void Agregar(string NombreCompleto,DateTime FechaNacimiento,string Sexo, int PeliculaID)
        {
            Actor actor = new Actor();
            string query = "insert into Actor(NombreCompleto,FechaNacimiento,Sexo,PeliculaID) values " +
                "(@NombreCompleto,@FechaNacimiento,@Sexo,@PeliculaID)";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreCompleto", NombreCompleto);
                command.Parameters.AddWithValue("@FechaNacimiento", FechaNacimiento);
                command.Parameters.AddWithValue("@Sexo", Sexo);
                command.Parameters.AddWithValue("@PeliculaID", PeliculaID);
                

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    actor = GetByName(NombreCompleto);
                    AgregarPeliculaActor(actor.ActorID, PeliculaID);
                    connection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error" + ex.Message);
                }


            }

        }
      
        // Esta funcion es para agregar los datos a PeliculaActor en la base de datos, la usaremos para poder mostrar detalles avanzados
        public void AgregarPeliculaActor(int ActorID, int PeliculaID)
        {
            string query = "insert into PeliculaActor(PeliculaID,ActorID) values " +
                "(@PeliculaID,@ActorID)";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PeliculaID", PeliculaID);
                command.Parameters.AddWithValue("@ActorID", ActorID);


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

        // Esta funcion es para actualizar los datos
        public void Update(string NombreCompleto, DateTime FechaNacimiento, string Sexo, int PeliculaID, int ActorID)
        {
            string query = "update Actor set NombreCompleto=@NombreCompleto, FechaNacimiento=@FechaNacimiento,Sexo=@Sexo, PeliculaID=@PeliculaID"
                + " where ActorID=@ActorID";

            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreCompleto", NombreCompleto);
                command.Parameters.AddWithValue("@FechaNacimiento", FechaNacimiento);
                command.Parameters.AddWithValue("@Sexo", Sexo);
                command.Parameters.AddWithValue("@PeliculaID", PeliculaID);
                command.Parameters.AddWithValue("@ActorID", ActorID);

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

    // Creamos una clase publica Actor, para obtener los diferentes datos y que se guarden en la base de Datos
    public class Actor{
        public int ActorID { get; set; }
        public string NombreCompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Sexo { get; set; }

        public int PeliculaID { get; set; }

        

    }
    // Clase Pelicula
    public class Pelicula
    {
        public int PeliculaID { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }

        public DateTime FechaEstreno { get; set; }
        public string Foto { get; set; }
    }
}
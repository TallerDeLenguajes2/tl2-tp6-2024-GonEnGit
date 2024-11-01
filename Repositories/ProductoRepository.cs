namespace EspacioRepositorios;

using EspacioModelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

public class ProductoRepository
{
// esta cadena de conexion es parte de la clase no de los metodos
    string cadenaDeConexion = "Data Source = db\\Tienda.db";

// metodos, cada uno lleva su consulta pre armada
    public void CargarNuevoProducto(Productos producto)
    {
    // no te comas las @, estan para mapear los atributos mas abajo
        string consulta = @"INSERT INTO Productos(Descripcion, Precio) VALUES (@Descripcion, @Precio)";

    // a esto lo podes seguir como está en la teoria
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();
        // esto mencionaron en la teoria, 'Parameters' lo usas
        // para ligar el parametro del query con el valor que corresponda
            comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void ActualizarProducto(int id, string descripcion, double precio)
    {
        string consulta = @"UPDATE productos SET Descripcion = @descripcion, Precio = @precio WHERE idProducto = @id";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@descripcion", descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", precio));
            comando.Parameters.Add(new SqliteParameter("@id", id));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public List<Productos> ListarProductos()
    {
        List<Productos> lista = new List<Productos>();
        string consulta = @"SELECT * FROM Productos";

        using(SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();
            using(SqliteDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    Productos productoLeido = new Productos();
                    productoLeido.Id = Convert.ToInt32(lector["idProducto"]);
                    productoLeido.Descripcion = lector["Descripcion"].ToString();
                    productoLeido.Precio = Convert.ToDouble(lector["Precio"]);

                    lista.Add(productoLeido);
                }
            }
            conexion.Close();

            return lista;
        }
    }

    public List<int> ObtenerListaId(string campo, string tabla)
    {
        List<int> listaId = new List<int>();

    // parece que no se puede usar parameter con nombre de tablas, pregunta si está bien esto
    // así la funcion podrias usarse siempre que haga falta sin importar la tabla
        string consulta = $"SELECT {campo} FROM {tabla}";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            using (SqliteDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    listaId.Add(Convert.ToInt32(lector["idProducto"]));
                }
            }

            conexion.Close();
        }

        return listaId;
    }
}
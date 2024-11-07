namespace EspacioRepositorios;

using EspacioModelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

public class ProductoRepository
{
    string cadenaDeConexion = "Data Source = db\\Tienda.db;Cache=Shared";

    public void CargarNuevoProducto(Productos producto) // esto te esta dando un error 405
    {
    // no te comas las @, estan para mapear los atributos mas abajo
        string consulta = @"INSERT INTO Productos(Descripcion, Precio) VALUES (@desc, @precio)";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            var comando = new SqliteCommand(consulta, conexion);
            conexion.Open();
            comando.Parameters.Add(new SqliteParameter("@desc", producto.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
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

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using EspacioModels;
using EspacioViewModels;
using EspacioInterfaces;
using Microsoft.Data.Sqlite;

namespace EspacioRepositorios;


public class ProductoRepository : IProductoRepository
{
// ahora la cadena de conexion es privada y la pasas
// a un constructor usando la inyeccion
    private readonly string _CadenaDeConexion;
    public ProductoRepository(string CadenaDeConexion)
    {
        _CadenaDeConexion = CadenaDeConexion;
    }

    public void CargarNuevoProducto(Producto producto) // esto te esta dando un error 405
    {
    // no te comas las @, estan para mapear los atributos mas abajo
        string consulta = @"INSERT INTO Producto(Descripcion, Precio) VALUES (@desc, @precio)";

        using (SqliteConnection conexion = new SqliteConnection(_CadenaDeConexion))
        {
        // a throw se lo puede usar para probar los try catch, forzando errores
            //throw new InvalidOperationException("No se pudo establecer la conexión con la base de datos.");

            var comando = new SqliteCommand(consulta, conexion);
            conexion.Open();
            comando.Parameters.Add(new SqliteParameter("@desc", producto.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void ActualizarProducto(Producto producto)
    {
        string consulta = @"UPDATE Producto SET Descripcion = @descripcion, Precio = @precio WHERE idProducto = @id";

        using (SqliteConnection conexion = new SqliteConnection(_CadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
            comando.Parameters.Add(new SqliteParameter("@id", producto.Id));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public List<Producto> ListarProducto()
    {
        List<Producto> lista = new List<Producto>();
        string consulta = @"SELECT * FROM Producto";

        using(SqliteConnection conexion = new SqliteConnection(_CadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            using(SqliteDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    Producto productoLeido = new Producto();
                    productoLeido.Id = Convert.ToInt32(lector["idProducto"]);
                    productoLeido.Descripcion = lector["Descripcion"].ToString();
                    productoLeido.Precio = Convert.ToDecimal(lector["Precio"]);
                    lista.Add(productoLeido);
                }
            }
            conexion.Close();

            return lista;
        }
    }

    public void BorrarProducto(int id)
    {
        string consulta = @"DELETE FROM Producto WHERE idProducto = @id";

        using (SqliteConnection conexion = new SqliteConnection(_CadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

// esto es solo para listar los id de Producto
    public List<int> ObtenerListaId(string campo, string tabla)
    {
        List<int> listaId = new List<int>();

        string consulta = $"SELECT {campo} FROM {tabla}";

        using (SqliteConnection conexion = new SqliteConnection(_CadenaDeConexion))
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
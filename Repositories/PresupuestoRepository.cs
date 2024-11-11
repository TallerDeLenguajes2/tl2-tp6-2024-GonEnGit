namespace EspacioRepositorios;

using System.Runtime.InteropServices;
using EspacioModelos;
using Microsoft.Data.Sqlite;

public class PresupuestoRepository
{
    string cadenaDeConexion = "Data Source = db\\Tienda.db;Cache=Shared";

    public List<Presupuesto> ConsultarPresupuestosPrueba()
    {
        List<Presupuesto> lista = new List<Presupuesto>();
// C# es mas estricto con SQL, cuando usas USING, el atributo tiene que estar entre parentesis
// igual que lo estas haciendo en bases, cuando usas ON parece que los parentesis no hacen falta
// pero igualmente tendrias que usar 'ON Presupuestos.idPresupuesto = ...'
        string consulta =   "SELECT idPresupuesto, NombreDestinatario, FechaCreacion, idProducto, cantidad, descripcion, precio " +
                            "FROM Presupuestos " +
                            "INNER JOIN PresupuestosDetalle USING (idPresupuesto) " +
                            "INNER JOIN Productos USING (idProducto)";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            using (SqliteDataReader lector = comando.ExecuteReader())
            {
                while(lector.Read())
                {
                    int idPresupuestoDB = Convert.ToInt32(lector["idPresupuesto"]);

                    if (lista.FirstOrDefault(presu => presu.IdPresupuesto == idPresupuestoDB) == null)
                    {
                        Presupuesto presupuestoLeido = new Presupuesto();
                        presupuestoLeido.IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]);
                        presupuestoLeido.NombreDestinatario = lector["NombreDestinatario"].ToString();
                        presupuestoLeido.FechaCreacion = lector["FechaCreacion"].ToString();
                    // no está inicializada en el model
                        presupuestoLeido.Productos = new List<Producto>();

                        lista.Add(presupuestoLeido);
                    }
                    Presupuesto pesupuestoLeido = lista.FirstOrDefault(presu => presu.IdPresupuesto == idPresupuestoDB);

                    Producto productoLeido = new Producto();
                    productoLeido.Id = Convert.ToInt32(lector["idProducto"]);
                    productoLeido.Descripcion = lector["Descripcion"].ToString();
                    productoLeido.Precio = Convert.ToDouble(lector["Precio"]);
                    pesupuestoLeido.Productos.Add(productoLeido);
                }
            }
            conexion.Close();
        }

        return lista;
    }


    public List<Presupuesto> ConsultarPresupuestos()
    {
        List<Presupuesto> lista = new List<Presupuesto>();
        string consulta = "SELECT * FROM Presupuestos";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            using (SqliteDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    Presupuesto presupuestoLeido = new Presupuesto();
                    presupuestoLeido.IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]);
                    presupuestoLeido.NombreDestinatario = lector["NombreDestinatario"].ToString();
                    presupuestoLeido.FechaCreacion = lector["FechaCreacion"].ToString();
                    lista.Add(presupuestoLeido);
                }
            }
            conexion.Close();

            return lista;
        }
    }

    public void CrearPresupuesto(Presupuesto presupuesto)
    {
        string consulta = @"INSERT INTO Presupuestos(NombreDestinatario, FechaCreacion) VALUES (@destinatario, @fecha)";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            conexion.Open();
            SqliteCommand comando = new SqliteCommand(consulta, conexion);

            comando.Parameters.Add(new SqliteParameter("@destinatario", presupuesto.NombreDestinatario));
            comando.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void ActualizarPresupuesto(Presupuesto presupuesto)
    {
        string consulta = "UPDATE Presupuestos SET NombreDestinatario = @nombre, FechaCreacion = @fecha WHERE idPresupuesto = @id";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@nombre", presupuesto.NombreDestinatario));
            comando.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion));
            comando.Parameters.Add(new SqliteParameter("@id", presupuesto.IdPresupuesto));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void BorrarPresupuesto(int id)
    {
        string consulta = @"DELETE FROM Presupuestos WHERE idPresupuesto = @id";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

// ---------

    public void AgregarDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        string consulta = @"INSERT INTO PresupuestosDetalle(idPresupuesto, idProducto, cantidad) VALUES (@idPres, @idProd, @cant)";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@idPres", idPresupuesto));
            comando.Parameters.Add(new SqliteParameter("@idProd", idProducto));
            comando.Parameters.Add(new SqliteParameter("@cant", cantidad));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void BorrarDetalles(int id)
    {
        string consulta = @"DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @id";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
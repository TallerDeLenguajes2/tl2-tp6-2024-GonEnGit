namespace EspacioRepositorios;

using System.Runtime.InteropServices;
using EspacioModelos;
using Microsoft.Data.Sqlite;

public class PresupuestoRepository
{
    string cadenaDeConexion = "Data Source = db\\Tienda.db;Cache=Shared";

    public List<Presupuesto> ConsultarPresupuestos()
    {
        List<Presupuesto> lista = new List<Presupuesto>();

// ----
    // una nota sobre esto al final * -------------------------
        string consulta =   "SELECT idPresupuesto, NombreDestinatario, FechaCreacion, idProducto, cantidad, descripcion, precio " +
                            "FROM Presupuestos " +
                            "LEFT JOIN PresupuestosDetalle USING (idPresupuesto) " +
                            "LEFT JOIN Productos USING (idProducto)";

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

                        presupuestoLeido.Cantidades = new List<int>();         // no estan inicializadas en el model
                        presupuestoLeido.Productos = new List<Producto>();

                        lista.Add(presupuestoLeido);
                    }

                // una nota sobre esto al final ** -------------------------
                    if (lector["idProducto"] != DBNull.Value)
                    {
                    // agregas la cantidad, los ordenes de las listas van a coincidir
                        Presupuesto pesupuestoLeido = lista.FirstOrDefault(presu => presu.IdPresupuesto == idPresupuestoDB);
                        pesupuestoLeido.Cantidades.Add(Convert.ToInt32(lector["cantidad"]));
                    // creas y guardas el producto
                        Producto productoLeido = new Producto();
                        productoLeido.Id = Convert.ToInt32(lector["idProducto"]);
                        productoLeido.Descripcion = lector["Descripcion"].ToString();
                        productoLeido.Precio = Convert.ToDouble(lector["Precio"]);
                        pesupuestoLeido.Productos.Add(productoLeido);
                    }
                }
            }
            conexion.Close();
        }

        return lista;
    }
// ----

// ----
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
// ----

// ----
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
// ----

// ----
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

// ----
    public void AgregarDetalle(PresupuestoDetalle detalle)
    {
        string consulta = @"INSERT INTO PresupuestosDetalle(idPresupuesto, idProducto, cantidad) VALUES (@idPres, @idProd, @cant)";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@idPres", detalle.IdPresupuesto));
            comando.Parameters.Add(new SqliteParameter("@idProd", detalle.IdProducto));
            comando.Parameters.Add(new SqliteParameter("@cant", detalle.Cantidad));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
// ----

// ----
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
// ----



/* NOTAS

/ --- * --- /
C# es mas estricto con SQL, cuando usas USING, el atributo tiene que estar entre parentesis
igual que lo estas haciendo en bases, cuando usas ON parece que los parentesis no hacen falta
pero igualmente tendrias que usar 'ON Presupuestos.idPresupuesto = ...'

/ --- ** --- /
Parece que te da un problema para tratar de signar un valor nulo
'InvalidCastException: Object cannot be cast from DBNull to other types.'
osea, no podes convertir un null a un int, esto es por el LEFT JOIN...
existe una clase DBNull, podes usarla como valor?

Segun encontraste DBNull es unico, osea, existe una sola instancia en todo momento
como estas usando un lecto y tratas los datos linea por linea no hay problema
podes usar el metodo DBNull.value para hacer comparaciones usando este valor nulo
el 'null' parece que trae problemas a veces...

*/
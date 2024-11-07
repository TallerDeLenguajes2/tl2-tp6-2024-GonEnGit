namespace EspacioRepositorios;

using EspacioModelos;
using Microsoft.Data.Sqlite;

public class PresupuestoRepository
{
    string cadenaDeConexion = "Data Source = db\\Tienda.db;Cache=Shared";

    public void CrearPresupuesto(Presupuesto presupuesto)
    {
        string consulta = @"INSERT INTO Presupuestos(NombreDestinatario, FechaCreacion) VALUES (@destinatario, @fecha)";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@destinatario", presupuesto.NombreDestinatario));
            comando.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

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

// puede haber mas de un detalle con el mismo n° de presupuesto... esto debe estar mal
// tendria que ser un delete aparentemente revisá
    public void ActualizarDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        string consulta = "UPDATE PresupuestoDetalle SET idProducto = @idProducto, Cantidad = @cantidad WHERE idPresupuesto = @idPresupuesto";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
            comando.Parameters.Add(new SqliteParameter("@cantidad", cantidad));
            comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
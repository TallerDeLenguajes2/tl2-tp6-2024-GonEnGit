namespace EspacioRepositorios;

using System.Runtime.InteropServices;
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
            conexion.Open();
            SqliteCommand comando = new SqliteCommand(consulta, conexion);

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

    public void ActualizarPresupuesto(Presupuesto presupuesto)
    {
        string consulta = "UPDATE Presupuesto SET NombreDestinatario = @nombre, FechaCreacion = @fecha WHERE idPresupuesto = @id";

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
            comando.Parameters.Add(new SqliteParameter());
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
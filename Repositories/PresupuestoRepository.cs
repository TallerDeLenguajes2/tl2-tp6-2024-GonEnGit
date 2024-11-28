
namespace EspacioRepositorios;

using System.Data;
using System.Runtime.InteropServices;
using EspacioModels;
using EspacioViewModels;

using Microsoft.Data.Sqlite;
using SQLitePCL;


public class PresupuestoRepository
{
    string cadenaDeConexion = "Data Source = db\\Tienda.db;Cache=Shared";

// todos estos metodos tienen que cambiar, ya no traes un string si no un objeto

// ----
    public PresupuestoViewModel ConsultarPresupuestos()
    {
        List<Cliente> listaCli = new List<Cliente>();
        List<Presupuesto> listaPres = new List<Presupuesto>();

    // una nota sobre esto al final * ---------- buscarla en TP6
        string consultaClientes = "SELECT * FROM Cliente";
        string consultaPresupuestos = "SELECT * FROM Presupuesto";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comandoCli = new SqliteCommand(consultaClientes, conexion);
            conexion.Open();

            using (SqliteDataReader lector = comandoCli.ExecuteReader())
            {
                while (lector.Read())
                {
                    Cliente clienteLeido = new Cliente();
                    clienteLeido.IdCliente = Convert.ToInt32(lector["idCliente"]);
                    clienteLeido.Nombre = lector["nombre"].ToString();
                    clienteLeido.Direccion = lector["direccion"].ToString();
                    clienteLeido.Telefono = lector["telefono"].ToString();

                    listaCli.Add(clienteLeido);
                }
            }
            conexion.Close();
    // ----
            SqliteCommand comandoPres = new SqliteCommand(consultaPresupuestos,conexion);
            conexion.Open();

            using (SqliteDataReader lector = comandoPres.ExecuteReader())
            {
                while (lector.Read())
                {
                    Presupuesto presupuestoLeido = new Presupuesto();
                    presupuestoLeido.IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]);
                    presupuestoLeido.IdCliente = Convert.ToInt32(lector["IdCliente"]);
                    presupuestoLeido.FechaCreacion = lector["FechaCreacion"].ToString();

                    listaPres.Add(presupuestoLeido);
                }
            }
            conexion.Close();
        }

        PresupuestoViewModel modelo = new PresupuestoViewModel(listaPres, listaCli);

        return modelo;
    }
// ----

// ---- revisa 1 por 1
    public ListaPresupuestosViewModel ListarPresupuestos()
    {
        List<Cliente> listaCli = new List<Cliente>();
        List<Presupuesto> listaPres = new List<Presupuesto>();
        List<PresupuestoDetalle> listaDet = new List<PresupuestoDetalle>();
        List<Producto> listaProd = new List<Producto>();

        string consultaClientes = "SELECT * FROM Cliente";
        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comandoCli = new SqliteCommand(consultaClientes, conexion);
            conexion.Open();

            using (SqliteDataReader lectorCli = comandoCli.ExecuteReader())
            {
                while (lectorCli.Read())
                {
                    Cliente clienteLeido = new Cliente();
                    clienteLeido.IdCliente = Convert.ToInt32(lectorCli["idCliente"]);
                    clienteLeido.Nombre = lectorCli["nombre"].ToString();
                    clienteLeido.Direccion = lectorCli["direccion"].ToString();
                    clienteLeido.Telefono = lectorCli["telefono"].ToString();

                    listaCli.Add(clienteLeido);
                }
            }
            //conexion.Close();
    // ----
            string consultaPresupuestos = "SELECT * FROM Presupuesto";
            SqliteCommand comandoPres = new SqliteCommand(consultaPresupuestos,conexion);
            //conexion.Open();

            using (SqliteDataReader lectorPres = comandoPres.ExecuteReader())
            {
                while (lectorPres.Read())
                {
                    Presupuesto presupuestoLeido = new Presupuesto();
                    presupuestoLeido.IdPresupuesto = Convert.ToInt32(lectorPres["idPresupuesto"]);
                    presupuestoLeido.IdCliente = Convert.ToInt32(lectorPres["idCliente"]);
                    presupuestoLeido.FechaCreacion = lectorPres["FechaCreacion"].ToString();

                    listaPres.Add(presupuestoLeido);
                }
            }
            //conexion.Close();
    // ----
            string consultaDetalles = "SELECT * FROM PresupuestoDetalle";
            SqliteCommand comandoDets = new SqliteCommand(consultaDetalles,conexion);
            //conexion.Open();

            using (SqliteDataReader lectorDet = comandoDets.ExecuteReader())
            {
                while (lectorDet.Read())
                {
                    PresupuestoDetalle detalleLeido = new PresupuestoDetalle();
                    detalleLeido.IdPresupuesto = Convert.ToInt32(lectorDet["idPresupuesto"]);
                    detalleLeido.IdProducto = Convert.ToInt32(lectorDet["idProducto"]);
                    detalleLeido.Cantidad = Convert.ToInt32(lectorDet["Cantidad"]);

                    listaDet.Add(detalleLeido);
                }
            }
            //conexion.Close();
    // ----
            string consultaProductos = "SELECT * FROM Producto";
            SqliteCommand comandoProd = new SqliteCommand(consultaProductos,conexion);
            //conexion.Open();

            using (SqliteDataReader lectorProd = comandoProd.ExecuteReader())
            {
                while (lectorProd.Read())
                {
                    Producto productoLeido = new Producto();
                    productoLeido.Id = Convert.ToInt32(lectorProd["idProducto"]);
                    productoLeido.Descripcion = lectorProd["Descripcion"].ToString();
                    productoLeido.Precio = Convert.ToDecimal(lectorProd["Precio"]);

                    listaProd.Add(productoLeido);
                }
            }
            conexion.Close();
        }

        ListaPresupuestosViewModel nuevo = new ListaPresupuestosViewModel(listaCli, listaPres, listaDet, listaProd);
        return nuevo;
    }
// ----

// ----
    public void CrearPresupuesto(Presupuesto pres)
    {
        string consulta = @"INSERT INTO Presupuesto(idCliente, FechaCreacion) VALUES (@idCli, @fecha)";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@idCli", pres.IdCliente));
            comando.Parameters.Add(new SqliteParameter("@fecha", pres.FechaCreacion));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
// ----

// ----
    public void ActualizarPresupuesto(Presupuesto presupuesto)
    {
        string consulta = "UPDATE Presupuesto SET idCliente = @idCli, FechaCreacion = @fecha WHERE idPresupuesto = @id";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@idCli", presupuesto.IdCliente));
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
        string consulta = @"DELETE FROM Presupuesto WHERE idPresupuesto = @id";

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
        string consulta = @"INSERT INTO PresupuestoDetalle(idPresupuesto, idProducto, Cantidad) VALUES (@idPres, @idProd, @cant)";

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
    public void BorrarDetalleUnico(int nroPres, int nroProd)
    {
        string consulta = "DELETE FROM PresupuestoDetalle WHERE idPresupuesto = @idPres AND idProducto = @idProd";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta,conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@idPres", nroPres));
            comando.Parameters.Add(new SqliteParameter("@idProd", nroProd));
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void BorrarDetalles(int id)
    {
        string consulta = @"DELETE FROM PresupuestoDetalle WHERE idPresupuesto = @id";

        using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
        {
            SqliteCommand comando = new SqliteCommand(consulta, conexion);
            conexion.Open();

            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
// ----
}
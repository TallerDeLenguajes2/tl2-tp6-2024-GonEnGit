namespace EspacioRepositorios;

using System.Runtime.InteropServices;
using EspacioModels;
using Microsoft.Data.Sqlite;
using SQLitePCL;

public class ClienteRepository
{
	string cadenaDeConexion = "Data Source = db\\Tienda.db;Cache=Shared";

	public List<Cliente> ListarClientes()
	{
		List<Cliente> lista = new List<Cliente>();
		string consulta = "SELECT * From Cliente";

		using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
		{
			SqliteCommand comando = new SqliteCommand(consulta, conexion);
			conexion.Open();

			using (SqliteDataReader lector = comando.ExecuteReader())
			{
				while (lector.Read())
				{
					Cliente clienteLeido = new Cliente();
					clienteLeido.IdCliente = Convert.ToInt32(lector["idCliente"]);
					clienteLeido.Nombre = lector["nombre"].ToString();
					clienteLeido.Direccion = lector["direccion"].ToString();
					clienteLeido.Telefono = lector["telefono"].ToString();
					lista.Add(clienteLeido);
				}
			}
			conexion.Close();

			return lista;
		}
	}

	public void CargarCliente(Cliente cliente)
	{
		string consulta = "INSERT INTO Cliente(nombre, direccion, telefono) VALUES (@nom, @dir, @tel)";

		using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
		{
			SqliteCommand comando = new SqliteCommand(consulta, conexion);
			conexion.Open();

			comando.Parameters.Add(new SqliteParameter("@nom", cliente.Nombre));
			comando.Parameters.Add(new SqliteParameter("@dir", cliente.Direccion));
			comando.Parameters.Add(new SqliteParameter("@tel", cliente.Telefono));

			comando.ExecuteNonQuery();
			conexion.Close();
		}
	}

	public void ActualizarCliente(Cliente cliente)
	{
		string consulta = "UPDATE Cliente SET nombre = @nom, direccion = @dir, telefono = @tel WHERE idCliente = @id";

		using (SqliteConnection conexion = new SqliteConnection(cadenaDeConexion))
		{
			SqliteCommand comando = new SqliteCommand(consulta, conexion);
			conexion.Open();

			comando.Parameters.Add(new SqliteParameter("@nom", cliente.Nombre));
			comando.Parameters.Add(new SqliteParameter("@dir", cliente.Direccion));
			comando.Parameters.Add(new SqliteParameter("@tel", cliente.Telefono));
			comando.Parameters.Add(new SqliteParameter("@id", cliente.IdCliente));

			comando.ExecuteNonQuery();
			conexion.Close();
		}
	}

	public void BorrarCliente(int id)
	{
		string consulta = @"DELETE FROM Cliente WHERE idCliente = @id";

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
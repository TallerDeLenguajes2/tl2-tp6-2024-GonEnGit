namespace EspacioRepositorios;

using System.Runtime.InteropServices;
using EspacioModelos;
using Microsoft.Data.Sqlite;

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
}
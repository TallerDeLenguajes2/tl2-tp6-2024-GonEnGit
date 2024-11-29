
using SQLitePCL;

using EspacioModels;
using EspacioInterfaces;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc;

namespace EspacioRepositorios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _CadenaDeConexion;
    public UsuarioRepository(string CadenaDeConexion)
    {
        _CadenaDeConexion = CadenaDeConexion;
    }

	public Usuario BuscarUsuario(string userName, string pass)
	{
		Usuario usuarioBuscado = null;
		string consulta = "SELECT * FROM Usuario WHERE usuario = @alias AND contrasenia = @contra";

		using (SqliteConnection conexion = new SqliteConnection(_CadenaDeConexion))
		{
			SqliteCommand comando = new SqliteCommand(consulta, conexion);
			conexion.Open();

			comando.Parameters.Add(new SqliteParameter("@alias", userName));
			comando.Parameters.Add(new SqliteParameter("@contra", pass));

			using (SqliteDataReader lector = comando.ExecuteReader())
			{
				if (lector.HasRows)
				{
					while (lector.Read())
					{
						usuarioBuscado = new Usuario();
						usuarioBuscado.IdUsuario = Convert.ToInt32(lector["idUsuario"]);
						usuarioBuscado.Nombre = lector["nombre"].ToString();
						usuarioBuscado.Alias = lector["usuario"].ToString();
						usuarioBuscado.Contrasenia = lector["contrasenia"].ToString();
						usuarioBuscado.Rol = lector["rol"].ToString();
					}
				}
			}
			conexion.Close();
		}
		return usuarioBuscado;
	}
}
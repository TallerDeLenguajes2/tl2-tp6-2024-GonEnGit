
using EspacioModels;

namespace EspacioInterfaces;

public interface IUsuarioRepository
{
	public Usuario BuscarUsuario(string userName, string pass);
}
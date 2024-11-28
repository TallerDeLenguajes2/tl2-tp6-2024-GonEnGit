
namespace EspacioModels;

public class Usuario
{
	private int idUsuario;
	private string nombre;
	private string alias;
	private string contrasenia;
	private string rol;

	public int IdUsuario { get => idUsuario; set => idUsuario = value; }
	public string Nombre { get => nombre; set => nombre = value; }
	public string Alias { get => alias; set => alias = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
	public string Rol { get => rol; set => rol = value; }
}
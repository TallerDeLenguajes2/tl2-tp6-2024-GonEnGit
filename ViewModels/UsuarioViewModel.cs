
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace EspacioViewModels;

public class UsuarioViewModel : SharedViewModel
{
	[Required]
	private string alias;

	[Required]
	[PasswordPropertyText(true)]
	private string pass;

	private bool logueado;

	public string Alias { get => alias; set => alias = value; }
	public string Pass { get => pass; set => pass = value; }
    public bool Logueado { get => logueado; set => logueado = value; }
}
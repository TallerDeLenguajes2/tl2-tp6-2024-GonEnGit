
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace EspacioViewModels;

public class UsuarioViewModel
{
	[Required]
	public string Alias;

	[Required]
	[PasswordPropertyText(true)]
	public string Pass;
}
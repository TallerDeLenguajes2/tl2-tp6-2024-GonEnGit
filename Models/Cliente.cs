
using System.ComponentModel.DataAnnotations;

namespace EspacioModelos;

public class Cliente
{
	private int idCliente;
	[Required]
	private string nombre;
	[Required]
	[EmailAddress]
	private string direccion;
	[Phone]
	private string telefono;

	public int IdCliente { get => idCliente; set => idCliente = value; }
	public string Nombre { get => nombre; set => nombre = value; }
	public string Direccion { get => direccion; set => direccion = value; }
	public string Telefono { get => telefono; set => telefono = value; }
}
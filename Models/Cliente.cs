
namespace EspacioModelos;

public class Cliente
{
	private int idCliente;
	private string nombre;
	private string direccion;
	private string telefono;

	public int IdCliente { get => idCliente; set => idCliente = value; }
	public string Nombre { get => nombre; set => nombre = value; }
	public string Direccion { get => direccion; set => direccion = value; }
	public string Telefono { get => telefono; set => telefono = value; }
}
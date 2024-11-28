
namespace EspacioInterfaces;

using EspacioModels;

public interface IClienteRepository
{
	public List<Cliente> ListarClientes();
	public Cliente BuscarCliente(int idCliente);
	public void CargarCliente(Cliente cliente);
	public void ActualizarCliente(Cliente cliente);
	public void BorrarCliente(int id);
}
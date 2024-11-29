
using EspacioModels;

class ClienteViewModel : SharedViewModel
{
	public List<Cliente> clientes;

	public ClienteViewModel (List<Cliente> listaCli)
	{
		clientes = listaCli;
	}
}
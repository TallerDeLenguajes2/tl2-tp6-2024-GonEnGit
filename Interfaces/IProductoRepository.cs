
using EspacioModels;
using EspacioViewModels;

namespace EspacioInterfaces;

public interface IProductoRepository
{
	public void CargarNuevoProducto(Producto producto);
	public void ActualizarProducto(Producto producto);
	public List<Producto> ListarProducto();
	public void BorrarProducto(int id);
	public List<int> ObtenerListaId(string campo, string tabla);
}
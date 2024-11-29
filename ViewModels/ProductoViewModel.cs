
using EspacioModels;

class ProductoViewModel : SharedViewModel
{
	public List<Producto> productos;

	public ProductoViewModel (List<Producto> listaProd)
	{
		productos = listaProd;
	}
}

using EspacioModels;

namespace EspacioViewModels;

public class AltaProductosViewModel
{
    public int cantidad;
    public Cliente cliente;
    public Producto producto;
    public Presupuesto presupuesto;
    public List<Producto> productosDisponibles;

    public AltaProductosViewModel(Cliente cli, Producto prod, Presupuesto pres, List<Producto> lista)
    {
        cliente = cli;
        producto = prod;
        presupuesto = pres;
        productosDisponibles = lista;
    }
}
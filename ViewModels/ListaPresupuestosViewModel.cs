
using EspacioModels;

namespace EspacioViewModels;

public class ListaPresupuestosViewModel
{
    public List<Cliente> clientes;
    public List<Presupuesto> presupuestos;
    public List<PresupuestoDetalle> detalles;
    public List<Producto> productos;

    public ListaPresupuestosViewModel(List<Cliente> cli, List<Presupuesto> pres, List<PresupuestoDetalle> det, List<Producto> prod)
    {
        clientes = cli;
        presupuestos = pres;
        detalles = det;
        productos = prod;
    }
}
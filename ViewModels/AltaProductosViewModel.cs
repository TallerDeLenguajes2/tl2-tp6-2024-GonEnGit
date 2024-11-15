
using EspacioModelos;

namespace EspacioViewModels;

public class AltaProductosViewModel
{
    Presupuesto presupuesto;
    List<Producto> productosDisponibles;

    public AltaProductosViewModel(Presupuesto pres, List<Producto> lista)
    {
        presupuesto = pres;
        productosDisponibles = lista;
    }
}
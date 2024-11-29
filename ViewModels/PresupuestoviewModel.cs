
using EspacioModels;

namespace EspacioViewModels;

public class PresupuestoViewModel : SharedViewModel
{
    public List<Presupuesto> Presupuestos;
    public List<Cliente> Clientes;

    public PresupuestoViewModel(List<Presupuesto> ListaPres, List<Cliente> ListaCli)
    {
        Presupuestos = ListaPres;
        Clientes = ListaCli;
    }
}
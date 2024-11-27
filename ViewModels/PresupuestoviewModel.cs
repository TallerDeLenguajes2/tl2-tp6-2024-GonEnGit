
using EspacioModels;

namespace EspacioViewModels;

public class PresupuestoViewModel
{
    public List<Presupuesto> Presupuestos;
    public List<Cliente> Clientes;

    public PresupuestoViewModel(List<Presupuesto> ListaPres, List<Cliente> ListaCli)
    {
        Presupuestos = ListaPres;
        Clientes = ListaCli;
    }
}
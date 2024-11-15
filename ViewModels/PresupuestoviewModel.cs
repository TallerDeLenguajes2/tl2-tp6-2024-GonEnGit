
using EspacioModelos;

namespace EspacioViewModels;

public class PresupuestoViewModel
{
    public Presupuesto presupuesto;
    public Cliente clientes;

    public PresupuestoViewModel(Presupuesto pres, Cliente cli)
    {
        presupuesto = pres;
        clientes = cli;
    }
}
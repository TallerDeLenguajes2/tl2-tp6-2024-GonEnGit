
namespace EspacioInterfaces;

using EspacioModels;
using EspacioViewModels;


public interface IPresupuestoRepository
{
	public PresupuestoViewModel ConsultarPresupuestos();
	public ListaPresupuestosViewModel ListarPresupuestos();
	public void CrearPresupuesto(Presupuesto pres);
	public void ActualizarPresupuesto(Presupuesto presupuesto);
	public void BorrarPresupuesto(int id);
	public void AgregarDetalle(PresupuestoDetalle detalle);
	public void BorrarDetalleUnico(int nroPres, int nroProd);
	public void BorrarDetalles(int id);
}

using System.Data.SqlTypes;

namespace EspacioModelos;

public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private string fechaCreacion;
    private List<PresupuestoDetalle> detalles;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestoDetalle> Detalles { get => detalles; set => detalles = value; }

}
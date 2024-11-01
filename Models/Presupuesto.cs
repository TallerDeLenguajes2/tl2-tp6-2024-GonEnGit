
using System.Data.SqlTypes;

namespace EspacioModelos;

public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private string fechaCreacion;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
}
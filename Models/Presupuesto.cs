
using System.Data.SqlTypes;

namespace EspacioModelos;

public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private string fechaCreacion;
    private List<int> cantidades;
    private List<Producto> productos;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<int> Cantidades { get => cantidades; set => cantidades = value; }
    public List<Producto> Productos { get => productos; set => productos = value; }

}
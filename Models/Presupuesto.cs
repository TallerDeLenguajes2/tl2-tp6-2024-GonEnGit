
using System.Data.SqlTypes;

namespace EspacioModels;

public class Presupuesto
{
    private int idPresupuesto;
    private int idCliente;
    private string fechaCreacion;
    //private List<int> cantidades;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public int IdCliente { get => idCliente; set => idCliente = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    //public List<int> Cantidades { get => cantidades; set => cantidades = value; }

}
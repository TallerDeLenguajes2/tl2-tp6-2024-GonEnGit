
using System.Data.SqlTypes;

namespace EspacioModelos;

public class Presupuesto
{
    private int idPresupuesto;
    private Cliente cliente;
    private string fechaCreacion;
    private List<int> cantidades;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<int> Cantidades { get => cantidades; set => cantidades = value; }
}
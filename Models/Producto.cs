
namespace EspacioModelos;

public class Productos
{
    private int id;
    private string descripcion;
    private double precio;

    public int Id { get => id; set => id = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public double Precio { get => precio; set => precio = value; }
}
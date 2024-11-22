
namespace EspacioModels;

public class PresupuestoDetalle
{
    private int idPresupuesto;
    private int idProducto;
    private int cantidad;
    private List<Producto> productos;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
    public List<Producto> Productos { get => productos; set => productos = value; }

}
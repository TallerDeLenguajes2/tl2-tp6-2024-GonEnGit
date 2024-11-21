
using System.ComponentModel.DataAnnotations;

namespace EspacioModelos;

public class Producto
{
    private int id;
    [StringLength(250)]
    private string descripcion;
    [Required]
    [Range(0, 9999999.99)]
    private decimal precio;

    public int Id { get => id; set => id = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public decimal Precio { get => precio; set => precio = value; }
}
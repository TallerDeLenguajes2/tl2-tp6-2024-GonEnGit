
using EspacioModels;

public class ProductoNuevoViewModel : SharedViewModel
{
    private string desc;
    private decimal prec;

    public string Desc { get => desc; set => desc = value; }
    public decimal Prec { get => prec; set => prec = value; }
}

// cuidado, muchas paginas se rompieron gracias al
// SharedViewModel, lo solucionaste con este ViewModel
// y reemplazandolo en la View RegistrarProducto
// tambien en el controller que RegistrarProducto
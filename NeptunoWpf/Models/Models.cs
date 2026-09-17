using System;
using System.Collections.Generic;
using System.Text;

namespace NeptunoWpf.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; } = "";
    public decimal? PrecioUnidad { get; set; }
    public short? UnidadesEnExistencia { get; set; }
    public string CategoriaProducto { get; set; } = "";
}

public class Categoria
{
    public int IdCategoria { get; set; }
    public string NombreCategoria { get; set; } = "";
    public string? Descripcion { get; set; }
    public bool? Activo { get; set; }
    public string? CodCategoria { get; set; }
}

public class Proveedor
{
    public int IdProveedor { get; set; }
    public string NombreCompania { get; set; } = "";
    public string? NombreContacto { get; set; }
    public string? CargoContacto { get; set; }
    public string? Ciudad { get; set; }
    public string? Pais { get; set; }
    public string? Telefono { get; set; }
}

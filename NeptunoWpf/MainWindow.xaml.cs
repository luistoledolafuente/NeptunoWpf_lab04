using Microsoft.Data.SqlClient;
using NeptunoWpf.Models;
using System.Data;
using System.Windows;

namespace NeptunoWpf;

public partial class MainWindow : Window
{
    private const string CadenaConexion =
        "Server=.\\SQLEXPRESS;Database=Neptuno;Trusted_Connection=True;TrustServerCertificate=True;";

    public MainWindow()
    {
        InitializeComponent();

        CargarProductos();
        CargarCategorias();
    }

    private void CargarProductos()
    {
        var productos = new List<Producto>();

        using SqlConnection conexion = new(CadenaConexion);
        using SqlCommand comando = new("dbo.usp_Productos_Listar", conexion);

        comando.CommandType = CommandType.StoredProcedure;

        conexion.Open();

        using SqlDataReader reader = comando.ExecuteReader();

        while (reader.Read())
        {
            productos.Add(new Producto
            {
                IdProducto = reader.GetInt32(reader.GetOrdinal("idproducto")),
                NombreProducto = reader["nombreProducto"]?.ToString() ?? "",
                PrecioUnidad = reader["precioUnidad"] == DBNull.Value
                    ? null
                    : reader.GetDecimal(reader.GetOrdinal("precioUnidad")),
                UnidadesEnExistencia = reader["unidadesEnExistencia"] == DBNull.Value
                    ? null
                    : reader.GetInt16(reader.GetOrdinal("unidadesEnExistencia")),
                CategoriaProducto = reader["categoriaProducto"]?.ToString() ?? ""
            });
        }

        dgProductos.ItemsSource = productos;
    }

    private void CargarCategorias()
    {
        var categorias = new List<Categoria>();

        using SqlConnection conexion = new(CadenaConexion);
        using SqlCommand comando = new("dbo.usp_Categorias_Listar", conexion);

        comando.CommandType = CommandType.StoredProcedure;

        conexion.Open();

        using SqlDataReader reader = comando.ExecuteReader();

        while (reader.Read())
        {
            categorias.Add(new Categoria
            {
                IdCategoria = reader.GetInt32(reader.GetOrdinal("idcategoria")),
                NombreCategoria = reader["nombrecategoria"]?.ToString() ?? "",
                Descripcion = reader["descripcion"]?.ToString(),
                Activo = reader["Activo"] == DBNull.Value
                    ? null
                    : reader.GetBoolean(reader.GetOrdinal("Activo")),
                CodCategoria = reader["CodCategoria"]?.ToString()
            });
        }

        dgCategorias.ItemsSource = categorias;
    }

    private void BuscarProveedores_Click(object sender, RoutedEventArgs e)
    {
        var proveedores = new List<Proveedor>();

        using SqlConnection conexion = new(CadenaConexion);
        using SqlCommand comando = new(
            "dbo.usp_Proveedores_BuscarPorContactoYCiudad", conexion);

        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.Add("@NombreContacto", SqlDbType.VarChar, 30)
            .Value = txtContacto.Text.Trim();

        comando.Parameters.Add("@Ciudad", SqlDbType.VarChar, 15)
            .Value = txtCiudad.Text.Trim();

        conexion.Open();

        using SqlDataReader reader = comando.ExecuteReader();

        while (reader.Read())
        {
            proveedores.Add(new Proveedor
            {
                IdProveedor = reader.GetInt32(reader.GetOrdinal("idProveedor")),
                NombreCompania = reader["nombreCompañia"]?.ToString() ?? "",
                NombreContacto = reader["nombrecontacto"]?.ToString(),
                CargoContacto = reader["cargocontacto"]?.ToString(),
                Ciudad = reader["ciudad"]?.ToString(),
                Pais = reader["pais"]?.ToString(),
                Telefono = reader["telefono"]?.ToString()
            });
        }

        dgProveedores.ItemsSource = proveedores;
    }
}
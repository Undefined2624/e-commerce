using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DataLayer
{
    public class DL_Producto
    {

        public List<Producto> ListarProductos()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                using (SqlConnection osqlConnection = new SqlConnection(Connection.cn)) //clase Connection con cadena de conexión
                {
                    
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT p.idProducto, p.nombre, p.descripcion, p.precioUnitario, p.stock, p.activo,");
                    sb.AppendLine("c.idCategoria, c.nombre, c.descripcion as descripcionCategoria,");
                    sb.AppendLine("m.idMarca, m.nombre as nombreMarca,");
                    sb.AppendLine("p.rutaImagen, p.nombreImagen");
                    sb.AppendLine("FROM Producto P");
                    sb.AppendLine("INNER JOIN Marca M on M.idMarca = P.idMarca");
                    sb.AppendLine("INNER JOIN Categoria C ON C.idCategoria = p.idCategoria");



                    SqlCommand cmd = new SqlCommand(sb.ToString(), osqlConnection);
                    cmd.CommandType = CommandType.Text;

                    osqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(
                                new Producto()
                                {
                                    idProducto = Convert.ToInt32(reader["idMarca"]),
                                    nombre = reader["nombre"].ToString(),
                                    descripcion = reader["descripcion"].ToString(),
                                    Marca = new Marca()
                                    {
                                        idMarca = Convert.ToInt32(reader["idMarca"]),
                                        nombre = reader["nombre"].ToString()
                                    },
                                    Categoria = new Categoria()
                                    {
                                        idCategoria = Convert.ToInt32(reader["idCategoria"]),
                                        nombre = reader["nombre"].ToString(),
                                        descripcion = reader["descripcionCategoria"].ToString()
                                    },
                                    precioUnitario = Convert.ToDecimal(reader["precioUnitario"]),                                                                       
                                    stock = Convert.ToInt32(reader["stock"]),
                                    activo = Convert.ToBoolean(reader["activo"]),
                                    rutaImagen = reader["rutaImagen"].ToString(),
                                    nombreImagen = reader["nombreImagen"].ToString()

                                });
                        }
                    }

                }

            }
            catch
            {
                productos = new List<Producto>();
            }

            return productos;
        }

        public int AgregarProducto(Producto obj, out string Mensaje)
        {
            int idAutogerenado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Connection.cn))
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_AgregarProducto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure; // Especifica que el comando es un procedimiento almacenado
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@idMarca", obj.Marca.idMarca);
                    cmd.Parameters.AddWithValue("@idCategoria", obj.Categoria.idCategoria);
                    cmd.Parameters.AddWithValue("@precioUnitario", obj.precioUnitario);
                    cmd.Parameters.AddWithValue("@stock", obj.stock);
                    cmd.Parameters.AddWithValue("@activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idAutogerenado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }
            }
            catch (Exception e)
            {
                idAutogerenado = 0;
                Mensaje = e.Message;
            }

            return idAutogerenado;

        }

        public bool EditarProducto(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Connection.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProducto", obj.idProducto);
                    cmd.Parameters.AddWithValue("@idMarca", obj.Marca.idMarca);
                    cmd.Parameters.AddWithValue("@idCategoria", obj.Categoria.idCategoria);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@precioUnitario", obj.precioUnitario);
                    cmd.Parameters.AddWithValue("@stock", obj.stock);
                    cmd.Parameters.AddWithValue("@activo", obj.activo);
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }
            }
            catch (Exception e)
            {
                resultado = false;
                Mensaje = e.Message;
            }

            return resultado;

        }

        public bool EliminarProducto(int idProducto, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Connection.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }
            }
            catch (Exception e)
            {
                resultado = false;
                Mensaje = e.Message;
            }

            return resultado;

        }



    }
}

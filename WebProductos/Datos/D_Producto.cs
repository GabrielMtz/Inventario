using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using WebProductos.Models;

namespace WebProductos.Datos
{
    public class D_Producto
    {
        // variable global privada
        private string CadenaConexion = "server=LAPTOP-1D3DCMJ8\\SQLEXPRESS;database=generacion43;" +
            "user=sa;password=12345;TrustServerCertificate=true";

        public List<E_Producto> ObtenerTodos()
        {

            //creamos la lista de productoss vacia
            List<E_Producto> lista = new List<E_Producto>();
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                string query = "SELECT  idProducto,Descripcion,Precio,fechaIngreso,disponible FROM Productos ";
                SqlCommand comando = new SqlCommand(query, conexion);
                //creamos un objeto de la clase sqldatareader y ahi guardamos el resultado de ejecutar el query 
                SqlDataReader reader = comando.ExecuteReader();//este comando ayuda a leer cada fila y si existen datos continua con la siguiente 
                                                               //recorremos las filas del reader con un ciclo while
                while (reader.Read())
                {
                    E_Producto producto = new E_Producto();
                    producto.idProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = Convert.ToString(reader["Descripcion"]);
                    producto.Precio = Convert.ToDecimal(reader["Precio"]);
                    producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                    producto.Disponible = Convert.ToBoolean(reader["disponible"]);

                    //agregamos el producto a la lista
                    lista.Add(producto);
                }
               
            }
            catch (Exception ex)
            {
                //lanzamos nuevamente el error para qur el controlador se entere del error
                throw ex;
            }
            finally
            {
                //cerrar la conexion
                conexion.Close();
            }
           
            
            //regresamos la lusta de productos
            return lista;

        }


        public void AgregarProducto(E_Producto producto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            conexion.Open();
            //sinitizar:quiatr caracteres o palabras clave, por ehjemplo un delete o inyeccion sql
            //creamos el query utiizando parametros:@nombreParametro
            string query = "INSERT INTO Productos(descripcion,precio,fechaIngreso,disponible)" +
                            "VALUES (@descripcion,@precio,@fechaIngreso,@disponible) ";
            SqlCommand comando = new SqlCommand(query, conexion);
            //asignar valores a los parametros del query
            comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
            comando.Parameters.AddWithValue("@disponible", producto.Disponible);
            comando.ExecuteNonQuery();

        }

        public void EditarProductos(E_Producto producto)
        {
            SqlConnection conexion=new SqlConnection(CadenaConexion);

            try
            {
                conexion.Open();

                string query = "UPDATE Productos SET  descripcion=@Descripcion, precio=@Precio, fechaIngreso=@fechaIngreso, " +
                    "disponible=@disponible  WHERE  idProducto=@idProducto";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idProducto", producto.idProducto);
                comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
                comando.Parameters.AddWithValue("@disponible", producto.Disponible);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {

                conexion.Close();

            }

        }


        public void EliminarProducto(int idProducto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);

            try
            {
                conexion.Open();

                string query = "DELETE FROM Productos WHERE idProducto=@idProducto";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idProducto", idProducto);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conexion.Close(); }

            
        }


        public E_Producto ObtenerProductosPorId(int idProducto)
        {
           
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            conexion.Open();
            string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM productos " +
                            "WHERE idProducto=@id";
            SqlCommand comando =new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id", idProducto);
            SqlDataReader reader=comando.ExecuteReader();
            //creamos un prducto
            E_Producto producto=new E_Producto();
            //leemos los datos del reader y los guarda en el prducto
            if(reader.Read()) 
            {
                producto.idProducto = Convert.ToInt32(reader["idProducto"]);
                producto.Descripcion = Convert.ToString(reader["Descripcion"]);
                producto.Precio = Convert.ToDecimal(reader["Precio"]);
                producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                producto.Disponible = Convert.ToBoolean(reader["disponible"]);
            }
            conexion.Close();

            return producto;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using EasyBuy.Models;
using System.Data;

namespace EasyBuyCR.Models
{
    public class OracleConection
    {
        public static OracleConection instancia;
        String cadena = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;
        OracleConnection conexion;
        OracleCommand cmd;
        OracleTransaction tran;

        public static OracleConection obtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new OracleConection();
            }

            return instancia;
        }

        public String[] validarUsuario(String correo, String contrasena)
        {
            String[] usuario = null;
            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd = new OracleCommand("select tipo from usuario where correo = :correo", conexion);
            cmd.Parameters.Add("correo", OracleDbType.Varchar2).Value = correo;

            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                usuario = new String[4];
                usuario[0] = reader.IsDBNull(0) ? "" : reader.GetString(0);
            }
            

            if (usuario != null) 
            {
                if (usuario[0].Equals("C"))
                {
                    cmd = new OracleCommand("select nombre_cliente,apellido_cliente,correo_cliente from cliente where correo_cliente = :correo and password = :contra", conexion);
                    cmd.Parameters.Add("correo", OracleDbType.Varchar2).Value = correo;
                    cmd.Parameters.Add("contra", OracleDbType.Varchar2).Value = ObtenerHash(contrasena);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario[1] = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        usuario[2] = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        usuario[3] = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    }
                }
                else {
                    cmd = new OracleCommand("select nombre_empresa,correo_tienda from empresa where correo_tienda = :correo and password_empresa = :contra", conexion);
                    cmd.Parameters.Add("correo", OracleDbType.Varchar2).Value = correo;
                    cmd.Parameters.Add("contra", OracleDbType.Varchar2).Value = ObtenerHash(contrasena);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario[1] = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        usuario[2] = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    }
                }
            }

            // Libera todo los recursos utilizados
            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
            conexion.Dispose();

            return usuario;
        }

        public void RegistrarEmpresa(CompanyRegisterViewModel model) {

            RegistrarUsuario(model.Email, "E");

            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "fun_insertar_empresa";

            OracleParameter Resultado = new OracleParameter("Resultado", OracleDbType.Int32, ParameterDirection.ReturnValue);
            cmd.Parameters.Add(Resultado);

            cmd.Parameters.Add("Pnombre_empresa", model.CompanyName);
            cmd.Parameters.Add("Ppassword_empresa", ObtenerHash(model.Password));
            cmd.Parameters.Add("Pnumero_telefono", model.CompanyPhoneNumber);
            cmd.Parameters.Add("Pdirección", model.CompanyAddress);
            cmd.Parameters.Add("Pcorreo_tienda", model.Email);
            cmd.Parameters.Add("Pprovincia", model.CompanyCity);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void RegistrarCliente(RegisterViewModel model)
        {

            RegistrarUsuario(model.Email, "C");

            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prc_insertar_cliente";

            cmd.Parameters.Add("PNombre_cliente", model.Name);
            cmd.Parameters.Add("PApellido_cliente", model.Lastname);
            cmd.Parameters.Add("PPassword", ObtenerHash(model.Password));
            cmd.Parameters.Add("PCorreo_cliente", model.Email);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void RegistrarUsuario(String correo, String tipo)
        {

            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prc_insertar_usuario";

            cmd.Parameters.Add("Pcorreo", correo);
            cmd.Parameters.Add("PTipo", tipo);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        // Generar hash.
        public string ObtenerHash(string input)
        {
            MD5 md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verificar el hash.
        public bool VerificarHash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = ObtenerHash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GuardarProducto(Producto producto)
        {
             conexion = new OracleConnection(cadena);
            conexion.Open();
            cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "fun_insertar_producto"; 
            OracleParameter Resultado = new OracleParameter("Resultado", OracleDbType.Int32, ParameterDirection.ReturnValue);
            cmd.Parameters.Add(Resultado);
            cmd.Parameters.Add("correo_tienda", producto.id_empresa);
            cmd.Parameters.Add("descripcion",producto.description);
            cmd.Parameters.Add("categoria", producto.categoria);


            cmd.ExecuteNonQuery();

            int idPlan = int.Parse(Resultado.Value.ToString());

            conexion.Close();
            return idPlan;
        }

        public void guardarDetalle(detalle_producto detalle)
        {
            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prc_insertar_det_producto";
            cmd.Parameters.Add("id_producto", detalle.id_producto);
            cmd.Parameters.Add("cantidad", detalle.cantidad);
            cmd.Parameters.Add("color", detalle.color);
            cmd.Parameters.Add("talla", detalle.talla);
            cmd.Parameters.Add("precio", detalle.precio);
            cmd.Parameters.Add("imagen", detalle.imagen);
            cmd.Parameters.Add("promocion", detalle.promocion.ToString());
            cmd.Parameters.Add("genero", detalle.genero.ToString());
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        //---------------CLIENTE---------------

        public List<Producto> ObtenerProductosHombre(String categoria) {
            List<Producto> listaAbrigos = new List<Producto>();

            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd = new OracleCommand("select p.id_producto,p.correo_tienda,p.descripcion,p.categoria from producto p, detalle_producto d where p.categoria = :categoria and d.genero = :genero and p.id_producto = d.id_producto", conexion);
            cmd.Parameters.Add("categoria", OracleDbType.Varchar2).Value = categoria;
            cmd.Parameters.Add("genero", OracleDbType.Varchar2).Value = "Hombre";

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Producto abrigo = new Producto();
                abrigo.id_producto = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                abrigo.id_empresa = reader.IsDBNull(1) ? "" : reader.GetString(1);
                abrigo.description = reader.IsDBNull(2) ? "" : reader.GetString(2);
                abrigo.categoria = reader.IsDBNull(3) ? "" : reader.GetString(3);
                abrigo.list_detalle_producto = getDetalles(abrigo.id_producto);
                listaAbrigos.Add(abrigo);
            }

            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
            conexion.Dispose();

            return listaAbrigos;
        }

        public List<int> ObtenerPreciosProductosHombre(String categoria)
        {
            List<int> listaPrecios = new List<int>();

            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd = new OracleCommand("select distinct d.precio from producto p, detalle_producto d where p.categoria = :categoria and d.genero = :genero", conexion);
            cmd.Parameters.Add("categoria", OracleDbType.Varchar2).Value = categoria;
            cmd.Parameters.Add("genero", OracleDbType.Varchar2).Value = "Hombre";

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int precio = 0;
                precio = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                listaPrecios.Add(precio);
            }

            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
            conexion.Dispose();

            return listaPrecios;
        }

        public List<String> ObtenerColoresProductosHombre(String categoria)
        {
            List<String> listaPrecios = new List<String>();

            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd = new OracleCommand("select distinct d.color from producto p, detalle_producto d where p.categoria = :categoria and d.genero = :genero", conexion);
            cmd.Parameters.Add("categoria", OracleDbType.Varchar2).Value = categoria;
            cmd.Parameters.Add("genero", OracleDbType.Varchar2).Value = "Hombre";

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String precio = "";
                precio = reader.IsDBNull(0) ? "" : reader.GetString(0);
                listaPrecios.Add(precio);
            }

            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
            conexion.Dispose();

            return listaPrecios;
        }

        public List<Producto> ObtenerProductosHombreFiltros(Filtrar model)
        {
            List<Producto> listaAbrigos = new List<Producto>();

            conexion = new OracleConnection(cadena);
            conexion.Open();

            String cad = "";

            if (model.color != null) {
                if (model.color.Count == 1)
                    cad += " and d.color = '" + model.color.ElementAt(0) + "'";
                else {
                    cad += " and (";
                    for (int i = 0; i < model.color.Count; i++) {
                        if (model.color.Count != i + 1)
                            cad += " d.color = '" + model.color.ElementAt(i) + "' or";
                        else
                            cad += " d.color = '" + model.color.ElementAt(i) + "'";
                    }
                    cad += ")";
                }
            }
            if (model.precio != null) {
                if (model.precio.Count == 1)
                    cad += " and d.precio = '" + model.precio.ElementAt(0) + "'";
                else
                {
                    cad += " and (";
                    for (int i = 0; i < model.precio.Count; i++)
                    {
                        if (model.precio.Count != i + 1)
                            cad += " d.precio = '" + model.precio.ElementAt(i) + "' or";
                        else
                            cad += " d.precio = '" + model.precio.ElementAt(i) + "'";
                    }
                    cad += ")";
                }
            }

            cmd = new OracleCommand("select p.id_producto,p.correo_tienda,p.descripcion,p.categoria from producto p, detalle_producto d where p.categoria = :categoria and d.genero = :genero and p.id_producto = d.id_producto"+cad, conexion);
            cmd.Parameters.Add("categoria", OracleDbType.Varchar2).Value = model.categoria;
            cmd.Parameters.Add("genero", OracleDbType.Varchar2).Value = model.genero;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Producto abrigo = new Producto();
                abrigo.id_producto = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                abrigo.id_empresa = reader.IsDBNull(1) ? "" : reader.GetString(1);
                abrigo.description = reader.IsDBNull(2) ? "" : reader.GetString(2);
                abrigo.categoria = reader.IsDBNull(3) ? "" : reader.GetString(3);
                abrigo.list_detalle_producto = getDetalles(abrigo.id_producto);
                listaAbrigos.Add(abrigo);
            }

            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
            conexion.Dispose();

            return listaAbrigos;
        }

        //---------------EMPRESA---------------
        public void insertarProducto(String description, string correo_tienda, String categoria, List<detalle_producto> Lista_detalles)
        {
            conexion = new OracleConnection(cadena);
            conexion.Open();
            cmd = new OracleCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "fun_insertar_producto";
            OracleParameter Resultado = new OracleParameter("Resultado", OracleDbType.Int32, ParameterDirection.ReturnValue);
            cmd.Parameters.Add(Resultado);
            cmd.Parameters.Add("descripcion", description);
            cmd.Parameters.Add("categoria", categoria);
            cmd.Parameters.Add("correo_tienda", correo_tienda);

            cmd.ExecuteNonQuery();
            int idArticulo = int.Parse(Resultado.Value.ToString());
            foreach (detalle_producto detalle in Lista_detalles)
            {
                if (detalle.cantidad != 0 && detalle.color != null && detalle.precio != 0)
                {
                    cmd = new OracleCommand();
                    cmd.Connection = conexion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "fun_insertar_detalle";
                    OracleParameter Resultado2 = new OracleParameter("Resultado", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add(Resultado2);
                    cmd.Parameters.Add("id_producto", Resultado);
                    cmd.Parameters.Add("cantidad", detalle.cantidad);
                    cmd.Parameters.Add("color", detalle.color);
                    cmd.Parameters.Add("talla", detalle.talla);
                    cmd.Parameters.Add("precio", detalle.precio);
                    cmd.Parameters.Add("imagen", detalle.imagen);
                    cmd.Parameters.Add("promocion", detalle.promocion);
                    cmd.Parameters.Add("genero", detalle.genero);
                    cmd.ExecuteNonQuery();
                }
            }
            conexion.Close();
        }

        public List<Producto> getProducto(String correo_tienda)
        {
            Producto item = new Producto();
            List<Producto> listaItem = new List<Producto>();
            listaItem.Clear();
            conexion = new OracleConnection(cadena);
            String sql = String.Format("select id_producto,descripcion,categoria from producto  where correo_tienda='{0}'", correo_tienda);
            conexion.Open();
            cmd = new OracleCommand(sql, conexion);
            OracleDataReader reader = cmd.ExecuteReader();
            List<detalle_producto> listadet = new List<detalle_producto>();
            while (reader.Read())
            {
                item = new Producto();
                listadet = new List<detalle_producto>();
                item.id_empresa = correo_tienda;
                item.id_producto = reader.GetInt32(0);
                item.description = reader.IsDBNull(1) ? "" : reader.GetString(1);
                item.categoria = reader.IsDBNull(2) ? "" : reader.GetString(2);
                listadet = getDetalles(item.id_producto);
                item.list_detalle_producto = listadet;
                listaItem.Add(item);
            }
           
            reader.Dispose();
            cmd.Dispose();
            conexion.Close();

            return listaItem;
        }

        public void insertarDetalle() { }

        public List<detalle_producto> getDetalles(int id_producto)
        {
            detalle_producto item = new detalle_producto();
            List<detalle_producto> listaItem = new List<detalle_producto>();
            listaItem.Clear();
            conexion = new OracleConnection(cadena);
            conexion.Open();
            String sql = String.Format("select id_detalle,cantidad,color,talla,precio,imagen,promocion,genero from detalle_producto  where id_producto={0}", id_producto);
            cmd = new OracleCommand(sql, conexion);
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                item = new detalle_producto();
                item.id_detalle = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                item.cantidad = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                item.color = reader.IsDBNull(2) ? "" : reader.GetString(2);
                item.talla = reader.IsDBNull(3) ? "" : reader.GetString(3);
                item.precio = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                item.imagen = reader.IsDBNull(5) ? "" : reader.GetString(5);
                if (reader.GetString(6).Equals("True"))
                {
                    item.promocion = true;
                }
                else
                {
                    item.promocion = false;
                }
                item.genero = reader.IsDBNull(7) ? "" : reader.GetString(7);
                listaItem.Add(item);
            }
            reader.Dispose();
            cmd.Dispose();
            conexion.Close();

            return listaItem;
        }

        public detalle_producto ObtenerDetalle(int id_detalle)
        {
            detalle_producto detalle = new detalle_producto();

            conexion = new OracleConnection(cadena);
            conexion.Open();
            String sql = String.Format("select id_detalle,id_producto, cantidad,color,talla,precio,imagen,promocion,genero from detalle_producto  where id_detalle={0}", id_detalle);
            cmd = new OracleCommand(sql, conexion);
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                detalle = new detalle_producto();
                detalle.id_detalle = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                detalle.id_producto = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                detalle.cantidad = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                detalle.color = reader.IsDBNull(3) ? "" : reader.GetString(3);
                detalle.talla = reader.IsDBNull(4) ? "" : reader.GetString(4);
                detalle.precio = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                detalle.imagen = reader.IsDBNull(6) ? "" : reader.GetString(6);
              
                if (reader.GetString(7).Equals("True"))
                {
                    detalle.promocion = true;
                }
                else
                {
                    detalle.promocion = false;
                }
                detalle.genero = reader.IsDBNull(8) ? "" : reader.GetString(8);
            }
            reader.Dispose();
            cmd.Dispose();
            conexion.Close();

            return detalle;
        }

        public Producto ObtenerProducto(int id_producto)
        {
            Producto producto = new Producto();
            List<detalle_producto> listadet = new List<detalle_producto>();
            conexion = new OracleConnection(cadena);
            conexion.Open();
            String sql = String.Format("select id_producto, correo_tienda,descripcion,categoria from Producto  where id_producto={0}", id_producto);
            cmd = new OracleCommand(sql, conexion);
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                producto = new Producto();
                listadet = new List<detalle_producto>();
                producto.id_producto = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                producto.id_empresa = reader.IsDBNull(1) ? "" : reader.GetString(1);
                producto.description = reader.IsDBNull(2) ? "" : reader.GetString(2);
                producto.categoria = reader.IsDBNull(3) ? "" : reader.GetString(3);
                listadet = getDetalles(producto.id_producto);
                producto.list_detalle_producto = listadet;
            }
            reader.Dispose();
            cmd.Dispose();
            conexion.Close();

            return producto;
        }

        public void editarDetalle(detalle_producto detalle)
        {
            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PRC_actualizar_detalle";
          

            cmd.Parameters.Add("id_detalle", detalle.id_detalle);
            cmd.Parameters.Add("Pcantidad", detalle.cantidad);
            cmd.Parameters.Add("Pcolor", detalle.color);
            cmd.Parameters.Add("Ptalla", detalle.talla);
            cmd.Parameters.Add("Pprecio", detalle.precio);
            cmd.Parameters.Add("Pimagen", detalle.imagen);
            cmd.Parameters.Add("Ppromocion", detalle.promocion.ToString());
            cmd.Parameters.Add("Pgenero", detalle.genero);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }


        public void EliminarDetalle(int idDeta)
        {
            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prc_eliminar_detalle";

            cmd.Parameters.Add("Pid_detalle", idDeta);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conexion.Close();
        }

        public void EliminarDetallesProducto(int id_producto)
        {
            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();
            String sql = String.Format("delete detalle_producto where id_producto = {0}", id_producto);
            cmd = new OracleCommand(sql, conexion);
            OracleDataReader reader = cmd.ExecuteReader();
            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
        }
        public void EliminarProducto(int id_producto)
        {
            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();
            String sql = String.Format("delete Producto where id_producto = {0}", id_producto);
            cmd = new OracleCommand(sql, conexion);
            OracleDataReader reader = cmd.ExecuteReader();
            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
        }

        public void guardarPromocion(Promocion promocion)
        {
            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "fun_insertar_promocion";
            OracleParameter Resultado = new OracleParameter("Resultado", OracleDbType.Int32, ParameterDirection.ReturnValue);
            cmd.Parameters.Add(Resultado);
            cmd.Parameters.Add("Pid_detalle", promocion.id_detalle);
            cmd.Parameters.Add("Pnuevo_precio", promocion.nuevo_precio);
            cmd.Parameters.Add("Pfecha_inicio", promocion.fecha_inicio);
            cmd.Parameters.Add("Pfecha_final", promocion.fecha_final);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
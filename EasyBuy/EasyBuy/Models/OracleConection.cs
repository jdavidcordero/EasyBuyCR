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

        public Usuario validarUsuario(String correo, String contrasena)
        {
            Usuario usuario = null;
            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd = new OracleCommand("select id_cliente, nombre_cliente, apellido_cliente,correo_cliente from cliente where password = :contra and correo_cliente= :correo", conexion);

            cmd.Parameters.Add("contra", OracleDbType.Varchar2).Value = ObtenerHash(contrasena);
            cmd.Parameters.Add("correo", OracleDbType.Varchar2).Value = correo;

            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                usuario = new Usuario();
                usuario.CEDULA = reader.IsDBNull(0) ? "" : reader.GetString(0);
                usuario.NOMBRE = reader.IsDBNull(1) ? "" : reader.GetString(1);
                usuario.APELLIDO1 = reader.IsDBNull(2) ? "" : reader.GetString(2);
                usuario.CORREO = reader.IsDBNull(3) ? "" : reader.GetString(3);

            }

            // Libera todo los recursos utilizados
            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
            conexion.Dispose();

            return usuario;
        }

        public void RegistrarEmpresa(CompanyRegisterViewModel model) {

            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prc_insertar_empresa";

            cmd.Parameters.Add("PNombre_empresa", model.CompanyName);
            cmd.Parameters.Add("PNumero_telefono", model.CompanyPhoneNumber);
            cmd.Parameters.Add("PDirección", model.CompanyAddress);
            cmd.Parameters.Add("PCorreo_tienda", model.Email);
            cmd.Parameters.Add("PPassword", model.Password);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void RegistrarCliente(RegisterViewModel model)
        {

            cmd = new OracleCommand();
            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prc_insertar_cliente";

            cmd.Parameters.Add("PNombre_cliente", model.Name);
            cmd.Parameters.Add("PApellido_cliente", model.Lastname);
            cmd.Parameters.Add("PPassword", model.Password);
            cmd.Parameters.Add("PCorreo_cliente", model.Email);
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
            cmd.Parameters.Add("descripcion",producto.description);
            cmd.Parameters.Add("id_empresa", producto.id_empresa);

            cmd.ExecuteNonQuery();

            int idPlan = int.Parse(Resultado.Value.ToString());

            conexion.Close();
            return idPlan;
        }

        public void insertarProducto(String description, int id_empresa, List<detalle_producto> Lista_detalles)
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
            cmd.Parameters.Add("id_empresa", id_empresa);

            cmd.ExecuteNonQuery();
            int idArticulo = int.Parse(Resultado.Value.ToString());
            foreach (detalle_producto detalle in Lista_detalles)
            {
                if (detalle.cantidad != 0 && detalle.color != null && detalle.precio != null)
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
                    cmd.ExecuteNonQuery();
                }
            }
            conexion.Close();
        }

        public void insertarDetalle() { }
    }
}
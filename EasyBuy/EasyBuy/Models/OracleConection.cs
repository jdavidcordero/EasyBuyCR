﻿using System;
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

        //---------------CLIENTE---------------

        public List<Producto> ObtenerAbrigosHombre() {
            List<Producto> listaAbrigos = null;

            conexion = new OracleConnection(cadena);
            conexion.Open();

            cmd = new OracleCommand("select id_producto,correo_tienda,descripcion,categoria from producto where categoria = :categoria", conexion);
            cmd.Parameters.Add("categoria", OracleDbType.Varchar2).Value = "abrigos";

            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Producto abrigo = new Producto();
                abrigo.id_producto = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                abrigo.id_empresa = reader.IsDBNull(1) ? "" : reader.GetString(1);
                abrigo.description = reader.IsDBNull(2) ? "" : reader.GetString(2);
                abrigo.categoria = reader.IsDBNull(3) ? "" : reader.GetString(3);
                List<detalle_producto> listaDetalle = new List<detalle_producto>();
                //listaDetalle = getDetalles(abrigo.id_producto);
            }

            reader.Dispose();
            cmd.Dispose();
            conexion.Close();
            conexion.Dispose();

            return listaAbrigos;
        }

        

        //---------------EMPRESA---------------
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
    }
}
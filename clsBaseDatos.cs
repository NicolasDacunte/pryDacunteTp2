using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace pryDacunteTp2
{
    

    internal class clsBaseDatos
    {
        OleDbConnection conexionBD;
        OleDbCommand comandoBD;
        OleDbDataReader lectorBD;

        OleDbDataAdapter adaptadorBD;
        DataSet objDS;

        

        string rutaArchivo;
        public string estadoConexion;

        public clsBaseDatos()
        {
            try
            {
                rutaArchivo = @"../../Archivos/BDusuarios.accdb";

                conexionBD = new OleDbConnection();
                conexionBD.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= BDusuarios.mdb";
                conexionBD.Open();

                objDS = new DataSet();

                estadoConexion = "Conectado";
                
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }
        }

        public void RegistrarUsuario(string nombreUsuario, string contraseña)
        {
            try
            {
                comandoBD = new OleDbCommand();

                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.Text;
                comandoBD.CommandText = "INSERT INTO Usuario (NombreUsuario, Contraseña) VALUES (@nombreUsuario, @contraseña)";

                // Agregar parámetros para evitar la inyección de SQL
                comandoBD.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                comandoBD.Parameters.AddWithValue("@contraseña", contraseña);

                // Ejecutar la consulta para insertar el nuevo usuario
                comandoBD.ExecuteNonQuery();

                estadoConexion = "Usuario registrado correctamente";
                MessageBox.Show("Usuario registrado correctamente");
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
                MessageBox.Show("Error al registrar usuario: " + error.Message);
            }
        }

        public void RegistroLogInicioSesion()
        {
            try
            {
                comandoBD = new OleDbCommand();

                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = System.Data.CommandType.TableDirect;
                comandoBD.CommandText = "Logs";

                adaptadorBD = new OleDbDataAdapter(comandoBD);

                adaptadorBD.Fill(objDS, "Logs");

                DataTable objTabla = objDS.Tables["Logs"];
                DataRow nuevoRegistro = objTabla.NewRow();

                nuevoRegistro["Categoria"] = "Inicio Sesión";
                nuevoRegistro["FechaHora"] = DateTime.Now;
                nuevoRegistro["Descripcion"] = "Inicio exitoso";

                objTabla.Rows.Add(nuevoRegistro);

                OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptadorBD);
                adaptadorBD.Update(objDS, "Logs");

                estadoConexion = "Registro exitoso de log";
                MessageBox.Show("Registro exitoso de log");
            }
            catch (Exception error)
            {

                estadoConexion = error.Message;
            }

        }

        public void ValidarUsuario(string nombreUser, string passUser)
        {
            try
            {
                comandoBD = new OleDbCommand();

                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = System.Data.CommandType.TableDirect;
                comandoBD.CommandText = "Usuario";

                lectorBD = comandoBD.ExecuteReader();

                if (lectorBD.HasRows)
                {
                    while (lectorBD.Read())
                    {
                        if (lectorBD[1].ToString() == nombreUser && lectorBD[2].ToString() == passUser)
                        {
                            estadoConexion = "Usuario EXISTE";
                            MessageBox.Show("Usuario EXISTE");
                        }
                    }
                }

            }
            catch (Exception error)
            {

                estadoConexion = error.Message;
            }
        }
    }
}

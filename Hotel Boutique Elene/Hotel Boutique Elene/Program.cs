using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace HotelBoutiqueElene
{

    class Program
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["HotelBoutiqueElene"].ConnectionString;
        static SqlConnection conexion = new SqlConnection(connectionString);
        static string cadena;
        static SqlCommand comando;


        static void Main(string[] args)
        {
            Menu();
            /*

            //INSERTAMOS UN REGISTRO A LA TABLA LIBRERIA
            conexion.Open();

            cadena = "INSERT INTO  VALUES (dni,nombre,apellido)";
            cadena = "INSERT INTO  VALUES (NHab,CheckIn)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();
            */

            /*
            //EDITAMOS UN REGISTRO DE LA TABLA LIBRERIA
            conexion.Open();

            cadena = "UPDATE LIBRERIA SET EJEMPLARES = 15 WHERE TEMA LIKE'MECANICA'";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();

            //HACEMOS UNA CONSULTA A LA TABLA LIBRERIA
            conexion.Open();

            cadena = "SELECT * FROM LIBRERIA";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                Console.WriteLine(registros["TEMA"].ToString() + "\t" + registros["ESTANTE"].ToString() + "\t" + registros["EJEMPLARES"].ToString());
                Console.WriteLine();
            }

            */
            //conexion.Close();
        }
        static void RegistrarCliente()
        {
            string dni, nombre, apellido;
            Console.WriteLine("Registro de Clientes\n");
            Console.WriteLine("Ingresa DNI : ");
            dni = Console.ReadLine();
            Console.WriteLine("Ingresa Nombre : ");
            nombre = Console.ReadLine();
            Console.WriteLine("Ingresa Apellido : ");
            apellido = Console.ReadLine();

            conexion.Open();


            cadena = "INSERT INTO Clientes VALUES ('" + dni + "','" + nombre + "','" + apellido + "')";
            //cadena = "INSERT INTO  VALUES (NHab,CheckIn)";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();

            //conexion.Open();
            //cadena = "INSERT INTO Reservas VALUES ('" + dni + "')";
            //comando = new SqlCommand(cadena, conexion);
            //comando.ExecuteNonQuery();

            conexion.Close();
        }
        static void Menu()
        {
            int optMenu = 0;
            string dni;
            do
            {
                Console.WriteLine("Bienvenido a Hotel Boutique Elene \n");
                Console.WriteLine(" 1)  Registro de Clientes  \n 2)  Modificar registro de Clientes \n 3)  Check - In  \n 4)  Check - Out  \n 5)  Salir");
                optMenu = Convert.ToInt32(Console.ReadLine());

                switch (optMenu)
                {
                    case 1:
                        RegistrarCliente();
                        break;
                    case 2:
                        Console.WriteLine("Ingresa el DNI del cliente");
                        dni = Console.ReadLine();
                        EditarCliente(dni);
                        break;
                    case 3:
                        Console.WriteLine("Ingresa el DNI del cliente para verificar su reserva:");
                        dni = Console.ReadLine();
                        CheckIn(dni);
                        break;
                    case 4:
                        Console.WriteLine("Ingresa el DNI del cliente para realizar el CheckOut: ");
                        dni = Console.ReadLine();
                        CheckOut(dni);
                        break;
                    case 5:
                        Console.WriteLine("Adios !");
                        break;
                    default:
                        Console.WriteLine("Error Ingresa de nuevo");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            } while (optMenu != 5);
        }

        static void EditarCliente(string dni)
        {
            bool vDni = true;
            string cdni = dni;
            do
            {
                conexion.Open();
                cadena = "SELECT * FROM Clientes WHERE dni = '" + cdni + "'";
                comando = new SqlCommand(cadena, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                //nregistro = Convert.ToString(SqlDataReader registro = comando.ExecuteReader());

                if (registros.Read())
                {
                    vDni = false;
                }
                else
                {
                    vDni = true;
                    Console.WriteLine("DNI erroneo , ingresa un nuevo DNI");
                    cdni = Console.ReadLine();
                }
                conexion.Close();
                registros.Close();
            } while (vDni != false);

            conexion.Open();

            string nNombre, nApellido;
            Console.WriteLine("Ingresa el Nuevo nombre");
            nNombre = Console.ReadLine();
            Console.WriteLine("Ingresa el Nuevo apellido");
            nApellido = Console.ReadLine();

            cadena = "UPDATE Clientes SET nombre = '" + nNombre + "', apellido = '" + nApellido +
                "' WHERE DNI LIKE('" + cdni + "')";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();
        }

        static void CheckIn(string dni)
        {
            bool vDni = true;
            string cdni = dni;
            do
            {
                conexion.Open();
                cadena = "SELECT * FROM Clientes WHERE dni = '" + cdni + "'";
                comando = new SqlCommand(cadena, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                //nregistro = Convert.ToString(SqlDataReader registro = comando.ExecuteReader());

                if (registros.Read())
                {
                    //Console.WriteLine("Le aparecera un listado con habitaciones disponibles.");
                    registros.Close();
                    habDisponibles(dni);

                    vDni = false;
                }
                else
                {
                    vDni = false;
                    Console.WriteLine("No estas registrado , te invitamos a registrarte ;)");
                    cdni = Console.ReadLine();
                }

                conexion.Close();
                registros.Close();

            } while (vDni != false);


        }

        static void habDisponibles(string dni)
        {
            string cdni = dni;
            string habEleg;
            cadena = "SELECT * FROM Habitaciones  WHERE Estado LIKE 'LIBRE'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Console.WriteLine(registros["CodHabitacion"].ToString() + "\t"
                    + registros["Estado"].ToString());
                Console.WriteLine();
            }

            Console.WriteLine(" Elige una habitacion disponible");
            habEleg = Console.ReadLine();
            conexion.Close();
            registros.Close();

            //CREAR CODIGO DE RESERVA
            conexion.Open();
            cadena = "SELECT max(CodReserva)FROM Reservas ";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader codReservaR = comando.ExecuteReader();
            int codReserva = Convert.ToInt32(codReservaR.Read()) + 1;

            cadena = "UPDATE Habitaciones SET Estado = 'OCUPADO' WHERE codHabitacion LIKE  '" + habEleg + "' ";
            conexion.Close();
            registros.Close();
            conexion.Open();
            cadena += "INSERT INTO Reservas(CodReserva,FechaCheckIn,DNI,CodHabitacion) values ('" +codReservaR+ "', '" + DateTime.Now + "','" + cdni + "','" + habEleg + "')";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            registros.Close();

            Console.WriteLine("La habitacion numero " + habEleg + " ha sido elegida en la fecha : " + DateTime.Now);

        }

        static void CheckOut(string dni)
        {
            bool vDni = true;
            string cdni = dni;
            do
            {
                conexion.Open();
                cadena = "SELECT * FROM Reservas WHERE dni = '" + cdni + "' AND FechaCheckOut is NULL";
                comando = new SqlCommand(cadena, conexion);
                SqlDataReader registros = comando.ExecuteReader();


                if (registros.Read())
                {
                    //string fecha;
                    //fecha= DateTime.Now;
                    conexion.Close();
                    cadena = "UPDATE Reservas SET FechaCheckOut = GETDATE() WHERE DNI LIKE  '" + cdni + "' ";
                    comando = new SqlCommand(cadena, conexion);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    registros.Close();
                    Console.WriteLine("Se acaba de hacer el checkout del dni: " + cdni);
                    vDni = false;
                }
                else
                {
                    vDni = false;
                    Console.WriteLine("Dni incorrecto");
                    cdni = Console.ReadLine();
                }
                conexion.Close();
                registros.Close();

            } while (vDni != false);
        }

        static void habOcupadas()
        {
            string habOcup;
            //si nHab = libre 
            cadena = "SELECT * FROM Habitaciones  WHERE Estado LIKE 'OCUPADO'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Console.WriteLine(registros["CodHabitacion"].ToString() + "\t"
                    + registros["Estado"].ToString());
                Console.WriteLine();
            }
            Console.WriteLine(" Elige una habitacion ocupada");
            habOcup = Console.ReadLine();
            conexion.Close();

            conexion.Open();

            cadena = "UPDATE Habitaciones SET Estado = 'LIBRE' WHERE codHabitacion LIKE  '" + habOcup + "' ";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            registros.Close();


            conexion.Open();
            cadena = "UPDATE Reservas SET FechaCheckOut=GETDATE() WHERE codHabitacion LIKE  '" + habOcup + "' ";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            registros.Close();


            Console.WriteLine("Se ha realizado el Checkout de la habitacion " + habOcup + " con fecha: " + DateTime.Now);

        }

    }
}





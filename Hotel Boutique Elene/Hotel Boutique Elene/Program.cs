using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ConexionBBDD
{
    class Program
    {
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

            Console.ReadLine();
            //conexion.Close();


        }
        static void RegistrarCliente()


        {
            String connectionString = ConfigurationManager.ConnectionStrings["HotelBoutiqueElene"].ConnectionString;
            SqlConnection conexion = new SqlConnection(connectionString);
            string cadena;
            SqlCommand comando;


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

        }
        static void Menu()
        {
            int optMenu = 0;



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
                        Console.WriteLine("Registro de Clientes");
                        break;
                    case 3:
                        Console.WriteLine("Registro de Clientes");
                        break;
                    case 4:
                        Console.WriteLine("Registro de Clientes");
                        break;
                    case 5:
                        Console.WriteLine("Registro de Clientes");
                        break;

                    default:
                        Console.WriteLine("Error Ingresa de nuevo");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }

            } while (optMenu != 5);


        }

    }
}

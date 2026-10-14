using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace actividad_sem_09
{
    internal class Program
    {
        static int max_emp = 50;
        static string[] nombre_empl = new string[max_emp];
        static string[] correo_emp = new string[max_emp];
        static int cont_empl = 0;
        static string rutaArchivo = "empleados_datos.txt";
        static public void Titulo()
        {
            Console.WriteLine("*******************************************");
            Console.WriteLine("**SISTEMA DE EMPLEADOS ARREGLOS Y CADENAS**");
            Console.WriteLine("*******************************************");
        }
        static public void Registrar_empleados()
        {
            Console.WriteLine("****REGISTRO DE EMPLEADOS****");
            if (cont_empl >= max_emp)
            {
                Console.WriteLine("Capacidad maxima alcanzada");
                return;

            }
            Console.Write("Ingresar nombres y apellidos: ");
            string nombres = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nombres))
            {
                Console.WriteLine("Error: El nombre no puede estar vacio...!");
                return;
            }
            string nombres_Mayus = nombres.ToUpper();
            string[] partes = nombres_Mayus.Split(' ');
            string primer_nombre;
            if (partes.Length > 0)
                primer_nombre = partes[0];
            else
                primer_nombre = "Usuario";
            string correo = $"{primer_nombre.ToLower()}@upn.edu.pe";
            nombre_empl[cont_empl] = nombres_Mayus;
            correo_emp[cont_empl] = correo;
            cont_empl++;
        }
        static public void buscar_emp()
        {
            Console.WriteLine("****BUSCAR EMPLEADO****");
            Console.WriteLine("***********************");
            if (cont_empl == 0)
            {
                Console.WriteLine("No hay empleado a buscar: ");
                return;
            }
            Console.WriteLine("Ingresar el nombre del empleado a buscar");
            string buscar_nombre = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(buscar_nombre))
                return;
            bool encontrado = false;
            for (int i = 0; i < cont_empl; i++)
            {
                if (nombre_empl[i].Contains(buscar_nombre))
                {
                    Console.WriteLine("Nombre: " + nombre_empl[i] + " correo: " + correo_emp[i]);
                    encontrado = true;
                }

            }
            if (!encontrado)
                Console.WriteLine("Empleado no encontrado");

        }

        static public void ordenar_alfa()
        {
            if (cont_empl <= 1)
            {
                Console.WriteLine("No hay suficientes empleados para ordenar");
                return;
            }
            for (int i = 0; i < cont_empl - 1; i++)
            {
                for (int j = 0; j < cont_empl - 1; j++)
                {
                    if (string.Compare(nombre_empl[j], nombre_empl[j + 1]) > 0)
                    {
                        string temp_nombre = nombre_empl[j];
                        nombre_empl[j] = nombre_empl[j + 1];
                        nombre_empl[j + 1] = temp_nombre;

                        string temp_correo = correo_emp[j];
                        correo_emp[j] = correo_emp[j + 1];
                        nombre_empl[j + 1] = temp_correo;
                    }

                }
            }
        }

        static public void Mostrar()
        {
            Console.WriteLine("****Listado de empleados****");
            Console.WriteLine("****************************");
            if (cont_empl == 0)
            {
                Console.WriteLine("No hay empleados registrados");
                return;
            }
            Console.WriteLine("Nombres y Apellidos\t\tCorreo corporativo");
            for (int i = 0; i < cont_empl; i++)
            {
                Console.WriteLine(nombre_empl[i] + "\t\t" + correo_emp[i]);
            }
        }
        static public void Guardar_datos_archivos()
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("*****GUARDAR DATOS EN ARCHIVO*****");
            Console.WriteLine("**********************************");
            if (cont_empl == 0)
            {
                Console.WriteLine("No hay empleados registrados");
                return;
            }
            using (StreamWriter escribir = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i < cont_empl; i++)
                {
                    escribir.WriteLine(nombre_empl[i] + "," + correo_emp[i]);
                }
            }
            Console.WriteLine(cont_empl + " empleados guardados");
        }
        static public void leer_datos_archivos()
        {
            Console.WriteLine("******LEER DATOS DESDE ARCHIVO******");
            Console.WriteLine("************************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("Archivo no existe");
                return;
            }
            using (StreamReader lector = new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null)
                {

                    string[] partes = linea.Split(',');
                    if (partes.Length == 2)
                    {
                        Console.WriteLine("Nombres:" + partes[0] + " Correo: " + partes[1]);
                    }
                }
            }
        }
        static public void Cargar_datos_desde_Archivo()
        {
            Console.WriteLine("*****CARGAR DATOS DESDE ARCHIVO*****");
            Console.WriteLine("************************************");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("Archivo no existe");
                return;
            }
            string[] lineas = File.ReadAllLines(rutaArchivo);
            cont_empl = 0;
            for (int i = 0; i < lineas.Length && i < max_emp; i++)
            {
                string[] partes = lineas[i].Split(',');
                if (partes.Length == 2)
                {
                    nombre_empl[cont_empl] = partes[0];
                    correo_emp[cont_empl] = partes[1];
                    cont_empl++;
                }
            }
        }
        static void Main(string[] args)
        {
            int opc = 0;
            while (opc != 8)
            {
                Console.Clear();
                Console.WriteLine("[1]Registrar empelado");
                Console.WriteLine("[2]Buscar empleado");
                Console.WriteLine("[3]Ordenar alfabeticamente");
                Console.WriteLine("[4]Mostrar empleados");
                Console.WriteLine("[5]Guardar datos en archivo");
                Console.WriteLine("[6]Leer datos del archivos");
                Console.WriteLine("[7]Cargar datos del archivo");
                Console.WriteLine("[8]Salir");
                Console.Write("Ingresar opcion:");
                if (!int.TryParse(Console.ReadLine(), out opc))
                {
                    Console.WriteLine("Error:Ingrese una opcion numerica");
                    continue;
                }
                switch (opc)
                {
                    case 1:
                        Registrar_empleados(); break;
                    case 2:
                        buscar_emp(); break;
                    case 3:
                        ordenar_alfa(); break;
                    case 4:
                        Mostrar(); break;
                    case 5:
                        Guardar_datos_archivos(); break;
                    case 6:
                        leer_datos_archivos(); break;
                    case 7:
                        Cargar_datos_desde_Archivo(); break;
                    case 8:
                        Console.WriteLine("Gracias por usar el sistema"); break;
                    default:
                        Console.WriteLine("Opcion no valida"); break;
                }
                Console.ReadKey();
            }


        }
    }
}
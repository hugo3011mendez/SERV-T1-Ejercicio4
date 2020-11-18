using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ejercicio4
{
    class Ejercicio
    {
        static Thread[] caballos = new Thread[5]; // Creo el array de 5 caballos

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Por qué caballo apuestas?");
                int apuesta = Convert.ToInt32(Console.ReadLine());

            }
            catch (Exception ex)
            {
                Console.WriteLine();

                if (ex is FormatException)
                {
                    Console.WriteLine("Tienes que escribir un número! Los caballos se diferencian por sus números");
                }
                else if (ex is OverflowException)
                {
                    Console.WriteLine("Has escrito un dato demasiado grande! Los caballos sólo van del 1 al 5...");
                }
            }
        }
    }
}

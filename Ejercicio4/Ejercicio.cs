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
        static Caballo[] caballos = new Caballo[5]; // Creo el array de 5 caballos
        static int meta = 400; // Creo y declaro la variable meta para determinar a cuántos metros está la meta

        static bool flag = false; // Creo y declaro el flag
        static readonly object l = new object(); // Creo el objeto del lock, porque los hilos están usando recursos iguales


        // Aquí hago la función que van a realizar los hilos de los caballos
        static void accionesCaballo(Caballo caballo)
        {
            lock (l)
            {
                if (!flag)
                {

                    while (!flag)
                    {
                        Random generador = new Random();
                        int dormir = generador.Next(100, 5000);

                        caballo.correr(); // Llamo a la función correr para ver cuánta distancia recorre el caballo
                        Thread.Sleep(dormir);

                        if (caballo.Posicion >= meta)
                        {
                            flag = true;
                        }
                    }
                } 
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Por qué caballo apuestas?");
                int apuesta = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i] = new Caballo();

                }

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

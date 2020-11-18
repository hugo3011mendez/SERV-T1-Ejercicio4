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
        static readonly int meta = 400; // Creo y declaro la variable meta para determinar a cuántos metros está la meta

        static bool flag = false; // Creo y declaro el flag
        static readonly object l = new object(); // Creo el objeto del lock, porque los hilos están usando recursos iguales


        // Aquí hago la función que van a realizar los hilos de los caballos
        static void accionesCaballo(object param)
        {
            Caballo caballo = (Caballo)param;

            while (!flag)
            {
                lock (l)
                {
                    if (!flag)
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
            Caballo[] caballos = new Caballo[5]; // Creo el array de 5 caballos
            Thread[] hilos = new Thread[5];

            try
            {
                Console.WriteLine("Por qué caballo apuestas?");
                int apuesta = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i] = new Caballo();
                    hilos[i] = new Thread(accionesCaballo);
                    hilos[i].Start(caballos[i]);
                }

                // Uso el Join para que el hilo Main se encuentre a la espera de que uno de los hilos acabe, y como acaban todos a la vez no importa el hilo que se debe poner en el Join
                hilos[0].Join();

                // Así muestro el caballo ganador
                for (int j = 0; j < caballos.Length; j++)
                {
                    if (caballos[j].Posicion >= meta)
                    {
                        Console.WriteLine("El caballo Nº " + j+1 + " gana!");
                    }
                }
                Console.ReadLine();
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

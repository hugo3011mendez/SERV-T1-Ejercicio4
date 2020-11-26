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
        static readonly int meta = 100; // Creo y declaro la variable meta para determinar a cuántos metros está la meta
        static Caballo[] caballos; // Creo el array de 5 caballos

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
                        int dormir = generador.Next(300, 1501);

                        caballo.correr(); // Llamo a la función correr para ver cuánta distancia recorre el caballo

                        // Así muestro qué caballo lleva recorridos cuántos metros
                        for (int i = 0; i < caballos.Length; i++)
                        {
                            if (caballo == caballos[i])
                            {
                                Console.SetCursorPosition(0, i+4);

                                for (int j = 0; j < caballo.Posicion; j++)
                                {
                                    Console.Write("*");
                                }

                            }
                        }

                        Thread.Sleep(dormir); // Mando a dormir al hilo

                        // Aquí compruebo si el caballo ha llegado a la meta
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
            bool repetir = true;

            while (repetir)
            {
                Console.Clear(); // Limpio la pantalla cada vez que se repite para que las posiciones donde se muestra todo sigan funcionando

                // Escribo la línea de meta
                for (int i = 4; i <= 9; i++)
                {
                    Console.SetCursorPosition(101, i);
                    Console.Write("| Caballo" + (i-3));
                }

                Console.SetCursorPosition(0, 1); // Devuelvo el cursor a su posición original

                // Actualización de las variables al repetirse el programa
                caballos = new Caballo[5];
                Thread[] hilos = new Thread[5];
                flag = false;

                try
                {
                    Console.WriteLine("Por qué caballo apuestas?");
                    int apuesta = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

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
                            Console.SetCursorPosition(1, 10);
                            Console.WriteLine("El caballo Nº " + (j+1) + " gana!");
                            if (apuesta == (j+1))
                            {
                                Console.WriteLine("HAS GANADO!!!");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Quieres repetir? S/N");

                    if (Console.ReadLine().ToUpper() != "N")
                    {
                        repetir = true;
                    }
                    else
                    {
                        repetir = false;
                        Console.WriteLine("Pulsa Enter para salir : ");
                        Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();

                    if (ex is FormatException)
                    {
                        Console.WriteLine("Has escrito un tipo de dato que no vale, repite...");
                        repetir = true;
                    }
                    else if (ex is OverflowException)
                    {
                        Console.WriteLine("Has escrito un dato demasiado grande!");
                        repetir = true;
                    }
                }
            }
        }
    }
}

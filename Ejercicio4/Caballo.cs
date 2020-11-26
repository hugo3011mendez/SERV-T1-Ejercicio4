using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4
{
    class Caballo
    {
        public int Posicion { get; set; }

        public void correr()
        {
            Random generador = new Random();

            // Pongo 20 de distancia mínima porque los caballos suelen correr más que una persona, que recorriera 5m en un máximo de 5 segundos es poco creíble
            this.Posicion += generador.Next(1, 11);
        }

        public Caballo()
        {
            this.Posicion = 0;
        }
    }
}

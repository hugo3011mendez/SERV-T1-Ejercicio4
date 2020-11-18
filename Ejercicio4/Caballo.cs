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
            
            this.Posicion += generador.Next(1, 101);
        }

        public Caballo()
        {
            this.Posicion = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosEntity.Model
{
    public partial class Aula
    {
        public override string ToString()
        {
            return $"El nombre del aula es {nombre} y tiene una capacidad de {capacidad}";
        }
    }
}

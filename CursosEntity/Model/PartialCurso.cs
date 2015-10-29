using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosEntity.Model
{
    public partial class Curso
    {
        public override string ToString()
        {
            return $"Este es el curso de {nombre} que inicia en la fecha de {inicio} y acaba en la fecha de {fin}, teniendo una duración de {duracion}";
        }
    }
}

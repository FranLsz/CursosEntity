using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosEntity.Model
{
    public partial class Profesor
    {
        public override string ToString()
        {
            return $"El nombre del profesor es {nombre} y su edad es {edad}";
        }
    }
}

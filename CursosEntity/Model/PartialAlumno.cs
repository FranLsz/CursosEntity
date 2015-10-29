using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosEntity.Model
{
    public partial class Alumno
    {
        public override string ToString()
        {
            return $"El alumno se llama {nombre} y su DNI es {dni}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosEntity.Model
{
    public partial class Alumno15Entities
    {
        public Alumno15Entities(bool lazy) : base("name=Alumno15Entities")
        {
            Configuration.LazyLoadingEnabled = lazy;
        }

    }
}

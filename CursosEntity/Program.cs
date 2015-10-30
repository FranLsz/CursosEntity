using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursosEntity.Model;

namespace CursosEntity
{
    class Program
    {
        public static void InitBd()
        {

            using (var ctx = new Alumno15Entities())
            {
                if (ctx.Aula.Any())
                    return;

                var la = new List<Aula>()
                {
                     new Aula() { capacidad = 25, nombre = "Aula 01", },
                new Aula() { capacidad = 30, nombre = "Aula 05" },
                new Aula() { capacidad = 20, nombre = "Aula 14" },
                new Aula() { capacidad = 40, nombre = "Aula 21" }
            };


                ctx.Aula.AddRange(la);

                var lc = new List<Curso>()
                {
                    new Curso() {Aula = la[2], duracion = 120, inicio = DateTime.Now, fin = DateTime.Now.AddDays(30), nombre = "C++"},
                    new Curso() {Aula = la[0], duracion = 5, inicio = DateTime.Now, fin = DateTime.Now.AddDays(1), nombre = "Inteligencia emocional"}
                };

                ctx.Curso.AddRange(lc);

                var lp = new List<Profesor>()
                {
                    new Profesor() {edad = 29, nombre = "Pepe perez"},
                    new Profesor() {edad = 33, nombre = "Eva Jimenez"}
                };

                ctx.Profesor.AddRange(lp);

                var lal = new List<Alumno>()
                {
                    new Alumno() { dni = "37374859D", nombre = "Juan Martín" },
                    new Alumno() { dni = "23439483D", nombre = "Maria Ramírez" },
                    new Alumno() { dni = "48384833K", nombre = "Pepe Lucas" }
                };

                lal[0].Curso.Add(lc[1]);
                lal[0].Curso.Add(lc[0]);
                lal[1].Curso.Add(lc[1]);
                lal[2].Curso.Add(lc[1]);
                lal[2].Curso.Add(lc[0]);

                ctx.Alumno.AddRange(lal);

                var lpc = new List<ProfesorCurso>()
                {
                    new ProfesorCurso() {Curso = lc[1], Profesor = lp[0],duracion = 100},
                    new ProfesorCurso() {Curso = lc[0], Profesor = lp[0],duracion = 120},
                    new ProfesorCurso() {Curso = lc[1], Profesor = lp[1],duracion = 210},
                    new ProfesorCurso() {Curso = lc[0], Profesor = lp[1],duracion = 90}
                };


                ctx.ProfesorCurso.AddRange(lpc);

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

        }

        static void Main(string[] args)
        {

            var input = 6;

            do
            {
                Console.WriteLine("----------MENU----------");
                Console.WriteLine("1. Listar cursos");
                Console.WriteLine("2. Buscar cursos de un profesor");
                Console.WriteLine("3. Total de horas de un profesor");
                Console.WriteLine("4. Listar alumnos de un curso");
                Console.WriteLine("5. Ver los profesores de un alumno");
                Console.WriteLine("6. Salir");
                Console.Write("Opción: ");
                Int32.TryParse(Console.ReadLine(), out input);

                switch (input)
                {
                    case 1:
                        ListarCursos();
                        break;
                    case 2:
                        CursosDeProfe();
                        break;
                    case 3:
                        HorasDeProfe();
                        break;
                    case 4:
                        AlumnosDeCurso();
                        break;
                    case 5:
                        ProfesDeAlumno();
                        break;
                }



            } while (input != 6);


        }

        private static void ProfesDeAlumno()
        {
            using (var ctx = new Alumno15Entities())
            {
                Console.WriteLine("Introduce nombre del alumno: ");
                var nombre = Console.ReadLine();
                var alumno = ctx.Alumno.FirstOrDefault(o => o.nombre.Equals(nombre));
                if (alumno != null)
                {
                    var profesores = alumno.Curso.Select(o => o.ProfesorCurso.Select(oo => oo.Profesor));


                    if (profesores.Any())
                    {
                        foreach (var lp in profesores)
                        {
                            foreach (var p in lp)
                            {
                                Console.WriteLine(p);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("---Alumno no encontrado---");

                }
            }
        }

        private static void AlumnosDeCurso()
        {
            using (var ctx = new Alumno15Entities())
            {
                Console.WriteLine("Introduce nombre del curso: ");
                var nombre = Console.ReadLine();
                var curso = ctx.Curso.FirstOrDefault(o => o.nombre.Equals(nombre));
                if (curso != null)
                {
                    var alumnos = curso.Alumno.ToList();
                    if (alumnos.Count > 0)
                    {
                        foreach (var a in alumnos)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    else
                    {
                        Console.WriteLine("---Este curso no tiene alumnos---");
                    }
                }
                else
                {
                    Console.WriteLine("---Curso no encontrado---");

                }

            }


        }


        private static void HorasDeProfe()
        {
            using (var ctx = new Alumno15Entities())
            {
                Console.WriteLine("Introduce nombre del profesor: ");
                var nombre = Console.ReadLine();
                try
                {
                    var horas = ctx.Profesor.Where(o => o.nombre.Equals(nombre)).Sum(oo => oo.ProfesorCurso.Sum(ooo => ooo.duracion));
                    Console.WriteLine($"Este profesor tiene {horas}");
                }
                catch (Exception e)
                {
                    // Console.WriteLine(e.Message);
                    Console.WriteLine("---Imposible obtener las horas del profesor---");
                }
            }
        }

        private static void CursosDeProfe()
        {
            using (var ctx = new Alumno15Entities())
            {
                Console.WriteLine("Introduce nombre del profesor: ");
                var nombre = Console.ReadLine();
                var cursos = ctx.Profesor.Where(o => o.nombre.Equals(nombre)).
                    Select(oo => oo.ProfesorCurso.Select(ooo => ooo.Curso));
                if (cursos.Any())
                {
                    foreach (var lc in cursos)
                    {
                        foreach (var c in lc)
                        {
                            Console.WriteLine("---");
                            Console.WriteLine(c);
                            Console.WriteLine("---");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("---Ningun curso encontrado---");
                }
            }
        }

        private static void ListarCursos()
        {
            using (var ctx = new Alumno15Entities())
            {
                var cursos = ctx.Curso;

                if (cursos.Any())
                {
                    foreach (var c in cursos)
                    {
                        Console.WriteLine("---");
                        Console.WriteLine(c);
                        Console.WriteLine("---");
                    }
                }
                else
                {
                    Console.WriteLine("---Ningun curso encontrado---");
                }
            }
        }

        /*
                public static void ConsultaSimple()
                {
                    using (var ctx = new Alumno15Entities())
                    {
                        var data = ctx.Profesor.Where(o => o.nombre.Contains("Fran"));

                        foreach (var profesor in data)
                        {
                            Console.WriteLine(profesor);
                        }
                    }
                }

                public static void Suma()
                {
                    using (var ctx = new Alumno15Entities())
                    {
                        var data = ctx.Curso.Sum(o=>o.duracion);

                            Console.WriteLine(data);
                    }
                }

                public static void ObjetoDinamico()
                {
                    using (var ctx = new Alumno15Entities())
                    {
                        //poniendo Select devuelve un objeto dinamico con los miembros indicados
                        var data = ctx.Profesor.Where(o => o.nombre.Contains("Fran")).Select(o=>new {o.nombre,Antiguedad=o.edad});

                        foreach (var profesor in data)
                        {
                            Console.WriteLine(profesor.Antiguedad);
                        }
                    }
                }

                public static void BusquedaEnlazada()
                {
                    using (var ctx = new Alumno15Entities())
                    {
                        String[] dnis = {"1", "2"};
                        var curpro = ctx.ProfesorCurso.Where(o=> o.idProfesor == 1).Select(o => o.Curso);

                        curpro = curpro.Where(o => o.inicio == DateTime.Now).OrderBy(o => o.duracion);

                        curpro =curpro.Where(o => o.Alumno.Select(oo => oo.dni).Contains("1234A"));

                    }
                }

                public static void Subselect()
                {
                    using (var ctx = new Alumno15Entities())
                    {
                        var curpro = ctx.Alumno.Find("12345678A").Curso.Select(o => o.ProfesorCurso);

                    }
                }

                public static void SinLazy()
                {
                    using (var ctx = new Alumno15Entities())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        var alu = ctx.Alumno.Where(o => o.dni.Contains("A"));

                    }
                }
        */

    }

}

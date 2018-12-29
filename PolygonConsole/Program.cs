using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var printer = new Printer();
            Vector v = new Vector {X = 4, Y = 7};

            printer.Print("Hello World!");

            var rnd = new Random();
            printer.SetRandom(rnd);
            printer.SetVector(v);

            printer.Print("qwe");

            printer.SetRandom(null);

            printer.Print("123");

            Console.ReadLine();
        }
    }

    class Printer
    {
        private Random _Rnd;
        private Vector _Vector;

        public void SetVector(Vector vector)
        {
            _Vector = vector;
        }

        public void SetRandom(Random rnd)
        {
            _Rnd = rnd;
        }

        public void Print(string str)
        {
            if(_Rnd == null)
                Console.WriteLine(str);
            else
            {
                Console.WriteLine("{0} - {1}", _Rnd.Next(), str);
            }
            if(_Vector != null)
                Console.WriteLine($"{_Vector.X}:{_Vector.Y}");
        }
    }

    class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}

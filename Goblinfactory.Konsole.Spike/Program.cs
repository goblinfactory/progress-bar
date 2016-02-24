using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Goblinfactory.Konsole.Mocks;

namespace Goblinfactory.Konsole.Spike
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 50; i+=10)
            {
                Line(new XY(5, 5), new XY(80, i));                
            }
            Console.WriteLine("");
            Console.WriteLine(new String('-', 151));
            Console.ReadLine();
        }

        public static void Line(XY start, XY end)
        {
            int width = end.X - start.X;
            float yinc = ((float) (end.Y - start.Y))/width;
            float y = start.Y;
            for (int x = start.X; x <  end.X; x++)
            {
                Console.CursorTop = (int)y;
                Console.CursorLeft = x;
                Console.Write("*");
                y += yinc;
            }            
        }
    }
}

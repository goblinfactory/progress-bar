using System;
using Goblinfactory.Konsole;

namespace Goblinfactory.Konsole
{
    public class ConsoleWriter : IConsole
    {
        public void WriteLine(string format, params object[] args) { System.Console.WriteLine(format, args); }
        public void Write(string format, params object[] args) { System.Console.Write(format, args); }
        public int CursorTop { get { return System.Console.CursorTop; } set { System.Console.CursorTop = value; } }
        public int WindowWidth() { return System.Console.WindowWidth; }
        public ConsoleColor ForegroundColor { get { return System.Console.ForegroundColor; } set { System.Console.ForegroundColor = value; } }
    }
}
using System;

namespace Goblinfactory.ProgressBar
{
    public class ConsoleWriter : IConsole
    {
        public void WriteLine(string format, params object[] args) { Console.WriteLine(format, args); }
        public void Write(string format, params object[] args) { Console.Write(format, args); }
        public int CursorTop { get { return Console.CursorTop; } set { Console.CursorTop = value; } }
        public int WindowWidth() { return Console.WindowWidth; }
        public ConsoleColor ForegroundColor { get { return Console.ForegroundColor; } set { Console.ForegroundColor = value; } }
    }
}
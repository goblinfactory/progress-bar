using System;

namespace Goblinfactory.Konsole
{
    public interface IConsole
    {
        void WriteLine(string format, params object[] args);
        void Write(string format, params object[] args);
        int WindowWidth();
        int CursorTop { get; set; }
        ConsoleColor ForegroundColor { get; set; }
    }
}
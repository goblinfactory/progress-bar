using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// refactor mock console to support color
// and to use seperate 'object' per textposition to track what is written.

namespace Goblinfactory.Konsole.Mocks
{
    //public struct XY { public int X; public int Y; }
    /// <summary>
    /// mock console writer that emulates setting the line (y) position,
    /// which is sufficient detail for us to acceptance test this progressbar
    /// </summary>
    public class MockConsole : IConsole
    {
        private readonly int _windowWidth;
        private readonly bool _echo;
        private Dictionary<int,MockLine> _buffer;
        private int _y = 0;
        private int _x = 0;

        public MockConsole(int windowWidth, bool echo = true)
        {
            _windowWidth = windowWidth;
            _echo = echo;
            _buffer = new Dictionary<int, MockLine>();
        }

        public string Buffer
        {
            get { return string.Join("\r\n", Lines); }
        }

        public string[] Lines
        {
            get { return _buffer.Values.Select(b=>b.Text).ToArray();  }
        }

        public void WriteLine(string format, params object[] args)
        {
            var line = string.Format(format, args);
            _writeLine(line);
            if (_echo) Console.WriteLine(format,args);
        }

        private void _write(string text)
        {
            if (!_buffer.ContainsKey(_y)) _buffer.Add(_y, new MockLine());
            var line = _buffer[_y];
            if (line.Overwrite)
            {
                _buffer[_y] = new MockLine(text);
            }
            else
                _buffer[_y] = new MockLine(line.Text + text);
        }

        private void _writeLine(string text)
        {
            if (_buffer.ContainsKey(_y))
            {
                var line = _buffer[_y];
                if (line.Overwrite)
                {
                    _buffer[_y] = new MockLine(text);
                }
                else
                    _buffer[_y] = new MockLine(line.Text + text);
            }
            else
            {
                _buffer.Add(_y, new MockLine(false, text));
            }
            _y++;
        }

        public void Write(string format, params object[] args)
        {
            var line = string.Format(format, args);
            _write(line);
            if (_echo) Console.Write(line);
        }

        public int Y
        {
            get { return _y; }
            set
            {
                // mark all lines so that next write and writelines will overwrite the first next write only against each line.
                for (int i = value; i < _buffer.Count(); i++) _buffer[i].Overwrite = true;
                _y = value;
            }
        }

        public int X
        {
            get { return _x; }
            set
            {
                //// mark all lines so that next write and writelines will overwrite the first next write only against each line.
                //for (int i = value; i < _buffer.Count(); i++) _buffer[i].Overwrite = true;
                _x = value;
            }
        }

        public int WindowWidth()
        {
            return _windowWidth;
        }

        public ConsoleColor ForegroundColor
        {
            get
            {
                return Console.ForegroundColor;
            }
            set
            {
                Console.ForegroundColor = value;
            }
        }

        public void PrintAt(int x, int y, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void PrintAt(int x, int y, string text)
        {
            throw new NotImplementedException();
        }
    }
}

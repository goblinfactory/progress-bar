using System;
using System.Collections.Generic;
using System.Linq;
using Goblinfactory.Konsole;

namespace Goblinfactory.Konsole
{
    /// <summary>
    /// mock console writer that emulates setting the line (y) position,
    /// which is sufficient detail for us to acceptance test this progressbar
    /// </summary>
    public class MockConsole : IConsole
    {
        private readonly int _windowWidth;
        private readonly bool _echo;
        private Dictionary<int,MockLine> _buffer;
        private int _lineNumber = 0;

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
            if (!_buffer.ContainsKey(_lineNumber)) _buffer.Add(_lineNumber, new MockLine());
            var line = _buffer[_lineNumber];
            if (line.Overwrite)
            {
                _buffer[_lineNumber] = new MockLine(text);
            }
            else
                _buffer[_lineNumber] = new MockLine(line.Text + text);
        }

        private void _writeLine(string text)
        {
            if (_buffer.ContainsKey(_lineNumber))
            {
                var line = _buffer[_lineNumber];
                if (line.Overwrite)
                {
                    _buffer[_lineNumber] = new MockLine(text);
                }
                else
                    _buffer[_lineNumber] = new MockLine(line.Text + text);
            }
            else
            {
                _buffer.Add(_lineNumber, new MockLine(false, text));
            }
            _lineNumber++;
        }

        public void Write(string format, params object[] args)
        {
            var line = string.Format(format, args);
            _write(line);
            if (_echo) Console.Write(line);
        }

        public int CursorTop
        {
            get { return _lineNumber; }
            set
            {
                // mark all lines so that next write and writelines will overwrite the first next write only against each line.
                for (int i = value; i < _buffer.Count(); i++) _buffer[i].Overwrite = true;
                _lineNumber = value;
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
    }
}

using System;

namespace Konsole
{
    public class Writer : IConsole
    {
        private bool _isWindows;

        public Writer()
        {
            _isWindows = PlatformCheck.IsWindows;
        }

        public Writer(bool isWindows)
        {
            _isWindows = isWindows;
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WriteLine(ConsoleColor color, string format, params object[] args)
        {
            var foreground = ForegroundColor;
            try
            {
                ForegroundColor = color;
                WriteLine(format, args);
            }
            finally
            {
                ForegroundColor = foreground;
            }
        }

        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void Write(ConsoleColor color, string format, params object[] args)
        {
            var foreground = ForegroundColor;
            try
            {
                ForegroundColor = color;
                Write(format, args);
            }
            finally
            {
                ForegroundColor = foreground;
            }
        }



        public ConsoleState State
        {
            get {  return new ConsoleState(Console.ForegroundColor,Console.BackgroundColor, Console.CursorTop, Console.CursorLeft, Console.CursorVisible );}
            set
            {
                Console.ForegroundColor = value.ForegroundColor;
                Console.BackgroundColor = value.BackgroundColor;
                Console.CursorTop = value.Top;
                Console.CursorLeft = value.Left;
                if(_isWindows)
                {
                    Console.CursorVisible = value.CursorVisible;
                }
            }
        }

        public int AbsoluteX => 0;
        public int AbsoluteY => 0;

        public int WindowWidth => Console.WindowWidth;
        public int WindowHeight => Console.WindowHeight;

        public int CursorLeft
        {
            get { return Console.CursorLeft; }
            set { Console.CursorLeft = value;  }
        }


        public Colors Colors
        {
            get
            {
                return new Colors(ForegroundColor, BackgroundColor);
            }
            set
            {
                ForegroundColor = value.Foreground;
                BackgroundColor = value.Background;
            }
        }
        public int CursorTop
        {
            get { return Console.CursorTop; }
            set { Console.CursorTop = value;  }
        }

        public XY XY
        {
            get { return new XY(Console.CursorLeft, Console.CursorTop); }

            set
            {
                Console.CursorLeft = value.X;
                Console.CursorTop = value.Y;
            }
        }
        
        public int Y
        {
            get { return Console.CursorTop; } 
            set { Console.CursorTop = value; }
        }

        public int X
        {
            get { return Console.CursorLeft; } 
            set { Console.CursorLeft= value; }
        }

        /// <summary>
        /// Run command and preserve the state, i.e. restore the console state after running command.
        /// </summary>
        public void DoCommand(IConsole console, Action action)
        {
            if (console == null)
            {
                action();
                return;
            }
            var state = console.State;
            try
            {
                action();
            }
            finally
            {
                console.State = state;
            }
        }

        public ConsoleColor ForegroundColor
        {
            get { return Console.ForegroundColor; } 
            set { Console.ForegroundColor = value; }
        }

        public ConsoleColor BackgroundColor {
            get { return Console.BackgroundColor; }
            set { Console.BackgroundColor = value; }
        }

        public bool CursorVisible
        {
            get
            {
                return _isWindows ? Console.CursorVisible : true;
            }
            set
            {
                if (!_isWindows) return;
                Console.CursorVisible = value;
            }
        }

        public void PrintAt(int x, int y, string format, params object[] args)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(format, args);            
        }

        public void PrintAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);            
        }
        public void PrintAt(int x, int y, char c)
        {
            if (x >= Console.WindowWidth || x>=Console.BufferWidth) return;
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Clear(ConsoleColor? background)
        {
            Console.Clear();
        }

        public void PrintAtColor(ConsoleColor foreground, int x, int y, string text, ConsoleColor? background = null)
        {
            DoCommand(this, () =>
            {
                State = new ConsoleState(foreground, background ?? BackgroundColor, y, x, CursorVisible);
                Write(text);
            });
        }
    }
}
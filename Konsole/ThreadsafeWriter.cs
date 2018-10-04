using System;

namespace Konsole
{
    public class ThreadsafeWriter : IConsole
    {
        private ConsoleState _state;
        private static object _lock = new object();
        private bool _isWindows;

        public ThreadsafeWriter()
        {
            _isWindows = PlatformCheck.IsWindows;
            _state = State;
        }

        public ThreadsafeWriter(bool isWindows)
        {
            _isWindows = isWindows;
        }

        public void WriteLine(string format, params object[] args)
        {
            lock(_lock)
            {
                Console.WriteLine(format, args);
            }
        }

        public void WriteLine(ConsoleColor color, string format, params object[] args)
        {
            lock(_lock)
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
        }

        public void Write(string format, params object[] args)
        {
            lock(_lock)
            {
                Console.Write(format, args);
            }
        }

        public void Write(ConsoleColor color, string format, params object[] args)
        {
            lock(_lock)
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
        }



        public ConsoleState State
        {
            get
            {
                lock(_lock)
                {
                    if(PlatformCheck.IsWindows)
                    {
                        return new ConsoleState(Console.ForegroundColor, Console.BackgroundColor, Console.CursorTop, Console.CursorLeft, Console.CursorVisible);
                    }
                    else
                    {
                        return new ConsoleState(Console.ForegroundColor, Console.BackgroundColor, Console.CursorTop, Console.CursorLeft, true);
                    }
                }
            }
            set
            {
                lock(_lock)
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
        }

        public int AbsoluteX => 0;
        public int AbsoluteY => 0;

        public int Width => Console.BufferWidth;
        public int Height => Console.WindowHeight;

        public int CursorLeft
        {
            get { 
                    lock(_lock)
                    {
                        return Console.CursorLeft; 
                    }
                }
        set {
                lock(_lock)
                {
                    Console.CursorLeft = value;  
                } 
            }
        }


        public Colors Colors
        {
            get
            {
            lock(_lock)
                {
                    return new Colors(ForegroundColor, BackgroundColor);
                }
            }
            set
            {
            lock(_lock)
                {
                    ForegroundColor = value.Foreground;
                    BackgroundColor = value.Background;
                }
            }
        }
        public int CursorTop
        {
            get {
                lock(_lock)
                    {
                    return Console.CursorTop; 
                    }
                 }

            set {
                lock(_lock)
                    {
                        Console.CursorTop = value;  
                    } 
                }
        }

        public XY XY
        {
            get {
                lock(_lock)
                    {
                    return new XY(Console.CursorLeft, Console.CursorTop); 
                    } 
                }
            set
            {
                lock(_lock)
                {
                    Console.CursorLeft = value.X;
                    Console.CursorTop = value.Y;
                }
            }
        }
        
        public int Y
        {
            get {
                lock(_lock)
                    {
                        return Console.CursorTop; 
                    } 
                } 
            set {
                lock(_lock)
                    {
                        Console.CursorTop = value; 
                    } 
                }
        }

        public int X
        {
            get {
                lock(_lock)
                    {
                        return Console.CursorLeft; 
                    } 
                } 
            set {
                lock(_lock)
                    {
                        Console.CursorLeft= value; 
                    } 
                }
        }

        private void UnwindStateAfter(Action action)
        {
            lock(_lock)
            {
                var state = State;
                try
                {
                    action();
                }
                finally
                {
                    State = state;
                }
            }
        }

        public ConsoleColor ForegroundColor
        {
            get {
                lock(_lock)
                    {
                    return Console.ForegroundColor; 
                    } 
                } 
            set {
                lock(_lock)
                    {
                        Console.ForegroundColor = value; 
                    } 
                }
        }

        public ConsoleColor BackgroundColor {
            get { 
                lock(_lock)
                    {
                    return Console.BackgroundColor;  
                    }
                }
            set {
                lock(_lock)
                    {
                        Console.BackgroundColor = value; 
                    } 
                }
        }

        public bool CursorVisible
        {
            get
            {
                lock(_lock)
                {
                    return _isWindows ? Console.CursorVisible : true;
                }
            }
            set
            {
                lock(_lock)
                {
                    if (!_isWindows) return;
                    Console.CursorVisible = value;
                }
            }
        }

        public void PrintAt(int x, int y, string format, params object[] args)
        {
            var text = string.Format(format, args);
            PrintAt(x, y, text);     
        }

        public void PrintAt(int x, int y, string text)
        {
            UnwindStateAfter(()=> {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(text);            
            });
        }
        public void PrintAt(int x, int y, char c)
        {
            if (x >= Console.WindowWidth || x>=Console.BufferWidth) return;
            UnwindStateAfter(()=> {
                Console.SetCursorPosition(x, y);
                Console.Write(c);
            });
        }

        public void Clear()
        {
            lock(_lock)
            {
                Console.Clear();
            }
        }

        public void Clear(ConsoleColor? background)
        {
            lock(_lock)
            {
                Console.Clear();
            }
        }

        public void PrintAtColor(ConsoleColor foreground, int x, int y, string text, ConsoleColor? background = null)
        {
            UnwindStateAfter(() =>
            {
                State = new ConsoleState(foreground, background ?? BackgroundColor, y, x, CursorVisible);
                Write(text);
            });
        }
    }
}
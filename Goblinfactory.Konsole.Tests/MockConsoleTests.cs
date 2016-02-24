using System;
using ApprovalTests.Maintenance;
using Goblinfactory.Konsole;
using Goblinfactory.Konsole.Mocks;
using NUnit.Framework;


namespace Goblinfactory.ProgressBar.Tests.Internal
{
    public class MockConsoleTests
    {
        [Test]
        public void cursor_X_andY_tests()
        {
            
            var console = new MockConsole(20);
            //var console = new MockConsole(20,20);
            console.Write("X");
            //console.X

        }

        [Test]
        public void write_and_write_line_simple_usages()
        {
            var console = new MockConsole(80);
            console.WriteLine("line1");
            console.Write("This ");
            console.Write("is ");
            console.WriteLine("a test line.");
            console.WriteLine("line 3");

            var expected = new[]
            {
                "line1",
                "This is a test line.",
                "line 3"
            };

            Assert.That(console.Lines, Is.EqualTo(expected));
        }

        [Test]
        public void cursor_top_should_show_current_line()
        {
            var console = new MockConsole(80);
            Assert.AreEqual(0, console.Y);
            console.WriteLine("line1");
            Assert.AreEqual(1, console.Y);
            console.Write("This ");
            Assert.AreEqual(1, console.Y);
            console.Write("is ");
            Assert.AreEqual(1, console.Y);
            console.WriteLine("a test line.");
            Assert.AreEqual(2, console.Y);
            console.WriteLine("line 3");
            Assert.AreEqual(3, console.Y);
        }

        [Test]
        public void setting_cursor_top_should_allow_us_to_overwrite_lines()
        {
            // setting echo to false, since nunit console does not support CursorTop,
            // and will not echo what you will see in a console application.
            var console = new MockConsole(80,false);
            console.WriteLine("line 0");
            console.WriteLine("line 1");
            console.WriteLine("line 2");
            console.Y = 1;
            console.WriteLine("new line 1");
            var expected = new[]
            {
                "line 0",
                "new line 1",
                "line 2"
            };
            Console.WriteLine(console.Buffer);
            Assert.That(console.Lines, Is.EqualTo(expected));
            
        }
    }
}

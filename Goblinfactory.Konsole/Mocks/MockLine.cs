namespace Goblinfactory.Konsole.Mocks
{
    public class MockLine
    {
        public MockLine() : this(false, ""){}
        public MockLine(string text) : this(false, text) {}
        public MockLine(bool overwrite, string text)
        {
            Overwrite = overwrite;
            Text = text;
        }
        public bool Overwrite { get; set; }
        public string Text { get; set; }
    }
}
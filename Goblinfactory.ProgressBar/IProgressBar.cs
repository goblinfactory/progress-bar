namespace Konsole
{
    public interface IProgressBar
    {
        string Line1 { get; }
        string Line2 { get; }
        int Y { get; }
        int Max { get; set; }
        void Refresh(int current, string item);
        void Refresh(int current, string format, params object[] args);
        void Next(string item);
    }


}
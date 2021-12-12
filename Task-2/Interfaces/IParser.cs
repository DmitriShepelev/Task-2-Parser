using System.IO;

namespace Task_2.Interfaces
{
    public interface IParser
    {
        ITextUnit Parse(StreamReader streamReader);
        bool TryParse(StreamReader streamReader);
    }
}
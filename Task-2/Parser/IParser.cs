using System.IO;
using Task_2.TextModel;

namespace Task_2.Parser
{
    public interface IParser
    {
        ITextUnit Parse(StreamReader streamReader);
        bool TryParse(StreamReader streamReader);
    }
}
using System;
using System.Diagnostics.CodeAnalysis;

namespace dol_con.Utilities
{
    public interface IConsoleWrapper
    {
        void WriteLine(string value);
        void Write(string value);
        string ReadLine();
        void Clear();
    }

    [ExcludeFromCodeCoverage]
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
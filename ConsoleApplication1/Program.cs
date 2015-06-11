using System;

using LogParser;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("argument: logfile name");
                return;
            }

            var parser = new BuildogParser(args[0]);
            parser.Parse();
        }
    }
}

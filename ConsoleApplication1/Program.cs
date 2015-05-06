using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    public class Project
    {
        string projectLine;     // "1>------ Build started: Project: srv.interface, Configuration: Release Any CPU ------"
        string buildNumberStr;  // ie "1>", "2>" ...
        List<string> lines;

        static Regex projectBuildRE = new Regex(@"^([0-9]+>)------ Build started:");

        public Project()
        {
        }

        static public bool IsNewProjectBuild(string line, out string buildNumberStr)
        {
            buildNumberStr = "";

            var match = projectBuildRE.Match(line);
            if (match.Success)
                buildNumberStr = match.Groups[1].ToString();

            return match.Success;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("argument: logfile name");
                return;
            }

            string line;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(args[0]);

                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);

                    string buildNbrStr;
                    if (Project.IsNewProjectBuild(line, out buildNbrStr))
                    {
                        Console.WriteLine(line);
                        Console.WriteLine("found start of project build: " + buildNbrStr);
                        Console.WriteLine();
                    }
                }

                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}

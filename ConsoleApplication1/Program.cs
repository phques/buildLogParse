using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using LogParser;

namespace ConsoleApplication1
{
    class BuildogParser
    {
        string filename;

        public BuildogParser(string filename)
        {
            this.filename = filename;
        }

        public void Parse()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(filename);

                Build build = new Build();
                int buildNo = 0;
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    if (build.IsEndOfBuild(line))
                    {
                        Console.WriteLine("end of build");
                    }
                    
                    Project project = Project.IsNewProjectBuild(line);
                    if (project != null)
                    {
                        string msg = String.Format("found start of project \"{0}\", build tag \"{1}\", Config {2}",
                                                    project.Name, project.BuildNumberTag, project.Config);
                        Console.WriteLine(line);
                        Console.WriteLine(msg);
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

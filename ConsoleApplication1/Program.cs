using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

            string line;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(args[0]);

                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);

                    Project project = Project.IsNewProjectBuild(line);
                    if (project != null)
                    {
                        string msg = String.Format("found start of project \"{0}\", build tag \"{1}\"",
                                                    project.Name, project.BuildNumberTag);
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
}

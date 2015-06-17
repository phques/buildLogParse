using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace LogParser
{
    class BuildogParser
    {
        string filename;

        public BuildogParser(string filename)
        {
            this.filename = filename;
        }

        /*
         * if isInBuild
         *    if endOfBuild
         *       isInBuild = false
         *       create new build
         *    elif new start project build
         *       add new project to build
         *    elif tagged build line ("99..>")
         *       add to corresponding project
         *  else 
         *     // !in build
         *     add line to build
         */
        public void Parse()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(filename);

                var projectsByTag = new Dictionary<string,Project>();
                Build build = new Build();
                bool newBuild = true;
                //int buildNo = 0;
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);

                    if (build.IsEndOfBuild(line))
                    {
                        Console.WriteLine("end of build");

                        projectsByTag.Clear();
                    }
                    else
                    {
                        Project project = Project.IsNewProjectBuild(line);
                        if (project != null)
                        {
                            string msg = String.Format("found start of project \"{0}\", build tag \"{1}\", Config {2}",
                                                        project.Name, project.BuildNumberTag, project.Config);
                            Console.WriteLine(line);
                            Console.WriteLine(msg);
                            Console.WriteLine();

                            build.AddProject(project);

                            if (newBuild)
                            {
                                newBuild = false;

                                // what else ??
                            }
                        }
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

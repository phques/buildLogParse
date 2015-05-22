using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogParser
{

    public class Project
    {
        string startLine;       // "1>------ Build started: Project: srv.interface, Configuration: Release Any CPU ------"
        string name;            // "srv.interface"
        string buildNumberTag;  // "1>", "2>" ...
        string config;          // "Release"
        List<string> lines;

        // "1>------ Build started: Project: srv.interface, Configuration: Release Any CPU ------"
        static Regex projectBuildRE = new Regex(@"^([0-9]+>)------ Build started: Project: (.+), Configuration: ([^ ]+)");

        public Project(string startLine, string projectName, string buildNumberStr, string config)
        {
            lines = new List<string>();
            this.startLine = startLine;
            this.name = projectName;
            this.buildNumberTag = buildNumberStr;
            this.config = config;
            this.lines.Add(startLine);
        }

        public string Name { get { return name; } }
        public string StartLine { get { return startLine; } }
        public string BuildNumberTag { get { return buildNumberTag; } }
        public string Config { get { return config; } }

        // returns true if line starts with this project's buildNumberTag ("12>")
        public bool IsProjectLine(string line)
        {
            return line.StartsWith(buildNumberTag);
        }

        static public Project IsNewProjectBuild(string line)
        {
            Project project = null;

            var match = projectBuildRE.Match(line);
            if (match.Success)
            {
                project = new Project(line, match.Groups[2].ToString(), match.Groups[1].ToString(), match.Groups[3].ToString());
            }

            return project;
        }
    }

}
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Project
{
    string startLine;     // "1>------ Build started: Project: srv.interface, Configuration: Release Any CPU ------"
    string name;     // ie "srv.interface"
    string buildNumberTag;  // ie "1>", "2>" ...
    List<string> lines;

    static Regex projectBuildRE = new Regex(@"^([0-9]+>)------ Build started: Project: (.+), Configuration:");

    public Project(string startLine, string projectName, string buildNumberStr)
    {
        lines = new List<string>();
        this.startLine = startLine;
        this.name = projectName;
        this.buildNumberTag = buildNumberStr;
        this.lines.Add(startLine);
    }

    public string Name { get { return name; } }
    public string StartLine { get { return startLine; } }
    public string BuildNumberTag { get { return buildNumberTag; } }

    static public Project IsNewProjectBuild(string line)
    {
        Project project = null;

        var match = projectBuildRE.Match(line);
        if (match.Success)
        {
            project = new Project(line, match.Groups[2].ToString(), match.Groups[1].ToString());
        }

        return project;
    }
}

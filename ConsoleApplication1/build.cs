using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogParser
{
    public class Build
    {
        List<string> lines;
        string nbrSucceeded;
        string nbrFailed;
        string nbrUpToDate;
        string nbrSkipped;

        // "========== Build: 52 succeeded, 0 failed, 12 up-to-date, 0 skipped =========="
        static Regex endBuilddRE = new Regex(@"========== Build: ([0-9]+) succeeded, ([0-9]+) failed, ([0-9]+) up-to-date, ([0-9]+) skipped ==========");

        public Build()
        {
        }

        public bool IsEndOfBuild(string line)
        {
            var match = endBuilddRE.Match(line);
            if (match.Success)
            {
                nbrSucceeded = match.Groups[1].ToString();
                nbrFailed = match.Groups[2].ToString();
                nbrUpToDate = match.Groups[3].ToString();
                nbrSkipped = match.Groups[4].ToString();

                //## debug
                Console.WriteLine(line);
                Console.WriteLine("succeeded : {0}, failed: {1}, up-to-date: {2}, skipped: {3}",
                    nbrSucceeded, nbrFailed, nbrUpToDate, nbrSkipped);

                return true;
            }

            return false;
        }
    }
}
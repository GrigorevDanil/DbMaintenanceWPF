using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service
{
    public static class CommandParameterExtractor
    {
        public static string[] FindParametrsInCommand(string com)
        {
            MatchCollection matches = Regex.Matches(com, @"@\w+");
            string[] parametrs = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++) parametrs[i] = matches[i].Value;
            return parametrs;
        }
    }
}

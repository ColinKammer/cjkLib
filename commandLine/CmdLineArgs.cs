using System;
using System.Collections.Generic;

namespace csCjkLib
{
    using SwitchArguments = List<string>;
    public class CmdLineArgs
    {

        readonly IDictionary<string,SwitchArguments> Switches = new Dictionary<string, SwitchArguments>();

        public CmdLineArgs(in string cmdLine) : this(cmdLine.Split(' '))
        {
        }

        public CmdLineArgs(string[] argv)
        {
            string currentSwitch = "";
            Switches.Add(currentSwitch, new SwitchArguments());

            foreach(var arg in argv)
            {
                if (arg.StartsWith("-") || arg.StartsWith("/"))
                {
                    currentSwitch = arg.Substring(1); //remove leading dash
                    Switches.Add(currentSwitch, new SwitchArguments());
                }
                else
                {
                    Switches[currentSwitch].Add(arg);
                }
            }
        }

        public bool HasSwitch(in string switchName)
        {
            return Switches.ContainsKey(switchName);
        }

        public SwitchArguments GetSwitchArguments(in string switchName, SwitchArguments defaultValue = null)
        {
            if (!HasSwitch(switchName)) return defaultValue;
            return Switches[switchName];
        }

        public string GetFirstSwitchArgument(in string switchName, in string defaultValue = null)
        {
            if (!HasSwitch(switchName)) return defaultValue;
            if (Switches[switchName].Count < 1) return defaultValue;
            return Switches[switchName][0];
        }
    }
}

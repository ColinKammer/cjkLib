#pragma once

#include <string>
#include <map>
#include <vector>

#include "string_util.h"

namespace cjk
{
	using SwitchArguments = std::vector<std::string>;
	class CmdLineArgs
	{
	private:
		std::map<std::string, SwitchArguments> m_switches;

		void parseArgs(const std::vector<std::string>& rawArgs)
		{
			std::string currentSwitch = "";

			m_switches.insert({ currentSwitch, SwitchArguments() });

			for (auto arg : rawArgs)
			{
				if (arg.length() == 0) continue; //ignore empty elements

				if ((arg.at(0) == '-') || (arg.at(0) == ('/')))
				{
					currentSwitch = arg.substr(1); //remove leading dash
					m_switches.insert({ currentSwitch, SwitchArguments() });
				}
				else
				{
					m_switches[currentSwitch].push_back(arg);
				}
			}
		};

	public:
		CmdLineArgs(const std::vector<std::string>& rawArgs)
		{
			parseArgs(rawArgs);
		};

		CmdLineArgs(const std::string args)
		{
			parseArgs(split(args, ' '));
		};

		CmdLineArgs(int argc, const char* argv[])
		{
			std::vector<std::string> rawArgs;
			rawArgs.reserve(argc);

			for (int i = 0; i < argc; i++)
			{
				rawArgs.push_back(argv[i]);
			}

			parseArgs(rawArgs);
		};

		bool HasSwitch(const std::string& switchName)
		{
			return m_switches.find(switchName) != m_switches.end();
		};

		SwitchArguments* GetSwitchArguments(const std::string& switchName, SwitchArguments* defaultValue = nullptr)
		{
			if (!HasSwitch(switchName)) return defaultValue;
			return &m_switches[switchName];
		};

		std::string GetFirstSwitchArgument(const std::string& switchName, const std::string& defaultValue = "")
		{
			if (!HasSwitch(switchName)) return defaultValue;
			if (m_switches[switchName].size() < 1) return defaultValue;
			return m_switches[switchName][0];
		};
	};
}

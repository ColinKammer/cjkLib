#pragma once

#include <string>
#include <vector>
#include <algorithm>

#ifdef CMD_LINE_ARGS_IMPL_SPLITTING
#include "string_util.h"
#endif 


namespace cjk
{
    class CmdLineArgsSS
    {
    private:
        class KeyValuePair
        {
        public:
            std::string m_key;
            std::string m_value;

            KeyValuePair(const std::string& key)
                : m_key(key)
                , m_value()
            { }
        };

        std::vector<KeyValuePair> m_namedArgs;
        std::vector<std::string> m_unnamedArgs;

        void parseArgs(const std::vector<std::string>& rawArgs)
        {
            KeyValuePair* lastKVPair = nullptr;

            for (auto arg : rawArgs)
            {
                if (arg.length() == 0) continue; //ignore empty elements

                if ((arg.at(0) == '-') || (arg.at(0) == ('/')))
                {
                    auto currentSwitch = arg.substr(1); //remove leading dash
                    m_namedArgs.emplace_back(currentSwitch);
                    lastKVPair = &m_namedArgs.back();
                }
                else
                {
                    if (lastKVPair != nullptr)
                    {
                        lastKVPair->m_value = arg;
                        lastKVPair = nullptr;
                    }
                    else
                    {
                        m_unnamedArgs.emplace_back(arg);
                    }
                }
            }
        };

    public:
        CmdLineArgsSS(const std::vector<std::string>& rawArgs)
        {
            parseArgs(rawArgs);
        };

#ifdef CMD_LINE_ARGS_IMPL_SPLITTING
        CmdLineArgsSS(const std::string args)
        {
            parseArgs(split(args, ' '));
        };
#endif

        CmdLineArgsSS(int argc, const char* argv[])
        {
            std::vector<std::string> rawArgs;
            rawArgs.reserve(argc);

            for (int i = 0; i < argc; i++)
            {
                rawArgs.push_back(argv[i]);
            }

            parseArgs(rawArgs);
        };

        bool HasSwitch(const std::string& argName)
        {
            auto findResult = std::find_if(m_namedArgs.begin(), m_namedArgs.end(), [=](const KeyValuePair& kvp) { return kvp.m_key == argName; });
            return findResult != m_namedArgs.end();
        };

        std::string GetNamedArg(const std::string& argName, const std::string& defaultValue = "")
        {
            auto findResult = std::find_if(m_namedArgs.begin(), m_namedArgs.end(), [=](const KeyValuePair& kvp) { return kvp.m_key == argName; });
            return (findResult != m_namedArgs.end()) ? findResult->m_value : defaultValue;
        }

        std::vector<std::string> GetUnnamedArgs()
        {
            return m_unnamedArgs;
        }
    };
}

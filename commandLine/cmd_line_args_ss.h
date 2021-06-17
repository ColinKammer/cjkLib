#pragma once

#include <string>
#include <vector>
#include <algorithm>

#ifdef CMD_LINE_ARGS_IMPL_SPLITTING
#include "string_util.h"
#endif 


namespace cjk
{
    class CmdLineArgsSS {
    private:
        using StringList = std::vector<std::string>;

        struct FlagConfig
        {
            std::string m_name;
            bool m_value;

            FlagConfig(const char* name)
                : m_name(name)
                , m_value(false)
            { }
        };

        struct ParamConfig
        {
            std::string m_name;
            std::string m_value;

            ParamConfig(const char* name)
                : m_name(name)
                , m_value()
            { }
        };

        struct ListConfig
        {
            std::string m_name;
            StringList m_value;

            ListConfig(const char* name)
                : m_name(name)
                , m_value()
            { }
        };

    private:
        std::vector<FlagConfig> m_flags;
        std::vector<ParamConfig> m_params;
        std::vector<ListConfig> m_namedLists;
        StringList m_unnamedList;
        size_t m_unnamedList_minimum = 0; //accept both: with and without call by default
        size_t m_unnamedList_maximum = 1; //accept both: with and without call by default

    private:
        enum class ParseState
        {
            NoContext,
            ParamContext,
            ListContext
        };
        ParseState m_parseState = ParseState::NoContext;
        decltype(m_params)::iterator m_selectedParam;
        decltype(m_namedLists)::iterator m_selectedlist;

        [[nodiscard]] const char* ParseSection(const char* spacelessSection)
        {
            if (spacelessSection == nullptr) return nullptr; //ignore empty sections
            if (spacelessSection[0] == NULL) return nullptr; //ignore empty sections

            bool startsWithDash = (spacelessSection[0] == '-') || (spacelessSection[0] == '/');
            if (startsWithDash)
            {
                const char* name = &spacelessSection[1];
                auto flagItter = std::find_if(m_flags.begin(), m_flags.end(), [&](const auto& elem) {return elem.m_name == name; });
                auto paramItter = std::find_if(m_params.begin(), m_params.end(), [&](const auto& elem) {return elem.m_name == name; });
                auto listItter = std::find_if(m_namedLists.begin(), m_namedLists.end(), [&](const auto& elem) {return elem.m_name == name; });

                if (flagItter != m_flags.end())
                {
                    flagItter->m_value = true;
                    m_parseState = ParseState::NoContext;
                }
                else if (paramItter != m_params.end())
                {
                    m_selectedParam = paramItter;
                    m_parseState = ParseState::ParamContext;
                }
                else if (listItter != m_namedLists.end())
                {
                    m_selectedlist = listItter;
                    m_parseState = ParseState::ListContext;
                }
                else
                {
                    return "Unknown Flag";
                }
            }
            else
            {
                switch (m_parseState)
                {
                case ParseState::NoContext:
                    m_unnamedList.emplace_back(spacelessSection);
                    break;
                case ParseState::ParamContext:
                    m_selectedParam->m_value = spacelessSection;
                    m_parseState = ParseState::NoContext;
                    break;
                case ParseState::ListContext:
                    m_selectedlist->m_value.emplace_back(spacelessSection);
                    break;
                }
            }

            return nullptr;
        }

        [[nodiscard]] const char* ValidateUnnamedList()
        {
            if (m_unnamedList.size() > m_unnamedList_maximum) return "UnnamedList is too long";
            if (m_unnamedList.size() < m_unnamedList_minimum) return "UnnamedList is too short";
            return nullptr;
        }

    public:
        CmdLineArgsSS(size_t reservedFlags, size_t reservedParams, size_t reservedLists)
        {
            //failing to reserve enough during construcion will result in invalidated refernces making the result useless
            m_flags.reserve(reservedFlags);
            m_params.reserve(reservedParams);
            m_namedLists.reserve(reservedLists);
        }

        bool& ConfigureFlag(const char* name)
        {
            m_flags.emplace_back(name);
            return m_flags.back().m_value;
        }

        std::string& ConfigureParam(const char* name)
        {
            m_params.emplace_back(name);
            return m_params.back().m_value;
        }

        StringList& ConfigureNamedList(const char* name)
        {
            m_namedLists.emplace_back(name);
            return m_namedLists.back().m_value;
        }

        StringList& ConfigureUnnamedList(size_t minimumLength, size_t maximumLength)
        {
            m_unnamedList_minimum = minimumLength;
            m_unnamedList_maximum = maximumLength;
            return m_unnamedList;
        }

        [[nodiscard]] const char* Parse(const std::vector<std::string>& rawArgs)
        {
            for (auto& s : rawArgs)
            {
                auto res = ParseSection(s.c_str());
                if (res != nullptr)
                    return res;
            }
            return ValidateUnnamedList();
        };
#ifdef CMD_LINE_ARGS_IMPL_SPLITTING
        [[nodiscard]] const char* Parse(const std::string args)
        {
            return Parse(split(args, ' '));
        };
#endif

        [[nodiscard]] const char* Parse(int argc, const char* argv[])
        {
            for (int i = 0; i < argc; i++)
            {
                auto res = ParseSection(argv[i]);
                if (res != nullptr)
                    return res;
            }
            return ValidateUnnamedList();
        };

        void Reset()
        {
            for (auto& e : m_flags) e.m_value = false;
            for (auto& e : m_params) e.m_value.clear();
            for (auto& e : m_namedLists) e.m_value.clear();
            m_unnamedList.clear();
        }
    };
}

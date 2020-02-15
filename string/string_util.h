#pragma once

#include <string>
#include <vector>

namespace cjk {
	static std::vector<std::string> split(const std::string& s, const char delim) {
		std::vector<std::string> result;

		std::string currentString;

		for (size_t i = 0; i < s.length(); i++)
		{
			if (s[i] == delim)
			{
				result.push_back(currentString);
				currentString = std::string();
			}
			else
			{
				currentString += s[i];
			}
		}
		result.push_back(currentString);

		return result;
	}
}
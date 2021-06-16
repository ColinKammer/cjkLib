#pragma once

#include <string>
#include <vector>
#include <stack>

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

	static std::string itoa(int32_t i) {
		std::stack<char> digits;
		std::string result;

		if (i == 0)
		{
			return "0";
		}

		if (i < 0)
		{
			result.push_back('-');
			i = -i;
		}

		while (i > 0)
		{
			auto currentDigit = i % 10;
			digits.push(currentDigit);
			i = i / 10;
		}

		while (digits.empty() == false)
		{
			result.push_back(digits.top() + '0');
			digits.pop();
		}

		return result;
	}
}
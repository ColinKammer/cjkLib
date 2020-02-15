#pragma once

#include <iostream>
#include <string>

static bool doCheck(const std::string& message, bool condition, const std::string& errorHint)
{
	std::cout << "\n  " << message << "...  ";
	if (condition)
	{
		std::cout << "OK";
	}
	else
	{
		std::cout << "ERROR: " << errorHint;
	}
	return condition;
}

template<typename T>
static bool incrementIfFalse(T& variable, bool condition)
{
	if (condition == false)
	{
		variable++;
	}
	return condition;
}
#pragma once
#include "numeric_util.h"

#include "test_helper.h"
#include <iostream>

using namespace cjk;

int tst_numericUtil()
{
	int errors = 0;
	
	incrementIfFalse(errors, doCheck("value", (roundUpToAllignment(5, 8) == 8), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (roundUpToAllignment(8, 8) == 8), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (roundUpToAllignment(0, 8) == 0), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (roundUpToAllignment(1, 8) == 8), "Unexpected Result"));

	incrementIfFalse(errors, doCheck("value", (roundUpToAllignment(5, 13) == 13), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (roundUpToAllignment(27, 13) == 39), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (roundUpToAllignment(0, 13) == 0), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (roundUpToAllignment(1, 13) == 13), "Unexpected Result"));

	return errors;
}

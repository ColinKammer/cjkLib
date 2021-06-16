#pragma once
#include "vector_util.h"

#include "test_helper.h"
#include <iostream>
#include <vector>

using namespace cjk;

int tst_vectorUtil()
{
	int errors = 0;

	std::vector<int> v1 = { 1, 2, 3, 4 };
	std::vector<int> v2 = { 5, 6, 7, 8 };
	push_back_vectorOnVector(v1, v2);
	

	incrementIfFalse(errors, doCheck("length", (v1.size() == 8), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (v1.at(0) == 1), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (v1.at(1) == 2), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (v1.at(2) == 3), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (v1.at(3) == 4), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (v1.at(4) == 5), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (v1.at(5) == 6), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (v1.at(6) == 7), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("value", (v1.at(7) == 8), "Unexpected Result"));

	return errors;
}

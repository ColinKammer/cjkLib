#pragma once
#include "string_util.h"

#include "test_helper.h"
#include <iostream>

using namespace cjk;

int tst_stringUtil()
{
	int errors = 0;

	incrementIfFalse(errors, doCheck("normal_size", (split("tst1 tst2", ' ').size() == 2), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("normal_item0", (split("tst1 tst2", ' ').at(0) == "tst1"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("normal_item1", (split("tst1 tst2", ' ').at(1) == "tst2"), "Unexpected Result"));


	incrementIfFalse(errors, doCheck("positive", (cjk::itoa(123) == "123"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("zero", (cjk::itoa(0) == "0"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("negative", (cjk::itoa(-5) == "-5"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("big", (cjk::itoa(1234567890) == "1234567890"), "Unexpected Result"));


	return errors;
}
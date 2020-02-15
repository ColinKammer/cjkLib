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

	return errors;
}
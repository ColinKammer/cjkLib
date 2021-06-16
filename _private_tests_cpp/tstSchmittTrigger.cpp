#pragma once

#include "test_helper.h"
#include "SchmittTrigger.h"
#include <iostream>

int tst_schmittTrigger()
{
	int errors = 0;

	auto schmittTrigger = SchmittTrigger<int32_t>(10, 20, false);

	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(5) == false, "unexpected Value"));
	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(10) == false, "unexpected Value"));
	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(20) == false, "unexpected Value"));
	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(21) == true, "unexpected Value"));
	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(15) == true, "unexpected Value"));
	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(10) == true, "unexpected Value"));
	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(9) == false, "unexpected Value"));
	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(15) == false, "unexpected Value"));
	incrementIfFalse(errors, doCheck("Checking Result", schmittTrigger.ProcessSample(30) == true, "unexpected Value"));

	return errors;
}
#pragma once

#include "cppCjkLib/public/include/test_helper.h"
#include <iostream>

#include "../../MRR Execution/FIRFilter.h"

#define MAXERROR 0.001
#define ABOUT_EQUALS(A,B) ( ((A-B) < MAXERROR) && ((B-A) < MAXERROR) )

int tst_ltdSystem()
{
	int errors = 0;
	double outputSample;

	constexpr double tstFactors[] = { 1.0,1.0,1.0,2.2 };
	auto tstSystem = FIRFilter<double, 4>(tstFactors);

	outputSample = tstSystem.ProcessSample(1.0);
	incrementIfFalse(errors, doCheck("Checking Result", ABOUT_EQUALS(outputSample, 1.0), "unexpected Value"));
	outputSample = tstSystem.ProcessSample(2.1);
	incrementIfFalse(errors, doCheck("Checking Result", ABOUT_EQUALS(outputSample, 3.1), "unexpected Value"));
	outputSample = tstSystem.ProcessSample(3.2);
	incrementIfFalse(errors, doCheck("Checking Result", ABOUT_EQUALS(outputSample, 6.3), "unexpected Value"));
	outputSample = tstSystem.ProcessSample(4.4);
	incrementIfFalse(errors, doCheck("Checking Result", ABOUT_EQUALS(outputSample, 11.9), "unexpected Value"));
	outputSample = tstSystem.ProcessSample(6.7);
	incrementIfFalse(errors, doCheck("Checking Result", ABOUT_EQUALS(outputSample, 18.92), "unexpected Value"));

	return errors;
}
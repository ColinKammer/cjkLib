#pragma once
#include "callDivider.h"

#include "test_helper.h"
#include <iostream>


int tst_callDivider()
{
	int errors = 0;

	int t = 0;

	for (size_t i = 0; i < 10; i++)
	{
		CALL_EVERY(3, uniqueIdentidier, t++;);
	}


	incrementIfFalse(errors, doCheck("correct call count", t == 3, "wrong call count"));


	return errors;
}
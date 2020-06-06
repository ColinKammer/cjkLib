#pragma once
#include "sequencer.h"

#include "test_helper.h"
#include <iostream>

using namespace cjk;

int tst_sequencer()
{
	int errors = 0;

	Sequencer seq = Sequencer([&](char c) -> void {
		static int callCount = 0;
		switch (callCount)
		{
		case 0:
			incrementIfFalse(errors, doCheck("Step 0", c == '1', "Unexpected Result"));
			break;
		case 1:
			incrementIfFalse(errors, doCheck("Step 1", c == '2', "Unexpected Result"));
			break;
		case 2:
			incrementIfFalse(errors, doCheck("Step 2", c == '3', "Unexpected Result"));
			break;
		case 3:
			incrementIfFalse(errors, doCheck("Step 3", c == '1', "Unexpected Result"));
			break;
		case 4:
			incrementIfFalse(errors, doCheck("Step 4", c == '2', "Unexpected Result"));
			break;
		default:
			errors++;
			std::cout << "ERROR: " << "More CallBacks than expected";
			break;
		}
		callCount++;
		});

	seq.m_sequence = "123";

	incrementIfFalse(errors, doCheck("Step 0 retVal", seq.ExecuteStep() == '1', "Unexpected Result"));
	incrementIfFalse(errors, doCheck("Step 1 retVal", seq.ExecuteStep() == '2', "Unexpected Result"));
	incrementIfFalse(errors, doCheck("Step 2 retVal", seq.ExecuteStep() == '3', "Unexpected Result"));
	incrementIfFalse(errors, doCheck("Step 3 retVal", seq.ExecuteStep() == '1', "Unexpected Result"));
	incrementIfFalse(errors, doCheck("Step 4 retVal", seq.ExecuteStep() == '2', "Unexpected Result"));

	return errors;
}
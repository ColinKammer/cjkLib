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

	seq.ExecuteStep();
	seq.ExecuteStep();
	seq.ExecuteStep();
	seq.ExecuteStep();
	seq.ExecuteStep();

	return errors;
}
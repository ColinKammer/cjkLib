#pragma once
#include "edgeDetector.h"
#include "sequencer.h"

#include "test_helper.h"
#include <iostream>

using namespace cjk;

int tst_edgeDetector()
{
	int errors = 0;

	bool risingEdgeExpected = false;
	bool fallingEdgeExpected = false;


	Sequencer benchSequencer;
	benchSequencer.m_sequence = "0010011";

	EdgeDetector edgeDetect = EdgeDetector([&]() -> bool {
		return (benchSequencer.ExecuteStep() == '1');
		});

	edgeDetect.m_risingEdgeDetected.push_back([&](void) ->void {
		incrementIfFalse(errors, doCheck("rising Edge Detected", risingEdgeExpected, "No Rising Edge expected"));
		});
	edgeDetect.m_fallingEdgeDetected.push_back([&](void) ->void {
		incrementIfFalse(errors, doCheck("falling Edge Detected", fallingEdgeExpected, "No Falling Edge expected"));
		});

	edgeDetect.service();
	risingEdgeExpected = true;
	edgeDetect.service();
	risingEdgeExpected = false;
	fallingEdgeExpected = true;
	edgeDetect.service();
	fallingEdgeExpected = false;
	edgeDetect.service();
	risingEdgeExpected = true;
	edgeDetect.service();
	risingEdgeExpected = false;
	edgeDetect.service();

	return errors;
}
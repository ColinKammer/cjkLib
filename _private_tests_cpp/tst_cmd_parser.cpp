#pragma once
#include "cmd_line_args.h"

#include "test_helper.h"
#include <iostream>

using namespace cjk;

int tst_cmdParser()
{
	int errors = 0;

	auto tstArgs = CmdLineArgs("testfile.c -o out.asm -opt1 -settings f1.set f2.set f3.set");

	incrementIfFalse(errors, doCheck("has Switch positive", (tstArgs.HasSwitch("o") == true), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("has Switch negative", (tstArgs.HasSwitch("notPresent") == false), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetFirstSwitchArgument("o","todo:check against nullptr") == "out.asm"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetFirstSwitchArgument("notPresent", "default") == "default"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetFirstSwitchArgument("opt1", "default") == "default"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetFirstSwitchArgument("settings", "todo:check against nullptr") == "f1.set"), "Unexpected Result"));

	//Assert.AreEqual(null, tstArgs.GetSwitchArguments("notPresent"));
	//Assert.AreEqual(new string[]{ "f1.set", "f2.set", "f3.set" }, tstArgs.GetSwitchArguments("settings"));

	return errors;
}
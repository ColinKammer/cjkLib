#pragma once
#include "cmd_line_args.h"

#define CMD_LINE_ARGS_IMPL_SPLITTING
#include "cmd_line_args_ss.h"

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

int tst_cmdParserSS()
{
	int errors = 0;

	auto tstArgs = CmdLineArgsSS("call.exe lose1 -1 eins -noVal -2 zwei lose2 lose3");

	incrementIfFalse(errors, doCheck("has Switch positive", (tstArgs.HasSwitch("noVal") == true), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("has Switch negative", (tstArgs.HasSwitch("notPresent") == false), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetNamedArg("1") == "eins"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetNamedArg("2") == "zwei"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetNamedArg("noVal", "default") == ""), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetNamedArg("notPresent") == ""), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (tstArgs.GetNamedArg("notPresent", "default") == "default"), "Unexpected Result"));

	auto unnamed = tstArgs.GetUnnamedArgs();
	incrementIfFalse(errors, doCheck("tst", (unnamed.size() == 4), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (unnamed.at(0) == "call.exe"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (unnamed.at(1) == "lose1"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (unnamed.at(2) == "lose2"), "Unexpected Result"));
	incrementIfFalse(errors, doCheck("tst", (unnamed.at(3) == "lose3"), "Unexpected Result"));

	return errors;
}
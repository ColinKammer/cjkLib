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

	CmdLineArgsSS args{ 2,1,1 };
	auto& flag1 = args.ConfigureFlag("flag1");
	auto& flag2 = args.ConfigureFlag("flag2");
	auto& param1 = args.ConfigureParam("param1");
	auto& list1 = args.ConfigureNamedList("list1");
	auto& unnamed = args.ConfigureUnnamedList(2, 2);

	{
		auto parseErr = args.Parse("call.exe -flag1 -list1 1 2 3 -param1 p1 unnamed");
		incrementIfFalse(errors, doCheck("parse sucessfull", (parseErr == nullptr), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("has Switch positive", (flag1 == true), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("has Switch negative", (flag2 == false), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("param", (param1 == "p1"), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("list1_size", (list1.size() == 3), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("list1_0", (list1.at(0) == "1"), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("list1_1", (list1.at(1) == "2"), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("list1_2", (list1.at(2) == "3"), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("unnamed_size", (unnamed.size() == 2), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("unnamed_0", (unnamed.at(0) == "call.exe"), "Unexpected Result"));
		incrementIfFalse(errors, doCheck("unnamed_1", (unnamed.at(1) == "unnamed"), "Unexpected Result"));
	}

	args.Reset();
	{
		auto parseErr = args.Parse("call.exe -flag1 -list1 1 2 3 -param1 p1 unnamed anotherUnnamed");
		incrementIfFalse(errors, doCheck("parse failed, because to many unnamed", (parseErr != nullptr), "Unexpected Result"));
	}

	args.Reset();
	{
		auto parseErr = args.Parse("call.exe -flag1 -list1 1 2 3 -param1 p1");
		incrementIfFalse(errors, doCheck("parse failed, because to few unnamed", (parseErr != nullptr), "Unexpected Result"));
	}

	return errors;
}
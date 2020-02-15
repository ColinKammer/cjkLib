using csCjkLib;
using NUnit.Framework;

namespace Tests
{
    public class CmdLineParserTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var tstArgs = new CmdLineArgs("testfile.c -o out.asm -opt1 -settings f1.set f2.set f3.set");

            Assert.AreEqual(true, tstArgs.HasSwitch("o"));
            Assert.AreEqual(false, tstArgs.HasSwitch("notPresent"));
            Assert.AreEqual("out.asm", tstArgs.GetFirstSwitchArgument("o"));
            Assert.AreEqual("default", tstArgs.GetFirstSwitchArgument("notPresent", "default"));
            Assert.AreEqual("default", tstArgs.GetFirstSwitchArgument("opt1", "default"));
            Assert.AreEqual("f1.set", tstArgs.GetFirstSwitchArgument("settings"));
            Assert.AreEqual(null, tstArgs.GetSwitchArguments("notPresent"));
            Assert.AreEqual(new string[] { "f1.set", "f2.set", "f3.set" }, tstArgs.GetSwitchArguments("settings"));
        }
    }
}
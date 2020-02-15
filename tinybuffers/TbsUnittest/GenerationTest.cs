using NUnit.Framework;
using System.Collections.Generic;
using TbsLib;
using TbsLib.outputGenerators;

namespace Tests
{
    public class EnumGenerationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        static readonly TbsParseEnum tstEnum = new TbsParseEnum("TstEnum", new string[] {
            "opt1",
            "apz2"
        });
        static readonly TbsParseStruct tstSimpleStruct = new TbsParseStruct("TstSimpleStruct", new TbsParseStructMember[] {
            new TbsParseStructMember("UINT8", "a"),
            new TbsParseStructMember("INT32", "b", 12)
        });
        static readonly TbsParseStruct tstComplexStruct = new TbsParseStruct("TstComplexStruct", new TbsParseStructMember[] {
            new TbsParseStructMember("TstEnum", "enumeration"),
            new TbsParseStructMember("TstSimpleStruct", "customStruct"),
            new TbsParseStructMember("TstSimpleStruct", "structArray", 3)
        });
        static readonly ParseResult tstParseResult = new ParseResult(new ITbsParseType[] {
            tstEnum,
            tstSimpleStruct,
            tstComplexStruct
        });

        static readonly AnalysisResult tstAnalysisResult = new AnalysisResult(tstParseResult);

        [Test]
        public void TestCppGenerator()
        {
            IOutputGenerator generator = new CppGenerator("tstNSpace");
            string outputCode = generator.generateCode(tstAnalysisResult);

            //ToDo
        }

        [Test]
        public void TestCsGenerator()
        {
            IOutputGenerator generator = new CsGenerator("tstNSpace", true);
            string outputCode = generator.generateCode(tstAnalysisResult);

            //ToDo
        }

    }
}
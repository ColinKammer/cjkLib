using NUnit.Framework;

using TbsLib;

namespace Tests
{
    public class TbsParseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestValidEnum()
        {
            string code = "enum Tst {opt0; opt1; opt2; }";
            var parseResult = new ParseResult(code);

            Assert.AreEqual(1, parseResult.ParsedTypes.Count);
            var parseEnum = parseResult.ParsedTypes[0] as TbsParseEnum;
            Assert.IsNotNull(parseEnum);

            Assert.AreEqual("Tst", parseEnum.Typename);
            Assert.AreEqual(3, parseEnum.Options.Count);
            Assert.AreEqual("opt0", parseEnum.Options[0]);


            //Check Analysis
            var analysisResult = new AnalysisResult(parseResult);

            Assert.IsFalse(analysisResult.AnalysisLog.ContainsErrors);
            Assert.IsFalse(analysisResult.AnalysisLog.ContainsWarnings);

            Assert.AreEqual(1, analysisResult.AnalysedTypes.Count);
            var analysisEnum = analysisResult.AnalysedTypes[0] as TbsEnum;
            Assert.IsNotNull(analysisEnum);

            Assert.AreEqual("Tst", analysisEnum.Typename);
            Assert.AreEqual(3, analysisEnum.Options.Count);
            Assert.AreEqual("opt0", analysisEnum.Options[0]);
            Assert.AreEqual(1, analysisEnum.PhysicalSize);
        }

        [Test]
        public void TestValidStruct()
        {
            string code = "struct Tst {UINT8 mem0; INT16 mem1; UINT32[7] mem2; }";
            var parseResult = new ParseResult(code);
            var types = parseResult.ParsedTypes;

            Assert.AreEqual(1, types.Count);
            var parseStruct = types[0] as TbsParseStruct;
            Assert.IsNotNull(parseStruct);

            Assert.AreEqual("Tst", parseStruct.Typename);
            Assert.AreEqual(3, parseStruct.ParseMembers.Count);

            Assert.AreEqual("mem0", parseStruct.ParseMembers[0].Identifier);
            Assert.AreEqual("UINT8", parseStruct.ParseMembers[0].Typename);
            Assert.AreEqual(1, parseStruct.ParseMembers[0].ArraySize);

            Assert.AreEqual("mem2", parseStruct.ParseMembers[2].Identifier);
            Assert.AreEqual("UINT32", parseStruct.ParseMembers[2].Typename);
            Assert.AreEqual(7, parseStruct.ParseMembers[2].ArraySize);

            //Check Analysis
            var analysisResult = new AnalysisResult(parseResult);

            Assert.IsFalse(analysisResult.AnalysisLog.ContainsErrors);
            Assert.IsFalse(analysisResult.AnalysisLog.ContainsWarnings);

            Assert.AreEqual(1, analysisResult.AnalysedTypes.Count);
            var analysisStruct = analysisResult.AnalysedTypes[0] as TbsStruct;
            Assert.IsNotNull(analysisStruct);

            Assert.AreEqual("Tst", analysisStruct.Typename);
            Assert.AreEqual(3, analysisStruct.Members.Count);

            Assert.AreEqual(0, analysisStruct.Members[0].LocalOffset);
            Assert.AreEqual(1, analysisStruct.Members[0].PhysicalSize);
            Assert.AreEqual(true, analysisStruct.Members[0].IsScalar);

            Assert.AreEqual(1, analysisStruct.Members[1].LocalOffset);
            Assert.AreEqual(2, analysisStruct.Members[1].PhysicalSize);
            Assert.AreEqual(true, analysisStruct.Members[1].IsScalar);

            Assert.AreEqual(3, analysisStruct.Members[2].LocalOffset);
            Assert.AreEqual(7 * 4, analysisStruct.Members[2].PhysicalSize);
            Assert.AreEqual(false, analysisStruct.Members[2].IsScalar);

        }

        [Test]
        public void TestComplex()
        {
            string code = "struct Tst {UINT8 mem0; TestT mem1; TestT[7] mem2; } " +
                "struct TestT {ET a;} " +
                "enum ET {opt0; opt1; opt2;}";
            var parseResult = new ParseResult(code);
            var types = parseResult.ParsedTypes;

            Assert.AreEqual(3, types.Count);

            var analysisResult = new AnalysisResult(parseResult);


        }
    }
}
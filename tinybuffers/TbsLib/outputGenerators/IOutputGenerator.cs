using System;
using System.Collections.Generic;
using System.Text;

namespace TbsLib.outputGenerators
{
    public interface IOutputGenerator
    {
        string DefaultExtension { get; }

        string generateCode(AnalysisResult from);

        CompileLog generateStructDeclaration(StringBuilder codeBuilder, TbsStruct tbs);
        CompileLog generateStructDefinition(StringBuilder codeBuilder, TbsStruct tbs);
        CompileLog generateEnumDeclration(StringBuilder codeBuilder, TbsEnum tbs);
        CompileLog generateEnumDefinition(StringBuilder codeBuilder, TbsEnum tbs);
    }
}
